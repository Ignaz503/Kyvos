using Kyvos.Maths.Graphs.Noise.Nodes.Base;
using Kyvos.Maths.Graphs.Noise.Nodes.InputLabelResolvers;
using System.Numerics;

namespace Kyvos.Maths.Graphs.Noise.Nodes;
public class AbitraryOneInputNode<TLabel> : OneChildNode<TLabel>
{
    public delegate float Calculation(float input);

    protected Calculation abitraryFunc;

    public AbitraryOneInputNode(Calculation func, IChildLabelToIndexResolver resolver) : base(resolver)
    {
        this.abitraryFunc = func;
    }

    public AbitraryOneInputNode(Calculation func) : this(func, ZeroIndexResolver.Instance)
    { }

    public override float Evaluate(Vector2 coords)
    {
        return abitraryFunc(Child.Evaluate(coords));
    }

    public override float Evaluate(Vector3 coords)
    {
        return abitraryFunc(Child.Evaluate(coords));
    }
}
