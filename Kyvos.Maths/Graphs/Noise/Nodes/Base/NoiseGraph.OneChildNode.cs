using Kyvos.Maths.Graphs.Noise.Nodes.InputLabelResolvers;
using System;
namespace Kyvos.Maths.Graphs.Noise.Nodes.Base;
public abstract class OneChildNode<TLabel> : Node<TLabel>
{
    protected OneChildNode(IChildLabelToIndexResolver resolver) : base(resolver)
    {
    }

    protected INoiseGraphNode<TLabel> Child { get; set; }

    public override void Validate(Action<TLabel> calllback)
    {
        base.Validate(calllback);
        Child.Validate(calllback);
    }

    public override void AppendChild(INoiseGraphNode<TLabel> node)
    {
        Child = node;
    }

    public override void AppendChildAt(INoiseGraphNode<TLabel> node, int idx)
    {
        if (idx > 0)
            throw new IndexOutOfRangeException($"Node '{Label}' can only have one child. Index needs to be 0");
        Child = node;
    }

    public override void AppendChildAs(INoiseGraphNode<TLabel> node, string childLabel)
    {
        AppendChildAt(node, ChildLabelIndexResolver.Resolve(childLabel));
    }

    public override bool HasChild(TLabel label)
    {
        return Child is null ? false : Child.Label.Equals(label);
    }

    public override void RemoveChild(TLabel label)
    {
        if (HasChild(label))
            Child = null;
    }

    public override int IdxOfChild(TLabel label)
    {
        return HasChild(label) ? 0 : -1;
    }
}
