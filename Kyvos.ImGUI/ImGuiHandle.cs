using Kyvos.Core;
using Kyvos.Core.Logging;
using Kyvos.VeldridIntegration;
using Veldrid;

namespace Kyvos.ImGUI;

public partial struct ImGuiHandle : IDisposable
{
    bool isDisposed;
    ImGuiWrapper wrapper;

    public GraphicsDeviceHandle GfxDeviceHandle => wrapper.GraphicsDeviceHandle;
    public Window Window => wrapper.Window;

    public IApplication Application { get; private set; }

    public ImGuiHandle() 
    {
        throw new InvalidOperationException($"Must supply {typeof(IApplication).Name} to ImguiData");
    }

    public ImGuiHandle(int width, int height, IApplication app, Framebuffer mainFrameBuffer)
    {
        
        isDisposed = false;
        Application = app;
        ImGuiWrapper.Reference();
        wrapper = ImGuiWrapper.Get(width, height, mainFrameBuffer, app);

    }

    public ImGuiHandle(int width, int height, IApplication app)
    {
        isDisposed = false;
        Application = app;
        ImGuiWrapper.Reference();
        wrapper = ImGuiWrapper.Get(width, height,app);
    }

    public ImGuiHandle(IApplication app)
    {
        isDisposed = false;
        Application = app;
        ImGuiWrapper.Reference();
        wrapper = ImGuiWrapper.Get(app);
    }

    public void Dispose()
    {
        if (isDisposed)
            return;

        Log<ImGuiHandle>.Information("Disposing {Obj}",nameof(ImGuiHandle));

        wrapper.Dispose();


        isDisposed = true;
    }

    internal void Render(GraphicsDevice gd, CommandList cl)
        => wrapper.Render(gd, cl);

    void Update(float deltaSeconds, InputSnapshot snapshot)
        => wrapper.Update(deltaSeconds, snapshot);

    public void RecreateFontDeviceTexture()
        => wrapper.RecreateFontDeviceTexture();

    public void RecreateFontDeviceTexture(GraphicsDevice gd)
        => wrapper.RecreateFontDeviceTexture(gd);

    public IntPtr GetOrCreateImGuiBinding(ResourceFactory factory, TextureView textureView)
        => wrapper.GetOrCreateImGuiBinding(factory, textureView);

    public void RemoveImGuiBinding(TextureView textureView)
        => wrapper.RemoveImGuiBinding(textureView);

    public IntPtr GetOrCreateImGuiBinding(ResourceFactory factory, Texture texture)
        => wrapper.GetOrCreateImGuiBinding(factory, texture);

    public void RemoveImGuiBinding(Texture texture)
        => wrapper.RemoveImGuiBinding(texture);

    public ResourceSet GetImageResourceSet(IntPtr imGuiBinding)
        => wrapper.GetImageResourceSet(imGuiBinding);

    public void ClearCachedImageResources()
        => wrapper.ClearCachedImageResources();


}
