namespace Kyvos.Maths.Graphs.Noise.Nodes;
public class ValueCubicNode<TLabel> : NoiseNode<NoiseSettings, TLabel>
{
    public ValueCubicNode(NoiseSettings settings) : base(settings)
    {
    }
    public ValueCubicNode() : base(NoiseSettings.Default)
    {
    }

    protected override float GetNoise(float x, float y)
    {
        return NoiseFunctions.Noise.ValueCubic(x, y, settings.Seed, settings.Frequency);
    }

    protected override float GetNoise(float x, float y, float z)
    {
        return NoiseFunctions.Noise.ValueCubic(x, y, z, settings.Seed, settings.Frequency, settings.RotationType3D);
    }
}