using Kyvos.Maths.Graphs.Noise.Nodes.Base;
using Kyvos.Maths.Graphs.Noise.Nodes.InputLabelResolvers;
using System;
using System.Numerics;

namespace Kyvos.Maths.Graphs.Noise.Nodes.Mathematics;
public class ClampNode<TLabel> : OneChildNode<TLabel>
{
    public float Min, Max;
    public ClampNode(float min, float max) : base(ZeroIndexResolver.Instance)
        => (Min, Max) = (min, max);

    public override float Evaluate(Vector2 coords)
    {
        return Math.Clamp(Child.Evaluate(coords), Min, Max);
    }

    public override float Evaluate(Vector3 coords)
    {
        return Math.Clamp(Child.Evaluate(coords), Min, Max);
    }
}

