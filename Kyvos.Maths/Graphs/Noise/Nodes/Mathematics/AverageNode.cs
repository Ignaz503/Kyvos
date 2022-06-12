using System.Numerics;

namespace Kyvos.Maths.Graphs.Noise.Nodes.Mathematics;
public class AverageNode<TLabel> : SumNode<TLabel>
{
    public override float Evaluate(Vector2 coords)
    {
        var res = base.Evaluate(coords);
        return res / (float)Children.Count;
    }

    public override float Evaluate(Vector3 coords)
    {
        var res = base.Evaluate(coords);
        return res / (float)Children.Count;
    }
}

