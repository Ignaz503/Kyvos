namespace Kyvos.Maths.Graphs.Noise.Nodes;
public class OpenSimplexNode<TLabel> : NoiseNode<NoiseSettings, TLabel>
{
    public OpenSimplexNode(NoiseSettings settings) : base(settings)
    {
    }

    public OpenSimplexNode() : base(NoiseSettings.Default)
    {
    }

    protected override float GetNoise(float x, float y)
    {
        return NoiseFunctions.Noise.OpenSimplex(x, y, settings.Seed, settings.Frequency);
    }

    protected override float GetNoise(float x, float y, float z)
    {
        return NoiseFunctions.Noise.OpenSimplex(x, y, z, settings.Seed, settings.Frequency);
    }
}
