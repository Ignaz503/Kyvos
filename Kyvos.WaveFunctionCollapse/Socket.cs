namespace Kyvos.WaveFunctionCollapse;

public struct SocketType<TType> : IComparable<SocketType<TType>>, IEquatable<SocketType<TType>>
    where TType : struct, IEquatable<TType>, IComparable<TType> 
{
    public TType Value { get; init; }

    public SocketType()
    {
        Value = default;
    }

    public SocketType(TType val)
    {
        Value = val;
    }

    public int CompareTo(SocketType<TType> other) => Value.CompareTo(other.Value); //if i am null return -1

    public bool Equals(SocketType<TType> other) => Value.Equals(other.Value);

    public override int GetHashCode() => Value.GetHashCode();

    public override bool Equals(object? obj)
    {
        if (obj is SocketType<TType> sok)
            return Equals(sok);
        return false;
    }

    public static bool operator ==(SocketType<TType> left, SocketType<TType> right) => left.Equals(right);

    public static bool operator !=(SocketType<TType> left, SocketType<TType> right) => !(left == right);

    public static bool operator <(SocketType<TType> left, SocketType<TType> right) => left.CompareTo(right) < 0;

    public static bool operator <=(SocketType<TType> left, SocketType<TType> right) => left.CompareTo(right) <= 0;

    public static bool operator >(SocketType<TType> left, SocketType<TType> right) => left.CompareTo(right) > 0;

    public static bool operator >=(SocketType<TType> left, SocketType<TType> right) => left.CompareTo(right) >= 0;
}

