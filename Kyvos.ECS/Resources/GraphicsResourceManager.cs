using DefaultEcs.Resource;
using Veldrid;

namespace Kyvos.ECS.Resources;
public abstract class GraphicsResourceManager<TInfo, TResource> : AResourceManager<TInfo, TResource>
{
    protected GraphicsDevice gfxDevice;
    protected ResourceFactory resourceFactory => gfxDevice.ResourceFactory;

    public GraphicsResourceManager(GraphicsDevice gfxD)
        => gfxDevice = gfxD;

}
