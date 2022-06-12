using Kyvos.Maths.Graphs.Noise.Nodes.Base;
using Kyvos.Maths.Graphs.Noise.Nodes.InputLabelResolvers;
using System.Numerics;

namespace Kyvos.Maths.Graphs.Noise.Nodes.Mathematics;
public class MapNode<TLabel> : OneChildNode<TLabel>
{
    float originMin, originMax;
    float destinationMin, destinationMax;
    public MapNode(float oMin = -1f, float oMax = 1f, float dMin = 0f, float dMax = 1f) : base(ZeroIndexResolver.Instance)
     => (originMin, originMax, destinationMin, destinationMax) = (oMin, oMax, dMin, dMax);

    public override float Evaluate(Vector2 coords)
    {
        return Mathf.Map(Child.Evaluate(coords), originMin, originMax, destinationMin, destinationMax);
    }

    public override float Evaluate(Vector3 coords)
    {
        return Mathf.Map(Child.Evaluate(coords), originMin, originMax, destinationMin, destinationMax);
    }
}


