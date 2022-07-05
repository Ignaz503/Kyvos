using Kyvos.Core;

namespace Kyvos.Assets;

public class AssetDatabase 
{
    readonly IAssetMapping mapping;

    internal AssetDatabase(IAssetMapping mapping)
    {
        this.mapping = mapping;
    }

    internal AssetDatabase(IApplication app)
    {
        mapping = new DictionaryAssetMapping(app);
    }

    /*
     * Basics of AssetDB:
     *  - map asset identifier to file(s) location
     *      . this might be a single file, or a directory containing multiple things
     *        eg: Material has multiple shader files for vertex and fragment shader, maybe a descriptor file for layout
     *            or define own file layout for shaders and resources that comnbines all in one (see unity shaders)
     *
     * - load asset only once and keep track how often it is referenced
     * - provide handle of asset to user
     * - unload asset if refrenced by noone
     *      . maybe in a worker thread similar to a GC
     *      . also correctly unload if it holds native resources 
     *        eg: texture also free GfxCard resources
     *        
     * - should be highly modular so that a developer can easily and with very few steps add new asset types
     *   without having to worry abut massive amounts of boilerplate
     * - async loading of assets
     *      . asset handlers registered provide default value
     *      . create handle with default value and say to handler load, and say to 
     *        user here is your handle
     *      . handle has wait for function that just busy waits if necessary for user
     */
    public AssetLocation GetAssetLocation(AssetIdentifier identifier)
        => mapping.GetAssetLocation(identifier);


    //internal void RegisterAssetStorage<T>(AssetStorage<T> assetStorage)
    //{
    //    throw new NotImplementedException();
    //}
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
