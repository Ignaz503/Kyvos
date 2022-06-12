using ENet;
using Kyvos.Core.Logging;
using System.Diagnostics.CodeAnalysis;

namespace Kyvos.Networking;

public class ClientLoggingNetworkEventHandler : INetworkEventHandler
{
    public void OnConnection(Peer peer)
    {
        Log.Information("Connected to {IP}", peer.IP);
    }

    public void OnDisconnection(Peer peer)
    {
        Log.Information("Disconnected from {IP}", peer.IP);
    }

    public void OnReceive(Peer peer, byte channelID, Span<byte> data)
    {
        Log.Information("Reicieved data from {IP} on channel {channel} with length {Length}", peer.IP, channelID, data.Length);
        ProcessData(data);
    }

    public void OnTimeout(Peer peer)
    {
        Log.Information("Timeout from {IP}", peer.IP);
    }

    protected virtual void ProcessData(Span<byte> data)
    { }
}

public class ServerLoggingNetworkEventHandler : IServerHandler<uint>
{
    Dictionary<uint, Peer> users = new();
    Dictionary<PeerWrapper, uint> reverseMapping = new();

    public Peer GetNativeHandle(uint user)
    {
        if (!users.TryGetValue(user, out var peer))
            throw new ArgumentException("user not found");
        return peer;
    }
    
    

    public uint GetUserFromNateiveHandle(Peer peer)
    {
        if (!reverseMapping.TryGetValue(peer, out var user))
            throw new ArgumentException("user not found");
        return user;
    }


    void AddUser(Peer p) 
    {
        if (!users.ContainsKey(p.ID)) 
        {
            users.Add(p.ID, p);
            reverseMapping.Add(p, p.ID);
        }
    }

    public void OnConnection(Peer user)
    {
        Log.Information("User {UserID} IP:{UserIP} connected", user.ID, user.IP);
        AddUser(user);
    }

    void Remove(uint user) 
    {
        var peerHandle = GetNativeHandle(user);
        reverseMapping.Remove(peerHandle);
        users.Remove(user);
    }

    void Remove(Peer peer)
    {
        var user = GetUserFromNateiveHandle(peer);
        reverseMapping.Remove(peer);
        users.Remove(user);
    }

    public void OnDisconnection(Peer user)
    {
        Log.Information("User {User} IP:{user.IP} disconnected", user.ID, user.IP);
        Remove(user);
    }

    public void OnReceive(uint user, byte channelID, Span<byte> data)
    {
        Log.Information("Reicieved data from {User} on channel {channel} with length {Length}", user, channelID,data.Length);
        ProcessData(data);
    }

    public void OnTimeout(Peer user)
    {
        Log.Information("User {User} IP:{UserIP} timed out", user.ID, user.IP);
        Remove(user);
    }

    protected virtual void ProcessData(Span<byte> data)
    { }

    struct PeerWrapper : IEquatable<PeerWrapper> , IEqualityComparer<PeerWrapper>
    {
        Peer peer;

        public PeerWrapper(Peer p)
        {
            this.peer = p;
        }
        public override bool Equals([NotNullWhen(true)] object? obj)
        {
            if (obj is PeerWrapper wrapper)
                return Equals(wrapper);
           
            return false;
        }
        public bool Equals(PeerWrapper other)
            => peer.ID == other.peer.ID;

        public bool Equals(PeerWrapper x, PeerWrapper y)
            => x.Equals(y);

        public override int GetHashCode()
        {
            return (int)peer.ID;
        }

        public int GetHashCode([DisallowNull] PeerWrapper obj)
            => obj.GetHashCode();

        public static implicit operator Peer(PeerWrapper wrapper)
            => wrapper.peer;

        public static implicit operator PeerWrapper(Peer peer)
            => new(peer);

    }

}
