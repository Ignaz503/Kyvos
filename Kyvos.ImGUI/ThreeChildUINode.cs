namespace Kyvos.ImGUI;

public abstract class ThreeChildUINode : FixedChildSizeUINode
{
    protected ThreeChildUINode() : base(3)
    { }

    protected IUINode? FirstChild => Children[0];
    protected IUINode? SecondChild => Children[1];
    protected IUINode? ThirdChild => Children[2];
}
