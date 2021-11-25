
using DefaultEcs;
using Veldrid;

namespace Kyvos.Core.Applications
{
    public struct ApplicationData
    {
        public GraphicsDevice GfxDevice { get; init; }
        public WindowData WindowData { get; init; }

        public ResourceFactory ResourceFactory => GfxDevice.ResourceFactory;

    }

}
