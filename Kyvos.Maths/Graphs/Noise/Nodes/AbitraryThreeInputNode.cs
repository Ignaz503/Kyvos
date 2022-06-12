using Kyvos.Maths.Graphs.Noise.Nodes.Base;
using Kyvos.Maths.Graphs.Noise.Nodes.InputLabelResolvers;
using System.Numerics;

namespace Kyvos.Maths.Graphs.Noise.Nodes;
public class AbitraryThreeInputNode<TLabel> : ThreeChildNode<TLabel>
{
    public delegate float Calculation(float lhs, float middle, float rhs);

    protected Calculation abitraryFunc;
    public AbitraryThreeInputNode(Calculation func, IChildLabelToIndexResolver resolver) : base(resolver)
    {
        this.abitraryFunc = func;
    }


    public override float Evaluate(Vector2 coords)
    {
        return abitraryFunc(FirstChild.Evaluate(coords), SecondChild.Evaluate(coords), ThirdChild.Evaluate(coords));
    }

    public override float Evaluate(Vector3 coords)
    {
        return abitraryFunc(FirstChild.Evaluate(coords), SecondChild.Evaluate(coords), ThirdChild.Evaluate(coords));
    }
}
