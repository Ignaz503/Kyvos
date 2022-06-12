using Kyvos.Maths.Graphs.Noise.Nodes.InputLabelResolvers;

namespace Kyvos.Maths.Graphs.Noise.Nodes.Base;
public abstract class TwoChildNode<TLabel> : FixedChildSizeNode<TLabel>
{
    protected INoiseGraphNode<TLabel> FirstChild => Children[0];
    protected INoiseGraphNode<TLabel> SecondChild => Children[1];

    public TwoChildNode(IChildLabelToIndexResolver resolver) : base(2, resolver)
    {

    }
}

