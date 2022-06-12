using Serilog.Core;
using Serilog.Events;

namespace Kyvos.Core.Logging;

internal class CachedCustomLogProperty : ILogEventEnricher
{
    LogEventProperty cachedProperty;

    public CachedCustomLogProperty(ILogProperty logProperty)
    {
        cachedProperty = new LogEventProperty(logProperty.Name, new ScalarValue(logProperty.Value));
    }

    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
    {
        logEvent.AddPropertyIfAbsent(cachedProperty);
    }
}
