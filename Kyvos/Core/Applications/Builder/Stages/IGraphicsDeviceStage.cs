using Kyvos.Core.Applications.Configuration;
using Veldrid;

namespace Kyvos.Core.Applications.Builder.Stages
{
    public interface IGraphicsDeviceStage
    {
        ITimingConfigureStage WithGraphcisDevice( GraphicsDeviceOptions graphicsDeviceOptions, GraphicsBackend preferedBackend );

        ITimingConfigureStage WithGraphcisDevice( GraphicsDeviceConfig config );
    }

}
