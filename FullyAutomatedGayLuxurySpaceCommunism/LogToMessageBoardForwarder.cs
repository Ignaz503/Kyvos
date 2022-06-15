using Kyvos.Core;
using Kyvos.ImGUI;
using Kyvos.Core.Logging;
using DefaultEcs;
using System.Diagnostics;

namespace FullyAutomatedGayLuxurySpaceCommunism;

public struct LogToMessageBoardForwarder 
{
    MessageBoard? messageBorad;
    public LogToMessageBoardForwarder(World w)
    {
        if (w.Has<MessageBoard>())
            messageBorad = w.Get<MessageBoard>();
        else
            messageBorad = null;

        Debug.Assert(w.Has<IApplication>());
        var app = w.Get<IApplication>();

        app.Subscribe<LogMessage>(Forward);        
    }

    public void Forward(LogMessage msg) 
    {
        messageBorad?.AddMessage(msg.Message);
    }

}
