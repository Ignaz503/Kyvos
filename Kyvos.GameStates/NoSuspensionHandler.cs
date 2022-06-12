namespace Kyvos.GameStates;

public class NoSuspensionHandler<T> : ISuspensionHandler<T>
{
    public static readonly NoSuspensionHandler<T> Instance = new();

    private NoSuspensionHandler() { }

    public void Suspend(T w) { }
    public void Resume(T w) { }
}
