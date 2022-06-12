namespace Kyvos.Maths.Graphs.Noise.Nodes;
public class ValueNode<TLabel> : NoiseNode<NoiseSettings, TLabel>
{
    public ValueNode(NoiseSettings settings) : base(settings)
    {
    }
    public ValueNode() : base(NoiseSettings.Default)
    {
    }

    protected override float GetNoise(float x, float y)
    {
        return NoiseFunctions.Noise.Value(x, y, settings.Seed, settings.Frequency);
    }

    protected override float GetNoise(float x, float y, float z)
    {
        return NoiseFunctions.Noise.Value(x, y, z, settings.Seed, settings.Frequency, settings.RotationType3D);
    }
}


