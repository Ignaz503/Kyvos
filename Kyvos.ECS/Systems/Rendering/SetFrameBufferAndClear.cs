using DefaultEcs.System;
using Veldrid;

namespace Kyvos.ECS.Systems.Rendering;

//TODO remove this render system takes care of this
//every cmdlist needs to have a framebuffer set but it should only be cleared once
public class SetFrameBufferAndClear : ISystem<RenderContext>
{
    public bool IsEnabled { get; set; } = true;

    public void Dispose()
    {}

    public void Update(RenderContext ctx)
    {
        ctx.CmdList.SetFramebuffer(ctx.GfxDevice.SwapchainFramebuffer);
        ctx.CmdList.ClearColorTarget(0, RgbaFloat.Grey);
    }
}