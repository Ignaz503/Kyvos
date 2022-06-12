using Serilog;
using Serilog.Events;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Kyvos.Core.Logging;

/// <summary>
/// Log Levels supported
/// currently basically a forward declaration of Serilog log levels
/// </summary>
public enum LogLevel
{
    //
    // Summary:
    //     Anything and everything you might want to know about a running block of code.
    Verbose,
    //
    // Summary:
    //     Internal system events that aren't necessarily observable from the outside.
    Debug,
    //
    // Summary:
    //     The lifeblood of operational intelligence - things happen.
    Information,
    //
    // Summary:
    //     Service is degraded or endangered.
    Warning,
    //
    // Summary:
    //     Functionality is unavailable, invariants are broken or data is lost.
    Error,
    //
    // Summary:
    //     If you have a pager, it goes off when one of these occurs.
    Fatal
}

/// <summary>
/// Context based logging
/// </summary>
/// <typeparam name="TContext"></typeparam>
public static class Log<TContext>
{
    /// <summary>
    /// Determine if events at the specified level will be passed through
    /// to the log sinks.
    /// </summary>
    /// <param name="level">Level to check.</param>
    /// <returns>True if the level is enabled; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsEnabled(LogLevel level) => Serilog.Log.ForContext<TContext>().IsEnabled((LogEventLevel)level);

    /// <summary>
    ///Write a log event with the <see cref="LogLevel.Verbose"/> level.
    /// </summary>
    /// <param name="messageTemplate">Message template describing the event.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Verbose(string messageTemplate)
    {
        Serilog.Log.ForContext<TContext>().Write(LogEventLevel.Verbose, messageTemplate);
    }

    /// <summary>
    /// Write a log event with the <see cref="LogLevel.Verbose"/> level.
    /// </summary>
    /// <param name="messageTemplate">Message template describing the event.</param>
    /// <param name="propertyValue">Object positionally formatted into the message template.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Verbose<T>(string messageTemplate, T propertyValue)
    {
        Serilog.Log.ForContext<TContext>().Write(LogEventLevel.Verbose, messageTemplate, propertyValue);
    }

    /// <summary>
    /// Write a log event with the <see cref="LogLevel.Verbose"/> level.
    /// </summary>
    /// <param name="messageTemplate">Message template describing the event.</param>
    /// <param name="propertyValue0">Object positionally formatted into the message template.</param>
    /// <param name="propertyValue1">Object positionally formatted into the message template.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Verbose<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
    {
        Serilog.Log.ForContext<TContext>().Write(LogEventLevel.Verbose, messageTemplate, propertyValue0, propertyValue1);
    }

    /// <summary>
    /// Write a log event with the <see cref="LogLevel.Verbose"/> level.
    /// </summary>
    /// <param name="messageTemplate">Message template describing the event.</param>
    /// <param name="propertyValue0">Object positionally formatted into the message template.</param>
    /// <param name="propertyValue1">Object positionally formatted into the message template.</param>
    /// <param name="propertyValue2">Object positionally formatted into the message template.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Verbose<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
    {
        Serilog.Log.ForContext<TContext>().Write(LogEventLevel.Verbose, messageTemplate, propertyValue0, propertyValue1, propertyValue2);
    }

    /// <summary>
    /// Write a log event with the <see cref="LogLevel.Verbose"/> level.
    /// </summary>
    /// <param name="messageTemplate">Message template describing the event.</param>
    /// <param name="propertyValues">Objects positionally formatted into the message template.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Verbose(string messageTemplate, params object[] propertyValues)
    {
        Serilog.Log.ForContext<TContext>().Verbose(messageTemplate, propertyValues);
    }

    /// <summary>
    /// Write a log event with the <see cref="LogLevel.Verbose"/> level and associated exception.
    /// </summary>
    /// <param name="exception">Exception related to the event.</param>
    /// <param name="messageTemplate">Message template describing the event.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Verbose(Exception exception, string messageTemplate)
    {
        Serilog.Log.ForContext<TContext>().Write(LogEventLevel.Verbose, exception, messageTemplate);
    }

    /// <summary>
    /// Write a log event with the <see cref="LogLevel.Verbose"/> level and associated exception.
    /// </summary>
    /// <param name="exception">Exception related to the event.</param>
    /// <param name="messageTemplate">Message template describing the event.</param>
    /// <param name="propertyValue">Object positionally formatted into the message template.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Verbose<T>(Exception exception, string messageTemplate, T propertyValue)
    {
        Serilog.Log.ForContext<TContext>().Write(LogEventLevel.Verbose, exception, messageTemplate, propertyValue);
    }

    /// <summary>
    /// Write a log event with the <see cref="LogLevel.Verbose"/> level and associated exception.
    /// </summary>
    /// <param name="exception">Exception related to the event.</param>
    /// <param name="messageTemplate">Message template describing the event.</param>
    /// <param name="propertyValue0">Object positionally formatted into the message template.</param>
    /// <param name="propertyValue1">Object positionally formatted into the message template.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Verbose<T0, T1>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
    {
        Serilog.Log.ForContext<TContext>().Write(LogEventLevel.Verbose, exception, messageTemplate, propertyValue0, propertyValue1);
    }

    /// <summary>
    /// Write a log event with the <see cref="LogLevel.Verbose"/> level and associated exception.
    /// </summary>
    /// <param name="exception">Exception related to the event.</param>
    /// <param name="messageTemplate">Message template describing the event.</param>
    /// <param name="propertyValue0">Object positionally formatted into the message template.</param>
    /// <param name="propertyValue1">Object positionally formatted into the message template.</param>
    /// <param name="propertyValue2">Object positionally formatted into the message template.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Verbose<T0, T1, T2>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
    {
        Serilog.Log.ForContext<TContext>().Write(LogEventLevel.Verbose, exception, messageTemplate, propertyValue0, propertyValue1, propertyValue2);
    }

    /// <summary>
    /// Write a log event with the <see cref="LogLevel.Verbose"/> level and associated exception.
    /// </summary>
    /// <param name="exception">Exception related to the event.</param>
    /// <param name="messageTemplate">Message template describing the event.</param>
    /// <param name="propertyValues">Objects positionally formatted into the message template.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Verbose(Exception exception, string messageTemplate, params object[] propertyValues)
    {
        Serilog.Log.ForContext<TContext>().Verbose(exception, messageTemplate, propertyValues);
    }

    /// <summary>
    /// Write a log event with the <see cref="LogLevel.Debug"/> level.
    /// </summary>
    /// <param name="messageTemplate">Message template describing the event.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining), Conditional("DEBUG")]
    public static void Debug(string messageTemplate)
    {
        Serilog.Log.ForContext<TContext>().Write(LogEventLevel.Debug, messageTemplate);
    }

    /// <summary>
    /// Write a log event with the <see cref="LogLevel.Debug"/> level.
    /// </summary>
    /// <param name="messageTemplate">Message template describing the event.</param>
    /// <param name="propertyValue">Object positionally formatted into the message template.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining), Conditional("DEBUG")]
    public static void Debug<T>(string messageTemplate, T propertyValue)
    {
        Serilog.Log.ForContext<TContext>().Write(LogEventLevel.Debug, messageTemplate, propertyValue);
    }

    /// <summary>
    /// Write a log event with the <see cref="LogLevel.Debug"/> level.
    /// </summary>
    /// <param name="messageTemplate">Message template describing the event.</param>
    /// <param name="propertyValue0">Object positionally formatted into the message template.</param>
    /// <param name="propertyValue1">Object positionally formatted into the message template.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining), Conditional("DEBUG")]
    public static void Debug<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
    {
        Serilog.Log.ForContext<TContext>().Write(LogEventLevel.Debug, messageTemplate, propertyValue0, propertyValue1);
    }

    /// <summary>
    /// Write a log event with the <see cref="LogLevel.Debug"/> level.
    /// </summary>
    /// <param name="messageTemplate">Message template describing the event.</param>
    /// <param name="propertyValue0">Object positionally formatted into the message template.</param>
    /// <param name="propertyValue1">Object positionally formatted into the message template.</param>
    /// <param name="propertyValue2">Object positionally formatted into the message template.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining), Conditional("DEBUG")]
    public static void Debug<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
    {
        Serilog.Log.ForContext<TContext>().Write(LogEventLevel.Debug, messageTemplate, propertyValue0, propertyValue1, propertyValue2);
    }

    /// <summary>
    /// Write a log event with the <see cref="LogLevel.Debug"/> level.
    /// </summary>
    /// <param name="messageTemplate">Message template describing the event.</param>
    /// <param name="propertyValues">Objects positionally formatted into the message template.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining), Conditional("DEBUG")]
    public static void Debug(string messageTemplate, params object[] propertyValues)
    {
        Serilog.Log.ForContext<TContext>().Debug(messageTemplate, propertyValues);
    }

    /// <summary>
    /// Write a log event with the <see cref="LogLevel.Debug"/> level and associated exception.
    /// </summary>
    /// <param name="exception">Exception related to the event.</param>
    /// <param name="messageTemplate">Message template describing the event.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining), Conditional("DEBUG")]
    public static void Debug(Exception exception, string messageTemplate)
    {
        Serilog.Log.ForContext<TContext>().Write(LogEventLevel.Debug, exception, messageTemplate);
    }

    /// <summary>
    /// Write a log event with the <see cref="LogLevel.Debug"/> level and associated exception.
    /// </summary>
    /// <param name="exception">Exception related to the event.</param>
    /// <param name="messageTemplate">Message template describing the event.</param>
    /// <param name="propertyValue">Object positionally formatted into the message template.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining), Conditional("DEBUG")]
    public static void Debug<T>(Exception exception, string messageTemplate, T propertyValue)
    {
        Serilog.Log.ForContext<TContext>().Write(LogEventLevel.Debug, exception, messageTemplate, propertyValue);
    }

    /// <summary>
    /// Write a log event with the <see cref="LogLevel.Debug"/> level and associated exception.
    /// </summary>
    /// <param name="exception">Exception related to the event.</param>
    /// <param name="messageTemplate">Message template describing the event.</param>
    /// <param name="propertyValue0">Object positionally formatted into the message template.</param>
    /// <param name="propertyValue1">Object positionally formatted into the message template.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining), Conditional("DEBUG")]
    public static void Debug<T0, T1>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
    {
        Serilog.Log.ForContext<TContext>().Write(LogEventLevel.Debug, exception, messageTemplate, propertyValue0, propertyValue1);
    }

    /// <summary>
    /// Write a log event with the <see cref="LogLevel.Debug"/> level and associated exception.
    /// </summary>
    /// <param name="exception">Exception related to the event.</param>
    /// <param name="messageTemplate">Message template describing the event.</param>
    /// <param name="propertyValue0">Object positionally formatted into the message template.</param>
    /// <param name="propertyValue1">Object positionally formatted into the message template.</param>
    /// <param name="propertyValue2">Object positionally formatted into the message template.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining), Conditional("DEBUG")]
    public static void Debug<T0, T1, T2>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
    {
        Serilog.Log.ForContext<TContext>().Write(LogEventLevel.Debug, exception, messageTemplate, propertyValue0, propertyValue1, propertyValue2);
    }

    /// <summary>
    /// Write a log event with the <see cref="LogLevel.Debug"/> level and associated exception.
    /// </summary>
    /// <param name="exception">Exception related to the event.</param>
    /// <param name="messageTemplate">Message template describing the event.</param>
    /// <param name="propertyValues">Objects positionally formatted into the message template.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining), Conditional("DEBUG")]
    public static void Debug(Exception exception, string messageTemplate, params object[] propertyValues)
    {
        Serilog.Log.ForContext<TContext>().Debug(exception, messageTemplate, propertyValues);
    }

    /// <summary>
    /// Write a log event with the <see cref="LogLevel.Information"/> level.
    /// </summary>
    /// <param name="messageTemplate">Message template describing the event.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Information(string messageTemplate)
    {
        Serilog.Log.ForContext<TContext>().Write(LogEventLevel.Information, messageTemplate);
    }

    /// <summary>
    /// Write a log event with the <see cref="LogLevel.Information"/> level.
    /// </summary>
    /// <param name="messageTemplate">Message template describing the event.</param>
    /// <param name="propertyValue">Object positionally formatted into the message template.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Information<T>(string messageTemplate, T propertyValue)
    {
        Serilog.Log.ForContext<TContext>().Write(LogEventLevel.Information, messageTemplate, propertyValue);
    }

    /// <summary>
    /// Write a log event with the <see cref="LogLevel.Information"/> level.
    /// </summary>
    /// <param name="messageTemplate">Message template describing the event.</param>
    /// <param name="propertyValue0">Object positionally formatted into the message template.</param>
    /// <param name="propertyValue1">Object positionally formatted into the message template.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Information<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
    {
        Serilog.Log.ForContext<TContext>().Write(LogEventLevel.Information, messageTemplate, propertyValue0, propertyValue1);
    }

    /// <summary>
    /// Write a log event with the <see cref="LogLevel.Information"/> level.
    /// </summary>
    /// <param name="messageTemplate">Message template describing the event.</param>
    /// <param name="propertyValue0">Object positionally formatted into the message template.</param>
    /// <param name="propertyValue1">Object positionally formatted into the message template.</param>
    /// <param name="propertyValue2">Object positionally formatted into the message template.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Information<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
    {
        Serilog.Log.ForContext<TContext>().Write(LogEventLevel.Information, messageTemplate, propertyValue0, propertyValue1, propertyValue2);
    }

    /// <summary>
    /// Write a log event with the <see cref="LogLevel.Information"/> level.
    /// </summary>
    /// <param name="messageTemplate">Message template describing the event.</param>
    /// <param name="propertyValues">Objects positionally formatted into the message template.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Information(string messageTemplate, params object[] propertyValues)
    {
        Serilog.Log.ForContext<TContext>().Information(messageTemplate, propertyValues);
    }

    /// <summary>
    /// Write a log event with the <see cref="LogLevel.Information"/> level and associated exception.
    /// </summary>
    /// <param name="exception">Exception related to the event.</param>
    /// <param name="messageTemplate">Message template describing the event.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Information(Exception exception, string messageTemplate)
    {
        Serilog.Log.ForContext<TContext>().Write(LogEventLevel.Information, exception, messageTemplate);
    }

    /// <summary>
    /// Write a log event with the <see cref="LogLevel.Information"/> level and associated exception.
    /// </summary>
    /// <param name="exception">Exception related to the event.</param>
    /// <param name="messageTemplate">Message template describing the event.</param>
    /// <param name="propertyValue">Object positionally formatted into the message template.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Information<T>(Exception exception, string messageTemplate, T propertyValue)
    {
        Serilog.Log.ForContext<TContext>().Write(LogEventLevel.Information, exception, messageTemplate, propertyValue);
    }

    /// <summary>
    /// Write a log event with the <see cref="LogLevel.Information"/> level and associated exception.
    /// </summary>
    /// <param name="exception">Exception related to the event.</param>
    /// <param name="messageTemplate">Message template describing the event.</param>
    /// <param name="propertyValue0">Object positionally formatted into the message template.</param>
    /// <param name="propertyValue1">Object positionally formatted into the message template.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Information<T0, T1>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
    {
        Serilog.Log.ForContext<TContext>().Write(LogEventLevel.Information, exception, messageTemplate, propertyValue0, propertyValue1);
    }

    /// <summary>
    /// Write a log event with the <see cref="LogLevel.Information"/> level and associated exception.
    /// </summary>
    /// <param name="exception">Exception related to the event.</param>
    /// <param name="messageTemplate">Message template describing the event.</param>
    /// <param name="propertyValue0">Object positionally formatted into the message template.</param>
    /// <param name="propertyValue1">Object positionally formatted into the message template.</param>
    /// <param name="propertyValue2">Object positionally formatted into the message template.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Information<T0, T1, T2>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
    {
        Serilog.Log.ForContext<TContext>().Write(LogEventLevel.Information, exception, messageTemplate, propertyValue0, propertyValue1, propertyValue2);
    }

    /// <summary>
    /// Write a log event with the <see cref="LogLevel.Information"/> level and associated exception.
    /// </summary>
    /// <param name="exception">Exception related to the event.</param>
    /// <param name="messageTemplate">Message template describing the event.</param>
    /// <param name="propertyValues">Objects positionally formatted into the message template.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Information(Exception exception, string messageTemplate, params object[] propertyValues)
    {
        Serilog.Log.ForContext<TContext>().Information(exception, messageTemplate, propertyValues);
    }

    /// <summary>
    /// Write a log event with the <see cref="LogLevel.Warning"/> level.
    /// </summary>
    /// <param name="messageTemplate">Message template describing the event.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Warning(string messageTemplate)
    {
        Serilog.Log.ForContext<TContext>().Write(LogEventLevel.Warning, messageTemplate);
    }

    /// <summary>
    /// Write a log event with the <see cref="LogLevel.Warning"/> level.
    /// </summary>
    /// <param name="messageTemplate">Message template describing the event.</param>
    /// <param name="propertyValue">Object positionally formatted into the message template.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Warning<T>(string messageTemplate, T propertyValue)
    {
        Serilog.Log.ForContext<TContext>().Write(LogEventLevel.Warning, messageTemplate, propertyValue);
    }

    /// <summary>
    /// Write a log event with the <see cref="LogLevel.Warning"/> level.
    /// </summary>
    /// <param name="messageTemplate">Message template describing the event.</param>
    /// <param name="propertyValue0">Object positionally formatted into the message template.</param>
    /// <param name="propertyValue1">Object positionally formatted into the message template.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Warning<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
    {
        Serilog.Log.ForContext<TContext>().Write(LogEventLevel.Warning, messageTemplate, propertyValue0, propertyValue1);
    }

    /// <summary>
    /// Write a log event with the <see cref="LogLevel.Warning"/> level.
    /// </summary>
    /// <param name="messageTemplate">Message template describing the event.</param>
    /// <param name="propertyValue0">Object positionally formatted into the message template.</param>
    /// <param name="propertyValue1">Object positionally formatted into the message template.</param>
    /// <param name="propertyValue2">Object positionally formatted into the message template.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Warning<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
    {
        Serilog.Log.ForContext<TContext>().Write(LogEventLevel.Warning, messageTemplate, propertyValue0, propertyValue1, propertyValue2);
    }

    /// <summary>
    /// Write a log event with the <see cref="LogLevel.Warning"/> level.
    /// </summary>
    /// <param name="messageTemplate">Message template describing the event.</param>
    /// <param name="propertyValues">Objects positionally formatted into the message template.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Warning(string messageTemplate, params object[] propertyValues)
    {
        Serilog.Log.ForContext<TContext>().ForContext<TContext>().Warning(messageTemplate, propertyValues);
    }

    /// <summary>
    /// Write a log event with the <see cref="LogLevel.Warning"/> level and associated exception.
    /// </summary>
    /// <param name="exception">Exception related to the event.</param>
    /// <param name="messageTemplate">Message template describing the event.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Warning(Exception exception, string messageTemplate)
    {
        Serilog.Log.ForContext<TContext>().Write(LogEventLevel.Warning, exception, messageTemplate);
    }

    /// <summary>
    /// Write a log event with the <see cref="LogLevel.Warning"/> level and associated exception.
    /// </summary>
    /// <param name="exception">Exception related to the event.</param>
    /// <param name="messageTemplate">Message template describing the event.</param>
    /// <param name="propertyValue">Object positionally formatted into the message template.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Warning<T>(Exception exception, string messageTemplate, T propertyValue)
    {
        Serilog.Log.ForContext<TContext>().Write(LogEventLevel.Warning, exception, messageTemplate, propertyValue);
    }

    /// <summary>
    /// Write a log event with the <see cref="LogLevel.Warning"/> level and associated exception.
    /// </summary>
    /// <param name="exception">Exception related to the event.</param>
    /// <param name="messageTemplate">Message template describing the event.</param>
    /// <param name="propertyValue0">Object positionally formatted into the message template.</param>
    /// <param name="propertyValue1">Object positionally formatted into the message template.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Warning<T0, T1>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
    {
        Serilog.Log.ForContext<TContext>().Write(LogEventLevel.Warning, exception, messageTemplate, propertyValue0, propertyValue1);
    }

    /// <summary>
    /// Write a log event with the <see cref="LogLevel.Warning"/> level and associated exception.
    /// </summary>
    /// <param name="exception">Exception related to the event.</param>
    /// <param name="messageTemplate">Message template describing the event.</param>
    /// <param name="propertyValue0">Object positionally formatted into the message template.</param>
    /// <param name="propertyValue1">Object positionally formatted into the message template.</param>
    /// <param name="propertyValue2">Object positionally formatted into the message template.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Warning<T0, T1, T2>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
    {
        Serilog.Log.ForContext<TContext>().Write(LogEventLevel.Warning, exception, messageTemplate, propertyValue0, propertyValue1, propertyValue2);
    }

    /// <summary>
    /// Write a log event with the <see cref="LogLevel.Warning"/> level and associated exception.
    /// </summary>
    /// <param name="exception">Exception related to the event.</param>
    /// <param name="messageTemplate">Message template describing the event.</param>
    /// <param name="propertyValues">Objects positionally formatted into the message template.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Warning(Exception exception, string messageTemplate, params object[] propertyValues)
    {
        Serilog.Log.ForContext<TContext>().ForContext<TContext>().Warning(exception, messageTemplate, propertyValues);
    }

    /// <summary>
    /// Write a log event with the <see cref="LogLevel.Error"/> level.
    /// </summary>
    /// <param name="messageTemplate">Message template describing the event.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Error(string messageTemplate)
    {
        Serilog.Log.ForContext<TContext>().Write(LogEventLevel.Error, messageTemplate);
    }

    /// <summary>
    /// Write a log event with the <see cref="LogLevel.Error"/> level.
    /// </summary>
    /// <param name="messageTemplate">Message template describing the event.</param>
    /// <param name="propertyValue">Object positionally formatted into the message template.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Error<T>(string messageTemplate, T propertyValue)
    {
        Serilog.Log.ForContext<TContext>().Write(LogEventLevel.Error, messageTemplate, propertyValue);
    }

    /// <summary>
    /// Write a log event with the <see cref="LogLevel.Error"/> level.
    /// </summary>
    /// <param name="messageTemplate">Message template describing the event.</param>
    /// <param name="propertyValue0">Object positionally formatted into the message template.</param>
    /// <param name="propertyValue1">Object positionally formatted into the message template.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Error<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
    {
        Serilog.Log.ForContext<TContext>().Write(LogEventLevel.Error, messageTemplate, propertyValue0, propertyValue1);
    }

    /// <summary>
    /// Write a log event with the <see cref="LogLevel.Error"/> level.
    /// </summary>
    /// <param name="messageTemplate">Message template describing the event.</param>
    /// <param name="propertyValue0">Object positionally formatted into the message template.</param>
    /// <param name="propertyValue1">Object positionally formatted into the message template.</param>
    /// <param name="propertyValue2">Object positionally formatted into the message template.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Error<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
    {
        Serilog.Log.ForContext<TContext>().Write(LogEventLevel.Error, messageTemplate, propertyValue0, propertyValue1, propertyValue2);
    }

    /// <summary>
    /// Write a log event with the <see cref="LogLevel.Error"/> level.
    /// </summary>
    /// <param name="messageTemplate">Message template describing the event.</param>
    /// <param name="propertyValues">Objects positionally formatted into the message template.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Error(string messageTemplate, params object[] propertyValues)
    {
        Serilog.Log.ForContext<TContext>().ForContext<TContext>().Error(messageTemplate, propertyValues);
    }

    /// <summary>
    /// Write a log event with the <see cref="LogLevel.Error"/> level and associated exception.
    /// </summary>
    /// <param name="exception">Exception related to the event.</param>
    /// <param name="messageTemplate">Message template describing the event.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Error(Exception exception, string messageTemplate)
    {
        Serilog.Log.ForContext<TContext>().Write(LogEventLevel.Error, exception, messageTemplate);
    }

    /// <summary>
    /// Write a log event with the <see cref="LogLevel.Error"/> level and associated exception.
    /// </summary>
    /// <param name="exception">Exception related to the event.</param>
    /// <param name="messageTemplate">Message template describing the event.</param>
    /// <param name="propertyValue">Object positionally formatted into the message template.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Error<T>(Exception exception, string messageTemplate, T propertyValue)
    {
        Serilog.Log.ForContext<TContext>().Write(LogEventLevel.Error, exception, messageTemplate, propertyValue);
    }

    /// <summary>
    /// Write a log event with the <see cref="LogLevel.Error"/> level and associated exception.
    /// </summary>
    /// <param name="exception">Exception related to the event.</param>
    /// <param name="messageTemplate">Message template describing the event.</param>
    /// <param name="propertyValue0">Object positionally formatted into the message template.</param>
    /// <param name="propertyValue1">Object positionally formatted into the message template.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Error<T0, T1>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
    {
        Serilog.Log.ForContext<TContext>().Write(LogEventLevel.Error, exception, messageTemplate, propertyValue0, propertyValue1);
    }

    /// <summary>
    /// Write a log event with the <see cref="LogLevel.Error"/> level and associated exception.
    /// </summary>
    /// <param name="exception">Exception related to the event.</param>
    /// <param name="messageTemplate">Message template describing the event.</param>
    /// <param name="propertyValue0">Object positionally formatted into the message template.</param>
    /// <param name="propertyValue1">Object positionally formatted into the message template.</param>
    /// <param name="propertyValue2">Object positionally formatted into the message template.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Error<T0, T1, T2>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
    {
        Serilog.Log.ForContext<TContext>().Write(LogEventLevel.Error, exception, messageTemplate, propertyValue0, propertyValue1, propertyValue2);
    }

    /// <summary>
    /// Write a log event with the <see cref="LogLevel.Error"/> level and associated exception.
    /// </summary>
    /// <param name="exception">Exception related to the event.</param>
    /// <param name="messageTemplate">Message template describing the event.</param>
    /// <param name="propertyValues">Objects positionally formatted into the message template.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Error(Exception exception, string messageTemplate, params object[] propertyValues)
    {
        Serilog.Log.ForContext<TContext>().ForContext<TContext>().Error(exception, messageTemplate, propertyValues);
    }

    /// <summary>
    /// Write a log event with the <see cref="LogLevel.Fatal"/> level.
    /// </summary>
    /// <param name="messageTemplate">Message template describing the event.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Fatal(string messageTemplate)
    {
        Serilog.Log.ForContext<TContext>().Write(LogEventLevel.Fatal, messageTemplate);
    }

    /// <summary>
    /// Write a log event with the <see cref="LogLevel.Fatal"/> level.
    /// </summary>
    /// <param name="messageTemplate">Message template describing the event.</param>
    /// <param name="propertyValue">Object positionally formatted into the message template.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Fatal<T>(string messageTemplate, T propertyValue)
    {
        Serilog.Log.ForContext<TContext>().Write(LogEventLevel.Fatal, messageTemplate, propertyValue);
    }

    /// <summary>
    /// Write a log event with the <see cref="LogLevel.Fatal"/> level.
    /// </summary>
    /// <param name="messageTemplate">Message template describing the event.</param>
    /// <param name="propertyValue0">Object positionally formatted into the message template.</param>
    /// <param name="propertyValue1">Object positionally formatted into the message template.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Fatal<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
    {
        Serilog.Log.ForContext<TContext>().Write(LogEventLevel.Fatal, messageTemplate, propertyValue0, propertyValue1);
    }

    /// <summary>
    /// Write a log event with the <see cref="LogLevel.Fatal"/> level.
    /// </summary>
    /// <param name="messageTemplate">Message template describing the event.</param>
    /// <param name="propertyValue0">Object positionally formatted into the message template.</param>
    /// <param name="propertyValue1">Object positionally formatted into the message template.</param>
    /// <param name="propertyValue2">Object positionally formatted into the message template.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Fatal<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
    {
        Serilog.Log.ForContext<TContext>().Write(LogEventLevel.Fatal, messageTemplate, propertyValue0, propertyValue1, propertyValue2);
    }

    /// <summary>
    /// Write a log event with the <see cref="LogLevel.Fatal"/> level.
    /// </summary>
    /// <param name="messageTemplate">Message template describing the event.</param>
    /// <param name="propertyValues">Objects positionally formatted into the message template.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Fatal(string messageTemplate, params object[] propertyValues)
    {
        Serilog.Log.ForContext<TContext>().Fatal(messageTemplate, propertyValues);
    }

    /// <summary>
    /// Write a log event with the <see cref="LogLevel.Fatal"/> level and associated exception.
    /// </summary>
    /// <param name="exception">Exception related to the event.</param>
    /// <param name="messageTemplate">Message template describing the event.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Fatal(Exception exception, string messageTemplate)
    {
        Serilog.Log.ForContext<TContext>().Write(LogEventLevel.Fatal, exception, messageTemplate);
    }

    /// <summary>
    /// Write a log event with the <see cref="LogLevel.Fatal"/> level and associated exception.
    /// </summary>
    /// <param name="exception">Exception related to the event.</param>
    /// <param name="messageTemplate">Message template describing the event.</param>
    /// <param name="propertyValue">Object positionally formatted into the message template.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Fatal<T>(Exception exception, string messageTemplate, T propertyValue)
    {
        Serilog.Log.ForContext<TContext>().Write(LogEventLevel.Fatal, exception, messageTemplate, propertyValue);
    }

    /// <summary>
    /// Write a log event with the <see cref="LogLevel.Fatal"/> level and associated exception.
    /// </summary>
    /// <param name="exception">Exception related to the event.</param>
    /// <param name="messageTemplate">Message template describing the event.</param>
    /// <param name="propertyValue0">Object positionally formatted into the message template.</param>
    /// <param name="propertyValue1">Object positionally formatted into the message template.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Fatal<T0, T1>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
    {
        Serilog.Log.ForContext<TContext>().Write(LogEventLevel.Fatal, exception, messageTemplate, propertyValue0, propertyValue1);
    }

    /// <summary>
    /// Write a log event with the <see cref="LogLevel.Fatal"/> level and associated exception.
    /// </summary>
    /// <param name="exception">Exception related to the event.</param>
    /// <param name="messageTemplate">Message template describing the event.</param>
    /// <param name="propertyValue0">Object positionally formatted into the message template.</param>
    /// <param name="propertyValue1">Object positionally formatted into the message template.</param>
    /// <param name="propertyValue2">Object positionally formatted into the message template.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Fatal<T0, T1, T2>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
    {
        Serilog.Log.ForContext<TContext>().Write(LogEventLevel.Fatal, exception, messageTemplate, propertyValue0, propertyValue1, propertyValue2);
    }

    /// <summary>
    /// Write a log event with the <see cref="LogLevel.Fatal"/> level and associated exception.
    /// </summary>
    /// <param name="exception">Exception related to the event.</param>
    /// <param name="messageTemplate">Message template describing the event.</param>
    /// <param name="propertyValues">Objects positionally formatted into the message template.</param>

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Fatal(Exception exception, string messageTemplate, params object[] propertyValues)
    {
        Serilog.Log.ForContext<TContext>().ForContext<TContext>().Fatal(exception, messageTemplate, propertyValues);
    }
}

/// <summary>
/// Log without context
/// </summary>
public static class Log 
{
    /// <summary>
    /// Determine if events at the specified level will be passed through
    /// to the log sinks.
    /// </summary>
    /// <param name="level">Level to check.</param>
    /// <returns>True if the level is enabled; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsEnabled(LogLevel level) => Serilog.Log.IsEnabled((LogEventLevel)level);

    /// <summary>
    /// Write a log event with the <see cref="LogLevel.Verbose"/> level.
    /// </summary>
    /// <param name="messageTemplate">Message template describing the event.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Verbose(string messageTemplate)
    {
        Serilog.Log.Write(LogEventLevel.Verbose, messageTemplate);
    }

    /// <summary>
    /// Write a log event with the <see cref="LogLevel.Verbose"/> level.
    /// </summary>
    /// <param name="messageTemplate">Message template describing the event.</param>
    /// <param name="propertyValue">Object positionally formatted into the message template.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Verbose<T>(string messageTemplate, T propertyValue)
    {
        Serilog.Log.Write(LogEventLevel.Verbose, messageTemplate, propertyValue);
    }

    /// <summary>
    /// Write a log event with the <see cref="LogLevel.Verbose"/> level.
    /// </summary>
    /// <param name="messageTemplate">Message template describing the event.</param>
    /// <param name="propertyValue0">Object positionally formatted into the message template.</param>
    /// <param name="propertyValue1">Object positionally formatted into the message template.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Verbose<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
    {
        Serilog.Log.Write(LogEventLevel.Verbose, messageTemplate, propertyValue0, propertyValue1);
    }

    /// <summary>
    /// Write a log event with the <see cref="LogLevel.Verbose"/> level.
    /// </summary>
    /// <param name="messageTemplate">Message template describing the event.</param>
    /// <param name="propertyValue0">Object positionally formatted into the message template.</param>
    /// <param name="propertyValue1">Object positionally formatted into the message template.</param>
    /// <param name="propertyValue2">Object positionally formatted into the message template.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Verbose<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
    {
        Serilog.Log.Write(LogEventLevel.Verbose, messageTemplate, propertyValue0, propertyValue1, propertyValue2);
    }

    /// <summary>
    /// Write a log event with the <see cref="LogLevel.Verbose"/> level.
    /// </summary>
    /// <param name="messageTemplate">Message template describing the event.</param>
    /// <param name="propertyValues">Objects positionally formatted into the message template.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Verbose(string messageTemplate, params object[] propertyValues)
    {
        Serilog.Log.Verbose(messageTemplate, propertyValues);
    }

    /// <summary>
    /// Serilog.Log.Write a log event with the <see cref="LogLevel.Verbose"/> level and associated exception.
    /// </summary>
    /// <param name="exception">Exception related to the event.</param>
    /// <param name="messageTemplate">Message template describing the event.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Verbose(Exception exception, string messageTemplate)
    {
        Serilog.Log.Write(LogEventLevel.Verbose, exception, messageTemplate);
    }

    /// <summary>
    /// Serilog.Log.Write a log event with the <see cref="LogLevel.Verbose"/> level and associated exception.
    /// </summary>
    /// <param name="exception">Exception related to the event.</param>
    /// <param name="messageTemplate">Message template describing the event.</param>
    /// <param name="propertyValue">Object positionally formatted into the message template.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Verbose<T>(Exception exception, string messageTemplate, T propertyValue)
    {
        Serilog.Log.Write(LogEventLevel.Verbose, exception, messageTemplate, propertyValue);
    }

    /// <summary>
    /// Serilog.Log.Write a log event with the <see cref="LogLevel.Verbose"/> level and associated exception.
    /// </summary>
    /// <param name="exception">Exception related to the event.</param>
    /// <param name="messageTemplate">Message template describing the event.</param>
    /// <param name="propertyValue0">Object positionally formatted into the message template.</param>
    /// <param name="propertyValue1">Object positionally formatted into the message template.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Verbose<T0, T1>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
    {
        Serilog.Log.Write(LogEventLevel.Verbose, exception, messageTemplate, propertyValue0, propertyValue1);
    }

    /// <summary>
    /// Serilog.Log.Write a log event with the <see cref="LogLevel.Verbose"/> level and associated exception.
    /// </summary>
    /// <param name="exception">Exception related to the event.</param>
    /// <param name="messageTemplate">Message template describing the event.</param>
    /// <param name="propertyValue0">Object positionally formatted into the message template.</param>
    /// <param name="propertyValue1">Object positionally formatted into the message template.</param>
    /// <param name="propertyValue2">Object positionally formatted into the message template.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Verbose<T0, T1, T2>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
    {
        Serilog.Log.Write(LogEventLevel.Verbose, exception, messageTemplate, propertyValue0, propertyValue1, propertyValue2);
    }

    /// <summary>
    /// Serilog.Log.Write a log event with the <see cref="LogLevel.Verbose"/> level and associated exception.
    /// </summary>
    /// <param name="exception">Exception related to the event.</param>
    /// <param name="messageTemplate">Message template describing the event.</param>
    /// <param name="propertyValues">Objects positionally formatted into the message template.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Verbose(Exception exception, string messageTemplate, params object[] propertyValues)
    {
        Serilog.Log.Verbose(exception, messageTemplate, propertyValues);
    }

    /// <summary>
    /// Write a log event with the <see cref="LogLevel.Debug"/> level.
    /// </summary>
    /// <param name="messageTemplate">Message template describing the event.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining), Conditional("DEBUG")]
    public static void Debug(string messageTemplate)
    {
        Serilog.Log.Write(LogEventLevel.Debug, messageTemplate);
    }

    /// <summary>
    /// Write a log event with the <see cref="LogLevel.Debug"/> level.
    /// </summary>
    /// <param name="messageTemplate">Message template describing the event.</param>
    /// <param name="propertyValue">Object positionally formatted into the message template.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining), Conditional("DEBUG")]
    public static void Debug<T>(string messageTemplate, T propertyValue)
    {
        Serilog.Log.Write(LogEventLevel.Debug, messageTemplate, propertyValue);
    }

    /// <summary>
    /// Write a log event with the <see cref="LogLevel.Debug"/> level.
    /// </summary>
    /// <param name="messageTemplate">Message template describing the event.</param>
    /// <param name="propertyValue0">Object positionally formatted into the message template.</param>
    /// <param name="propertyValue1">Object positionally formatted into the message template.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining), Conditional("DEBUG")]
    public static void Debug<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
    {
        Serilog.Log.Write(LogEventLevel.Debug, messageTemplate, propertyValue0, propertyValue1);
    }

    /// <summary>
    /// Write a log event with the <see cref="LogLevel.Debug"/> level.
    /// </summary>
    /// <param name="messageTemplate">Message template describing the event.</param>
    /// <param name="propertyValue0">Object positionally formatted into the message template.</param>
    /// <param name="propertyValue1">Object positionally formatted into the message template.</param>
    /// <param name="propertyValue2">Object positionally formatted into the message template.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining), Conditional("DEBUG")]
    public static void Debug<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
    {
        Serilog.Log.Write(LogEventLevel.Debug, messageTemplate, propertyValue0, propertyValue1, propertyValue2);
    }

    /// <summary>
    /// Write a log event with the <see cref="LogLevel.Debug"/> level.
    /// </summary>
    /// <param name="messageTemplate">Message template describing the event.</param>
    /// <param name="propertyValues">Objects positionally formatted into the message template.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining), Conditional("DEBUG")]
    public static void Debug(string messageTemplate, params object[] propertyValues)
    {
        Serilog.Log.Debug(messageTemplate, propertyValues);
    }

    /// <summary>
    /// Serilog.Log.Write a log event with the <see cref="LogLevel.Debug"/> level and associated exception.
    /// </summary>
    /// <param name="exception">Exception related to the event.</param>
    /// <param name="messageTemplate">Message template describing the event.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining), Conditional("DEBUG")]
    public static void Debug(Exception exception, string messageTemplate)
    {
        Serilog.Log.Write(LogEventLevel.Debug, exception, messageTemplate);
    }

    /// <summary>
    /// Serilog.Log.Write a log event with the <see cref="LogLevel.Debug"/> level and associated exception.
    /// </summary>
    /// <param name="exception">Exception related to the event.</param>
    /// <param name="messageTemplate">Message template describing the event.</param>
    /// <param name="propertyValue">Object positionally formatted into the message template.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining), Conditional("DEBUG")]
    public static void Debug<T>(Exception exception, string messageTemplate, T propertyValue)
    {
        Serilog.Log.Write(LogEventLevel.Debug, exception, messageTemplate, propertyValue);
    }

    /// <summary>
    /// Serilog.Log.Write a log event with the <see cref="LogLevel.Debug"/> level and associated exception.
    /// </summary>
    /// <param name="exception">Exception related to the event.</param>
    /// <param name="messageTemplate">Message template describing the event.</param>
    /// <param name="propertyValue0">Object positionally formatted into the message template.</param>
    /// <param name="propertyValue1">Object positionally formatted into the message template.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining), Conditional("DEBUG")]
    public static void Debug<T0, T1>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
    {
        Serilog.Log.Write(LogEventLevel.Debug, exception, messageTemplate, propertyValue0, propertyValue1);
    }

    /// <summary>
    /// Serilog.Log.Write a log event with the <see cref="LogLevel.Debug"/> level and associated exception.
    /// </summary>
    /// <param name="exception">Exception related to the event.</param>
    /// <param name="messageTemplate">Message template describing the event.</param>
    /// <param name="propertyValue0">Object positionally formatted into the message template.</param>
    /// <param name="propertyValue1">Object positionally formatted into the message template.</param>
    /// <param name="propertyValue2">Object positionally formatted into the message template.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining), Conditional("DEBUG")]
    public static void Debug<T0, T1, T2>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
    {
        Serilog.Log.Write(LogEventLevel.Debug, exception, messageTemplate, propertyValue0, propertyValue1, propertyValue2);
    }

    /// <summary>
    /// Serilog.Log.Write a log event with the <see cref="LogLevel.Debug"/> level and associated exception.
    /// </summary>
    /// <param name="exception">Exception related to the event.</param>
    /// <param name="messageTemplate">Message template describing the event.</param>
    /// <param name="propertyValues">Objects positionally formatted into the message template.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining), Conditional("DEBUG")]
    public static void Debug(Exception exception, string messageTemplate, params object[] propertyValues)
    {
        Serilog.Log.Debug(exception, messageTemplate, propertyValues);
    }

    /// <summary>
    /// Write a log event with the <see cref="LogLevel.Information"/> level.
    /// </summary>
    /// <param name="messageTemplate">Message template describing the event.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Information(string messageTemplate)
    {
        Serilog.Log.Write(LogEventLevel.Information, messageTemplate);
    }

    /// <summary>
    /// Write a log event with the <see cref="LogLevel.Information"/> level.
    /// </summary>
    /// <param name="messageTemplate">Message template describing the event.</param>
    /// <param name="propertyValue">Object positionally formatted into the message template.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Information<T>(string messageTemplate, T propertyValue)
    {
        Serilog.Log.Write(LogEventLevel.Information, messageTemplate, propertyValue);
    }

    /// <summary>
    /// Write a log event with the <see cref="LogLevel.Information"/> level.
    /// </summary>
    /// <param name="messageTemplate">Message template describing the event.</param>
    /// <param name="propertyValue0">Object positionally formatted into the message template.</param>
    /// <param name="propertyValue1">Object positionally formatted into the message template.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Information<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
    {
        Serilog.Log.Write(LogEventLevel.Information, messageTemplate, propertyValue0, propertyValue1);
    }

    /// <summary>
    /// Write a log event with the <see cref="LogLevel.Information"/> level.
    /// </summary>
    /// <param name="messageTemplate">Message template describing the event.</param>
    /// <param name="propertyValue0">Object positionally formatted into the message template.</param>
    /// <param name="propertyValue1">Object positionally formatted into the message template.</param>
    /// <param name="propertyValue2">Object positionally formatted into the message template.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Information<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
    {
        Serilog.Log.Write(LogEventLevel.Information, messageTemplate, propertyValue0, propertyValue1, propertyValue2);
    }

    /// <summary>
    /// Write a log event with the <see cref="LogLevel.Information"/> level.
    /// </summary>
    /// <param name="messageTemplate">Message template describing the event.</param>
    /// <param name="propertyValues">Objects positionally formatted into the message template.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Information(string messageTemplate, params object[] propertyValues)
    {
        Serilog.Log.Information(messageTemplate, propertyValues);
    }

    /// <summary>
    /// Serilog.Log.Write a log event with the <see cref="LogLevel.Information"/> level and associated exception.
    /// </summary>
    /// <param name="exception">Exception related to the event.</param>
    /// <param name="messageTemplate">Message template describing the event.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Information(Exception exception, string messageTemplate)
    {
        Serilog.Log.Write(LogEventLevel.Information, exception, messageTemplate);
    }

    /// <summary>
    /// Serilog.Log.Write a log event with the <see cref="LogLevel.Information"/> level and associated exception.
    /// </summary>
    /// <param name="exception">Exception related to the event.</param>
    /// <param name="messageTemplate">Message template describing the event.</param>
    /// <param name="propertyValue">Object positionally formatted into the message template.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Information<T>(Exception exception, string messageTemplate, T propertyValue)
    {
        Serilog.Log.Write(LogEventLevel.Information, exception, messageTemplate, propertyValue);
    }

    /// <summary>
    /// Write a log event with the <see cref="LogLevel.Information"/> level and associated exception.
    /// </summary>
    /// <param name="exception">Exception related to the event.</param>
    /// <param name="messageTemplate">Message template describing the event.</param>
    /// <param name="propertyValue0">Object positionally formatted into the message template.</param>
    /// <param name="propertyValue1">Object positionally formatted into the message template.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Information<T0, T1>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
    {
        Serilog.Log.Write(LogEventLevel.Information, exception, messageTemplate, propertyValue0, propertyValue1);
    }

    /// <summary>
    /// Write a log event with the <see cref="LogLevel.Information"/> level and associated exception.
    /// </summary>
    /// <param name="exception">Exception related to the event.</param>
    /// <param name="messageTemplate">Message template describing the event.</param>
    /// <param name="propertyValue0">Object positionally formatted into the message template.</param>
    /// <param name="propertyValue1">Object positionally formatted into the message template.</param>
    /// <param name="propertyValue2">Object positionally formatted into the message template.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Information<T0, T1, T2>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
    {
        Serilog.Log.Write(LogEventLevel.Information, exception, messageTemplate, propertyValue0, propertyValue1, propertyValue2);
    }

    /// <summary>
    /// Write a log event with the <see cref="LogLevel.Information"/> level and associated exception.
    /// </summary>
    /// <param name="exception">Exception related to the event.</param>
    /// <param name="messageTemplate">Message template describing the event.</param>
    /// <param name="propertyValues">Objects positionally formatted into the message template.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Information(Exception exception, string messageTemplate, params object[] propertyValues)
    {
        Serilog.Log.Information(exception, messageTemplate, propertyValues);
    }

    /// <summary>
    /// Serilog.Log.Write a log event with the <see cref="LogLevel.Warning"/> level.
    /// </summary>
    /// <param name="messageTemplate">Message template describing the event.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Warning(string messageTemplate)
    {
        Serilog.Log.Write(LogEventLevel.Warning, messageTemplate);
    }

    /// <summary>
    /// Write a log event with the <see cref="LogLevel.Warning"/> level.
    /// </summary>
    /// <param name="messageTemplate">Message template describing the event.</param>
    /// <param name="propertyValue">Object positionally formatted into the message template.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Warning<T>(string messageTemplate, T propertyValue)
    {
        Serilog.Log.Write(LogEventLevel.Warning, messageTemplate, propertyValue);
    }

    /// <summary>
    /// Write a log event with the <see cref="LogLevel.Warning"/> level.
    /// </summary>
    /// <param name="messageTemplate">Message template describing the event.</param>
    /// <param name="propertyValue0">Object positionally formatted into the message template.</param>
    /// <param name="propertyValue1">Object positionally formatted into the message template.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Warning<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
    {
        Serilog.Log.Write(LogEventLevel.Warning, messageTemplate, propertyValue0, propertyValue1);
    }

    /// <summary>
    /// Write a log event with the <see cref="LogLevel.Warning"/> level.
    /// </summary>
    /// <param name="messageTemplate">Message template describing the event.</param>
    /// <param name="propertyValue0">Object positionally formatted into the message template.</param>
    /// <param name="propertyValue1">Object positionally formatted into the message template.</param>
    /// <param name="propertyValue2">Object positionally formatted into the message template.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Warning<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
    {
        Serilog.Log.Write(LogEventLevel.Warning, messageTemplate, propertyValue0, propertyValue1, propertyValue2);
    }

    /// <summary>
    /// Write a log event with the <see cref="LogLevel.Warning"/> level.
    /// </summary>
    /// <param name="messageTemplate">Message template describing the event.</param>
    /// <param name="propertyValues">Objects positionally formatted into the message template.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Warning(string messageTemplate, params object[] propertyValues)
    {
        Serilog.Log.Warning(messageTemplate, propertyValues);
    }

    /// <summary>
    /// Write a log event with the <see cref="LogLevel.Warning"/> level and associated exception.
    /// </summary>
    /// <param name="exception">Exception related to the event.</param>
    /// <param name="messageTemplate">Message template describing the event.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Warning(Exception exception, string messageTemplate)
    {
        Serilog.Log.Write(LogEventLevel.Warning, exception, messageTemplate);
    }

    /// <summary>
    /// Write a log event with the <see cref="LogLevel.Warning"/> level and associated exception.
    /// </summary>
    /// <param name="exception">Exception related to the event.</param>
    /// <param name="messageTemplate">Message template describing the event.</param>
    /// <param name="propertyValue">Object positionally formatted into the message template.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Warning<T>(Exception exception, string messageTemplate, T propertyValue)
    {
        Serilog.Log.Write(LogEventLevel.Warning, exception, messageTemplate, propertyValue);
    }

    /// <summary>
    /// Write a log event with the <see cref="LogLevel.Warning"/> level and associated exception.
    /// </summary>
    /// <param name="exception">Exception related to the event.</param>
    /// <param name="messageTemplate">Message template describing the event.</param>
    /// <param name="propertyValue0">Object positionally formatted into the message template.</param>
    /// <param name="propertyValue1">Object positionally formatted into the message template.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Warning<T0, T1>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
    {
        Serilog.Log.Write(LogEventLevel.Warning, exception, messageTemplate, propertyValue0, propertyValue1);
    }

    /// <summary>
    /// Write a log event with the <see cref="LogLevel.Warning"/> level and associated exception.
    /// </summary>
    /// <param name="exception">Exception related to the event.</param>
    /// <param name="messageTemplate">Message template describing the event.</param>
    /// <param name="propertyValue0">Object positionally formatted into the message template.</param>
    /// <param name="propertyValue1">Object positionally formatted into the message template.</param>
    /// <param name="propertyValue2">Object positionally formatted into the message template.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Warning<T0, T1, T2>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
    {
        Serilog.Log.Write(LogEventLevel.Warning, exception, messageTemplate, propertyValue0, propertyValue1, propertyValue2);
    }

    /// <summary>
    /// Write a log event with the <see cref="LogLevel.Warning"/> level and associated exception.
    /// </summary>
    /// <param name="exception">Exception related to the event.</param>
    /// <param name="messageTemplate">Message template describing the event.</param>
    /// <param name="propertyValues">Objects positionally formatted into the message template.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Warning(Exception exception, string messageTemplate, params object[] propertyValues)
    {
        Serilog.Log.Warning(exception, messageTemplate, propertyValues);
    }

    /// <summary>
    /// Write a log event with the <see cref="LogLevel.Error"/> level.
    /// </summary>
    /// <param name="messageTemplate">Message template describing the event.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Error(string messageTemplate)
    {
        Serilog.Log.Write(LogEventLevel.Error, messageTemplate);
    }

    /// <summary>
    /// Write a log event with the <see cref="LogLevel.Error"/> level.
    /// </summary>
    /// <param name="messageTemplate">Message template describing the event.</param>
    /// <param name="propertyValue">Object positionally formatted into the message template.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Error<T>(string messageTemplate, T propertyValue)
    {
        Serilog.Log.Write(LogEventLevel.Error, messageTemplate, propertyValue);
    }

    /// <summary>
    /// Write a log event with the <see cref="LogLevel.Error"/> level.
    /// </summary>
    /// <param name="messageTemplate">Message template describing the event.</param>
    /// <param name="propertyValue0">Object positionally formatted into the message template.</param>
    /// <param name="propertyValue1">Object positionally formatted into the message template.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Error<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
    {
        Serilog.Log.Write(LogEventLevel.Error, messageTemplate, propertyValue0, propertyValue1);
    }

    /// <summary>
    /// Write a log event with the <see cref="LogLevel.Error"/> level.
    /// </summary>
    /// <param name="messageTemplate">Message template describing the event.</param>
    /// <param name="propertyValue0">Object positionally formatted into the message template.</param>
    /// <param name="propertyValue1">Object positionally formatted into the message template.</param>
    /// <param name="propertyValue2">Object positionally formatted into the message template.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Error<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
    {
        Serilog.Log.Write(LogEventLevel.Error, messageTemplate, propertyValue0, propertyValue1, propertyValue2);
    }

    /// <summary>
    /// Write a log event with the <see cref="LogLevel.Error"/> level.
    /// </summary>
    /// <param name="messageTemplate">Message template describing the event.</param>
    /// <param name="propertyValues">Objects positionally formatted into the message template.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Error(string messageTemplate, params object[] propertyValues)
    {
        Serilog.Log.Error(messageTemplate, propertyValues);
    }

    /// <summary>
    /// Write a log event with the <see cref="LogLevel.Error"/> level and associated exception.
    /// </summary>
    /// <param name="exception">Exception related to the event.</param>
    /// <param name="messageTemplate">Message template describing the event.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Error(Exception exception, string messageTemplate)
    {
        Serilog.Log.Write(LogEventLevel.Error, exception, messageTemplate);
    }

    /// <summary>
    /// Write a log event with the <see cref="LogLevel.Error"/> level and associated exception.
    /// </summary>
    /// <param name="exception">Exception related to the event.</param>
    /// <param name="messageTemplate">Message template describing the event.</param>
    /// <param name="propertyValue">Object positionally formatted into the message template.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Error<T>(Exception exception, string messageTemplate, T propertyValue)
    {
        Serilog.Log.Write(LogEventLevel.Error, exception, messageTemplate, propertyValue);
    }

    /// <summary>
    /// Write a log event with the <see cref="LogLevel.Error"/> level and associated exception.
    /// </summary>
    /// <param name="exception">Exception related to the event.</param>
    /// <param name="messageTemplate">Message template describing the event.</param>
    /// <param name="propertyValue0">Object positionally formatted into the message template.</param>
    /// <param name="propertyValue1">Object positionally formatted into the message template.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Error<T0, T1>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
    {
        Serilog.Log.Write(LogEventLevel.Error, exception, messageTemplate, propertyValue0, propertyValue1);
    }

    /// <summary>
    /// Write a log event with the <see cref="LogLevel.Error"/> level and associated exception.
    /// </summary>
    /// <param name="exception">Exception related to the event.</param>
    /// <param name="messageTemplate">Message template describing the event.</param>
    /// <param name="propertyValue0">Object positionally formatted into the message template.</param>
    /// <param name="propertyValue1">Object positionally formatted into the message template.</param>
    /// <param name="propertyValue2">Object positionally formatted into the message template.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Error<T0, T1, T2>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
    {
        Serilog.Log.Write(LogEventLevel.Error, exception, messageTemplate, propertyValue0, propertyValue1, propertyValue2);
    }

    /// <summary>
    /// Write a log event with the <see cref="LogLevel.Error"/> level and associated exception.
    /// </summary>
    /// <param name="exception">Exception related to the event.</param>
    /// <param name="messageTemplate">Message template describing the event.</param>
    /// <param name="propertyValues">Objects positionally formatted into the message template.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Error(Exception exception, string messageTemplate, params object[] propertyValues)
    {
        Serilog.Log.Error(exception, messageTemplate, propertyValues);
    }

    /// <summary>
    /// Write a log event with the <see cref="LogLevel.Fatal"/> level.
    /// </summary>
    /// <param name="messageTemplate">Message template describing the event.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Fatal(string messageTemplate)
    {
        Serilog.Log.Write(LogEventLevel.Fatal, messageTemplate);
    }

    /// <summary>
    /// Write a log event with the <see cref="LogLevel.Fatal"/> level.
    /// </summary>
    /// <param name="messageTemplate">Message template describing the event.</param>
    /// <param name="propertyValue">Object positionally formatted into the message template.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Fatal<T>(string messageTemplate, T propertyValue)
    {
        Serilog.Log.Write(LogEventLevel.Fatal, messageTemplate, propertyValue);
    }

    /// <summary>
    /// Write a log event with the <see cref="LogLevel.Fatal"/> level.
    /// </summary>
    /// <param name="messageTemplate">Message template describing the event.</param>
    /// <param name="propertyValue0">Object positionally formatted into the message template.</param>
    /// <param name="propertyValue1">Object positionally formatted into the message template.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Fatal<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
    {
        Serilog.Log.Write(LogEventLevel.Fatal, messageTemplate, propertyValue0, propertyValue1);
    }

    /// <summary>
    /// Write a log event with the <see cref="LogLevel.Fatal"/> level.
    /// </summary>
    /// <param name="messageTemplate">Message template describing the event.</param>
    /// <param name="propertyValue0">Object positionally formatted into the message template.</param>
    /// <param name="propertyValue1">Object positionally formatted into the message template.</param>
    /// <param name="propertyValue2">Object positionally formatted into the message template.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Fatal<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
    {
        Serilog.Log.Write(LogEventLevel.Fatal, messageTemplate, propertyValue0, propertyValue1, propertyValue2);
    }

    /// <summary>
    /// Write a log event with the <see cref="LogLevel.Fatal"/> level.
    /// </summary>
    /// <param name="messageTemplate">Message template describing the event.</param>
    /// <param name="propertyValues">Objects positionally formatted into the message template.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Fatal(string messageTemplate, params object[] propertyValues)
    {
        Serilog.Log.Fatal(messageTemplate, propertyValues);
    }

    /// <summary>
    /// Write a log event with the <see cref="LogLevel.Fatal"/> level and associated exception.
    /// </summary>
    /// <param name="exception">Exception related to the event.</param>
    /// <param name="messageTemplate">Message template describing the event.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Fatal(Exception exception, string messageTemplate)
    {
        Serilog.Log.Write(LogEventLevel.Fatal, exception, messageTemplate);
    }

    /// <summary>
    /// Write a log event with the <see cref="LogLevel.Fatal"/> level and associated exception.
    /// </summary>
    /// <param name="exception">Exception related to the event.</param>
    /// <param name="messageTemplate">Message template describing the event.</param>
    /// <param name="propertyValue">Object positionally formatted into the message template.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Fatal<T>(Exception exception, string messageTemplate, T propertyValue)
    {
        Serilog.Log.Write(LogEventLevel.Fatal, exception, messageTemplate, propertyValue);
    }

    /// <summary>
    /// Write a log event with the <see cref="LogLevel.Fatal"/> level and associated exception.
    /// </summary>
    /// <param name="exception">Exception related to the event.</param>
    /// <param name="messageTemplate">Message template describing the event.</param>
    /// <param name="propertyValue0">Object positionally formatted into the message template.</param>
    /// <param name="propertyValue1">Object positionally formatted into the message template.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Fatal<T0, T1>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
    {
        Serilog.Log.Write(LogEventLevel.Fatal, exception, messageTemplate, propertyValue0, propertyValue1);
    }

    /// <summary>
    /// Write a log event with the <see cref="LogLevel.Fatal"/> level and associated exception.
    /// </summary>
    /// <param name="exception">Exception related to the event.</param>
    /// <param name="messageTemplate">Message template describing the event.</param>
    /// <param name="propertyValue0">Object positionally formatted into the message template.</param>
    /// <param name="propertyValue1">Object positionally formatted into the message template.</param>
    /// <param name="propertyValue2">Object positionally formatted into the message template.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Fatal<T0, T1, T2>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
    {
        Serilog.Log.Write(LogEventLevel.Fatal, exception, messageTemplate, propertyValue0, propertyValue1, propertyValue2);
    }

    /// <summary>
    /// Write a log event with the <see cref="LogLevel.Fatal"/> level and associated exception.
    /// </summary>
    /// <param name="exception">Exception related to the event.</param>
    /// <param name="messageTemplate">Message template describing the event.</param>
    /// <param name="propertyValues">Objects positionally formatted into the message template.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Fatal(Exception exception, string messageTemplate, params object[] propertyValues)
    {
        Serilog.Log.Fatal(exception, messageTemplate, propertyValues);
    }

}