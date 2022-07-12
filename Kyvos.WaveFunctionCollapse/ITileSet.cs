namespace Kyvos.WaveFunctionCollapse;

public interface ITileSet<TSocketData, TId, TDataCollection> 
    where TSocketData : struct, IComparable<TSocketData>, IEquatable<TSocketData>
    where TId : IEquatable<TId>
{
    IReadOnlyCollection<ITile<TSocketData, TId, TDataCollection>> Tiles { get; }
    ITile<TSocketData, TId, TDataCollection> this[TId id] { get; }

    public IEnumerable<ITile<TSocketData, TId, TDataCollection>> GetValidNeighbors(TId id, Direction dir, ISocketTypeCollectionMatcher<TSocketData> socketMatcher);
    public IEnumerable<ITile<TSocketData, TId, TDataCollection>> GetValidNeighbors(ITile<TSocketData, TId, TDataCollection> tile, Direction dir, ISocketTypeCollectionMatcher<TSocketData> socketMatcher);
}

