using Kyvos.Maths.NoiseFunctions;
using System;

namespace Kyvos.Maths.Graphs.Noise.Nodes;
public class PingPongNode<TGen, TLabel> : LayerdNoiseNode<TGen, PingPongNoiseSettings, TLabel>
        where TGen : INoiseGenerator
{
    public PingPongNode(PingPongNoiseSettings settings, Func<TGen> noiseGenBuilder) : base(settings, noiseGenBuilder)
    {
    }

    protected override float GetNoise(float x, float y)
    {
        return NoiseFunctions.Noise.Layered.PingPong(x, y, noiseGenerator, settings.Seed, settings.Octaves, settings.Gain, settings.PingPongStrength, settings.Lacunarity, settings.WeightedStrength, settings.Frequency);
    }

    protected override float GetNoise(float x, float y, float z)
    {
        return NoiseFunctions.Noise.Layered.PingPong(x, y, z, noiseGenerator, settings.Seed, settings.Octaves, settings.Gain, settings.PingPongStrength, settings.Lacunarity, settings.WeightedStrength, settings.Frequency, settings.RotationType3D);
    }
}


