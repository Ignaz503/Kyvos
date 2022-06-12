using Kyvos.Maths.Graphs.Noise.Nodes.Base;
using Kyvos.Maths.Graphs.Noise.Nodes.InputLabelResolvers;
using System;
using System.Numerics;

namespace Kyvos.Maths.Graphs.Noise.Nodes.Mathematics;
    public class Log10Node<TLabel> : OneChildNode<TLabel>
{
    public Log10Node() : base(ZeroIndexResolver.Instance)
    { }

    public override float Evaluate(Vector2 coords)
    {
        return MathF.Log10(Child.Evaluate(coords));
    }

    public override float Evaluate(Vector3 coords)
    {
        return MathF.Log10(Child.Evaluate(coords));
    }
}

