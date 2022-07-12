using Kyvos.Assets;
using Kyvos.Core;
using System.Diagnostics;
using System.Collections.Concurrent;
using Kyvos.Core.Logging;
using SixLabors.ImageSharp.PixelFormats;
using System.Numerics;
using System.Diagnostics.CodeAnalysis;

namespace Kyvos.Graphics;



public class UndefinedEnumValue<T> : Exception 
{
    public UndefinedEnumValue(T val) : base($"{val} is not defined") 
    {}
}

public class TextureLoader : ITextureLoader, IDisposable
{
    #region sepcific loaders
    static Type TwoDType = typeof(TextureData2D);
    static Type OneDType = typeof(TextureData1D);
    static Type CubeMapType = typeof(TextureDataCubeMap);

    public ITextureLoader<TextureData2D> TwoDimTexture => (textureLoaders[TwoDType] as ITextureLoader<TextureData2D>)!;
    public ITextureLoader<TextureDataCubeMap> CubeMap => (textureLoaders[CubeMapType] as ITextureLoader<TextureDataCubeMap>)!;
    public ITextureLoader<TextureData1D> OneDimTexture => (textureLoaders[OneDType] as ITextureLoader<TextureData1D>)!;
    #endregion
    Dictionary<Type, ITextureLoader> textureLoaders;

    public TextureLoader(IApplication app)
    {
        textureLoaders = new();
        
        textureLoaders.Add(typeof(TextureData2D),new TextureLoader<TextureData2D>(app, GenericTextureFactory.Instance));
        textureLoaders.Add(typeof(TextureDataCubeMap),new TextureLoader<TextureDataCubeMap>(app,GenericTextureFactory.Instance));
        textureLoaders.Add(typeof(TextureData1D),new TextureLoader<TextureData1D>(app, GenericTextureFactory.Instance ));
    }

    public TextureHandle<T> Load<T>(AssetIdentifier id, bool mipmap = true, bool srgb = false)
        where T : TextureData
    {
        if (!textureLoaders.TryGetValue(typeof(T), out var loader))
            throw new InvalidOperationException($"Unmapped texture data type {typeof(T)}");
        return loader.Load<T>(id,mipmap,srgb);
    }

    public void Dispose() 
    {
        foreach(var elem in textureLoaders.Values)
            elem.Dispose();
    }
}


public class TextureLoader<T> : ITextureLoader<T>
    where T : TextureData
{
    bool isDisposed = false;
    readonly AssetDatabase assetDatabase;

    readonly ITextureDataFactory<T> factory;

    readonly ConcurrentDictionary<AssetIdentifier, TextureHandle<T>> assetsLoaded;

    public TextureLoader(IApplication app, ITextureDataFactory<T> factory)
    {
        Debug.Assert(app.HasComponent<AssetDatabase>(), $"{nameof(AssetDatabase)} requires {nameof(AssetDatabase)} to function");
        assetDatabase = app.GetComponent<AssetDatabase>()!;
        assetsLoaded = new();
        TextureHandle<T>.OnNoReference += OnNoReferenceLeft;
        this.factory = factory;
    }

    public TextureHandle<T> Load(AssetIdentifier id, bool mipmap=true, bool srgb=false)
    {
        //TODO lock concurrent dict as multi add is possible whilst inside the AddMethod of GetOrAdd
        return assetsLoaded.GetOrAdd(id, (_id) => new TextureHandle<T>(_id, factory.Create(assetDatabase.GetAssetLocation(_id),mipmap,srgb)));
    }

    public void Dispose()
    {
        if (isDisposed)
            return;

        Log<TextureLoader>.Debug("Disposing Texture Loader for: {Obj}", typeof(T).Name);
        TextureHandle<T>.OnNoReference-= OnNoReferenceLeft;
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
