using Kyvos.Maths.Graphs.Noise.Nodes.Base;
using Kyvos.Maths.Graphs.Noise.Nodes.InputLabelResolvers;
using System.Numerics;

namespace Kyvos.Maths.Graphs.Noise.Nodes.Mathematics;
public class LerpNode<TLabel> : ThreeChildNode<TLabel>
{
    private class LerpLableResolver : IChildLabelToIndexResolver
    {
        public readonly static LerpLableResolver Instance = new();
        private LerpLableResolver()
        {

        }
        public int Resolve(string childLabel)
        {
            var lower = childLabel.ToLower();
            return lower == "a" ? 0 : lower == "b" ? 1 : lower == "t" ? 2 : -1;
        }
    }

    public LerpNode() : base(LerpLableResolver.Instance)
    {
    }

    public override float Evaluate(Vector2 coords)
    {
        return Interpolation.Linear(FirstChild.Evaluate(coords), SecondChild.Evaluate(coords), ThirdChild.Evaluate(coords));
    }

    public override float Evaluate(Vector3 coords)
    {
        return Interpolation.Linear(FirstChild.Evaluate(coords), SecondChild.Evaluate(coords), ThirdChild.Evaluate(coords));
    }
}


