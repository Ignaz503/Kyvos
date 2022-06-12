using Kyvos.Maths.Graphs.Noise.Nodes.InputLabelResolvers;
using System;
using System.Numerics;

namespace Kyvos.Maths.Graphs.Noise.Nodes.Base;

public abstract class Node<TLabel> : INoiseGraphNode<TLabel>
{
    public IChildLabelToIndexResolver ChildLabelIndexResolver { get; set; }

    public TLabel Label { get; set; }

    public Node(IChildLabelToIndexResolver resolver)
    {
        this.ChildLabelIndexResolver = resolver;
    }

    public abstract float Evaluate(Vector2 coords);
    public abstract float Evaluate(Vector3 coords);

    public abstract void AppendChild(INoiseGraphNode<TLabel> node);

    public virtual void Validate(Action<TLabel> callback)
    {
        callback(Label);
    }

    public abstract void AppendChildAt(INoiseGraphNode<TLabel> node, int idx);
    public abstract void AppendChildAs(INoiseGraphNode<TLabel> node, string childLabel);
    public abstract bool HasChild(TLabel label);
    public abstract void RemoveChild(TLabel label);
    public abstract int IdxOfChild(TLabel label);
}


