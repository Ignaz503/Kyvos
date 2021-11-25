using DefaultEcs;
using Kyvos.Core.Applications.Builder.Stages;
using System;
using Veldrid;
using Veldrid.StartupUtilities;
using Kyvos.Core.Applications.Configuration;
using Kyvos.Core.GameStates.Builder;
using Kyvos.Core.GameStates.Builder.Stages;
using DefaultEcs.Resource;
using System.Collections.Generic;
using Kyvos.Util.Collections;
using Kyvos.Util.Exceptions;
using System.Runtime.Serialization;

namespace Kyvos.Core.Applications.Builder
{
    public class KyvosGame :
        IWindowConfigStage,
        IGraphicsDeviceStage,
        ITimingConfigureStage,
        IApplictationGameStateStage,
        IApplicationBuildStage,
        IApplicationResourceManagerSetupStage
    {
        private WindowData windowData;
        private GraphicsDevice graphicsDevice;


        private List<IStateBuilderFinalStage> stateBuilders;

        private DisposableList<IDisposable> resourceManagers;
        private List<Action<World>> resourceManagerManageCallbacks;
        private string initialStateName  ="";

        public WindowData CurrentWindowData => windowData;

        public GraphicsDevice CurrentGraphicsDevice => graphicsDevice;

        private KyvosGame()
        {
            resourceManagers = new();
            resourceManagerManageCallbacks = new();
            stateBuilders = new();
        }

        IApplication IApplicationBuildStage.Build()
            => new Game( resourceManagers, stateBuilders, initialStateName ) { AppData = new ApplicationData { WindowData = windowData, GfxDevice = graphicsDevice } };

        public ITimingConfigureStage WithGraphcisDevice( GraphicsDeviceOptions graphicsDeviceOptions, GraphicsBackend preferedBackend )
        {
            graphicsDevice = VeldridStartup.CreateGraphicsDevice( windowData, graphicsDeviceOptions, preferedBackend );
            return this;
        }

        public IApplicationResourceManagerSetupStage WithTiming( Time.Config config )
        {
            Time.Initialize( config, windowData.DisplayRefreshPerSecond );
            return this;
        }

        public IGraphicsDeviceStage WithWindow( WindowCreateInfo windowCreateInfo )
        {
            windowData = new WindowData { Window = VeldridStartup.CreateWindow( windowCreateInfo ) };
            return this;
        }


        public ITimingConfigureStage WithGraphcisDevice( GraphicsDeviceConfig config )
            => WithGraphcisDevice( config.Options, config.Backend );

        public IApplicationResourceManagerSetupStage WithResourceManager<TManager, TInfo, TResource>( Func<IApplicationResourceManagerSetupStage.Data, TManager> factory ) where TManager : AResourceManager<TInfo, TResource>
        {
            var newManager = factory(new() {WindowData = windowData, GfxDevice = graphicsDevice} );

            resourceManagers.Add( newManager );
            resourceManagerManageCallbacks.Add( ( world ) => newManager.Manage( world ) );

            return this;
        }

        public IApplictationGameStateStage WithGameState( Func<IStateBuilderInitStage, IStateBuilderFinalStage> setup, bool isInitialState = false, bool resourceManagedByApplication = true )
        {
            var stateBuilder = setup( GameStateBuilder.Create() );

            if (resourceManagedByApplication)
                stateBuilder.AddApplicationResourceManagers( resourceManagerManageCallbacks );

            if (isInitialState)
            {
                if (!string.IsNullOrEmpty( initialStateName ))
                    throw new MultipleDefinedInitialiStatesException( initialStateName, stateBuilder.Name );
                initialStateName = stateBuilder.Name;
            }


            stateBuilders.Add( stateBuilder );
            return this;

        }

        public static IWindowConfigStage Builder
            => new KyvosGame();

    }


    public abstract class KyvosGameBuilderException : Exception
    {
        public KyvosGameBuilderException()
        {
        }

        public KyvosGameBuilderException( string message ) : base( message )
        {
        }

        public KyvosGameBuilderException( string message, Exception innerException ) : base( message, innerException )
        {
        }

        protected KyvosGameBuilderException( SerializationInfo info, StreamingContext context ) : base( info, context )
        {
        }
    }

    public class MultipleDefinedInitialiStatesException : KyvosGameBuilderException
    {
        public MultipleDefinedInitialiStatesException(string alreadyDefinedName, string secondDefinitionName):base($"{secondDefinitionName} can't be the initial state of the game, as {alreadyDefinedName} was already marked for it")
        {

        }
    }

}
