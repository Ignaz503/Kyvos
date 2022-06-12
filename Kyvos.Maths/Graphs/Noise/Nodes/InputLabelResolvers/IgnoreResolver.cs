namespace Kyvos.Maths.Graphs.Noise.Nodes.InputLabelResolvers;
public sealed class IgnoreResolver : IChildLabelToIndexResolver
{
    public readonly static IgnoreResolver Instance = new();

    private IgnoreResolver() { }

    public int Resolve(string childLabel)
    {
        return -1;
    }
}

