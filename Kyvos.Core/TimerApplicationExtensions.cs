using System.Diagnostics;
using Kyvos.Core.Configuration;
namespace Kyvos.Core;

public static class TimerApplicationExtensions 
{
    public static IModifyableApplication With(this IModifyableApplication app, Timer timer)
    {
        app.AddComponent(timer);
        return app;
    }
    public static IModifyableApplication WithTimer(this IModifyableApplication app, float vSyncFrameRate = 1f / 60f)
    {
        Debug.Assert(app.HasComponent<IConfig>(), "Application must have config component defined before adding timer");

        app.AddComponent(new Timer(app.GetComponent<IConfig>()!.ReadValue<Timer.Config>(Timer.CONFIG_KEY), vSyncFrameRate));
        return app;
    }
}
