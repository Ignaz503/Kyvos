//https://markheath.net/post/fire-and-forget-audio-playback-with

using Kyvos.Assets;
using Kyvos.Core;
using Kyvos.Core.Logging;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace Kyvos.Audio;

public interface ISoundLoader 
{
    public SoundAsset Load(AssetIdentifier id);
}

internal class SoundLoader : ISoundLoader, IDisposable
{
    bool isDisposed = false;
    readonly AssetDatabase assetDatabase;
    readonly ConcurrentDictionary<AssetIdentifier, SoundAsset> assetsLoaded;

    public SoundLoader(IApplication app)
    {

        Debug.Assert(app.HasComponent<AssetDatabase>(), $"{nameof(SoundLoader)} needs asset database");
        assetDatabase = app.GetComponent<AssetDatabase>()!;
        assetsLoaded = new();
        SoundAsset.OnNoReference += OnNoRefrenceRemaining;
    }

    public void Dispose()
    {
        if (isDisposed)
            return;
        SoundAsset.OnNoReference -= OnNoRefrenceRemaining;
        Log<SoundLoader>.Debug("Disposing {Obj}", nameof(SoundLoader));
        foreach (var elem in assetsLoaded.Values)
            elem.DisposeInternal();
        isDisposed = true;
    }

    public SoundAsset Load(AssetIdentifier id)
        => assetsLoaded.GetOrAdd(id, (ide) => new SoundAsset(ide, assetDatabase.GetAssetLocation(ide).First));


    void OnNoRefrenceRemaining(AssetIdentifier id) 
    {
        if(assetsLoaded.TryRemove(id, out var asset))
            asset.DisposeInternal();
    }
    //TODO load async
}
