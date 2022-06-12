using Kyvos.Maths.NoiseFunctions;

namespace Kyvos.Maths.Graphs.Noise.Nodes;
public record LayerNoiseSettings(int Seed, float Frequency, int Octaves, float Gain, float Lacunarity, float WeightedStrength, RotationType3D RotationType3D) : NoiseSettings(Seed, Frequency, RotationType3D)
{
    public new readonly static LayerNoiseSettings Default = new(Seed: NoiseHelpers.DefaultSeed, Frequency: NoiseHelpers.DefaultFrequency, Octaves: NoiseHelpers.DefaultOctaves, Gain: NoiseHelpers.DefaultGain, Lacunarity: NoiseHelpers.DefaultLacunarity, WeightedStrength: NoiseHelpers.DefaultWeightedStrengh, RotationType3D: NoiseHelpers.DefaultRotationType);
}


