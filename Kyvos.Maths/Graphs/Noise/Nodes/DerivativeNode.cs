using Kyvos.Maths.NoiseFunctions;
using System;

namespace Kyvos.Maths.Graphs.Noise.Nodes;
public class DerivativeNode<TGen, TLabel> : LayerdNoiseNode<TGen, LayerNoiseSettings, TLabel> where TGen : INoiseGenerator
{
    public DerivativeNode(LayerNoiseSettings settings, Func<TGen> noiseGenBuilder) : base(settings, noiseGenBuilder)
    {
    }

    protected override float GetNoise(float x, float y)
    {

        return NoiseFunctions.Noise.Layered.Derivative(x, y, noiseGenerator, settings.Seed, settings.Octaves, settings.Frequency, settings.Gain, settings.Lacunarity, settings.WeightedStrength);
    }

    protected override float GetNoise(float x, float y, float z)
    {
        return NoiseFunctions.Noise.Layered.Derivative(x, y, z, noiseGenerator, settings.Seed, settings.Octaves, settings.Frequency, settings.Gain, settings.Lacunarity, settings.WeightedStrength, settings.RotationType3D);
    }
}


