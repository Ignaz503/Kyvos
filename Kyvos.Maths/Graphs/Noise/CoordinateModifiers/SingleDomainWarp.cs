using Kyvos.Maths.NoiseFunctions;

namespace Kyvos.Maths.Graphs.Noise.CoordinateModifiers;

public class SingleDomainWarp : DomainWarpModifier<DomainWarpSettings>
{
    public SingleDomainWarp(DomainWarpSettings settings) : base(settings)
    { }

    protected override void Warp(ref float x, ref float y, ref float z)
    {
        DomainWarp.Single(ref x, ref y, ref z, settings.Seed, settings.WarpAmplitude, settings.FractalBounding, settings.Frequency, settings.RotationType3D, settings.WarpType);
    }

    protected override void Warp(ref float x, ref float y)
    {
        DomainWarp.Single(ref x, ref y, settings.Seed, settings.WarpAmplitude, settings.FractalBounding, settings.Frequency, settings.WarpType);
    }
}

