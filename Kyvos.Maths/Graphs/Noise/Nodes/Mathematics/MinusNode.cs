using Kyvos.Maths.Graphs.Noise.Nodes.Base;
using Kyvos.Maths.Graphs.Noise.Nodes.InputLabelResolvers;
using System.Numerics;

namespace Kyvos.Maths.Graphs.Noise.Nodes.Mathematics;
public class MinusNode<TLabel> : OneChildNode<TLabel>
{
    public MinusNode() : base(ZeroIndexResolver.Instance)
    {
    }

    public override float Evaluate(Vector2 coords)
    {
        return -Child.Evaluate(coords);
    }

    public override float Evaluate(Vector3 coords)
    {
        return -Child.Evaluate(coords);
    }
}
