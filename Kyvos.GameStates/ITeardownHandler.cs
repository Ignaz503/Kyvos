namespace Kyvos.GameStates;

public interface ITeardownHandler<T>
{
    void TearDown(T w);
}
