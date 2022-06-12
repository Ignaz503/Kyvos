namespace Kyvos.Maths.Graphs.Noise.Nodes.InputLabelResolvers;
public sealed class ABResolver : IChildLabelToIndexResolver
{
    public readonly static ABResolver Instance = new();

    private ABResolver()
    {

    }

    public int Resolve(string childLabel)
    {
        var lower = childLabel.ToLower();
        return lower == "a" ? 0 : lower == "b" ? 1 : -1;
    }
}


