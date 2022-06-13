using Serilog;
using System.Diagnostics;

namespace Kyvos.Core.Logging;

public interface ILogSetup
{
    IApplication? Application { get; init; }

    internal LoggerConfiguration Build();

    /// <summary>
    /// writes log events to console output
    /// </summary>
    /// <param name="minimumLevelToShow">the min level for an event to be shown</param>
    /// <param name="outputTemplate">template for output</param>
    ILogSetup WithConsoleLogging(LogLevel minimumLevelToShow = LogLevel.Verbose, string outputTemplate = "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}");

    /// <summary>
    /// writes log events to debug output
    /// </summary>
    /// <param name="minimumLevelToShow">the min level for an event to be shown</param>
    /// <param name="outputTemplate">template for output</param>
    ILogSetup WithDebugLogging(LogLevel minimumLevelToShow = LogLevel.Verbose, string outputTemplate = "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}");

    /// <summary>
    /// writes log events to file at path
    /// </summary>
    /// <param name="path">path of file</param>
    /// <param name="minimumLevelToShow">min level of event to be shown in log</param>
    /// <param name="outputTemplate">template for output</param>
    ILogSetup WithFileLogging(string path, LogLevel minimumLevelToShow = LogLevel.Verbose, string outputTemplate = "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}");


    ILogSetup WithApplicationLogging(LogLevel minimumLevelToShow = LogLevel.Verbose, string outputTemplate = "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}");

    /// <summary>
    /// adds thread name to log event which will be displayed if in template
    /// </summary>
    ILogSetup ShowThreadName();
    /// <summary>
    /// adds thread id to log event which will be displayed if in template
    /// </summary>
    ILogSetup ShowTheeadId();
    
    /// <summary>
    /// adds environment name to log event which will be displayed if in template
    /// </summary>
    ILogSetup ShowEnvironmentName();

    /// <summary>
    /// adds environment user name to log event which will be displayed if in template
    /// </summary>
    ILogSetup ShowEnvironmentUserName();

    /// <summary>
    /// adds machine name to log event which will be displayed if in template
    /// </summary>
    ILogSetup ShowMachineName();

    /// <summary>
    /// adds process name to log event which will be displayed if in template
    /// </summary>
    ILogSetup ShowProcessName();

    /// <summary>
    /// adds environment id to log event which will be displayed if in template
    /// </summary>
    ILogSetup ShowProcessId();

    ILogSetup ShowCustomProperty(ILogProperty logProperty, bool cached = false, LogLevel? minmumLevel = null);

    /// <summary>
    /// sets the minimum level of events to be shown.
    /// If never called defaults to <see cref="LogLevel.Information"/>
    /// </summary>
    /// <param name="eventLevel">minimum level for events to be logged</param>
    ILogSetup WithMinimumLevel(LogLevel eventLevel);

    public static ILogSetup Default =>
         new DefaultSerilogLogSetupHandler();

}
