namespace Kyvos.Maths.Graphs.Noise.Nodes.InputLabelResolvers;
public sealed class ZeroIndexResolver : IChildLabelToIndexResolver
{

    public readonly static ZeroIndexResolver Instance = new();
    private ZeroIndexResolver() { }

    public int Resolve(string childLabel)
    {
        return 0;
    }
}


