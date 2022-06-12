using System.Collections.Generic;

namespace Kyvos.GameStates;

internal class GameStateBuilders<TBuilder,TState, TData> : Dictionary<int, TBuilder>
    where TBuilder : GameState<TData>.Builder<TBuilder,TState>
    where TState : GameState<TData>
{ }