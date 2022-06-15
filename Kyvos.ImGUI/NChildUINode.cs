namespace Kyvos.ImGUI;

public abstract class NChildUINode : IUINode
{
    protected List<IUINode> children;

    public NChildUINode()
    {
        children = new();
    }

    public void AppendChild(IUINode node)
        => children.Add(node);
    

    public void AppendChildAt(IUINode node, int idx)
        => children.Insert(idx, node);
    

    public abstract bool Equals(IUINode? other);

    public int IdxOfChild(IUINode child)
        => children.IndexOf(child);


    public void RemoveChild(IUINode node)
        => children.Remove(node);

    public abstract void Show();
}
