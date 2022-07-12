namespace Kyvos.WaveFunctionCollapse;

public interface ISocketTypeCollection<TSocketData> : IEquatable<ISocketTypeCollection<TSocketData>> 
    where TSocketData : struct, IComparable<TSocketData>, IEquatable<TSocketData>
{
    SocketType<TSocketData> this[int idx] { get; set; }
    public int Count { get; }
    bool ReverseEquals(ISocketTypeCollection<TSocketData> inReverse);
}
