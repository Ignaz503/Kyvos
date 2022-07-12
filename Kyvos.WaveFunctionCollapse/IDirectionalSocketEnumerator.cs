namespace Kyvos.WaveFunctionCollapse;

public interface IDirectionalSocketEnumerator<TDataCollection, TMappedData>
    where  TMappedData : IEquatable<TMappedData>
{
    IEnumerable<TMappedData> Enumerate(Direction dir, TDataCollection dataCollection);
}

