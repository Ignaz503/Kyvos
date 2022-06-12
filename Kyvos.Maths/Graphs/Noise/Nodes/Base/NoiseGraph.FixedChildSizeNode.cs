using Kyvos.Maths.Graphs.Noise.Nodes.InputLabelResolvers;
using System;
using System.Linq;

namespace Kyvos.Maths.Graphs.Noise.Nodes.Base;
public abstract class FixedChildSizeNode<TLabel> : Node<TLabel>
{
    protected INoiseGraphNode<TLabel>[] Children { get; set; }
    int childIndexer = 0;

    public FixedChildSizeNode(int size, IChildLabelToIndexResolver resolver) : base(resolver)
    {
        Children = new INoiseGraphNode<TLabel>[size];
    }

    public override void Validate(Action<TLabel> calllback)
    {
        base.Validate(calllback);
        foreach (var child in Children)
            child.Validate(calllback);
    }

    public override void AppendChild(INoiseGraphNode<TLabel> node)
    {
        Children[childIndexer] = node;
        childIndexer = (childIndexer + 1) % Children.Length;
    }

    public override void AppendChildAt(INoiseGraphNode<TLabel> node, int idx)
    {
        if (idx > Children.Length)
            throw new IndexOutOfRangeException($"Idx for node '{Label}' needs to be in range [0..{Children.Length}[");
        Children[idx] = node;
        childIndexer = (idx + 1) % Children.Length;
    }

    public override void AppendChildAs(INoiseGraphNode<TLabel> node, string childLabel)
    {
        AppendChildAt(node, ChildLabelIndexResolver.Resolve(childLabel));
    }

    public override bool HasChild(TLabel label)
    {
        return Children.Any(c => c.Label.Equals(label));
    }

    public override void RemoveChild(TLabel label)
    {
        for (int i = 0; i < Children.Length; i++)
        {
            var child = Children[i];
            if (child is not null && child.Label.Equals(label))
            {
                Children[i] = null;
                childIndexer = i;
                return;
            }
        }
    }

    public override int IdxOfChild(TLabel label)
    {
        var c = Children.TakeWhile(n => !n.Label.Equals(label)).Count();
        return c < Children.Length ? c : -1;
    }
}

