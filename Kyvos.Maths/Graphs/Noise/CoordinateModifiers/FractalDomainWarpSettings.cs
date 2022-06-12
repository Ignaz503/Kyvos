using Kyvos.Maths.NoiseFunctions;

namespace Kyvos.Maths.Graphs.Noise.CoordinateModifiers;
public record FractalDomainWarpSettings( int Seed, float Frequency, float WarpAmplitude, float FractalBounding, int Octaves, float Gain, float Lacunarity, DomainWarp.Type WarpType, RotationType3D RotationType3D ) : DomainWarpSettings( Seed, Frequency, WarpAmplitude, FractalBounding, WarpType, RotationType3D )
{
    public new static readonly FractalDomainWarpSettings Default = new(Seed: NoiseHelpers.DefaultSeed, Frequency: NoiseHelpers.DefaultFrequency, WarpAmplitude: DomainWarp.DefaultAmplification,FractalBounding: NoiseHelpers.DefaultFractalBounding,Octaves: NoiseHelpers.DefaultOctaves,Gain: NoiseHelpers.DefaultGain, Lacunarity: NoiseHelpers.DefaultLacunarity ,WarpType: DomainWarp.DefaultWarpType, RotationType3D: NoiseHelpers.DefaultRotationType);
}

