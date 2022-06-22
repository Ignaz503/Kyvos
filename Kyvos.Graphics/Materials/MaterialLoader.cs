using Kyvos.Assets;
using Kyvos.Core;
using Kyvos.Core.Logging;
using Kyvos.Utility.Exceptions;
using Kyvos.VeldridIntegration;
using System.Collections.Concurrent;
using System.Diagnostics;
using Veldrid;

namespace Kyvos.Graphics.Materials;


public class MaterialLoader : IDisposable
{
    readonly ConcurrentDictionary<AssetIdentifier, Material> materials;
    bool isDisposed = false;

    AssetDatabase assetDB;
    GraphicsDeviceHandle gfxDeviceHandle;
    TextureLoader texLoader;

    public MaterialLoader(IApplication app) 
    {
        materials = new();
        Material.OnNoReference += OnNoReferenceLeft;  //does this open a memory leak cause we never unregister but then  again it's a static class with static lifetime

        assetDB = GetComponentFromApp<AssetDatabase>(app);
        gfxDeviceHandle = GetComponentFromApp<GraphicsDeviceHandle>(app);
        texLoader = GetComponentFromApp<TextureLoader>(app);
    }

    T GetComponentFromApp<T>(IApplication app) 
    {
        Debug.Assert(app.HasComponent<T>(), $"{nameof(MaterialLoader)} needs {nameof(T)} to function");
        return app.GetComponent<T>()!;
    }

    public void Dispose()
    {
        if (isDisposed)
            return;

        Log<MaterialLoader>.Debug("Disposing {Obj}", nameof(MaterialLoader));
        foreach (var elem in materials.Values)
            elem.DisposeInternal();

        isDisposed = true;
    }

    public  Material Get(MaterialDescription description) 
    {
        if (materials.TryGetValue(description.Name, out Material? m)) 
        {
            return m.Reference();
        }

        Material newMat = new(description, new() { GfxDevice = gfxDeviceHandle.GfxDevice, TextureLoader = texLoader});
        materials.TryAdd(description.Name, newMat);
        return newMat.Reference();
    }

    public Material Get(AssetIdentifier assetID) 
    {
        if (materials.TryGetValue(assetID, out Material? m))
        {
            return m.Reference();
        }

        Material newMat = new(GetMaterialDescription(assetID), new() { GfxDevice = gfxDeviceHandle.GfxDevice, TextureLoader = texLoader });
        materials.TryAdd(assetID, newMat);
        return newMat.Reference();
    }

    MaterialDescription GetMaterialDescription(AssetIdentifier assetID)
        => throw new NotImplementedException();

    public void OnNoReferenceLeft(AssetIdentifier id)
    {
        if (materials.TryRemove(id, out var matir))
            matir.DisposeInternal();
    }
}
