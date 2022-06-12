using Kyvos.Maths.Graphs.Noise.Nodes.InputLabelResolvers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Kyvos.Maths.Graphs.Noise.Nodes.Base;
public abstract class NChildNode<TLabel> : Node<TLabel>
{
    protected NChildNode(IChildLabelToIndexResolver resolver) : base(resolver)
    {
    }

    protected List<INoiseGraphNode<TLabel>> Children { get; set; } = new();

    public override void Validate(Action<TLabel> calllback)
    {
        base.Validate(calllback);
        foreach (var n in Children)
            n.Validate(calllback);
    }

    public override void AppendChild(INoiseGraphNode<TLabel> node)
    {
        if (!Children.Contains(node))
            Children.Add(node);
    }

    public override void AppendChildAt(INoiseGraphNode<TLabel> node, int idx)
    {
        Children.Insert(idx, node);
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
        Children.RemoveAll(node => node.Label.Equals(label));
    }

    public override int IdxOfChild(TLabel label)
    {
        var c = Children.TakeWhile(t => !t.Label.Equals(label)).Count();
        return c < Children.Count ? c : -1;
    }
}

