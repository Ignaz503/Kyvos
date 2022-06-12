using ENet;
using Kyvos.Core.Logging;
using System.Collections.Concurrent;

namespace Kyvos.Networking;

public abstract class EnetNetworkManager<THandler> : IDisposable
    where THandler : INetworkEventHandler
{
    private const int TimeOutMilliseconds = 15;//TODO not const but on create
    volatile bool shouldRun;
    bool isDisposed;
    Thread? listenThread;
    Thread? sendThread;

    protected Host host;

    protected THandler handler;

    ConcurrentQueue<MessageWrapper> toSend;

    public EnetNetworkManager(THandler eventHandler)
    {
        shouldRun = true;
        isDisposed = false;
        EnetNetworking.Initialize();

        host = new();
        toSend = new();
        this.handler = eventHandler;
        
    }

    protected void EstablishInternal() 
    {
        shouldRun = true;
        listenThread = new Thread(ListenRoutine);
        listenThread.Name = "Newtork Listen Thread";
        listenThread.IsBackground = true;
        sendThread = new Thread(SendRoutine);
        sendThread.Name = "Network Send Thread";
        sendThread.IsBackground = true;

        listenThread.Start();
        sendThread.Start();
    }

    protected void Dismantle() 
    {
        shouldRun = false;
        host?.Flush();
        host?.Dispose();

        EnetNetworking.Shutdown();


        if (listenThread != null && listenThread.IsAlive)
            listenThread.Join();
        if (sendThread != null && sendThread.IsAlive)
            sendThread.Join();

        Log.Information("Shut down network manager");

        isDisposed = true; //if dismantle called explicticly
    }

    void ListenRoutine() 
    {
        Event netEvent;
        var hostInstance = host ?? throw new ArgumentNullException("Can't listen without a host");
        while (shouldRun) 
        {
            bool polled = false;
            while (!polled)
            {
                //check if event is queued if none go inside if
                if (hostInstance.CheckEvents(out netEvent) <= 0)
                {
                    //wait for evnets (timeout for 15 milliseconds) if none break out of polled loop
                    if (hostInstance.Service(TimeOutMilliseconds, out netEvent) <= 0)
                        break;
                    polled = true;
                }

                switch (netEvent.Type)
                {
                    case EventType.None:
                        break;
                    case EventType.Connect:
                        //Console.WriteLine($"Client connected - ID: {netEvent.Peer.ID}, IP: {netEvent.Peer.IP}");
                        //Console.WriteLine($"ID: {netEvent.ChannelID}");
                        handler.OnConnection(netEvent.Peer);
                        break;
                    case EventType.Disconnect:
                        //Console.WriteLine($"Client disconnected - ID: {netEvent.Peer.ID}, IP: {netEvent.Peer.IP}");
                        handler.OnDisconnection(netEvent.Peer);
                        break;
                    case EventType.Timeout:
                        //Console.WriteLine($"Client timeout - ID: {netEvent.Peer.ID} IP: {netEvent.Peer.IP}");
                        handler.OnTimeout(netEvent.Peer);
                        break;
                    case EventType.Receive:
                        //Console.WriteLine($"Packet received from - ID: {netEvent.Peer.ID}, IP: {netEvent.Peer.IP}, Channel ID: {netEvent.ChannelID}, Data length: {netEvent.Packet.Length}");
                        unsafe 
                        {
                            handler.OnReceive(netEvent.Peer,netEvent.ChannelID, new(netEvent.Packet.Data.ToPointer(),netEvent.Packet.Length));
                        }
                        netEvent.Packet.Dispose();
                        break;
                }
            }
        }
    }

    void SendRoutine() 
    {
        MessageWrapper container;
        while (shouldRun) 
        {
            //also with this approach maybe a Queue pool can be used
            //before the loop exchange the current queue with one from the pool
            //then clear that queue and return it to the pool
            //might reduce locking needed compared to the try dequeue
            while (toSend.TryDequeue(out container))//all at once? or maybe one per outer loop (change if to while in that case)
            {
                container.Message.Send();
            }
        }
    }
    
    DataBuffer ConvertData<T>(T data, IDataConverter<T> converter) 
    {
        var buffer = NetworkMessage.RentBuffer(converter.SizeInBytes(data));
        converter.WriteInto(data, buffer);
        return buffer;
    }

    protected void SendInternal(byte[] data, byte channelID, PacketFlags flags, Peer receiver) 
    {
        toSend.Enqueue(new MessageWrapper(new NetworkMessageIndividual(receiver, data, channelID, flags)));
    }
    protected void SendInternal<T>(T data, byte channelID, PacketFlags packet, Peer reciever, IDataConverter<T> dataConverter)
    {
        toSend.Enqueue(new MessageWrapper(new NetworkMessageIndividual(reciever, ConvertData(data,dataConverter), channelID, packet)));
    }

    protected void BroadcastInternal(byte[] data, byte channelID, PacketFlags flags)
    {
        toSend.Enqueue(new MessageWrapper(new NetworkMessageBroadcast(host,data,channelID,flags)));
    }

    protected void BroadcastInternal<T>(T data, byte channelID, PacketFlags flags, IDataConverter<T> dataConverter)
    {
        toSend.Enqueue(new MessageWrapper(new NetworkMessageBroadcast(host, ConvertData(data,dataConverter), channelID, flags)));
    }
    
    protected void BroadcastInternal(byte[] data, byte channelID, PacketFlags flags, Peer excluded)
    {
        toSend.Enqueue(new MessageWrapper(new NetworkMessageExclusionaryBroadcast(host,excluded, data, channelID, flags)));
    }

    protected void BroadcastInternal<T>(T data, byte channelID, PacketFlags flags, Peer excluded, IDataConverter<T> dataConverter)
    {
        toSend.Enqueue(new MessageWrapper(new NetworkMessageExclusionaryBroadcast(host, excluded, ConvertData(data, dataConverter), channelID, flags)));
    }

    protected void BroadcastInternal(byte[] data, byte channelID, PacketFlags flags, Peer[] recieverSelection)
    {
        toSend.Enqueue(new MessageWrapper(new NetworkMessageSelectiveBroadcast(host, recieverSelection, data, channelID, flags)));
    }

    protected void BroadcastInternal<T>(T data, byte channelID, PacketFlags flags, Peer[] recieverSelection, IDataConverter<T> dataConverter)
    {
        toSend.Enqueue(new MessageWrapper(new NetworkMessageSelectiveBroadcast(host, recieverSelection, ConvertData(data, dataConverter), channelID, flags)));
    }

    protected static Address GetAddress(string ip, ushort port)
    {
        var address = new Address();
        address.SetHost(ip);
        address.Port = port;
        return address;
    }

    public void Dispose()
    {
        if (isDisposed)
            return;

        Dismantle();
    }


    struct MessageWrapper 
    {
        public INetwrokMessage Message;
        public MessageWrapper(INetwrokMessage message)
        {
            Message = message;
        }
    }
}

public class TestData
{
    public const string HOST = "127.0.0.1";
    public const ushort PORT = 8008;
    public const int MAX_CLIENTS = 255;
}

//public class NetworkTestManager : IDisposable
//{
//    private const int Port = 8008;
//    private const int TimeOutMilliseconds = 15;

//    Peer peer;

//    public NetworkTestManager()
//    {
//        Networking.Initialize();
//    }

//    public void Dispose()
//    {
//        Networking.Shutdown();
//    }

//    public void TestServer()
//    {
//        var maxClients = 255;
//        using var server = new Host();
//        var address = new Address();
//        address.SetHost("127.0.0.1");
//        address.Port = Port;

//        server.Create(address, maxClients);
//        Console.WriteLine($"server ip: {address.GetIP()}");

//        Event netEvent;

//        while (!Console.KeyAvailable) 
//        {
//            bool polled = false;
//            while (!polled) 
//            {
//                //check if event is queued if none go inside if
//                if (server.CheckEvents(out netEvent) <= 0) 
//                {
//                    //wait for evnets (timeout for 15 milliseconds) if none break out of polled loop
//                    if (server.Service(TimeOutMilliseconds, out netEvent) <= 0)
//                        break;
//                    polled = true;
//                }

//                switch (netEvent.Type) 
//                {
//                    case EventType.None:
//                        break;
//                    case EventType.Connect:
//                        Console.WriteLine($"Client connected - ID: { netEvent.Peer.ID }, IP: { netEvent.Peer.IP}");
//                        Console.WriteLine($"ID: {netEvent.ChannelID}");
//                        break;
//                    case EventType.Disconnect:
//                        Console.WriteLine($"Client disconnected - ID: { netEvent.Peer.ID }, IP: { netEvent.Peer.IP}");
//                        break;
//                    case EventType.Timeout:
//                        Console.WriteLine($"Client timeout - ID: {netEvent.Peer.ID } IP: { netEvent.Peer.IP}");

//                        break;
//                    case EventType.Receive:
//                        Console.WriteLine($"Packet received from - ID: {netEvent.Peer.ID}, IP: { netEvent.Peer.IP}, Channel ID: {netEvent.ChannelID}, Data length: {netEvent.Packet.Length}");
//                        netEvent.Packet.Dispose();
//                        break;
//                }
//            }
//        }


//        server.Flush();
//    }

//    public void TestClient(Func<bool> run)
//    {
//        using var client = new Host();
//        var address = new Address();
//        address.SetHost("127.0.0.1");
//        address.Port = Port;
//        Console.WriteLine($"Connecting to: {address.GetIP()} {address.GetHost()}");
//        client.Create();
//        peer = client.Connect(address);

//        Event netEvent;
//        while (run()) 
//        {
//            bool polled = false;
//            while (!polled) 
//            {
//                if (client.CheckEvents(out netEvent) <= 0)
//                {
//                    if (client.Service(TimeOutMilliseconds, out netEvent) <= 0)
//                        break;
//                    polled = true;
//                }
//                switch (netEvent.Type) 
//                {
//                    case EventType.None:
//                        break;
//                    case EventType.Connect:
//                        Console.WriteLine("Client connected to server");
//                        break;
//                    case EventType.Disconnect:
//                        Console.WriteLine("Client disconnected from server");
//                        break;

//                    case EventType.Timeout:
//                        Console.WriteLine("Client connection timeout");
//                        break;

//                    case EventType.Receive:
//                        Console.WriteLine($"Packet received from server - Channel ID: {netEvent.ChannelID }, Data length: { netEvent.Packet.Length}");
//                        netEvent.Packet.Dispose();
//                        break;
//                }
//            }
//        }

//        client.Flush();
//    }

//    public void Send(byte[] data, byte channelID = 0) 
//    {
//        var packet = default(Packet);
//        packet.Create(data,PacketFlags.Reliable);
//        packet.SetFreeCallback(PacketCallback);
//        peer.Send(channelID, ref packet);
//    }

//    void PacketCallback(Packet p) 
//    {
//        Console.WriteLine($"data is null: {p.Length}");
//    }

//}