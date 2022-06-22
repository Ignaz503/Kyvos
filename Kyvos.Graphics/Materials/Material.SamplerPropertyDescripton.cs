using Veldrid;

namespace Kyvos.Graphics.Materials;

public partial class Material
{
    public class SamplerPropertyDescripton : PropertyDescription
    {
        public enum DefaultSamplerType
        {
            Linear,
            Point,
            Aniso4x
        }

        public SamplerDescription SamplerDescription { get; init; }
        public bool UseDefaultSampler { get; init; }
        public DefaultSamplerType DefaultSampler { get; init; }

        public override Property Get(CreationContext ctx)
        {
            if (UseDefaultSampler) 
            {
                Sampler toUse = DefaultSampler switch
                {
                    DefaultSamplerType.Point => ctx.GfxDevice.PointSampler,
                    DefaultSamplerType.Linear => ctx.GfxDevice.LinearSampler,
                    DefaultSamplerType.Aniso4x => ctx.GfxDevice.Aniso4xSampler,
                    _ => throw new System.Exception("unkown default sampler type")
                };
                return new SamplerProperty(toUse, Order);
            }
            return new SamplerProperty(SamplerDescription, Order, ctx.GfxDevice);
        }
    }
}

