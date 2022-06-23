using ImGuiNET;

namespace Kyvos.ImGUI;

public class Button : UILeafNode
{
    public string Label { get; set; }

    protected Action callback;

    public Button(string label, Action callback)
    {
        Label = label;
        this.callback = callback;
    }


    public override bool Equals(IUINode? other)
    => this == other;
    

    public override void Show()
    {
        if (ImGui.Button(Label))
            callback();
    }

    public override void Dispose()
    {}
}
