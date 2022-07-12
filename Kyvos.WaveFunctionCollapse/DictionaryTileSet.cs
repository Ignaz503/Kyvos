namespace Kyvos.WaveFunctionCollapse;

public class DictionaryTileSet<TSocketData, TId, TDataCollection, TMappedData> : ITileSet<TSocketData, TId, TDataCollection>
    where TSocketData : struct, IComparable<TSocketData>, IEquatable<TSocketData>
    where TId : IEquatable<TId>
    where TMappedData : IEquatable<TMappedData>
{
    Dictionary<TId, ITile<TSocketData, TId, TDataCollection>> tiles;

    public DictionaryTileSet(IEnumerable<ITile<TSocketData, TId, TDataCollection>> tiles)
    {
        this.tiles = new(Expand(tiles));


        IEnumerable<KeyValuePair<TId, ITile<TSocketData, TId, TDataCollection>>> Expand(IEnumerable<ITile<TSocketData, TId, TDataCollection>> toExpand)
        {
            foreach (var entry in toExpand)
                yield return new(entry.Id, entry);
        }

    }

    public ITile<TSocketData, TId, TDataCollection> this[TId id]
    {
        get => tiles[id];
    }

    public IReadOnlyCollection<ITile<TSocketData, TId, TDataCollection>> Tiles => tiles.Values;

    public IEnumerable<ITile<TSocketData, TId, TDataCollection>> GetValidNeighbors(TId id, Direction dir, ISocketTypeCollectionMatcher<TSocketData> socketMatcher)
        => GetValidNeighbors(this[id], dir, socketMatcher);

    public IEnumerable<ITile<TSocketData, TId, TDataCollection>> GetValidNeighbors(ITile<TSocketData, TId, TDataCollection> tile, Direction dir, ISocketTypeCollectionMatcher<TSocketData> socketMatcher)
    {
        List<ITile<TSocketData, TId, TDataCollection>> validNeighbors = new();
        var sockets = tile.GetSocketsForSide(dir);
        var oppositeDir = Direction.Opposite(dir);

        foreach (var otherTile in Tiles)
        {
            var socketCollection = otherTile.GetSocketsForSide(oppositeDir);
            if (socketMatcher.Match(sockets, socketCollection))
                validNeighbors.Add(tile);
        }

        return validNeighbors;
    }
}

