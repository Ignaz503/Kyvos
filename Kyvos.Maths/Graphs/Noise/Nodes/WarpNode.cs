using Kyvos.Maths.Graphs.Noise.CoordinateModifiers;
using Kyvos.Maths.Graphs.Noise.Nodes.Base;
using Kyvos.Maths.Graphs.Noise.Nodes.InputLabelResolvers;
using System;
using System.Numerics;

namespace Kyvos.Maths.Graphs.Noise.Nodes;

public class WarpNode<TLabel> : OneChildNode<TLabel>
{
    ICoordinateModifier coordModifier;

    public WarpNode(ICoordinateModifier modifier) : base(ZeroIndexResolver.Instance)
    {
        this.coordModifier = modifier ?? throw new ArgumentNullException(nameof(modifier));
    }

    public override float Evaluate(Vector2 coords)
    {
        coords = coordModifier.Modify(coords);
        return Child.Evaluate(coords);
    }

    public override float Evaluate(Vector3 coords)
    {
        coords = coordModifier.Modify(coords);
        return Child.Evaluate(coords);
    }
}


