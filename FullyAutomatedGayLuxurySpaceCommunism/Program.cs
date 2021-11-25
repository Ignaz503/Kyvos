using DefaultEcs;
using System;
using System.IO;
using Kyvos.Core.Applications.Builder;
using Veldrid.StartupUtilities;
using Veldrid;
using Kyvos.Core;
using DefaultEcs.Resource;
using Kyvos.Core.GameStates.Builder;
using Kyvos.Core.GameStateRunner;
using DefaultEcs.Serialization;
using System.Collections.Generic;
using Kyvos.Core.Worlds;

namespace FullyAutomatedGayLuxurySpaceCommunism
{
    class Program
    {
        static void Main( string[] args )
        {

        }

        public class FakeResourceManager : AResourceManager<string, object>
        {
            protected override object Load( string info )
            {
                throw new NotImplementedException();
            }

            protected override void OnResourceLoaded( in Entity entity, string info, object resource )
            {
                throw new NotImplementedException();
            }
        }

        public class MainMenuSystems : IGameStateRunnerSetup
        {
            public GameStateRunnerBuilder Setup( GameStateRunnerBuilder builder )
            {
                throw new NotImplementedException();
            }
        }

        public class GameSystems : IGameStateRunnerSetup 
        {
            public GameStateRunnerBuilder Setup( GameStateRunnerBuilder builder )
            {
                throw new NotImplementedException();
            }
        }

        public class FakeSerialize : ISerializer
        {
            public World Deserialize( Stream stream )
            {
                throw new NotImplementedException();
            }

            public ICollection<Entity> Deserialize( Stream stream, World world )
            {
                throw new NotImplementedException();
            }

            public void Serialize( Stream stream, World world )
            {
                throw new NotImplementedException();
            }

            public void Serialize( Stream stream, IEnumerable<Entity> entities )
            {
                throw new NotImplementedException();
            }
        }

        public class FakeEntitySetup : IEntityInitializer
        {
            public void Setup( Entity entity )
            {
                throw new NotImplementedException();
            }

            public void Setup( int idx, Entity entity )
            {
                throw new NotImplementedException();
            }
        }

    }
}
