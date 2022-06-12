using Kyvos.Maths.Graphs.Noise.Nodes.Base;
using Kyvos.Maths.Graphs.Noise.Nodes.InputLabelResolvers;
using System.Numerics;

namespace Kyvos.Maths.Graphs.Noise.Nodes;
public class ConstValueNode<TLabel> : LeafNode<TLabel>
{
    public float ConstValue;
    public ConstValueNode(float constValue) : base(IgnoreResolver.Instance)
        => ConstValue = constValue;

    public override float Evaluate(Vector2 coords)
        => ConstValue;


    public override float Evaluate(Vector3 coords)
       => ConstValue;
}


