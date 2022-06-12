﻿using Kyvos.Maths.Graphs.Noise.Nodes.Base;
using Kyvos.Maths.Graphs.Noise.Nodes.InputLabelResolvers;
using System.Numerics;

namespace Kyvos.Maths.Graphs.Noise.Nodes.Mathematics;
public class ProductNode<TLabel> : NChildNode<TLabel>
{
    public ProductNode() : base(new NChildIndexResolver())
    {
        (ChildLabelIndexResolver as NChildIndexResolver).CountGetter = () => this.Children.Count;
    }

    public override float Evaluate(Vector2 coords)
    {
        float sum = 1;
        foreach (var child in Children)
            sum *= child.Evaluate(coords);
        return sum;
    }

    public override float Evaluate(Vector3 coords)
    {
        float sum = 1;
        foreach (var child in Children)
            sum *= child.Evaluate(coords);
        return sum;
    }
}
