namespace Kyvos.WaveFunctionCollapse;

public interface ITileCollectionSetOperationConductor<TSocketData, TId, TDataCollection>
    where TSocketData : struct, IComparable<TSocketData>, IEquatable<TSocketData>
    where TId : IEquatable<TId>
{
    ITileCollection<TSocketData, TId, TDataCollection> Union(ITileCollection<TSocketData, TId, TDataCollection> lhs, ITileCollection<TSocketData, TId, TDataCollection> rhs);
    ITileCollection<TSocketData, TId, TDataCollection> Intersect(ITileCollection<TSocketData, TId, TDataCollection> lhs, ITileCollection<TSocketData, TId, TDataCollection> rhs);
}

