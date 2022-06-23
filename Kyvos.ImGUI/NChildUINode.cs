namespace Kyvos.ImGUI;

public abstract class VariableChildUINode : IUINode
{
    bool isDisposed = false;
    protected List<IUINode> children;

    public VariableChildUINode()
    {
        children = new();
    }

    public void AppendChild(IUINode node)
        => children.Add(node);
    

    public void AppendChildAt(IUINode node, int idx)
        => children.Insert(idx, node);

    public virtual void Dispose()
    {
        if (isDisposed)
            return;

        foreach (var elem in children)
            elem.Dispose();

        isDisposed = true;
    }

    public abstract bool Equals(IUINode? other);

    public int IdxOfChild(IUINode child)
        => children.IndexOf(child);


    public void RemoveChild(IUINode node)
        => children.Remove(node);

    public abstract void Show();
}
