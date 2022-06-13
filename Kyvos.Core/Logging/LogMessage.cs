namespace Kyvos.Core.Logging;

public readonly struct LogMessage 
{
    public readonly LogLevel Level;
    public readonly string Message;

    public LogMessage()
    {
        Level = LogLevel.Verbose;
        Message = "";
    }

    public LogMessage(LogLevel level, string message)
    {
        Level = level;
        Message = message;
    }
    
    public static implicit operator string(LogMessage message)
        => message.Message;
}