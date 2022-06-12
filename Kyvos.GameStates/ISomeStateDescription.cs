namespace Kyvos.GameStates;

public interface ISomeStateDescription<TBuilder,TState, TData>
    where TBuilder : GameState<TData>.Builder<TBuilder,TState>
    where TState : GameState<TData>
{
    void Describe(TBuilder builder);
}


