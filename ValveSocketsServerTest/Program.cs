using ENet;
using Kyvos.Networking;
using Kyvos.Core.Logging;

using var logManger = LogManager.Instance;
var logSetup = ILogSetup.Default;


logSetup.WithConsoleLogging(outputTemplate: Templates.ThreadNameAndSourceContextMessage)
        .WithDebugLogging(outputTemplate: Templates.ThreadNameAndSourceContextMessage)
#if DEBUG
                .WithMinimumLevel(LogLevel.Verbose)
#endif
        .ShowThreadName();

logManger.Setup(logSetup);


using var server = new EnetServer<ServerLogging, uint>(new());

server.Create(TestData.HOST, TestData.PORT, TestData.MAX_CLIENTS);

while (true)
{
    if (Console.KeyAvailable) 
    {
        var key = Console.ReadKey(true);
        if (key.Key == ConsoleKey.Escape)
        {
            Console.WriteLine("Exiting...");
            break;
        }
    }
}

server.Stop();
logManger.Dispose();

Console.WriteLine("Goodbye (press any key to fully exit)");
Console.ReadLine();

class ServerLogging : ServerLoggingNetworkEventHandler 
{
    protected override void ProcessData(Span<byte> data)
    {
        Console.WriteLine($"message: {StringConverter.Instance.Read(data)}");
    }
}
