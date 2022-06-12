using Kyvos.Maths.NoiseFunctions;

namespace Kyvos.Maths.Graphs.Noise.Nodes;
public record VornoiSettings(float JitterModifier, VornoiDistanceFunction DistanceFunction, VornoiReturnType ReturnType, int Seed, float Frequency, RotationType3D RotationType3D) : NoiseSettings(Seed, Frequency, RotationType3D)
{
    public new readonly static VornoiSettings Default = new(JitterModifier: VornoiNoiseGenerator.DefaultJitterModifier, DistanceFunction: VornoiNoiseGenerator.DefaultDistanceFunction, ReturnType: VornoiNoiseGenerator.DefaultReturnType, Seed: NoiseHelpers.DefaultSeed, Frequency: NoiseHelpers.DefaultFrequency, RotationType3D: NoiseHelpers.DefaultRotationType);
}


