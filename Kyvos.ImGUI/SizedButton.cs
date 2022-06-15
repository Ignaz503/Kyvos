using ImGuiNET;
using System.Numerics;

namespace Kyvos.ImGUI;

public class SizedButton : Button
{
    public Vector2 Size { get; set; }
    public SizedButton(string label,Vector2 size, Action callback) : base(label, callback)
    {
        this.Size = size;
    }

    public override void Show()
    {
        if (ImGui.Button(Label, Size))
            callback();
    }

}
