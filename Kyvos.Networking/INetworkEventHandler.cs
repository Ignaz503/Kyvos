using ENet;

namespace Kyvos.Networking;

public interface INetworkEventHandler
{
    void OnConnection(Peer peer);
    
    void OnDisconnection(Peer peer);

    void OnTimeout(Peer peer);

    void OnReceive(Peer peer, byte channelID, Span<byte> data);
}
