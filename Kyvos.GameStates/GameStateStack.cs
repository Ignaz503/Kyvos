using System;
using Kyvos.Utility.Collections;
using Kyvos.Core;
using Kyvos.GameStates.Exceptions;


namespace Kyvos.GameStates;

public class GameStateStack<TBuilder, TState, TData> : IDisposable 
    where TState : GameState<TData>
    where TBuilder : GameState<TData>.Builder<TBuilder,TState>, new()
{
    bool isDisposed = false;
    IApplication application;
    GameStateBuilders<TBuilder, TState, TData> stateBuilders;
    Utility.Collections.Stack<TState> gameStateStack;

    //don't push mess with stack during update routine handle it at the start of next frame
    Action? bufferdStackAction;

    public GameStateStack(IApplication application)
    { 
        this.application = application;
        this.bufferdStackAction = null;
        stateBuilders = new();
        gameStateStack = new();
    }

    internal void SetStackCapacity(int capacity)
    {
        gameStateStack = new(capacity);
    }

    internal void InitializeStack()
    {
        //if set stack capacity is never called it might be null
        if (gameStateStack is null)
        {
            gameStateStack = new();
        }
        if (bufferdStackAction is null)
            throw new NoInitialGameStateException();
        //put initial state on the stack
        HandleActionBuffer();
    }

    internal void AddState(string name, ISomeStateDescription<TBuilder, TState, TData> stateDescription, bool initial)
    {
        var hascode = string.GetHashCode(name);
        if (stateBuilders.ContainsKey(hascode))
            throw new NonUniqueNameException(name);

        if (initial)
            bufferdStackAction = () => PushInternal(name);

        TBuilder builder = new();
        builder.SetName(name);
        stateDescription.Describe(builder);

        stateBuilders.Add(hascode, builder);
    }

    public void Push(string name)
    {
        bufferdStackAction = () => PushInternal(name);
    }

    public void Push(string name, ISleepHandler<TData> idleHandler)
    {
        bufferdStackAction = () => PushInternal(name, idleHandler);
    }

    public void Push(string name, Action<TData> sleep, Action<TData> awake)
        => Push(name, new SleepHandlerActions<TData>(awake, sleep));

    public void Pop()
    {
        bufferdStackAction = () => PopInternal();
    }

    private void PushInternal(ReadOnlySpan<char> name)
    {
        var builder = GetBuilder(name);
        if (gameStateStack.Count > 0)
        {
            if (builder.UsesNewWorld)
                gameStateStack.Peek()?.Sleep();
            else
                gameStateStack.Peek()?.Suspend();
        }
#if DEBUG
        ValidatePushBehaviour(builder.UsesNewWorld);
#endif
        BuildAndPush(builder);
    }

    private void PushInternal(ReadOnlySpan<char> name, ISleepHandler<TData> handler)
    {
        var builder = GetBuilder(name);
        if (gameStateStack.Count > 0)
        {
            if (builder.UsesNewWorld)
                gameStateStack.Peek()?.Sleep(handler);
            else
                gameStateStack.Peek()?.Suspend();
        }
#if DEBUG
        ValidatePushBehaviour(builder.UsesNewWorld);
#endif
        BuildAndPush(builder);
    }

    void ValidatePushBehaviour(bool newStateUsesNewWorld) 
    {
        if (gameStateStack.Count == 0 && !newStateUsesNewWorld)
            throw new InvalidOperationException("Trying to push a state that bases execution on previous states world onto empty stack.");
    }

    TBuilder GetBuilder(ReadOnlySpan<char> name) 
    {
        var hascode = string.GetHashCode(name);

#if DEBUG
        if (!stateBuilders.ContainsKey(hascode)) 
        {
            throw new UnknownGameStateException(name);
        }
#endif
        return stateBuilders[hascode];
    }

    void BuildAndPush(TBuilder builder)
    {
        gameStateStack.Push(builder.Build(application));
        gameStateStack.Peek()?.Start();
    }

    private void PopInternal()
    {
        var state = gameStateStack.Pop();
        //stop 
        state?.Stop();
        if (gameStateStack.Count > 0)
        {
            //awake new top
            gameStateStack.Peek()?.Start();
        }
    }

    internal void Update(float deltaTime)
    {
        HandleActionBuffer();

        if (gameStateStack.Count > 0)
        {
            gameStateStack.Peek()?.Update(deltaTime);
        }
    }

    void HandleActionBuffer()
    {
        bufferdStackAction?.Invoke();
        bufferdStackAction = null;
    }
    
    public TState? Peek(int distance = 0)
        => gameStateStack.Peek(distance);

    public void Dispose()
    {
        if (isDisposed)
            return;
        
        bufferdStackAction = null;

        while (gameStateStack.Count > 0)
        {
            var state = gameStateStack.Pop();
            state?.Stop();
        }
        isDisposed = true;
    }

    public class System : IAppComponentSystem<GameStateStack<TBuilder, TState, TData>>
    {
        Core.Timer timer;

        public System(IApplication application)
        {
            if (!application.HasComponent<Core.Timer>())
                throw new MissingComponentException("GameStateStack System needs a Timer component.");
            timer = application.GetComponent<Core.Timer>()!;
        }
        

        public void Initialize(GameStateStack<TBuilder, TState, TData> stack, IApplication ctx)
        {
            stack.InitializeStack();
        }

        public void Update(GameStateStack<TBuilder, TState, TData> stack, IApplication application)
        {
            if (application.IsAlive)
                stack.Update(timer.DeltaTime);
        }

        public void Dispose()
        { }
    }

}


