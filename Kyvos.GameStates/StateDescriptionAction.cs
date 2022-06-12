using System;

namespace Kyvos.GameStates;

internal class StateDescriptionAction<TBuilder,TState, TData> : ISomeStateDescription<TBuilder,TState, TData>
    where TBuilder : GameState<TData>.Builder<TBuilder,TState>
    where TState : GameState<TData>
{
    Action<TBuilder> action;

    public StateDescriptionAction(Action<TBuilder> action)
    {
        this.action = action ?? throw new ArgumentNullException(nameof(action));
    }

    public void Describe(TBuilder builder)
        => action(builder);
}


