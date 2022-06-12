using Kyvos.Core.Logging;
using Kyvos.Networking;
using System.Security.Cryptography;
using System.Text;

using var logManger = LogManager.Instance;
var logSetup = ILogSetup.Default;


logSetup.WithConsoleLogging(outputTemplate: Templates.ThreadNameAndSourceContextMessage)
        .WithDebugLogging(outputTemplate: Templates.ThreadNameAndSourceContextMessage)
#if DEBUG
                .WithMinimumLevel(LogLevel.Verbose)
#endif
        .ShowThreadName();

logManger.Setup(logSetup);


using var client = new EnetClient<ClientLoggingNetworkEventHandler>(new());

client.Connect(TestData.HOST, TestData.PORT);

bool @continue = true;
var message = "";
while (true) 
{
    if (Console.KeyAvailable) 
    {
        var consoleKey = Console.ReadKey(true);
        var key = consoleKey.Key;
        var keyChar = consoleKey.KeyChar;
        switch (key)
        {
            case ConsoleKey.Escape:
                @continue = false;
                break;
            case ConsoleKey.Enter:
                Console.WriteLine();
                Console.WriteLine($"Sending message of size {StringConverter.Instance.SizeInBytes(message)}");
                client.Send(message, 0, ENet.PacketFlags.Reliable, StringConverter.Instance);
                message = "";
                break;
            default:
                Console.Write(keyChar);
                message +=keyChar;
                break;
        }
        
    }

    if (!@continue)
        break;
}

client.Disconnect();

logManger.Dispose();

Console.WriteLine("Done");