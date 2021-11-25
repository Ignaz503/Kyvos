using DefaultEcs.System;
using System.Collections.Generic;

namespace Kyvos.Core.GameStateRunner
{
    public class MainRunner : TGameStateRunner
    {
        public bool IsEnabled { get; set; }

        SequentialSystem<float> internalSystem;

        public MainRunner( IEnumerable<ISystem<float>> stages)
        {
            internalSystem = new SequentialSystem<float>( stages );
        }

        public void Dispose()
        {
            internalSystem.Dispose();
        }

        public void Update( float elapsedTime )
        {
            internalSystem.Update( elapsedTime );
        }
    }

}
