using Veldrid;

namespace Kyvos.VeldridIntegration.Materials;

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

        public override Property Get(GraphicsDevice gfxDevice)
        {
            if (UseDefaultSampler) 
            {
                Sampler toUse = DefaultSampler switch
                {
                    DefaultSamplerType.Point => gfxDevice.PointSampler,
                    DefaultSamplerType.Linear => gfxDevice.LinearSampler,
                    DefaultSamplerType.Aniso4x => gfxDevice.Aniso4xSampler,
                    _ => throw new System.Exception("unkown default sampler type")
                };
                return new SamplerProperty(toUse, Order);
            }
            return new SamplerProperty(SamplerDescription, Order,gfxDevice);
        }
    }


}

