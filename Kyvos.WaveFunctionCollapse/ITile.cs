namespace Kyvos.WaveFunctionCollapse;

public interface ITile<TSocketData, TId, TData> : IEquatable<ITile<TSocketData, TId, TData>>
    where TSocketData : struct, IComparable<TSocketData>, IEquatable<TSocketData>
    where TId : IEquatable<TId>
{
    TId Id {get;}

    TData Data { get; }

    IReadOnlyList<ISocketTypeCollection<TSocketData>> Sockets { get; }

    ISocketTypeCollection<TSocketData> GetSocketsForSide(Direction dir);

}

