using Kyvos.Maths.Graphs.Noise.Nodes.Base;
using System.Numerics;

namespace Kyvos.Maths.Graphs.Noise;
public partial class NoiseGraph
{
    INoiseGraphNode root;

    private NoiseGraph(INoiseGraphNode root)
    {
        this.root = root;
    }

    public float Evaluate(Vector2 coords)
        => root.Evaluate(coords);

    public float Evaluate(Vector3 coords)
        => root.Evaluate(coords);

}

//ICodeEmittable { EmitCSharp(IEmitter emitter); }
//ICodeEmitter { void Emit(string code); string CreateLocalVariable(string ofType, string initializationCode); }
//INoiseGenInstantiator : ICodeEmittable { INoiseGenerator Instantiate(); } -> A node is not a instantiator

