using System.Diagnostics;

namespace Kyvos.ImGUI;

public abstract class FixedChildSizeUINode : IUINode
{
    protected IUINode?[] Children { get; set; }
    int childIndexer = 0;

    public FixedChildSizeUINode(int size)
    {
        Debug.Assert(size > 0, "Size must be greater than 0");
        Children = new IUINode[size];
    }

    public void AppendChild(IUINode node)
    {
        Children[childIndexer] = node;
        childIndexer = (childIndexer + 1) % Children.Length;
    }

    public void AppendChildAt(IUINode node, int idx)
    {
        if (idx > Children.Length)
            throw new IndexOutOfRangeException($"Idx for node '{GetType().Name}' needs to be in range [0..{Children.Length}[");
        Children[idx] = node;
        childIndexer = (idx + 1) % Children.Length;
    }

    public int IdxOfChild(IUINode child)
    {
        for (int i = 0; i < Children.Length; i++)
        {
            if (EqualityComparer<IUINode>.Default.Equals(child, Children[i]))
                return i;
        }
        return -1;
    }

    public void RemoveChild(IUINode node)
    {
        for (int i = 0; i < Children.Length; i++)
        {
            var child = Children[i];
            if (child is not null && EqualityComparer<IUINode>.Default.Equals(child, node))
            {
                Children[i] = null;
                childIndexer = i;
                return;
            }
        }
    }

    public abstract void Show();

    public abstract bool Equals(IUINode? other);
}
