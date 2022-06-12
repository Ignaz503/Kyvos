using Kyvos.Maths.NoiseFunctions;

namespace Kyvos.Maths.Graphs.Noise.CoordinateModifiers;

public class FractalProgressiveDomainWarp : DomainWarpModifier<FractalDomainWarpSettings>
{
    public FractalProgressiveDomainWarp(FractalDomainWarpSettings settings) : base(settings)
    {
    }

    protected override void Warp(ref float x, ref float y, ref float z)
    {
        DomainWarp.FractalProgressive(ref x, ref y, ref z, settings.Seed, settings.WarpAmplitude, settings.FractalBounding, settings.Frequency, settings.Octaves, settings.Gain, settings.Lacunarity, settings.RotationType3D, settings.WarpType);
    }

    protected override void Warp(ref float x, ref float y)
    {
        DomainWarp.FractalProgressive(ref x, ref y, settings.Seed, settings.WarpAmplitude, settings.FractalBounding, settings.Frequency, settings.Octaves, settings.Gain, settings.Lacunarity, settings.WarpType);
    }
}

