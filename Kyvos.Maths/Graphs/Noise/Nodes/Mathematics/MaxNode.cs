using Kyvos.Maths.Graphs.Noise.Nodes.Base;
using Kyvos.Maths.Graphs.Noise.Nodes.InputLabelResolvers;
using System;
using System.Numerics;

namespace Kyvos.Maths.Graphs.Noise.Nodes.Mathematics;
public class MaxNode<TLabel> : OneChildNode<TLabel>
{
    public float Max;
    public MaxNode(float max) : base(ZeroIndexResolver.Instance)
        => (Max) = max;
    public override float Evaluate(Vector2 coords)
        => MathF.Max(Child.Evaluate(coords), Max);

    public override float Evaluate(Vector3 coords)
        => MathF.Max(Child.Evaluate(coords), Max);
}


