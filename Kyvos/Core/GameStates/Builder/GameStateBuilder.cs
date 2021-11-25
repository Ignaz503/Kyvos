using DefaultEcs;
using DefaultEcs.Resource;
using DefaultEcs.Serialization;
using DefaultEcs.System;
using Kyvos.Core.Applications;
using Kyvos.Core.GameStateRunner;
using Kyvos.Core.GameStates.Builder.Stages;
using Kyvos.Util.Collections;
using Kyvos.Core.Worlds.Builder.Stages;
using System;
using System.Collections.Generic;
using Veldrid;
using Kyvos.Core.Worlds.Builder;

namespace Kyvos.Core.GameStates.Builder
{
    internal class GameStateBuilder :
        IStateBuilderInitStage,
        IStateDisposeSystemSetupStage,
        IStateRunnerStage,
        IStateBuilderFinalStage,
        IStateECSWorldSetupStage,
        IStateResourceManagerStage,
        IStateStackBehaviourSatge
    {
        GameStateRunnerBuilder stateRunner;
        IWorldBuilderFinalStage worldBuilder;
        WorldSetupList applicationResourceManagerSetupList;
        List<Action<WindowData, GraphicsDevice, WorldSetupList, DisposableList<IDisposable>>> resourceManagerBuildActions;


        private string name;

        GameState.IStackBehaviour stackBehaviour;

        private Func<IGameStateDisposeHandler> disposeFactory;

        string IStateBuilderFinalStage.Name => name;

        private GameStateBuilder()
        {
            applicationResourceManagerSetupList = new();
            resourceManagerBuildActions = new();
        }

        GameState IStateBuilderFinalStage.Build(WindowData windowData, GraphicsDevice graphicsDevice)
        {
            DisposableList<IDisposable> stateResourceManagers = new();

            WorldSetupList resourceManagerSetup = new(applicationResourceManagerSetupList);

            foreach (var action in resourceManagerBuildActions)
                action( windowData, graphicsDevice, resourceManagerSetup, stateResourceManagers );
            return new( worldBuilder, stateRunner, stateResourceManagers, applicationResourceManagerSetupList, disposeFactory(), stackBehaviour ) { Name = name };
        }

        public IStateStackBehaviourSatge WithRunner( IGameStateRunnerSetup setupHandler )
        {
            this.stateRunner = setupHandler.Setup( GameStateRunnerBuilder.Create() );
            return this;
        }

        public IStateStackBehaviourSatge WithRunner( Func<GameStateRunnerBuilder, GameStateRunnerBuilder> setup )
        {
            this.stateRunner = setup( GameStateRunnerBuilder.Create() );
            return this;
        }

        public IStateResourceManagerStage WithResourceManager<TManager, TInfo, TResource>( Func<IStateResourceManagerStage.Data, TManager> factory ) where TManager : AResourceManager<TInfo, TResource>
        {
            resourceManagerBuildActions.Add( ( windowData, graphicsDevice, setupList, managerList ) =>
             {
                 var newManager = factory(new() {WindowData = windowData, GfxDevice = graphicsDevice} );

                 managerList.Add( newManager );

                 setupList.Add( w => {
                     newManager.Manage( w );
                 } );

             } );
            return this;
        }

        IStateBuilderFinalStage IStateBuilderFinalStage.AddApplicationResourceManagers( List<Action<World>> callbacks )
        {
            applicationResourceManagerSetupList.AddRange( callbacks );
            return this;
        }

        public IStateRunnerStage WithDisposeHandler( Func<IGameStateDisposeHandler> factory )
        {
            this.disposeFactory = factory;
            return this;
        }

        public IStateRunnerStage WithNoDisposeStage()
            => WithDisposeHandler( () => IGameStateDisposeHandler.NoCleanup );

        public IStateDisposeSystemSetupStage WithECSWorld( Func<IWorldBuilderInitStage, IWorldBuilderFinalStage> build )
        {
            worldBuilder = build(WorldBuilder.Create());
            return this;
        }

        public IStateResourceManagerStage WithName( string name )
        {
            this.name = name;
            return this;
        }
        public IStateBuilderFinalStage WithStackBehaviour( Func<GameState.IStackBehaviour> behaviourFactory )
        {
            stackBehaviour = behaviourFactory();

            return this;
        }


        internal static IStateBuilderInitStage Create()
            => new GameStateBuilder();


    }

}
