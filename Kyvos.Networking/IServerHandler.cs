﻿using ENet;

namespace Kyvos.Networking;

public interface IServerHandler<TUser> : INetworkEventHandler 
{
    Peer GetNativeHandle(TUser user);
    
    TUser GetUserFromNateiveHandle(Peer peer);

    void INetworkEventHandler.OnReceive(Peer peer, byte channelID, Span<byte> data)
        => OnReceive(GetUserFromNateiveHandle(peer), channelID, data);

    void OnReceive(TUser peer, byte channelID, Span<byte> data);

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