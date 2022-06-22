using Kyvos.Core;
using Kyvos.Core.Configuration;
using Kyvos.Core.Logging;
using System.Diagnostics;

namespace Kyvos.Assets;

public class DictionaryAssetMapping : IAssetMapping
{


    private readonly Dictionary<AssetIdentifier, string> _assetMapping;

    public DictionaryAssetMapping(IApplication application)
    {
        Debug.Assert(application.HasComponent<IConfig>(), "Need Config to use AssetDatabase");
        var config = application.GetComponent<IConfig>()!;

        var dbFile = config.ReadValue<string>(IAssetMapping.CONFIG_KEY);

        if (string.IsNullOrEmpty(dbFile)) 
        {
            Log<DictionaryAssetMapping>.Error("No asset database file specified - using empty database");
            _assetMapping =
                new Dictionary<AssetIdentifier, string>();
            return;
        }
        _assetMapping = LoadFromFile(FileSystem.MakeAbsolute(dbFile, FileSystem.StorageLocation.InstallFolder));
    }

    private static Dictionary<AssetIdentifier, string> LoadFromFile(string path)
    {
        using StreamReader r = new(File.OpenRead(path));
        var dict = new Dictionary<AssetIdentifier, string>();

        var i = 0;
        while (!r.EndOfStream)
        {
            var line = r.ReadLine();
            if (string.IsNullOrEmpty(line))
            {
                continue;
            }

            var parts = line.Split(IAssetMapping.EntrySeperator);

            if (parts.Length != 2)
            {
                Log<AssetDatabase>.Debug("Invalid line in asset db file: {I}|| {Line}",i,line);
                continue;
            }

            var id = new AssetIdentifier(parts[0]);
            dict.Add(id, parts[1]);

            i++;
        }
        Log<AssetDatabase>.Debug("Loaded {Num} entrie(s) from asset db file", i);
        return dict;
    }

    public string GetPath(AssetIdentifier id)
    {
        if (_assetMapping.TryGetValue(id, out var path))
        {
            return path;
        }
        return string.Empty;
    }
    public ReadOnlySpan<char> GetPathSpan(AssetIdentifier id)
    {
        if (_assetMapping.TryGetValue(id, out var path))
        {
            return path;
        }
        return string.Empty;
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
