using Kyvos.Core;
using Kyvos.Core.Logging;
using Kyvos.Utility;
using Kyvos.VeldridIntegration;
using System.Diagnostics;
using Veldrid;

namespace Kyvos.ImGUI;

internal class ImGuiWrapper : IDisposable
{
    static ImGuiWrapper wrapper = new();

    ReferenceCounter refCounter; 
    ImGuiRenderer? renderer;
    GraphicsDeviceHandle? gfxDHandle;
    public GraphicsDeviceHandle GraphicsDeviceHandle => gfxDHandle!;
    Window? window;
    public Window Window => window!;
    
    bool isDisposed = false;
    object _lockObj = new();
    
    private ImGuiWrapper() 
    {
        refCounter = new(0);
    }

    void SetupImgui(int width, int height, Framebuffer mainFrameBuffer, IApplication application) 
    {
        Debug.Assert(application.HasComponent<GraphicsDeviceHandle>(), "Gfx device needed to use ImGui");
        Debug.Assert(application.HasComponent<Window>());
        this.window = application.GetComponent<Window>()!;
        this.gfxDHandle = application.GetComponent<GraphicsDeviceHandle>()!;
        renderer = new ImGuiRenderer(GraphicsDeviceHandle.GfxDevice, mainFrameBuffer.OutputDescription, width, height, ColorSpaceHandling.Linear);
        RegisterToWindowEvent(window);
    }

    private void RegisterToWindowEvent(Window window)
    {
        //no double register
        window.OnWindowEvent -= OnWindowEvent;
        window.OnWindowEvent += OnWindowEvent;
    }

    public IntPtr GetOrCreateImGuiBinding(ResourceFactory factory, TextureView textureView)
    {
        lock (_lockObj) 
        {
            return renderer?.GetOrCreateImGuiBinding(factory, textureView) ?? IntPtr.Zero;
        }
    }

    public void RemoveImGuiBinding(TextureView textureView)
    {
        lock (_lockObj) 
        {
            renderer?.RemoveImGuiBinding(textureView);
        }
    }

    public IntPtr GetOrCreateImGuiBinding(ResourceFactory factory, Texture texture)
    {
        lock (_lockObj) 
        {
            return renderer?.GetOrCreateImGuiBinding(factory, texture) ?? IntPtr.Zero;
        }
    }

    public void RemoveImGuiBinding(Texture texture)
    {
        lock (_lockObj) 
        {
            renderer?.RemoveImGuiBinding(texture);
        }
    }

    public ResourceSet GetImageResourceSet(IntPtr imGuiBinding)
    {
        lock(_lockObj)
        {
            return renderer?.GetImageResourceSet(imGuiBinding)!;//function throws internally if not mapped
        }
    }

    public void ClearCachedImageResources()
    {
        lock(_lockObj)
        {
            renderer?.ClearCachedImageResources();
        }
    }

    private void OnWindowEvent(Window.Event ev)
    {
        if (renderer is null)
            return;

        if (ev.Type is Window.EventType.Resized)
        {
            //width = ev.Window.Width;
            //height = ;
            renderer.WindowResized(ev.Window.Width, ev.Window.Height);
        }
    }

    private void UnregisterWindowEvent(Window window)
    {
        window.OnWindowEvent -= OnWindowEvent;
    }

    internal static ImGuiWrapper Get(IApplication application) 
    {
        Debug.Assert(application.HasComponent<GraphicsDeviceHandle>(), "Gfx device needed to render ImGui");
        Debug.Assert(application.HasComponent<Window>(), "Need window to render ImGui");
        lock (wrapper._lockObj) 
        {
            if(wrapper.renderer is null)
                wrapper.SetupImgui(application.GetComponent<Window>()!.Width, application.GetComponent<Window>()!.Height, application.GetComponent<GraphicsDeviceHandle>()!.GfxDevice.MainSwapchain.Framebuffer, application);
        }
        return wrapper;
    }

    internal static ImGuiWrapper Get(int width, int height, IApplication application)
    {
        lock (wrapper._lockObj)
        {
            if (wrapper.renderer is null)
                wrapper.SetupImgui(width, height, application.GetComponent<GraphicsDeviceHandle>()!.GfxDevice.MainSwapchain.Framebuffer, application);
        }
        return wrapper;
    }

    internal static ImGuiWrapper Get(int width, int height, Framebuffer mainFrameBuffer,  IApplication application)
    {
        lock (wrapper._lockObj)
        {
            if (wrapper.renderer is null)
                wrapper.SetupImgui(width, height, mainFrameBuffer, application);
        }
        return wrapper;
    }

    internal static void Reference()
        => wrapper.refCounter.Increment();

    public void RecreateFontDeviceTexture()
    {
        lock (_lockObj)
        {
            renderer?.RecreateFontDeviceTexture();
        }
    }

    public void RecreateFontDeviceTexture(GraphicsDevice gd)
    {
        lock(_lockObj)
        {
            renderer?.RecreateFontDeviceTexture(gd);
        }
    }

    public void Render(GraphicsDevice gd, CommandList cl) 
        => renderer?.Render(gd, cl);

    public void Update(float deltaSeconds, InputSnapshot snapshot)
        => renderer?.Update(deltaSeconds, snapshot);

    public void Dispose()
    {
        if (isDisposed)
            return;

        var c = refCounter.Decrement();

        if (c > 0)
            return;
        
        Log<ImGuiWrapper>.Debug("Disposing {Obj}",nameof(ImGuiWrapper));


        lock (_lockObj) 
        {
            renderer?.Dispose();
        }
        if (window is not null)
            UnregisterWindowEvent(window);
        window = null;
        isDisposed = true;
    }
} 
