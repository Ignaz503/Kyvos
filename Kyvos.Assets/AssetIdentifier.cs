using Kyvos.Utility;
using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;

namespace Kyvos.Assets;

public readonly struct AssetIdentifier : IEquatable<AssetIdentifier>
{
    public string Name { get; init; }

    public AssetIdentifier()
        => Name = string.Empty;

    public AssetIdentifier(string name)
        => Name = name;

    public static explicit operator AssetIdentifier(string name)
        => new(name);

    public static implicit operator string(AssetIdentifier assetID)
        => assetID.Name;

    public bool Equals(AssetIdentifier other)
        => Name.Equals(other.Name);

    public override int GetHashCode() 
        => Name.GetHashCode();

    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        if (obj is AssetIdentifier other)
        {
            return Equals(other);
        }
        return false;
    }

    public override string ToString()
        => $"{{AssetID: {Name}}}";

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
