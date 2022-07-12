namespace Kyvos.WaveFunctionCollapse;

//backtracking
// if history not all posibilites revert add last choice to history and revert it
// propagate to neighboring cells to update their possibilities
// make new choice
//if history has all possibilites
//  clear cell, clear history
//  propagate change to neighboring cells
//  and tell previous cell that it needs to make a new choice
//
public interface ICellHistory<TId> : IEnumerable<TId>
    where TId : IEquatable<TId>
{
    public int Count { get; }

    public void Clear();
    public void Record(TId choice);

    public void Contains(TId choice);

    public IEnumerable<TId> Choices { get; }
}

