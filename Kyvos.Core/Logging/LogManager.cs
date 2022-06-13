using Serilog;

namespace Kyvos.Core.Logging;

public class LogManager : IDisposable
{
    public readonly static LogManager Instance = new();

    private LogManager()
    { }

    public void Setup(ILogSetup setup) 
    {
        Serilog.Log.Logger = setup.Build().CreateLogger();
    }

    public void Dispose()
    {
        Serilog.Log.CloseAndFlush();
    }
}
