using ImGuiNET;
using System.Numerics;
using Kyvos.Graphics;
using Kyvos.Core.Logging;
using Kyvos.Assets;
using Kyvos.Core;
using System.Diagnostics;

namespace Kyvos.ImGUI;

public class Image : UILeafNode
{
    bool isDisposed = false;
    readonly IntPtr binding;
    ImGuiHandle handle;

    readonly Texture image;
    readonly Veldrid.TextureView view;

    Vector2 size;
    public Vector2 Size { get => size; set => SetSizeConstrained(ref size, value); }

    public Vector2 UV { get; set; }
    public Vector2 UV1 { get; set; }
    public Vector4 TintColor { get; set; }
    public Vector4 BorderColor { get; set; }

    public Image(ImGuiHandle handle, Texture texture, Vector2? size = null)
    {
        this.handle = handle;
        this.image = texture;

        if (size.HasValue)
            this.size = size.Value;
        else
            this.size = new(image.Width, image.Height);

        var gfxDevice = handle.GfxDeviceHandle.GfxDevice;
        view = texture.GetTextureView(gfxDevice);
        binding = handle.GetOrCreateImGuiBinding(gfxDevice.ResourceFactory, view);
        UV = Vector2.Zero;
        UV1 = Vector2.One;
        TintColor = Vector4.One;
        BorderColor = Vector4.Zero;
    }

    public Image(AssetIdentifier id, ImGuiHandle handle, Vector2? size = null)
    {
        this.handle = handle;
        this.image = GetComponent<TextureLoader>(handle.Application).Load(id,false);

        if (size.HasValue)
            this.size = size.Value;
        else
            this.size = new(image.Width, image.Height);

        var gfxDevice = handle.GfxDeviceHandle.GfxDevice;
        view = image.GetTextureView(gfxDevice);
        binding = handle.GetOrCreateImGuiBinding(gfxDevice.ResourceFactory, view);
        Log<Image>.Debug("binding is intptr zero: {val}", binding == IntPtr.Zero);
        Log<Image>.Debug("size: {Size}", Size);

        UV = Vector2.Zero;
        UV1 = Vector2.One;
        TintColor = Vector4.One;
        BorderColor = Vector4.Zero;
    }

    T GetComponent<T>(IApplication application)
    {
        Debug.Assert(application.HasComponent<T>(), $"{nameof(Image)} requires {nameof(T)} to function");
        return application.GetComponent<T>()!;
    }

    private void SetSizeConstrained(ref Vector2 size, Vector2 value)
    {
        if (value.X < 0 || value.Y < 0) 
        {
            Log<Image>.Debug("Size values can't be smaller than 0. Actual: {Value}", value);
            return;
        }
        if (value.X > image.Width || value.Y > image.Height) 
        {
            Log<Image>.Debug("Size values can't be biger than ({W},{H}). Actual: {Value}",image.Width,image.Height,value);
            return;
        }
        size = value;
    }

    public override bool Equals(IUINode? other)
        => this == other;

    public override void Show()
    {
        ImGui.Image(binding, Size, UV, UV1,TintColor, BorderColor);
        
    }

    public override void Dispose()
    {
        if (isDisposed)
            return;

        handle.RemoveImGuiBinding(view);
        view.Dispose();
        image.Dispose();

        isDisposed = true;
    }

}
