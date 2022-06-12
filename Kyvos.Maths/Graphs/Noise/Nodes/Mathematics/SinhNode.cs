﻿using Kyvos.Maths.Graphs.Noise.Nodes.Base;
using Kyvos.Maths.Graphs.Noise.Nodes.InputLabelResolvers;
using System;
using System.Numerics;

namespace Kyvos.Maths.Graphs.Noise.Nodes.Mathematics;
public class SinhNode<TLabel> : OneChildNode<TLabel>
{
    public SinhNode() : base(ZeroIndexResolver.Instance)
    {
    }

    public override float Evaluate(Vector2 coords)
    {
        return MathF.Sinh(Child.Evaluate(coords));
    }

    public override float Evaluate(Vector3 coords)
    {
        return MathF.Sinh(Child.Evaluate(coords));
    }
}
