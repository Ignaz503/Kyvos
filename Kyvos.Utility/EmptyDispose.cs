namespace Kyvos.Utility;

public class EmptyDispose : IDisposable
{
    public static readonly EmptyDispose Instance = new();

    private EmptyDispose() { }

    public void Dispose()
    {
        throw new NotImplementedException();
    }
}