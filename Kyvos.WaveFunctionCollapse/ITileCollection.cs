namespace Kyvos.WaveFunctionCollapse;

public interface ITileCollection<TSocketData, TId, TDataCollection> : IEnumerable<ITile<TSocketData,TId,TDataCollection>>
    where TSocketData : struct, IComparable<TSocketData>, IEquatable<TSocketData>
    where TId : IEquatable<TId>
{
    ITile<TSocketData, TId, TDataCollection> this[TId id] { get; }
    ITile<TSocketData, TId, TDataCollection> this[int i] { get; }

    int Count { get; }

    bool RemoveExcept(TId remains);
    bool RemoveExcept(ITile<TSocketData, TId, TDataCollection> remains);

}

