using ImGuiNET;

namespace Kyvos.ImGUI;

public class Group : VariableChildUINode
{
    public string? Name { get; set; }
    bool open = true;
    public bool Open { get => open; set => open = value; }

    public ImGuiWindowFlags Flags { get; set; }

    public override bool Equals(IUINode? other)
        => this == other;

    public override void Show()
    {
        ImGui.Begin(Name ?? "No Name", ref open, Flags);
        foreach(var child in children)
            child.Show();
        ImGui.End();
    }
}