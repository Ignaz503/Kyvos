using Kyvos.Core.Configuration;
using Kyvos.Core;
using System.Diagnostics;
using Veldrid;
using Veldrid.StartupUtilities;

namespace Kyvos.VeldridIntegration;

public static class GUIApplicationExtensions
{
    public static IModifyableApplication With(this IModifyableApplication app, Window window)
    {
        app.AddComponent(window);
        app.RegisterToAppLifetimeQuery(window.GetWindowExits);
        return app;
    }

    public static IModifyableApplication With(this IModifyableApplication app, WindowCreateInfo createInfo)
    {
        var window = app.AddComponent(new Window(createInfo));
        app.RegisterToAppLifetimeQuery(window.GetWindowExits);
        return app;
    }

    public static IModifyableApplication WithWindow(this IModifyableApplication app)
    {
        Debug.Assert(app.HasComponent<IConfig>(), "Application must have config component defined before adding window");
        var window = app.AddComponent(new Window(app.GetComponent<IConfig>()!.ReadValue<WindowCreateInfo>(Window.CONFIG_KEY)));
        app.RegisterToAppLifetimeQuery(window.GetWindowExits);
        return app;
    }

    public static IModifyableApplication With(this IModifyableApplication app, GraphicsDeviceConfig configuration)
    {
        Debug.Assert(app.HasComponent<Window>(), "Application must have window component defined before adding graphics device");

        app.AddComponent(new GraphicsDeviceHandle(configuration, app));
        return app;
    }

    public static IModifyableApplication With(this IModifyableApplication app, GraphicsDevice device)
    {
        app.AddComponent(new GraphicsDeviceHandle(device, app));
        return app;
    }

    public static IModifyableApplication WithGfxDevice(this IModifyableApplication app)
    {
        app.AddComponent(new GraphicsDeviceHandle(app));
        return app;
    }

    public static IModifyableApplication WithTimer(this IModifyableApplication app)
        => TimerApplicationExtensions.WithTimer(app, app.HasComponent<Window>() ? app.GetComponent<Window>()!.DisplayRefreshPerSecond : (1 / 60f));


}