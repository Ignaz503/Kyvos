using DefaultEcs;
using DefaultEcs.System;
using DefaultEcs.Threading;
using Kyvos.ECS.Systems.Rendering;

namespace Kyvos.ImGUI;
public class UIRenderSystem : AComponentSystem<RenderContext, ImGuiHandle>
{
    public UIRenderSystem(World world) : base(world)
    {
    }

    public UIRenderSystem(World world, IParallelRunner runner) : base(world, runner)
    {
    }

    public UIRenderSystem(World world, IParallelRunner runner, int minComponentCountByRunnerIndex) : base(world, runner, minComponentCountByRunnerIndex)
    {
    }

    protected override void PreUpdate(RenderContext ctx)
    {
        ctx.CmdList.SetFramebuffer(ctx.GfxDevice.SwapchainFramebuffer);
    }

    protected override void Update(RenderContext ctx, ref ImGuiHandle component)
    {
        component.Render(ctx.GfxDevice, ctx.CmdList);
    }

}   


