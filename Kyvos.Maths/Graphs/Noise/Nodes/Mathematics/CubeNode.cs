using Kyvos.Maths.Graphs.Noise.Nodes.Base;
using Kyvos.Maths.Graphs.Noise.Nodes.InputLabelResolvers;
using System.Numerics;

namespace Kyvos.Maths.Graphs.Noise.Nodes.Mathematics;
public class CubeNode<TLabel> : OneChildNode<TLabel>
{
    public CubeNode() : base(ZeroIndexResolver.Instance)
    {
    }

    public override float Evaluate(Vector2 coords)
    {
        var res = Child.Evaluate(coords);
        return res * res * res;
    }

    public override float Evaluate(Vector3 coords)
    {
        var res = Child.Evaluate(coords);
        return res * res * res;
    }
}
