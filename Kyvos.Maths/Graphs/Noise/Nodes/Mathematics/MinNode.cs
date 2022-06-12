using Kyvos.Maths.Graphs.Noise.Nodes.Base;
using Kyvos.Maths.Graphs.Noise.Nodes.InputLabelResolvers;
using System;
using System.Numerics;

namespace Kyvos.Maths.Graphs.Noise.Nodes.Mathematics;
public class MinNode<TLabel> : OneChildNode<TLabel>
{
    public float Min;
    public MinNode(float min) : base(ZeroIndexResolver.Instance)
        => (Min) = min;
    public override float Evaluate(Vector2 coords)
        => MathF.Min(Child.Evaluate(coords), Min);

    public override float Evaluate(Vector3 coords)
        => MathF.Min(Child.Evaluate(coords), Min);
}


