using Kyvos.Maths.Graphs.Noise.Nodes.Base;
using Kyvos.Maths.Graphs.Noise.Nodes.InputLabelResolvers;
using System;
using System.Numerics;

namespace Kyvos.Maths.Graphs.Noise.Nodes.Mathematics;
public class PowNode<TLabel> : OneChildNode<TLabel>
{
    public float RaiseTo;
    public PowNode(float raiseTo) : base(ZeroIndexResolver.Instance)
    {
        this.RaiseTo = raiseTo;
    }

    public override float Evaluate(Vector2 coords)
    {
        return MathF.Pow(Child.Evaluate(coords), RaiseTo);
    }

    public override float Evaluate(Vector3 coords)
    {
        return MathF.Pow(Child.Evaluate(coords), RaiseTo);
    }
}
