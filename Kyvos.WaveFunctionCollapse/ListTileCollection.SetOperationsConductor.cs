namespace Kyvos.WaveFunctionCollapse;

public partial class ListTileCollection<TSocketData, TId, TDataCollection> where TSocketData : struct, IComparable<TSocketData>, IEquatable<TSocketData>
    where TId : IEquatable<TId>
{
    public class SetOperationsConductor : ITileCollectionSetOperationConductor<ListTileCollection<TSocketData, TId, TDataCollection>, TSocketData, TId, TDataCollection>
    {
        public ListTileCollection<TSocketData, TId, TDataCollection> Intersect(ListTileCollection<TSocketData, TId, TDataCollection> lhs, ListTileCollection<TSocketData, TId, TDataCollection> rhs)
        {
            return new(lhs.data.Union(rhs));
        }

        public ListTileCollection<TSocketData, TId, TDataCollection> Union(ListTileCollection<TSocketData, TId, TDataCollection> lhs, ListTileCollection<TSocketData, TId, TDataCollection> rhs)
        {
            return new(lhs.data.Intersect(rhs).ToList());
        }
    }
}

