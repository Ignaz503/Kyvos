using ImGuiNET;

namespace Kyvos.ImGUI;

public sealed class MenuItem : UILeafNode
{
    public string Label { get; set; }
    public string ShortCut { get; set; }

    Func<bool> Selected { get; set; }
    Func<bool> Enabled { get; set; }
    Action callback;

    public MenuItem(string label, Action callback, string shortCut = ""):base()
    {
        this.Label = label;
        this.callback = callback;
        ShortCut = shortCut;
        Selected = () => false;
        Enabled = () => true;
    }

    public MenuItem(string label, Action callback, Func<bool> enabled, Func<bool> selected, string shortCut = "") : base()
    {
        this.Label = label;
        this.callback = callback;
        ShortCut = shortCut;
        Selected = selected;
        Enabled = enabled;
    }


    public override bool Equals(IUINode? other)
    {
        return this == other; //ref equality is enough
        //if (other is not MenuItem menuItem)
        //    return false;

        //if (Label != menuItem.Label)
        //    return false;

        //if (this.children.Count != menuItem.children.Count)
        //    return false;

        ////check all children are the same
        //for (int i = 0; i < children.Count; i++)
        //{
        //    if (!EqualityComparer<IUINode>.Default.Equals(children[i], menuItem.children[i]))
        //        return false;
        //}


        //return true;
    }

    public override void Show()
    {
        if (ImGui.MenuItem(Label, ShortCut, Selected(), Enabled()))
        {
            callback();
        }
    }

    public override void Dispose()
    { }
}
