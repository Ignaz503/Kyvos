using Serilog;
using Serilog.Core;

namespace Kyvos.Core.Logging;

internal sealed class DefaultSerilogLogSetupHandler : ILogSetup
{
    public IApplication? Application { get; init; }

    LoggerConfiguration logConfig;
    public DefaultSerilogLogSetupHandler(IApplication application)
    {
        logConfig = new();
        this.Application = application;
    }

    public DefaultSerilogLogSetupHandler()
    {
        Application = default;
        logConfig = new();
    }
    
    public ILogSetup ShowCustomProperty(ILogProperty logProperty, bool cached = false, LogLevel? minmumLevel = null )
    {
        if (cached)
        {
            AddEnricher(new CachedCustomLogProperty(logProperty), minmumLevel);
        }
        else
        {
            AddEnricher(new CustomLogProperty(logProperty), minmumLevel);
        }
        return this;           
    }

    private void AddEnricher(ILogEventEnricher enricher, LogLevel? minmumLevel)
    {
        if (minmumLevel.HasValue)
        {
            logConfig.Enrich.AtLevel((Serilog.Events.LogEventLevel)minmumLevel.Value, enr => enr.With(enricher));
        }
        else
        {
            logConfig.Enrich.With(enricher);
        }
    }


    public ILogSetup ShowEnvironmentName()
    {
        logConfig.Enrich.WithEnvironmentName();
        return this;
    }

    public ILogSetup ShowEnvironmentUserName()
    {
        logConfig.Enrich.WithEnvironmentUserName();
        return this;
    }

    public ILogSetup ShowMachineName()
    {
        logConfig.Enrich.WithMachineName();
        return this;
    }

    public ILogSetup ShowProcessId()
    {
        logConfig.Enrich.WithProcessId();
        return this;
    }

    public ILogSetup ShowProcessName()
    {
        logConfig.Enrich.WithProcessName();
        return this;
    }


    public ILogSetup ShowTheeadId()
    {
        logConfig.Enrich.WithThreadId();
        return this;
    }

    public ILogSetup ShowThreadName()
    {
        logConfig.Enrich.WithThreadName();
        return this;
    }

    public ILogSetup WithConsoleLogging(LogLevel minimumLevelToShow = LogLevel.Verbose, string outputTemplate = "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")
    {
        logConfig.WriteTo.Console(outputTemplate: outputTemplate, restrictedToMinimumLevel: (Serilog.Events.LogEventLevel)minimumLevelToShow);
        return this;
    }

    public ILogSetup WithDebugLogging(LogLevel minimumLevelToShow = LogLevel.Verbose, string outputTemplate = "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")
    {
        logConfig.WriteTo.Debug(restrictedToMinimumLevel: (Serilog.Events.LogEventLevel)minimumLevelToShow, outputTemplate: outputTemplate);
        return this;
    }

    public ILogSetup WithFileLogging(string path, LogLevel minimumLevelToShow = LogLevel.Verbose, string outputTemplate = "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")
    {
        logConfig.WriteTo.File(path,outputTemplate: outputTemplate, restrictedToMinimumLevel: (Serilog.Events.LogEventLevel)minimumLevelToShow);
        return this;
    }

    public ILogSetup WithApplicationLogging(LogLevel minimumLevelToShow = LogLevel.Verbose, string outputTemplate = "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")
    {
        if (Application is null)
            return this;
        logConfig.WriteTo.Application(Application, outputTemplate: outputTemplate, restrictedToMinimumLevel: minimumLevelToShow);
        return this;
    }

    
    public ILogSetup WithMinimumLevel(LogLevel eventLevel)
    {
        logConfig.MinimumLevel.Is((Serilog.Events.LogEventLevel)eventLevel);
        return this;
    }

    LoggerConfiguration ILogSetup.Build()
        => logConfig;
}