using Kyvos.Maths.Graphs.Noise.Nodes.InputLabelResolvers;

namespace Kyvos.Maths.Graphs.Noise.Nodes.Base;
public abstract class LeafNode<TLabel> : Node<TLabel>
{
    protected LeafNode(IChildLabelToIndexResolver resolver) : base(resolver)
    {
    }

    public override void AppendChild(INoiseGraphNode<TLabel> node)
        => throw new AppendChildToLeafException<TLabel>(Label, node.Label);


    public override void AppendChildAs(INoiseGraphNode<TLabel> node, string childLabel)
        => throw new AppendChildToLeafException<TLabel>(Label, node.Label);


    public override void AppendChildAt(INoiseGraphNode<TLabel> node, int idx)
        => throw new AppendChildToLeafException<TLabel>(Label, node.Label);


    public override bool HasChild(TLabel label) => false;

    public override int IdxOfChild(TLabel label)
    {
        return -1;
    }

    public override void RemoveChild(TLabel label)
    { }
}

