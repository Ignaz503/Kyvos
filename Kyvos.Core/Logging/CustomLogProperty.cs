using Serilog.Core;
using Serilog.Events;

namespace Kyvos.Core.Logging;

internal class CustomLogProperty : ILogEventEnricher 
{
    ILogProperty logProperty;

    public CustomLogProperty(ILogProperty logProperty)
    {
        this.logProperty = logProperty;
    }

    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
    {
        logEvent.AddPropertyIfAbsent(new LogEventProperty(logProperty.Name, new ScalarValue(logProperty.Value)));
    }
}
