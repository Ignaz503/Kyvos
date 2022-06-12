using Kyvos.Maths.NoiseFunctions;
using System;

namespace Kyvos.Maths.Graphs.Noise.Nodes;
public abstract class LayerdNoiseNode<TGenerator, TSettings, TLabel> : NoiseNode<TSettings, TLabel>
        where TSettings : LayerNoiseSettings
        where TGenerator : INoiseGenerator
{
    protected TGenerator noiseGenerator;

    public LayerdNoiseNode(TSettings settings, Func<TGenerator> noiseGenBuilder) : base(settings)
    {
        this.noiseGenerator = noiseGenBuilder();
    }
}
