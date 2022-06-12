using System;

namespace Kyvos.Maths.Graphs.Noise.Nodes.InputLabelResolvers;
public sealed class NChildIndexResolver : IChildLabelToIndexResolver
{
    public Func<int> CountGetter { get; set; }

    public NChildIndexResolver()
    {

    }

    public int Resolve(string childLabel)
    {
        return CountGetter();
    }
}

