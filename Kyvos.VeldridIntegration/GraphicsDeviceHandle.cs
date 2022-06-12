using Kyvos.Configuration;
using Kyvos.Core;
using System.Diagnostics;
using Veldrid;
using Veldrid.StartupUtilities;

namespace Kyvos.VeldridIntegration;

public class GraphicsDeviceHandle : IDisposable
{
    public GraphicsDevice GfxDevice { get; private set; }
    IApplication app;
    bool isDisposed = false;

    public GraphicsDeviceHandle(GraphicsDeviceConfig configuration, IApplication app)
    {
        Debug.Assert(app.HasComponent<Window>(), "Application must have window component defined before adding graphics device");
        GfxDevice = VeldridStartup.CreateGraphicsDevice(app.GetComponent<Window>()!.Instance, configuration.Options, configuration.Backend);
        this.app = app;
        RegisterToEvents();
    }

    public GraphicsDeviceHandle(GraphicsDevice device, IApplication app)
    {
        GfxDevice = device;
        this.app = app;
        RegisterToEvents();
    }

    public GraphicsDeviceHandle(IApplication app)
    {
        Debug.Assert(app.HasComponent<IConfig>(), "Application must have config component defined before adding graphics device");
        Debug.Assert(app.HasComponent<Window>(), "Application must have window component defined before adding graphics device");
        var configuration = app.GetComponent<IConfig>()!.ReadValue<GraphicsDeviceConfig>(ApplicationConfig.GFX);
        GfxDevice = VeldridStartup.CreateGraphicsDevice(app.GetComponent<Window>()!.Instance, configuration.Options, configuration.Backend);
        this.app = app;
        RegisterToEvents();

    }

    private void RegisterToEvents()
    {
        app.Subscribe<EarlyWindowResizeEvent>(OnWindowResize);
    }

    private void UnregisterFromEvents()
    {
        app.Unsubscribe<EarlyWindowResizeEvent>(OnWindowResize);
    }

    private void OnWindowResize(EarlyWindowResizeEvent ev) 
    {
        GfxDevice.ResizeMainWindow((uint)ev.NewWidth, (uint)ev.NewHeight);
    }

    public void Dispose()
    {
        if (isDisposed)
            return;

        GfxDevice.Dispose();
        app.Publish(new GfxDeviceDisposed());
        UnregisterFromEvents();

        isDisposed = true;
    }


}
