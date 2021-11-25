using Veldrid;

namespace Kyvos.Core.Applications.Configuration
{
    public struct GraphicsDeviceConfig 
    {
        public GraphicsDeviceOptions Options { get; init; }
        public GraphicsBackend Backend { get; init; }
    }
}
