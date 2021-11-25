using DefaultEcs.System;

namespace Kyvos.Core.GameStateRunner
{
    public class DefaultFixedUpdateSystem : ISystem<float>
    {
        ISystem<float> internalSystem;

        public DefaultFixedUpdateSystem(ISystem<float> stageSystem)
        {
            this.internalSystem = stageSystem;
        }

        public bool IsEnabled { get; set; }

        public void Dispose()
        {
            internalSystem.Dispose();
        }

        public void Update( float elpasedDeltaTime )
        {
            for (int i = 0; i < Time.NumberOfFixedUpdates; i++)
            { 
                internalSystem.Update( Time.FixedDeltaTime );
            }
        }
    }

}
