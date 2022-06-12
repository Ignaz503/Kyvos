using Kyvos.Maths.Graphs.Noise.Nodes.Base;
using Kyvos.Maths.Graphs.Noise.Nodes.InputLabelResolvers;
using System;
using System.Numerics;

namespace Kyvos.Maths.Graphs.Noise.Nodes;
public class RandomValueNode<TLabel> : LeafNode<TLabel>
{
    public float Min;
    public float Max;
    System.Random rng;
    public RandomValueNode(float min, float max, System.Random rng) : base(IgnoreResolver.Instance)
    {
        this.rng = rng;
        if (min > max)
        {
            var t = max;
            max = min;
            min = t;
        }
        Min = min;
        Max = max;
    }

    float value => (Max - Min) * (float)rng.NextDouble() + Min;

    public override float Evaluate(Vector2 coords)
        => value;

    public override float Evaluate(Vector3 coords)
        => value;
}


