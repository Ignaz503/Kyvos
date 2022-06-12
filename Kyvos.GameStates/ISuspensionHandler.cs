namespace Kyvos.GameStates;

public interface ISuspensionHandler<T>
{
    void Suspend(T w);
    void Resume(T w);
}
