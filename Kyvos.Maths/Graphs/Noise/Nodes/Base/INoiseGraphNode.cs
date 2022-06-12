using System;
using System.Numerics;

namespace Kyvos.Maths.Graphs.Noise.Nodes.Base;
public interface INoiseGraphNode
{
    float Evaluate(Vector2 coords);
    float Evaluate(Vector3 coords);
}

public interface INoiseGraphNode<TLabel> : INoiseGraphNode
{
    TLabel Label { get; set; }

    void Validate(Action<TLabel> calllback);

    void AppendChild(INoiseGraphNode<TLabel> node);

    void AppendChildAt(INoiseGraphNode<TLabel> node, int idx);

    void AppendChildAs(INoiseGraphNode<TLabel> node, string childLabel);

    bool HasChild(TLabel label);

    int IdxOfChild(TLabel label);

    void RemoveChild(TLabel label);

}
