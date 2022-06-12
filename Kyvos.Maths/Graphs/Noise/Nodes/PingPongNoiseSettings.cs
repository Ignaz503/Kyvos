using Kyvos.Maths.NoiseFunctions;

namespace Kyvos.Maths.Graphs.Noise.Nodes;
public record PingPongNoiseSettings(int Seed, float Frequency, int Octaves, float Gain, float Lacunarity, float WeightedStrength, float PingPongStrength, RotationType3D RotationType3D) : LayerNoiseSettings(Seed, Frequency, Octaves, Gain, Lacunarity, WeightedStrength, RotationType3D)
{
    public new readonly static PingPongNoiseSettings Default = new(Seed: NoiseHelpers.DefaultSeed, Frequency: NoiseHelpers.DefaultFrequency, Octaves: NoiseHelpers.DefaultOctaves, Gain: NoiseHelpers.DefaultGain, Lacunarity: NoiseHelpers.DefaultLacunarity, WeightedStrength: NoiseHelpers.DefaultWeightedStrengh, PingPongStrength: NoiseHelpers.DefaultPingPongStrength, RotationType3D: NoiseHelpers.DefaultRotationType);
}


