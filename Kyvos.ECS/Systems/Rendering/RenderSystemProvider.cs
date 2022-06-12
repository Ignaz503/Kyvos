using DefaultEcs.System;
using System;
using System.Collections.Generic;

namespace Kyvos.ECS.Systems.Rendering;

//TODO a lot
//eg frame buffers per rendering context
//

public struct RenderSystemProvider 
{
    public IEnumerable<ISystem<RenderContext>> Systems { get; private set; }

    public RenderSystemProvider(params ISystem<RenderContext>[] systems)
    {
        this.Systems = systems;
    }

    public RenderSystemProvider()
    {
        Systems = Array.Empty<ISystem<RenderContext>>();
    }

}


