namespace Kyvos.WaveFunctionCollapse;

public interface ITileCollectionSetOperationConductor<TTileCollection, TSocketData, TId, TDataCollection> : ITileCollectionSetOperationConductor<TSocketData, TId, TDataCollection>
    where TSocketData : struct, IComparable<TSocketData>, IEquatable<TSocketData>
    where TId : IEquatable<TId>
    where TTileCollection : ITileCollection<TSocketData, TId, TDataCollection>
{
    ITileCollection<TSocketData, TId, TDataCollection> ITileCollectionSetOperationConductor<TSocketData, TId, TDataCollection>.Union(ITileCollection<TSocketData, TId, TDataCollection> lhs, ITileCollection<TSocketData, TId, TDataCollection> rhs) 
    {
        if (lhs is not TTileCollection || rhs is not TTileCollection)
            throw new InvalidOperationException();

        return Union(((TTileCollection)lhs)!, ((TTileCollection)rhs)!);
    }

    ITileCollection<TSocketData, TId, TDataCollection> ITileCollectionSetOperationConductor<TSocketData, TId, TDataCollection>.Intersect(ITileCollection<TSocketData, TId, TDataCollection> lhs, ITileCollection<TSocketData, TId, TDataCollection> rhs) 
    {
        if (lhs is not TTileCollection || rhs is not TTileCollection)
            throw new InvalidOperationException();

        return Intersect(((TTileCollection)lhs)!, ((TTileCollection)rhs)!);
    }

    TTileCollection Union(TTileCollection lhs, TTileCollection rhs);
    TTileCollection Intersect(TTileCollection lhs, TTileCollection rhs);
}

