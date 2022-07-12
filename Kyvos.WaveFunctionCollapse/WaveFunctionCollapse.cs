namespace Kyvos.WaveFunctionCollapse;


public static class WaveFunctionCollapse
{
    public static IWaveFunctionCollapseOutput Perform(IWaveFunctionCollapseInput input)
    {
        throw new NotImplementedException();
    }
}



public struct Context<TSocketData, TSocketTypeMappedData, TDataCollection, TTileID>
    where TSocketTypeMappedData : IEquatable<TSocketTypeMappedData>
    where TSocketData : struct, IComparable<TSocketData>, IEquatable<TSocketData>
    where TTileID : IEquatable<TTileID>
{
    public Dimensions Dimensions { get; init; }
    public IRngProvider RngProvider { get; init; }

    public ISocketTypeMapping<TSocketTypeMappedData, TSocketData> SocketTypeMapping { get; init; }

    public ISocketTypeCollectionFactory<TSocketData> SocketTypeCollectionFactory { get; init; }

    public IDirectionalSocketEnumerator<TDataCollection, TSocketTypeMappedData> DirectionalSocketTypeEnumerator { get; init; }

    public ISocketTypeCollectionMatcher<TSocketData> SocketCollectionMatcher { get; init; }

    public ITileCollectionSetOperationConductor<TSocketData, TTileID, TDataCollection> SetOperationConductor { get; init; }

    public ITileCollectionFactory<TSocketData, TTileID, TDataCollection> TileCollectionFactory { get; init; }

}

public interface IGrid 
{}

public interface ICell<TSocketData, TId, TDataCollection>
    where TSocketData : struct, IComparable<TSocketData>, IEquatable<TSocketData>
    where TId : IEquatable<TId>
{
    ITileCollection<TSocketData, TId, TDataCollection> Data { get; }

    ICellHistory<TId> CellHistory { get; }

    bool IsCollapsed { get; }

    int Entropy { get; }

    void Collapse(IRngProvider rngProvider);

}

public interface IGridHistory 
{

}

