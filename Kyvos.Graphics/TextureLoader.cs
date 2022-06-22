using Kyvos.Assets;
using Kyvos.Core;
using System.Diagnostics;
using System.Collections.Concurrent;
using Kyvos.Core.Logging;

namespace Kyvos.Graphics;

public class TextureLoader : IDisposable
{
    bool isDisposed = false;
    readonly AssetDatabase assetDatabase;
    readonly ConcurrentDictionary<AssetIdentifier, Texture> assetsLoaded;

    public TextureLoader(IApplication app)
    {
        Debug.Assert(app.HasComponent<AssetDatabase>(), $"{nameof(AssetDatabase)} requires {nameof(AssetDatabase)} to function");
        assetDatabase = app.GetComponent<AssetDatabase>()!;
        assetsLoaded = new();
        Texture.OnNoReference += OnNoReferenceLeft;
    }

    public Texture Load(AssetIdentifier id) 
    {
        return assetsLoaded.GetOrAdd(id, (_id) => new Texture(_id, assetDatabase.GetPathToAsset(id)));
    }


    public void Dispose()
    {
        if (isDisposed)
            return;

        Log<TextureLoader>.Debug("Disposing {Obj}", nameof(TextureLoader));
        Texture.OnNoReference-= OnNoReferenceLeft;
        foreach (var elem in assetsLoaded.Values)
            elem.DisposeInternal();


        isDisposed = true;
    }

    void OnNoReferenceLeft(AssetIdentifier id)
    {
        if (assetsLoaded.TryRemove(id, out var texture))
            texture.DisposeInternal();
    }
}
