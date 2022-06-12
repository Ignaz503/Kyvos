namespace Kyvos.Core.Configuration;

public static class ConfigApplicationExtensions
{
    public static IModifyableApplication With(this IModifyableApplication app, IConfig config)
    {
        app.AddComponent(config);
        return app;
    }
}
