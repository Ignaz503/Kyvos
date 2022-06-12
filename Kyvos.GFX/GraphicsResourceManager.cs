using DefaultEcs.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veldrid;

namespace Kyvos.GFX;
public abstract class GraphicsResourceManager<TInfo, TResource> : AResourceManager<TInfo, TResource>
{
    protected GraphicsDevice gfxDevice;
    protected ResourceFactory resourceFactory => gfxDevice.ResourceFactory;

    public GraphicsResourceManager(GraphicsDevice gfxD)
        => gfxDevice = gfxD;

}
