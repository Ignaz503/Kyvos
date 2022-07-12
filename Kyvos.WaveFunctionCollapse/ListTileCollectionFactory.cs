namespace Kyvos.WaveFunctionCollapse;

public class ListTileCollectionFactory<TSocketData, TId, TDataCollection> : ITileCollectionFactory<TSocketData, TId, TDataCollection>
    where TSocketData : struct, IComparable<TSocketData>, IEquatable<TSocketData>
    where TId : IEquatable<TId>
{
    public ITileCollection<TSocketData, TId, TDataCollection> Get(int size)
        => new ListTileCollection<TSocketData, TId, TDataCollection>(size);

    public ITileCollection<TSocketData, TId, TDataCollection> Get(IEnumerable<ITile<TSocketData, TId, TDataCollection>> tiles)
        => new ListTileCollection<TSocketData, TId, TDataCollection>(tiles);
}

