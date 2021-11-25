using DefaultEcs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kyvos.Core.Worlds.Builder.Stages
{
    public interface IWorldBuilderPopulateStage : IWorldBuilderComponentRestrictionStage<IWorldBuilderFinalStage>, IWorldBuilderFinalStage
    {
        IWorldBuilderPopulateStage WithWorldComponent<T>( T value );

        IWorldBuilderPopulateStage WithWorldComponents<T>( IEnumerable<T> values );

        IWorldBuilderPopulateStage WithWorldComponents<T>( params T[] values );

        IWorldBuilderPopulateStage WithEntity( Action<Entity> setup );

        IWorldBuilderPopulateStage WithEntities( int k, Action<int, Entity> setup );

        IWorldBuilderPopulateStage WithEntity( IEntityInitializer initializer );

        IWorldBuilderPopulateStage WithEntities( int k, IEntityInitializer initializer );

    }
}
