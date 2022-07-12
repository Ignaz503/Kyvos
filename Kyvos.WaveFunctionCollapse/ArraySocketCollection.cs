namespace Kyvos.WaveFunctionCollapse;

public class ArraySocketCollection<TSocketData> : ISocketTypeCollection<TSocketData> where TSocketData : struct, IComparable<TSocketData>, IEquatable<TSocketData>
{
    static readonly EqualityComparer<SocketType<TSocketData>> equalityComparer = EqualityComparer<SocketType<TSocketData>>.Default;

    SocketType<TSocketData>[] data;

    public ArraySocketCollection(int maxSize)
    {
        data = new SocketType<TSocketData>[maxSize]; 
    }

    public ArraySocketCollection(IEnumerable<SocketType<TSocketData>> entries) 
    {
        data = entries.ToArray();
    }

    public SocketType<TSocketData> this[int idx] { get => data[idx]; set => data[idx]= value; }

    public int Count => data.Length;

    public bool Equals(ISocketTypeCollection<TSocketData>? other)
    {
        if (other is null || Count == other.Count)
            return false;
        var c = Count;
        for (int i = 0; i < c; i++)
        {
            var here = data[i];
            var fromOther = other[i];
            if (!equalityComparer.Equals(here, fromOther))
                return false;
        }
        return true;
    }

    public bool ReverseEquals(ISocketTypeCollection<TSocketData> inReverse)
    {
        if (inReverse is null || Count == inReverse.Count)
            return false;
        var c = Count;

        for (int i = 0, j = c -1; i < c; i++, j--)
        {
            var here = data[i];
            var reverse = inReverse[j];
            if (!equalityComparer.Equals(here, reverse))
                return false;
        }

        return true;
    }
}

