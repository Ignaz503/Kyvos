using DefaultEcs.Resource;
using Kyvos.Core.Applications;
using System;
using Veldrid;

namespace Kyvos.Core.GameStates.Builder.Stages
{
    public interface IStateResourceManagerStage : IStateECSWorldSetupStage
    {
        IStateResourceManagerStage WithResourceManager<TManager, TInfo, TResource>( Func<Data, TManager> factory ) where TManager : AResourceManager<TInfo, TResource>;

        public struct Data
        {
            public WindowData WindowData { get; init; }
            public GraphicsDevice GfxDevice { get; init; }
        }
    }
}
