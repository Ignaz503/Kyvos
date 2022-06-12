using Kyvos.Maths.Graphs.Noise.Nodes.Base;
using Kyvos.Maths.Graphs.Noise.Nodes.InputLabelResolvers;
using System;
using System.Numerics;

namespace Kyvos.Maths.Graphs.Noise.Nodes.Mathematics;
public class Atan2Node<TLabel> : TwoChildNode<TLabel>
{
    public Atan2Node() : base(XYResolver.Instance)
    {
    }

    public override float Evaluate(Vector2 coords)
    {
        return MathF.Atan2(FirstChild.Evaluate(coords), SecondChild.Evaluate(coords));
    }

    public override float Evaluate(Vector3 coords)
    {
        return MathF.Atan2(FirstChild.Evaluate(coords), SecondChild.Evaluate(coords));
    }
}
