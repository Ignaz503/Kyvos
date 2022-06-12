using Kyvos.Maths.NoiseFunctions;

namespace Kyvos.Maths.Graphs.Noise.Nodes;
public record NoiseSettings(int Seed, float Frequency, RotationType3D RotationType3D)
{
    public readonly static NoiseSettings Default = new(NoiseHelpers.DefaultSeed, NoiseHelpers.DefaultFrequency, NoiseHelpers.DefaultRotationType);
}


