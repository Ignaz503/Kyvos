namespace Kyvos.Maths.Graphs.Noise.Nodes;
public class PerlinNode<TLabel> : NoiseNode<NoiseSettings, TLabel>
{
    public PerlinNode(NoiseSettings settings) : base(settings)
    {
    }

    public PerlinNode() : base(NoiseSettings.Default)
    {
    }

    protected override float GetNoise(float x, float y)
    {
        return NoiseFunctions.Noise.Perlin(x, y, settings.Seed, settings.Frequency);
    }

    protected override float GetNoise(float x, float y, float z)
    {
        return NoiseFunctions.Noise.Perlin(x, y, z, settings.Seed, settings.Frequency, settings.RotationType3D);
    }
}


