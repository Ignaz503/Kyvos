namespace Kyvos.Maths.Graphs.Noise.Nodes.InputLabelResolvers;
public sealed class XYResolver : IChildLabelToIndexResolver
{
    public readonly static XYResolver Instance = new();

    private XYResolver()
    {

    }

    public int Resolve(string childLabel)
    {
        var lower = childLabel.ToLower();
        return lower == "x" ? 0 : lower == "y" ? 1 : -1;
    }
}


