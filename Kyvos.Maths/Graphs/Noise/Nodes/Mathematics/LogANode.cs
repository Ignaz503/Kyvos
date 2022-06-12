using Kyvos.Maths.Graphs.Noise.Nodes.Base;
using Kyvos.Maths.Graphs.Noise.Nodes.InputLabelResolvers;
using System;
using System.Numerics;

namespace Kyvos.Maths.Graphs.Noise.Nodes.Mathematics;
public class LogANode<TLabel> : OneChildNode<TLabel>
{
    public float a;
    public LogANode(float a) : base(ZeroIndexResolver.Instance)
    { this.a = a; }

    public override float Evaluate(Vector2 coords)
    {
        return MathF.Log(Child.Evaluate(coords)) / MathF.Log(a);
    }

    public override float Evaluate(Vector3 coords)
    {
        return MathF.Log(Child.Evaluate(coords)) / MathF.Log(a);
    }
}

