using DefaultEcs;
using Kyvos.Core.GameStates;
using Kyvos.Core.Worlds.Builder.Stages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kyvos.Core.Worlds.Builder
{
    public class WorldBuilder:
        IWorldBuilderInitStage,
        IWorldBuilderFinalStage,
        IWorldBuilderPopulateStage
    {
        int? maxCapacity;

        WorldSetupList initializer;

        private WorldBuilder() 
        {
            initializer = new();
        }

        World IWorldBuilderFinalStage.Build()
        {
            World world = maxCapacity.HasValue ? new( maxCapacity.Value ) : new();

            return world;
        }

        void IWorldBuilderFinalStage.InitializeWorld( World w ) 
        {
            initializer.Setup( w );
        }

        public IWorldBuilderPopulateStage WithMaxCapacity( int? maxCapacity )
        {
            this.maxCapacity = maxCapacity;
            return this;
        }

        public IWorldBuilderPopulateStage WithWorldComponent<T>( T value )
        {
            initializer.Add( w => w.Set( value ) );
            return this;
        }

        public IWorldBuilderPopulateStage WithEntity( Action<Entity> setup )
        {
            initializer.Add( w => setup( w.CreateEntity() ) );
            return this;
        }

        public IWorldBuilderPopulateStage WithEntities( int k, Action<int, Entity> setup )
        {
            initializer.Add( w =>
            {
                for (int i = 0; i < k; i++)
                    setup( k, w.CreateEntity() );
            } );

            return this;
        }

        public IWorldBuilderPopulateStage WithEntity( IEntityInitializer entityInitializer )
        {
            initializer.Add( w => entityInitializer.Setup( w.CreateEntity() ) );
            return this;
        }

        public IWorldBuilderPopulateStage WithEntities( int k, IEntityInitializer entityInitializer )
        {
            initializer.Add( w => {
                for (int i = 0; i < k; i++)
                    entityInitializer.Setup( i, w.CreateEntity() );
            } );
            return this;
        }

        public IWorldBuilderPopulateStage WithWorldComponents<T>( IEnumerable<T> values )
        {
            initializer.Add( w => {
                foreach (var value in values)
                    w.Set( value );
            } );
            return this;
        }


        public IWorldBuilderPopulateStage WithWorldComponents<T>( params T[] values )
         => WithWorldComponents( values as IEnumerable<T> );

        public IWorldBuilderFinalStage WithComponentRestriction<T>( int maxCapacity )
        {
            initializer.Add( w => w.SetMaxCapacity<T>( maxCapacity ) );
            return this;
        }

        internal static IWorldBuilderInitStage Create() 
            =>  new WorldBuilder();

    }
}
