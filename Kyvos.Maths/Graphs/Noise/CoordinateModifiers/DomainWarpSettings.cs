using Kyvos.Maths.NoiseFunctions;

namespace Kyvos.Maths.Graphs.Noise.CoordinateModifiers;
public record DomainWarpSettings(int Seed, float Frequency, float WarpAmplitude, float FractalBounding, DomainWarp.Type WarpType, RotationType3D RotationType3D)
{
    public static readonly DomainWarpSettings Default = new(Seed: NoiseHelpers.DefaultSeed, Frequency: NoiseHelpers.DefaultFrequency, WarpAmplitude: DomainWarp.DefaultAmplification, FractalBounding: NoiseHelpers.DefaultFractalBounding, WarpType: DomainWarp.DefaultWarpType, RotationType3D: NoiseHelpers.DefaultRotationType);
}

