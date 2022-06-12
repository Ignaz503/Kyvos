using Kyvos.Core;

namespace Kyvos.GameStates;

public static class GameStateApplicationExtensions
{
    public static IModifyableApplication With<TBuilder, TState, TData>(this IModifyableApplication app, GameStateStack<TBuilder, TState, TData> stack)
        where TBuilder : GameState<TData>.Builder<TBuilder, TState>, new()
        where TState : GameState<TData>
    {
        app.AddComponent(stack);
        return app;
    }

    public static IModifyableApplication WithGameStateStack<TBuilder, TState, TData>(this IModifyableApplication app)
        where TBuilder : GameState<TData>.Builder<TBuilder, TState>, new()
        where TState : GameState<TData>
    {
        app.EnsureExistence((application) => new GameStateStack<TBuilder, TState, TData>(application));
        return app;
    }

    public static IModifyableApplication WithStackCapacity<TBuilder, TState, TData>(this IModifyableApplication app, int stackCapacity)
        where TBuilder : GameState<TData>.Builder<TBuilder, TState>, new()
        where TState : GameState<TData>
    {
        var stack = app.EnsureExistence((application) => new GameStateStack<TBuilder, TState, TData>(application));
        stack.SetStackCapacity(stackCapacity);
        return app;
    }

    public static IModifyableApplication With<TBuilder, TState, TData>(this IModifyableApplication app, string name, ISomeStateDescription<TBuilder, TState, TData> stateFactory, bool isInitial = false)
        where TBuilder : GameState<TData>.Builder<TBuilder, TState>, new()
        where TState : GameState<TData>
    {
        var stack = app.EnsureExistence(application => new GameStateStack<TBuilder, TState, TData>(application));
        stack.AddState(name, stateFactory, isInitial);
        return app;
    }
 
    public static IModifyableApplication With<TBuilder, TState, TData>(this IModifyableApplication app, string name, Action<TBuilder> factory, bool isInitial = false)
        where TBuilder : GameState<TData>.Builder<TBuilder, TState>, new()
        where TState : GameState<TData>
    {
        var stack = app.EnsureExistence(application => new GameStateStack<TBuilder, TState, TData>(application));
        stack.AddState(name, new StateDescriptionAction<TBuilder, TState, TData>(factory), isInitial);
        return app;
    }
}