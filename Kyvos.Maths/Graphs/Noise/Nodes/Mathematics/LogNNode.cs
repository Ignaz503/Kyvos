﻿using Kyvos.Maths.Graphs.Noise.Nodes.Base;
using Kyvos.Maths.Graphs.Noise.Nodes.InputLabelResolvers;
using System;
using System.Numerics;

namespace Kyvos.Maths.Graphs.Noise.Nodes.Mathematics;
public class LogNNode<TLabel> : OneChildNode<TLabel>
{
    public LogNNode() : base(ZeroIndexResolver.Instance)
    { }

    public override float Evaluate(Vector2 coords)
    {
        return MathF.Log(Child.Evaluate(coords));
    }

    public override float Evaluate(Vector3 coords)
    {
        return MathF.Log(Child.Evaluate(coords));
    }
}

