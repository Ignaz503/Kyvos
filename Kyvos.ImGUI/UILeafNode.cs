namespace Kyvos.ImGUI;

public abstract class UILeafNode : IUINode
{
    public void AppendChild(IUINode node)
    {}

    public void AppendChildAt(IUINode node, int idx)
    {}

    public abstract bool Equals(IUINode? other);

    public int IdxOfChild(IUINode node)
        => -1;

    public void RemoveChild(IUINode node)
    { }

    public abstract void Show();
}
