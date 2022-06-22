using Kyvos.Core;

namespace Kyvos.Assets;

public static class AssetDatabaseApplicationExtension 
{
    public static IModifyableApplication WithAssetDatabase(this IModifyableApplication application)
    {
        application.AddComponent<AssetDatabase>(new AssetDatabase(application));
        return application;
    }

    public static IModifyableApplication WithAssetDatabase(this IModifyableApplication application, Func<IModifyableApplication, IAssetMapping> mappingFactory)
    {
        application.AddComponent<AssetDatabase>(new AssetDatabase(mappingFactory(application)));
        return application;
    }
}

//public class AssetHandle<T>
//{
//    internal ReferenceCounter refCounter;

//    T asset;
//    public T Asset 
//    {
//        get => asset;
//        internal set 
//        {
//            IsDefault = false;
//            asset = value;
//        }
//    }

//    public bool IsDefault { get; private set; }

//    public AssetHandle(T @default)
//    {
//        asset = @default;
//        refCounter = new ReferenceCounter(0);
//    }

//    internal void Cleanup()
//        => (asset as IDisposable)?.Dispose();

//    public void BusyWait()
//    {
//        while (IsDefault) 
//            Thread.Sleep(1);           
//    }
//}

//public interface IAssetLoadHandler<T>
//{
//    AssetHandle<T> Load(AssetIdentifier id);
//    AssetHandle<T> LoadAsync(AssetIdentifier id, Action<AssetHandle<T>> callback, bool callbackOnMainThread=false);
//}


//public class AssetStorage<T> : IDisposable
//{
//    static object _lockObj = new();
//    static AssetStorage<T>? instance;
    
//    private readonly ConcurrentDictionary<AssetIdentifier, AssetHandle<T>> _assets = new();

//    AssetDatabase db;

//    private AssetStorage(AssetDatabase db)
//    {
//        this.db = db;
//        db.RegisterAssetStorage(this);
//    }

//    internal void HandleCleanup() 
//    {
//        foreach (var (id,asset) in _assets)
//        {
//            if (asset.refCounter.Count == 0)
//            {
//                asset.Cleanup();
//            }

//        }
//    }

//    internal static AssetStorage<T> Get(AssetDatabase db)
//    {
//        lock (_lockObj)
//        {
//            return instance ??= new(db);
//        }
//    }

//    public void Dispose()
//    {
//        //called on app exit
//    }
//}
