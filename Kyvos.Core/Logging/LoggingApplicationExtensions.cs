using Kyvos.Core;
namespace Kyvos.Core.Logging;
public static class LoggingApplicationExtensions
{
    public static IModifyableApplication WithLogging(this IModifyableApplication app, Action<ILogSetup> setup) 
    {
        var manager = app.EnsureExistence(LogManager.Instance)!;

        ILogSetup setupConfig = new DefaultSerilogLogSetupHandler(app);
        setup(setupConfig);
        manager.Setup(setupConfig);
        return app;
    } 

}

