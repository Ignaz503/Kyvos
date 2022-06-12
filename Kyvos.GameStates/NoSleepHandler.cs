namespace Kyvos.GameStates;

public class NoSleepHandler<T> : ISleepHandler<T>
{
    internal static readonly NoSleepHandler<T> Instance = new();

    private NoSleepHandler() { }

    public void Awake(T w)
    {
    }


    public void Sleep(T w)
    {
    }

}