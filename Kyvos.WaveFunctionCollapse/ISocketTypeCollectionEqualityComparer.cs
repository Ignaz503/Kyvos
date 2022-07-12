using System.Diagnostics.CodeAnalysis;

namespace Kyvos.WaveFunctionCollapse;

public interface ISocketTypeCollectionMatcher<TSocketData>
    where TSocketData : struct,IComparable<TSocketData>, IEquatable<TSocketData>
{
    bool Match(ISocketTypeCollection<TSocketData> matchAgainst, ISocketTypeCollection<TSocketData> toMatch);
}

public class SocketTypeForwardEqualityComparerer<TSocketData> : ISocketTypeCollectionMatcher<TSocketData>
    where TSocketData : struct, IComparable<TSocketData>, IEquatable<TSocketData>
{
    public bool Match(ISocketTypeCollection<TSocketData> matchAgainst, ISocketTypeCollection<TSocketData> toMatch) 
        => matchAgainst.Equals(toMatch);
}
public class SocketTypeReverseEqualityComparerer<TSocketData> : ISocketTypeCollectionMatcher<TSocketData>
    where TSocketData : struct, IComparable<TSocketData>, IEquatable<TSocketData>
{
    public bool Match(ISocketTypeCollection<TSocketData> matchAgainst, ISocketTypeCollection<TSocketData> toMatch)
        => matchAgainst.ReverseEquals(toMatch);
}