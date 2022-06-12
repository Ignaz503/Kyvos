using Kyvos.Maths.Graphs.Noise.Nodes.Base;
using Kyvos.Maths.Graphs.Noise.Nodes.InputLabelResolvers;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace Kyvos.Maths.Graphs.Noise.Nodes;
public class AbitraryNInputNode<TLabel> : NChildNode<TLabel>
{
    public delegate float Calculation(IEnumerator<float> results, int childCount);

    Calculation abitraryFunc;

    public AbitraryNInputNode(Calculation func, IChildLabelToIndexResolver resolver) : base(resolver)
    {
        this.abitraryFunc = func;
    }

    public AbitraryNInputNode(Calculation func) : this(func, new NChildIndexResolver())
    {
        (this.ChildLabelIndexResolver as NChildIndexResolver).CountGetter = () => this.Children.Count;
    }

    public override float Evaluate(Vector2 coords)
    {
        return abitraryFunc(enumerate(node => node.Evaluate(coords)), Children.Count);
    }

    public override float Evaluate(Vector3 coords)
    {
        return abitraryFunc(enumerate(node => node.Evaluate(coords)), Children.Count);
    }

    IEnumerator<float> enumerate(Func<INoiseGraphNode, float> eval)
    {
        foreach (var child in Children)
        {
            yield return eval(child);
        }
    }

}

