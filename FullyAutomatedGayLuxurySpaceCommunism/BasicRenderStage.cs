using DefaultEcs;
using DefaultEcs.System;
using Kyvos.Core.Logging;
using Kyvos.ECS.Components;
using Kyvos.ECS.Components.Rendering;
using Kyvos.ECS.Systems.Rendering;
using Kyvos.Graphics.Materials;
using Veldrid;

namespace FullyAutomatedGayLuxurySpaceCommunism
{
    [With(typeof(Mesh))]
    [With(typeof(Material))]
    [With(typeof(Transform))]
    public class BasicRenderStage : AEntitySetSystem<RenderContext>
    {

        public BasicRenderStage(World w):base(w)
        {

        }


        protected override void PreUpdate(RenderContext ctx)
        {
            //TODO move this call to render system and always set the active frame buffer there
            ctx.CmdList.SetFramebuffer(ctx.GfxDevice.SwapchainFramebuffer);
            //ctx.CmdList.ClearColorTarget(0, RgbaFloat.Grey);

        }

        protected override void Update(RenderContext ctx, in Entity entity)
        {
            ref var mesh = ref entity.Get<Mesh>();
            var mat = entity.Get<Material>();
            ref var transform = ref entity.Get<Transform>();
            //ref var cam = ref World.Get<Camera>();
            //var app = World.Get<IApplication>();

            mat.Update(0, "WorldBuffer", transform.Matrix, ctx.CmdList);
            //mat.Update(0, "ViewBuffer",cam.ViewMatrix,ctx.CmdList);
            //mat.Update(0, "ProjectionBuffer", cam.ProjectionMatrix, ctx.CmdList);

            ctx.CmdList.SetVertexBuffer(0, mesh.VertexBuffer);
            ctx.CmdList.SetIndexBuffer(mesh.IndexBuffer, IndexFormat.UInt16);
            mat.Use(ctx.CmdList);
            ctx.CmdList.DrawIndexed(indexCount: 36, instanceCount: 1, indexStart: 0, vertexOffset: 0, instanceStart: 0);            
        }

    }

    public class CameraBufferUpdateSystem : AComponentSystem<RenderContext, Camera>
    {
        public CameraBufferUpdateSystem(World world) : base(world)
        {
        }

        protected override void Update(RenderContext ctx, ref Camera cam)
        {
            Material.PropertySet.Manager.Update(0, "ViewBuffer", cam.ViewMatrix, ctx.CmdList);
            Material.PropertySet.Manager.Update(0, "ProjectionBuffer", cam.ProjectionMatrix, ctx.CmdList);

        }
    }


}
