namespace Kyvos.WaveFunctionCollapse;

public class Tile<TSocketData, TId, TDataCollection, TMappedData> : ITile<TSocketData, TId, TDataCollection>
    where TSocketData : struct, IComparable<TSocketData>, IEquatable<TSocketData>
    where TId : IEquatable<TId>
    where TMappedData : IEquatable<TMappedData>
{

    public TId Id { get; init; }

    public TDataCollection Data { get; init; }

    public Tile(TId id, TDataCollection data, Context<TSocketData,TMappedData, TDataCollection, TId> context)
    {
        Id = id;
        Data = data;

        var sockets = new ISocketTypeCollection<TSocketData>[context.Dimensions.MaxNumberDirections()];

        foreach(var dir in Direction.Enumerate(context.Dimensions)) 
        {
            var socketCollection = context.SocketTypeCollectionFactory.Create(
                context.DirectionalSocketTypeEnumerator.Enumerate(dir, data)
                    .Select(d => context.SocketTypeMapping.GetOrCreateSocketForData(d)));
            sockets[dir] = socketCollection;
        }
        Sockets = sockets;
    }

    public IReadOnlyList<ISocketTypeCollection<TSocketData>> Sockets { get; private set; }

    public ISocketTypeCollection<TSocketData> GetSocketsForSide(Direction dir)
        => Sockets[dir];

    public bool Equals(ITile<TSocketData, TId, TDataCollection>? other) 
        => other is not null && Id.Equals(other.Id);

    public override int GetHashCode() 
        => Id.GetHashCode();

    public override bool Equals(object? obj) 
        => obj is ITile<TSocketData, TId, TDataCollection> other && Equals(other);

    public override string? ToString() 
        => $"{Id}: {Data}";
}

