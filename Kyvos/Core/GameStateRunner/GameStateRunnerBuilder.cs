using DefaultEcs;
using DefaultEcs.System;
using Kyvos.Core.Applications;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veldrid;
using Priority_Queue;
using System.Collections.Generic;

namespace Kyvos.Core.GameStateRunner
{

    public class GameStateRunnerBuilder
    {
        //TODO make guided

        SimplePriorityQueue<Func<Data,ISystem<float>>,int> gameSystemFactories;

        int internalOrder;

        private GameStateRunnerBuilder() 
        {
            gameSystemFactories = new();
        }

        public static GameStateRunnerBuilder Create()
            => new GameStateRunnerBuilder();

        public MainRunner Build( WindowData windowData, GraphicsDevice gfxDevice, World world )
        {
            return new( getGameSystems() );

            IEnumerable<ISystem<float>> getGameSystems( )
            {
                foreach (var factroy in gameSystemFactories)
                    yield return factroy( GetData( windowData, gfxDevice, world ) );
            }
        }

        int GetOrder( int? order ) 
        {
            var finalOrder = internalOrder;
            if (order.HasValue)
                finalOrder = order.Value;

            if (finalOrder >= internalOrder)
                internalOrder = finalOrder + 1;
            return finalOrder;
        }

        public GameStateRunnerBuilder DefaultSetup( Func<Data, ISystem<float>> fixedTimeStepStage, Func<Data, ISystem<float>> updateStage, Func<Data, ISystem<float>> drawStage ) 
        {
            gameSystemFactories.Enqueue( (data) =>new DefaultFixedUpdateSystem(fixedTimeStepStage( data )), 0 );
            gameSystemFactories.Enqueue( updateStage, 1 );
            gameSystemFactories.Enqueue( drawStage, 2 );
            return this;
        }

        public GameStateRunnerBuilder WithSystem( Func<Data, ISystem<float>> factory, int? order = null)
        {
            gameSystemFactories.Enqueue( factory, GetOrder(order) );
            return this;
        }

        public GameStateRunnerBuilder WithDefualtFixedUpdateSystem(Func<Data,ISystem<float>> internalSystemFactory, int? order = int.MinValue) 
        {
            gameSystemFactories.Enqueue( data => new DefaultFixedUpdateSystem( internalSystemFactory( data )), GetOrder( order ) );
            return this;
        }

        Data GetData( WindowData windowData, GraphicsDevice gfxDevice, World world )
            => new() { WindowData = windowData, GraphicsDevice = gfxDevice, World = world };

        public struct Data
        {
            public WindowData WindowData { get; init; }
            public GraphicsDevice GraphicsDevice { get; init; }
            public World World { get; init; }
        }
    }
}
