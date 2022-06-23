namespace Kyvos.ImGUI;

public abstract class OneChildNode : IUINode
{
    bool isDisposed = false;
    protected IUINode? Child { get; set; }

    public void AppendChild(IUINode node)
    {
        Child = node;
    }

    public void AppendChildAt(IUINode node, int idx)
    {
        if (idx > 0)
            throw new IndexOutOfRangeException($"Node '{GetType().Name}' can only have one child. Index needs to be 0");
        Child = node;
    }


    public void RemoveChild(IUINode uINode)
    {
        if (EqualityComparer<IUINode>.Default.Equals(uINode, Child))
            Child = null;
    }

    public int IdxOfChild(IUINode uiNode)
    {
        if (EqualityComparer<IUINode>.Default.Equals(uiNode, Child))
            return 0;
        return -1;
    
    }

    public abstract void Show();

    public abstract bool Equals(IUINode? other);

    public virtual void Dispose()
    {
        if(isDisposed)
            return;
        Child?.Dispose();
        isDisposed = true;
    }
}
