using Veldrid;

namespace Kyvos.Graphics.Materials;

public partial class Material
{
    public class SamplerProperty : Property
    {
        Sampler sampler;

        internal override BindableResource Bindable => sampler;

        internal SamplerProperty(Sampler sampler, int order):base(order)
            => this.sampler = sampler;

        internal SamplerProperty(SamplerDescription samplerDescription,int order,GraphicsDevice gfxDevice) : base(order)
            => this.sampler = gfxDevice.ResourceFactory.CreateSampler(samplerDescription);

        public void Update(Sampler sampler)
            => this.sampler = sampler;

        public override void Dispose()
        {
            if (isDisposed)
                return;
            sampler.Dispose();
            isDisposed = true;
        }
    }
}

