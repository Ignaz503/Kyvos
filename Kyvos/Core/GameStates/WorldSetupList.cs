using DefaultEcs;
using System;
using System.Collections.Generic;


namespace Kyvos.Core.GameStates
{
    public class WorldSetupList : List<Action<World>>, IGameStateInitailizationHandler
    {
        public WorldSetupList( IEnumerable<Action<World>> initial ) : base( initial ) 
        {
        }

        public WorldSetupList()
        {}

        public void Setup( World world )
        {
            foreach (var action in this)
                action( world );
        }
    }


}
