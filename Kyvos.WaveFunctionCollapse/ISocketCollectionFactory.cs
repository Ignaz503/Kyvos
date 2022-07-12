namespace Kyvos.WaveFunctionCollapse;

public interface ISocketTypeCollectionFactory<TSocketData> where TSocketData : struct, IComparable<TSocketData>, IEquatable<TSocketData> 
{
    ISocketTypeCollection<TSocketData> Create(int size);
    ISocketTypeCollection<TSocketData> Create(IEnumerable<SocketType<TSocketData>> entries);
}

