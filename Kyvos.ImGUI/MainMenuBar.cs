using ImGuiNET;

namespace Kyvos.ImGUI;

public sealed class MainMenuBar : VariableChildUINode
{
    public override bool Equals(IUINode? other)
    {
        return this == other; // just do a reference equality
        //if (other is not MainMenuBar menu)
        //    return false;

        //if (this.children.Count != menu.children.Count)
        //    return false;

        ////check all children are the same
        //for (int i = 0; i < children.Count; i++)
        //{
        //    if (!EqualityComparer<IUINode>.Default.Equals(children[i], menu.children[i]))
        //        return false;
        //}


        //return true;
    }

    public override void Show()
    {
        if (ImGui.BeginMainMenuBar()) 
        {
            foreach (var child in children)
                child.Show();
            ImGui.EndMainMenuBar();
        }
    }
}
