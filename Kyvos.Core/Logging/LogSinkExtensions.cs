using Serilog;
using Serilog.Configuration;
using Serilog.Events;
using Serilog.Formatting.Display;

namespace Kyvos.Core.Logging;

public static class LogSinkExtensions 
{
    public static LoggerConfiguration Application(this LoggerSinkConfiguration sinkConfiguration, IApplication application, LogLevel restrictedToMinimumLevel = LogLevel.Verbose, string outputTemplate = "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}", IFormatProvider? formatProvider = null)
    {
        if (sinkConfiguration == null)
        {
            throw new ArgumentNullException(nameof(sinkConfiguration));
        }
        
        var formatter = new MessageTemplateTextFormatter(outputTemplate, formatProvider);

        return sinkConfiguration.Sink(new ApplicationLogSink(application, formatter), (LogEventLevel)restrictedToMinimumLevel);
    }
}