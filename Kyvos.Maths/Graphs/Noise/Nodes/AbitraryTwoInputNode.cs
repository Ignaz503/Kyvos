using Kyvos.Maths.Graphs.Noise.Nodes.Base;
using Kyvos.Maths.Graphs.Noise.Nodes.InputLabelResolvers;
using System.Numerics;

namespace Kyvos.Maths.Graphs.Noise.Nodes;
public class AbitraryTwoInputNode<TLabel> : TwoChildNode<TLabel>
{
    public delegate float Calculation(float lhs, float rhs);

    protected Calculation abitraryFunc;
    public AbitraryTwoInputNode(Calculation func, IChildLabelToIndexResolver resolver) : base(resolver)
    {
        this.abitraryFunc = func;
    }

    public AbitraryTwoInputNode(Calculation func) : this(func, ABResolver.Instance) { }

    public override float Evaluate(Vector2 coords)
    {
        return abitraryFunc(FirstChild.Evaluate(coords), SecondChild.Evaluate(coords));
    }

    public override float Evaluate(Vector3 coords)
    {
        return abitraryFunc(FirstChild.Evaluate(coords), SecondChild.Evaluate(coords));
    }
}


