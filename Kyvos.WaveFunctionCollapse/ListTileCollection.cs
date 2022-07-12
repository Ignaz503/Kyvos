using System.Collections;

namespace Kyvos.WaveFunctionCollapse;

public partial class ListTileCollection<TSocketData, TId, TDataCollection> : ITileCollection<TSocketData, TId, TDataCollection>
    where TSocketData : struct, IComparable<TSocketData>, IEquatable<TSocketData>
    where TId : IEquatable<TId>
{
    List<ITile<TSocketData, TId, TDataCollection>> data;


    public ListTileCollection(int size)
    {
        data = new(size);
    }

    public ListTileCollection(IEnumerable<ITile<TSocketData,TId,TDataCollection>> tiles)
    {
        data = new(tiles);
    }

    public ITile<TSocketData, TId, TDataCollection> this[int i] => data[i];
    public ITile<TSocketData, TId, TDataCollection> this[TId id] => data.FirstOrDefault(tile => tile.Id.Equals(id))??throw new UnkownValueException<TId>(id);

    public int Count => data.Count;

    public bool RemoveExcept(TId remains)
    {
        var selection = data.Where(tile => tile.Id.Equals(remains)).ToList();
        if (selection.Count == 1)
        { 
            data = selection;
            return true;
        }

        return false;
    }

    public bool RemoveExcept(ITile<TSocketData, TId, TDataCollection> remains)
        => RemoveExcept(remains.Id);

    public IEnumerator<ITile<TSocketData, TId, TDataCollection>> GetEnumerator() 
        => data.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
        => GetEnumerator();
}

