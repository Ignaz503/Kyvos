namespace Kyvos.ImGUI;

public interface IUINode : IUIComponent, IEquatable<IUINode>
{
    void AppendChild(IUINode node);

    void AppendChildAt(IUINode node, int idx);

    int IdxOfChild(IUINode child);

    void RemoveChild(IUINode node);

}
