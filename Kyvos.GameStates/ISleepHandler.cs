namespace Kyvos.GameStates;

public interface ISleepHandler<T>
{
    void Sleep(T w);
    void Awake(T w);
}

