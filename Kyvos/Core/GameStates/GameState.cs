using DefaultEcs;
using DefaultEcs.Serialization;
using Kyvos.Core.Applications;
using Kyvos.Core.GameStateRunner;
using Kyvos.Core.GameStates.Builder.Stages;
using Kyvos.Core.GameStates.Exceptions;
using Kyvos.Core.Worlds.Builder.Stages;
using Kyvos.Util.Collections;
using System;
using static Kyvos.Core.GameStates.GameState;

namespace Kyvos.Core.GameStates
{
    public class GameState : IDisposable
    {
        public enum State 
        {
            Loaded,
            Unloaded,
            WorldDisposed,
            Disposed
        }


        public string Name { get; init; }

        internal State CurrentState { get; set; }

        bool isDisposed 
        { 
            get => CurrentState == State.Disposed;
            set 
            {
                if (value)
                    CurrentState = State.Disposed;
            }
        }
        public bool IsUnloaded 
        {
            get => CurrentState == State.Unloaded;
            set 
            {
                if (value)
                    CurrentState = State.Unloaded;
                else
                    CurrentState = State.Loaded;
            }
        }

        bool isWorldDisposed
        {
            get => CurrentState == State.WorldDisposed;
            set
            {
                if (value)
                    CurrentState = State.WorldDisposed;
            }
        }

        protected DisposableList<IDisposable> stateResourceManagers;
        protected WorldSetupList resourceManagerSetup;

        protected IWorldBuilderFinalStage worldBuilder;
        protected IGameStateDisposeHandler disposeHandler;
        protected GameStateRunnerBuilder stateRunnerBuilder;

        protected World world;
        protected MainRunner runner;

        internal IStackBehaviour StackBehaviour { get; private set; }

        internal GameState( IWorldBuilderFinalStage worldBuilder, GameStateRunnerBuilder stateRunnerBuilder, DisposableList<IDisposable> stateResourceManager, WorldSetupList resourceManagerSetup, IGameStateDisposeHandler disposeHandler, IStackBehaviour stackBehaviour)
        {
            this.stateResourceManagers = stateResourceManager;
            this.resourceManagerSetup = resourceManagerSetup;
            this.worldBuilder = worldBuilder;
            this.stateRunnerBuilder = stateRunnerBuilder;
            this.disposeHandler = disposeHandler;
            this.StackBehaviour = stackBehaviour;
        }


        internal void Run( float deltaTime )
        {
            if (isDisposed || IsUnloaded || isWorldDisposed)
                throw new TryingToRunUnloadedOrDisposedStateException();

            runner.Update( deltaTime );
        }

        public World InvokeWorldBuilder( bool initialize = false ) 
        {
            world = worldBuilder.Build();
            resourceManagerSetup.Setup( world );
            if (initialize)
                worldBuilder.InitializeWorld( world );
            runner = stateRunnerBuilder.Build(Application.Instance.AppData.WindowData, Application.Instance.AppData.GfxDevice, world );
            world.Optimize();
            return world;
        }

        public void InvokeWorldDisposeHandler() 
        {
            CurrentState = State.WorldDisposed;
            disposeHandler.Cleanup( world );
        }

        public void Dispose()
        {
            if (isDisposed)
                return;
            isDisposed = true;
            disposeHandler.Cleanup( world );
            runner.Dispose();
            stateResourceManagers.Dispose();
            world.Dispose();
        }

        public interface IStackBehaviour 
        {
            //TODO IWorldBuilder
            State PushBehaviour( GameState state);
            State PopBehaviour( GameState state );

            State MoveToTopBehaviour( GameState state);

            State MoveDownBehaviour( GameState state, bool newTopDesiresUnload = true );

        }
    }


    public class TestSatePushBehaviour : IStackBehaviour
    {
        public State MoveDownBehaviour( GameState state, bool newTopDesiresUnload = true )
        {
            throw new NotImplementedException();
        }

        public State MoveToTopBehaviour( GameState state)
        {
            throw new NotImplementedException();
        }

        public State PopBehaviour( GameState state )
        {
            throw new NotImplementedException();
        }

        public State PushBehaviour( GameState state)
        {
            throw new NotImplementedException();
        }
    }

}
