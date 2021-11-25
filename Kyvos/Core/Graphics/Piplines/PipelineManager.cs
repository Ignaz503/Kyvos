using DefaultEcs;
using DefaultEcs.Resource;
using Veldrid;


namespace Kyvos.Core.Graphics.Piplines
{
    public class PipelineManager : AResourceManager<GraphicsPipelineDescription, Pipeline>
    {
        private readonly GraphicsDevice gfxDevice;
        ResourceFactory factory => gfxDevice.ResourceFactory;

        public PipelineManager(GraphicsDevice gfxDevice) : base()
        {
            this.gfxDevice = gfxDevice;
        }


        protected override Pipeline Load( GraphicsPipelineDescription info )
            => factory.CreateGraphicsPipeline( info );
        protected override void OnResourceLoaded( in Entity entity, GraphicsPipelineDescription info, Pipeline resource )
        {
            //TODO maybe drawinfo
            entity.Set( resource );
        }

        protected override void Unload( GraphicsPipelineDescription info, Pipeline resource )
        {
            resource.Dispose();
        }
    }
}
