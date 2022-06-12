using ENet;
using Kyvos.Core.Logging;

namespace Kyvos.Networking;

public class EnetServer<THandler,TUser> : EnetNetworkManager<THandler>
    where THandler : IServerHandler<TUser>
{
    public EnetServer(THandler eventHandler) : base(eventHandler)
    {
    }
    
    public void Create(string ip, ushort port, int maxClients)
    {
        host.Create(GetAddress(ip, port),maxClients);
        EstablishInternal();

        Log<Server>.Information("Server created at Ip:{IP} Port:{PORT} Max Connections:{CONNECTIONS}", ip, port,maxClients);
    }

    public void Stop()
        => Dismantle();

    public void Send(TUser user, byte[] data, byte channelID, PacketFlags packet)
    {
        var peer = handler.GetNativeHandle(user);
        SendInternal(data, channelID, packet, peer);
    }

    public void Send<T>(TUser user, T data, byte channelID, PacketFlags packet, IDataConverter<T> dataConverter)
    {
        var peer = handler.GetNativeHandle(user);
        SendInternal(data, channelID, packet, peer, dataConverter);
    }

    public void Broadcast(byte[] data, byte channelID, PacketFlags packet)
    => BroadcastInternal(data, channelID, packet);

    public void Broadcast<T>(T data, byte channelID, PacketFlags packet, IDataConverter<T> dataConverter)
        => BroadcastInternal(data, channelID, packet, dataConverter);

    protected void Broadcast<T>(byte[] data, byte channelID, PacketFlags flags, TUser excluded)
        => BroadcastInternal(data, channelID, flags, handler.GetNativeHandle(excluded));

    protected void Broadcast<T>(T data, byte channelID, PacketFlags flags, TUser excluded, IDataConverter<T> dataConverter)
        => BroadcastInternal(data, channelID, flags, handler.GetNativeHandle(excluded), dataConverter);

    protected void Broadcast(byte[] data, byte channelID, PacketFlags flags, TUser[] recieverSelection)
        => BroadcastInternal(data, channelID, flags, ConvertUsers(recieverSelection));
    

    protected void Broadcast<T>(T data, byte channelID, PacketFlags flags, TUser[] recieverSelection, IDataConverter<T> dataConverter)
        => BroadcastInternal(data, channelID, flags, ConvertUsers(recieverSelection), dataConverter);

    Peer[] ConvertUsers(TUser[] users)
    {
        var peers = new Peer[users.Length];
        for (int i = 0; i < users.Length; i++)
        {
            var user = users[i];
            peers[i] = handler.GetNativeHandle(user);
        }
        return peers;
    }


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