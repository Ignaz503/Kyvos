using Kyvos.Core;
using System;

namespace Kyvos.GameStates;

public abstract partial class GameState<TData>
{
    public abstract class Builder<TBuilder, TState>
        where TState : GameState<TData>
        where TBuilder : Builder<TBuilder, TState>
    {
        #region members
        protected ISleepHandler<TData> sleepHandler;
        protected ISuspensionHandler<TData> suspensionHandler;
        protected ITeardownHandler<TData> teardownHandler;

        protected string name;


        bool usesNewWorld = false;
        public bool UsesNewWorld => usesNewWorld;
        #endregion
        public Builder()
        {
            this.name = string.Empty;

            teardownHandler = DefaultTeardown<TData>.Instance;
            sleepHandler = NoSleepHandler<TData>.Instance;
            suspensionHandler = NoSuspensionHandler<TData>.Instance;
        }

        public TBuilder SetName(string name)
        {
            this.name = name;
            return (TBuilder)this;
        }

        public TBuilder UseNewWorld()
        {
            usesNewWorld = true;
            return (TBuilder)this;
        }

        public TBuilder WithTeardown(ITeardownHandler<TData> system)
        {
            teardownHandler = system;
            return (TBuilder)this;
        }

        public TBuilder WithTeardown(Action<TData> teardown)
            => WithTeardown(new TearDownHandlerAction<TData>(teardown));

        public TBuilder WithSleepHandler(ISleepHandler<TData> system)
        {
            sleepHandler = system;
            return (TBuilder)this;
        }

        public TBuilder WithIdleHandler(Action<TData> sleep, Action<TData> awake)
            => WithSleepHandler(new SleepHandlerActions<TData>(awake, sleep));
        
        public TBuilder WithSuspensionHandler(ISuspensionHandler<TData> system)
        {
            suspensionHandler = system;
            return (TBuilder)this;
        }

        public TBuilder WithSuspensionHandler(Action<TData> sleep, Action<TData> awake)
            => WithSuspensionHandler(new SuspensionHandlerAction<TData>(awake, sleep));


        public abstract TState Build(IApplication app);

    }

}

