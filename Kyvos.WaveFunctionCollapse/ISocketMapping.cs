namespace Kyvos.WaveFunctionCollapse;

public interface ISocketTypeMapping<TDataType, TSocketData>
    where TDataType : IEquatable<TDataType>
    where TSocketData : struct, IComparable<TSocketData>, IEquatable<TSocketData>
{
    SocketType<TSocketData> GetOrCreateSocketForData(TDataType dataType);

    void RemoveSocketType(TDataType dataType);

}

