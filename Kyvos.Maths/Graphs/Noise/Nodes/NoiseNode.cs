using Kyvos.Maths.Graphs.Noise.CoordinateModifiers;
using Kyvos.Maths.Graphs.Noise.Nodes.Base;
using Kyvos.Maths.Graphs.Noise.Nodes.InputLabelResolvers;
using System.Numerics;

namespace Kyvos.Maths.Graphs.Noise.Nodes;
public abstract class NoiseNode<T, TLabel> : LeafNode<TLabel>
        where T : NoiseSettings
{
    protected ICoordinateModifier coordModifier;
    protected T settings;

    public NoiseNode(T settings) : base(IgnoreResolver.Instance)
    {
        this.settings = settings;
    }

    public override float Evaluate(Vector2 coords)
    {
        if (coordModifier is not null)
            coords = coordModifier.Modify(coords);
        return GetNoise(coords.X, coords.Y);
    }

    public override float Evaluate(Vector3 coords)
    {
        if (coordModifier is not null)
            coords = coordModifier.Modify(coords);
        return GetNoise(coords.X, coords.Y, coords.Z);
    }

    public NoiseNode<T, TLabel> With(ICoordinateModifier mod)
    {
        this.coordModifier = mod;
        return this;
    }

    protected abstract float GetNoise(float x, float y);
    protected abstract float GetNoise(float x, float y, float z);
}


