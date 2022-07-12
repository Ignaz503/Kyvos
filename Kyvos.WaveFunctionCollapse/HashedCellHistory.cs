using System.Collections;

namespace Kyvos.WaveFunctionCollapse;

public class HashedCellHistory<TId> : ICellHistory<TId>
    where TId : IEquatable<TId>
{
    HashSet<TId> choices;

    public HashedCellHistory()
    {
        choices = new();
    }

    public HashedCellHistory(int capacity)
    {
        choices = new(capacity);
    }

    public int Count => choices.Count;

    public IEnumerable<TId> Choices => choices;

    public void Clear() 
        => choices.Clear();

    public void Contains(TId choice)
        => choices.Contains(choice);

    public IEnumerator<TId> GetEnumerator()
        => choices.GetEnumerator();

    public void Record(TId choice) 
        => choices.Add(choice);

    IEnumerator IEnumerable.GetEnumerator()
        => GetEnumerator();
}

