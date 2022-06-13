using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting;

namespace Kyvos.Core.Logging;

public class ApplicationLogSink : ILogEventSink
{
    IApplication app;
    ITextFormatter? formatter;
    public ApplicationLogSink(IApplication app, ITextFormatter? formatter)
    {
        this.app = app;
        this.formatter = formatter;
    }
    
    public void Emit(LogEvent logEvent)
    {
        using StringWriter writer = new();
        formatter?.Format(logEvent, writer);

        app.Publish(new LogMessage((LogLevel)logEvent.Level, writer.ToString()));
    }
}
