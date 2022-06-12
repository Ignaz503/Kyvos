namespace Kyvos.Maths.Graphs.Noise.Nodes;
public class VornoiNode<TLabel> : NoiseNode<VornoiSettings, TLabel>
{
    public VornoiNode(VornoiSettings settings) : base(settings)
    {
    }

    public VornoiNode() : base(VornoiSettings.Default)
    {
    }

    protected override float GetNoise(float x, float y)
    {
        return NoiseFunctions.Noise.Vornoi(x, y, settings.Seed, settings.Frequency, settings.JitterModifier, settings.DistanceFunction, settings.ReturnType);
    }

    protected override float GetNoise(float x, float y, float z)
    {
        return NoiseFunctions.Noise.Vornoi(x, y, z, settings.Seed, settings.Frequency, settings.JitterModifier, settings.DistanceFunction, settings.ReturnType, settings.RotationType3D);
    }
}


