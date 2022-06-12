namespace Kyvos.Core.Logging;

public class Templates
{
    public const string ThreadName = "<{ThreadName}>";
    public const string ThreadId = "<{ThreadId}>";
    public const string ProcessName = "<|{ProcessName}|>";
    public const string ProcessId = "<|{ProcessId}|>";
    public const string EnvironmentName = "|{EnvironmentName}|";
    public const string EnvironmentUserName = "|{EnvironmentUserName}|";
    public const string MachineName = "|{MachineName}|";
    public const string TimeStampAndLevel = "[{Timestamp:HH:mm:ss:fff} {Level}]";
    public const string SourceContext = "({SourceContext})";
    public const string Message = "{Message:lj}{NewLine}";
    public const string Exception = "{Exception}";

    /// <summary>
    /// <{ThreadName}>[{Timestamp:HH:mm:ss:fff} {Level}] ({SourceContext}) {Message:lj}{NewLine}{Exception}
    /// </summary>
    public const string ThreadNameAndSourceContextMessage = $"{ThreadName}{TimeStampAndLevel} {SourceContext} {Message}{Exception}";
    /// <summary>
    /// <{ThreadId}>[{Timestamp:HH:mm:ss:fff} {Level}] ({SourceContext}) {Message:lj}{NewLine}{Exception}
    /// </summary>
    public const string ThreadIdAndSourceContextMessage = $"{ThreadId}{TimeStampAndLevel} {SourceContext} {Message}{Exception}";

    /// <summary>
    /// <{ThreadName}><{ThreadId}>[{Timestamp:HH:mm:ss:fff} {Level}] ({SourceContext}) {Message:lj}{NewLine}{Exception}
    /// </summary>
    public const string ThreadNameAndIdAndSourceContextMessage = $"{ThreadName}{ThreadId}{TimeStampAndLevel} {SourceContext} {Message}{Exception}";


    /// <summary>
    /// <{ThreadName}>[{Timestamp:HH:mm:ss:fff} {Level}] {Message:lj}{NewLine}{Exception}
    /// </summary>
    public const string ThreadNameMessage = $"{ThreadName}{TimeStampAndLevel} {Message}{Exception}";
    /// <summary>
    /// <{ThreadName}>[{Timestamp:HH:mm:ss:fff} {Level}] {Message:lj}{NewLine}{Exception}
    /// </summary>
    public const string ThreadIdMessage = $"{ThreadId}{TimeStampAndLevel} {Message}{Exception}";

    /// <summary>
    /// <{ThreadName}><{ThreadId}>[{Timestamp:HH:mm:ss:fff} {Level}] ({SourceContext}) {Message:lj}{NewLine}{Exception}
    /// </summary>
    public const string ThreadNameAndIdMessage = $"{ThreadName}{ThreadId}{TimeStampAndLevel} {Message}{Exception}";


    private Templates()
    { }
}
