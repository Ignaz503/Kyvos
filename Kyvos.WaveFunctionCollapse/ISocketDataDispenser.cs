namespace Kyvos.WaveFunctionCollapse;

public interface ISocketDataDispenser <TData>
    where TData: struct, IComparable<TData>, IEquatable<TData>
{
    TData GetFreeItem();

    void ReturnItem(TData item);

}

