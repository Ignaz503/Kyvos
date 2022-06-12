using Kyvos.Maths.NoiseFunctions;
using System;

namespace Kyvos.Maths.Graphs.Noise.Nodes;
public class FbmDerivativeNode<TGen, TLabel> : LayerdNoiseNode<TGen, LayerNoiseSettings, TLabel> where TGen : INoiseGenerator
{
    public FbmDerivativeNode(LayerNoiseSettings settings, Func<TGen> noiseGenBuilder) : base(settings, noiseGenBuilder)
    {
    }
    protected override float GetNoise(float x, float y)
    {
        return NoiseFunctions.Noise.Layered.FBmDerivative(x, y, noiseGenerator, settings.Seed, settings.Octaves, settings.Gain, settings.Lacunarity, settings.WeightedStrength, settings.Frequency);
    }

    protected override float GetNoise(float x, float y, float z)
    {
        return NoiseFunctions.Noise.Layered.FBmDerivative(x, y, z, noiseGenerator, settings.Seed, settings.Octaves, settings.Gain, settings.Lacunarity, settings.WeightedStrength, settings.Frequency, settings.RotationType3D);
    }
}

