using Kyvos.Maths.Graphs.Noise.Nodes.Base;
using System;

namespace Kyvos.Maths.Graphs.Noise;
public partial class NoiseGraph
{
    public interface INodeCreationPhase<TLabel>
    {
        INodeSetupPhase<TLabel> CreateNode(TLabel lable, Func<INoiseGraphNode<TLabel>> builder);

        INodeSetupPhase<TLabel> Select(TLabel label);

        NoiseGraph Build();
    }

}
