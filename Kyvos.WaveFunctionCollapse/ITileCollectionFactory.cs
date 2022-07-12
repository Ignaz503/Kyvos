namespace Kyvos.WaveFunctionCollapse;

public interface ITileCollectionFactory<TSocketData, TId, TDataCollection>
    where TSocketData : struct, IComparable<TSocketData>, IEquatable<TSocketData>
    where TId : IEquatable<TId>
{
    ITileCollection<TSocketData, TId, TDataCollection> Get(int size);
    ITileCollection<TSocketData, TId, TDataCollection> Get(IEnumerable<ITile<TSocketData,TId,TDataCollection>> tiles);
}

