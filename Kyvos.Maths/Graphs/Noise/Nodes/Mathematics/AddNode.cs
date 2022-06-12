using Kyvos.Maths.Graphs.Noise.Nodes.Base;
using Kyvos.Maths.Graphs.Noise.Nodes.InputLabelResolvers;
using System.Numerics;

namespace Kyvos.Maths.Graphs.Noise.Nodes.Mathematics;
public class AddNode<TLabel> : TwoChildNode<TLabel>
{
    public AddNode() : base(ABResolver.Instance)
    {
    }

    public override float Evaluate(Vector2 coords)
    {
        return FirstChild.Evaluate(coords) + SecondChild.Evaluate(coords);
    }

    public override float Evaluate(Vector3 coords)
    {
        return FirstChild.Evaluate(coords) + SecondChild.Evaluate(coords);
    }
}
