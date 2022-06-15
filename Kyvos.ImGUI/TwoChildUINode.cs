namespace Kyvos.ImGUI;

public abstract class TwoChildUINode : FixedChildSizeUINode
{
    protected TwoChildUINode() : base(2)
    {}

    protected IUINode? FirstChild => Children[0];
    protected IUINode? SecondChild => Children[1];
}
