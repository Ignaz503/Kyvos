namespace Kyvos.WaveFunctionCollapse;

public class ArraySocketCollectionFactory<TSocketData> : ISocketTypeCollectionFactory<TSocketData> where TSocketData : struct, IComparable<TSocketData>, IEquatable<TSocketData>
{
    public ISocketTypeCollection<TSocketData> Create(int size)
    {
        return new ArraySocketCollection<TSocketData>(size);    
    }

    public ISocketTypeCollection<TSocketData> Create(IEnumerable<SocketType<TSocketData>> entries)
    {
        return new ArraySocketCollection<TSocketData>(entries);
    }
}

