namespace Kyvos.Maths.Graphs.Noise.Nodes;
public class OpenSimplex2SNode<TLabel> : NoiseNode<NoiseSettings, TLabel>
{
    public OpenSimplex2SNode(NoiseSettings settings) : base(settings)
    { }
    public OpenSimplex2SNode() : base(NoiseSettings.Default)
    { }

    protected override float GetNoise(float x, float y)
    {
        return NoiseFunctions.Noise.OpenSimplex2S(x, y, settings.Seed, settings.Frequency);
    }

    protected override float GetNoise(float x, float y, float z)
    {
        return NoiseFunctions.Noise.OpenSimplex2S(x, y, z, settings.Seed, settings.Frequency, settings.RotationType3D);
    }
}


