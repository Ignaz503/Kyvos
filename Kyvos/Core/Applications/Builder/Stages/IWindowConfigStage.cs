using Veldrid.StartupUtilities;

namespace Kyvos.Core.Applications.Builder.Stages
{
    public interface IWindowConfigStage
    {
        IGraphicsDeviceStage WithWindow( WindowCreateInfo windowCreateInfo );
    }

}
