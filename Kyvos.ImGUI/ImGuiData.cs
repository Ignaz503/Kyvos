using Kyvos.Core;
using Kyvos.Core.Logging;
using Kyvos.VeldridIntegration;
using System.Diagnostics;
using Veldrid;

namespace Kyvos.ImGUI;

public partial struct ImGuiData : IDisposable
{
    bool isDisosed;
    int width;
    int height;
    internal ImGuiRenderer renderer; //TODO static resource?

    public ImGuiData() 
    {
        throw new InvalidOperationException($"Must supply {typeof(IApplication).Name} to ImguiData");
    }

    public ImGuiData(int width, int height, IApplication app, Framebuffer mainFrameBuffer)
    {

        isDisosed = false;
        Debug.Assert(app.HasComponent<GraphicsDeviceHandle>());
        var gfxHandle = app.GetComponent<GraphicsDeviceHandle>()!;

        this.width = width;
        this.height = height;
        renderer = new ImGuiRenderer(gfxHandle.GfxDevice, mainFrameBuffer.OutputDescription, width, height, ColorSpaceHandling.Linear);

    }

    public ImGuiData(int width, int height, IApplication app)
    {
        isDisosed = false;
        Debug.Assert(app.HasComponent<GraphicsDeviceHandle>());
        var gfxHandle = app.GetComponent<GraphicsDeviceHandle>()!;

        this.width = width;
        this.height = height;
        renderer = new ImGuiRenderer(gfxHandle.GfxDevice, gfxHandle.GfxDevice.SwapchainFramebuffer.OutputDescription, width, height, ColorSpaceHandling.Linear);

        RegisterToWindowEvent(app);
    }

    public ImGuiData(IApplication app)
    {
        isDisosed = false;
        Debug.Assert(app.HasComponent<GraphicsDeviceHandle>(), "Graphics device must be present");
        Debug.Assert(app.HasComponent<Window>(),"Window must be present");
        var gfxHandle = app.GetComponent<GraphicsDeviceHandle>()!;
        var window = app.GetComponent<Window>()!;

        this.width = window.Width;
        this.height = window.Height;
        renderer = new ImGuiRenderer(gfxHandle.GfxDevice, gfxHandle.GfxDevice.SwapchainFramebuffer.OutputDescription, width, height, ColorSpaceHandling.Linear);

        RegisterToWindowEvent(app);
    }

    private void RegisterToWindowEvent(IApplication app)
    {
        Debug.Assert(app.HasComponent<Window>());
        var window = app.GetComponent<Window>()!;

        //no double register
        window.OnWindowEvent -= OnWindowEvent;
        window.OnWindowEvent += OnWindowEvent;
    }

    private void OnWindowEvent(Window.Event ev)
    {
        if (ev.Type is Window.EventType.Resized) 
        {
            width = ev.Window.Width;
            height = ev.Window.Height;
            renderer.WindowResized(width, height);
        }
    }

    private void UnregisterWindowEvent(IApplication app) 
    {
        Debug.Assert(app.HasComponent<Window>());
        var window = app.GetComponent<Window>()!;
        window.OnWindowEvent -= OnWindowEvent;
    }    
    
    public void Dispose()
    {
        if (isDisosed)
            return;

        Log<ImGuiData>.Information("Disposing ImGui wrapper");

        renderer.Dispose();

        isDisosed = true;
    }

}
