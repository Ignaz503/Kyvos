using ImGuiNET;
using System.Numerics;

namespace Kyvos.ImGUI;

public class Text : UILeafNode 
{
    public string Content { get; set; }

    public Text(string content )
    {
        Content = content;
    }

    public override bool Equals(IUINode? other)
        => this == other;

    public override void Show()
    {
        ImGui.Text(Content);
    }

    public override void Dispose()
    {}
}

public class ColoredText : UILeafNode 
{
    public string Content { get; set; }
    public Vector4 Color { get; set; }

    public ColoredText()
    {
        Color = Vector4.One;
        Content = string.Empty;
    }

    public ColoredText(string content)
    {
        Color = Vector4.One;
        Content = content;
    }

    public ColoredText(Vector4 color)
    {
        Color = color;
        Content = string.Empty; 
    }
    public ColoredText(string content, Vector4 color)
    {
        Color = color;
        Content = content;
    }

    public override bool Equals(IUINode? other)
        => this == other;

    public override void Show()
    {
        ImGui.TextColored(Color,Content);
    }

    public override void Dispose()
    {}
}
