using ENet;
using System.Buffers;
using Microsoft.Extensions.ObjectPool;

namespace Kyvos.Networking;

public interface INetwrokMessage 
{
    void Send();
}

//TODO maybe add object pools for all message types


public abstract class NetworkMessage : INetwrokMessage
{
    protected DataBuffer data;
    protected byte channelId;
    protected PacketFlags packetFlags;

    protected bool isReliable => (packetFlags & PacketFlags.Reliable) > 0;

    protected NetworkMessage() 
    { }

    protected NetworkMessage(DataBuffer data, byte channelId, PacketFlags packetFlags)
    {
        this.data = data;
        this.channelId = channelId;
        this.packetFlags = packetFlags;
    }

    protected NetworkMessage(byte[] data, byte channelId, PacketFlags packetFlags)
    {
        
        this.data = new(data);
        this.channelId = channelId;
        this.packetFlags = packetFlags;
    }

    protected void ReturnData()
    {
        data.Return(); 
        data = new();
    }

    protected unsafe Packet CreatePacket()
    {
        var packet = default(Packet);

        fixed (void* dataPtr = data.Buffer)
        {
            packet.Create(new IntPtr(dataPtr), data.Size, packetFlags);
        }

        return packet;
    }

    protected virtual void NativeFreeCallback(Packet p)
        => ReturnData();

    public abstract void Send();

    /// <summary>
    /// rents a data buffer from the networkmessages data pool
    /// </summary>
    /// <param name="size">size of the buffer</param>
    /// <returns>a data buffer</returns>
    public static DataBuffer RentBuffer(int size)
        => DataPool.RentBuffer(size);

    internal static class DataPool
    {
        internal static ArrayPool<byte> poolImpl;

        static DataPool()
        {
            poolImpl = ArrayPool<byte>.Shared;
        }

        public static DataBuffer RentBuffer(int size)
            => new DataBuffer(size, poolImpl);
        

        public static byte[] Rent(int size)
        {
            return poolImpl.Rent(size);
        }

        public static void Return(byte[] data, bool clear = false)
        {
            poolImpl.Return(data, clear);
        }

    }

}

public class NetworkMessageIndividual : NetworkMessage
{
    public Peer peer;

    public NetworkMessageIndividual(Peer peer, DataBuffer data, byte channelId, PacketFlags packetFlags)
        : base(data, channelId, packetFlags)
    {
        this.peer = peer;
    }

    public NetworkMessageIndividual(Peer peer, byte[] data, byte channelId, PacketFlags packetFlags)
        : base(data, channelId, packetFlags)
    {
        this.peer = peer;
    }

    public override void Send()
    {
        if (data.Size == 0)
        {
            ReturnData();
            return;
        }

        var reliablity = isReliable;
        var packet = CreatePacket();
        if (reliablity)
            packet.SetFreeCallback(NativeFreeCallback);
        peer.Send(channelId, ref packet);
        if (!reliablity) //i think this is fine to do here
            ReturnData();//if ever there is a hard to find bug here i am sorry to my future self
    }
}

public class NetworkMessageBroadcast : NetworkMessage 
{
    Host host;
    public NetworkMessageBroadcast(Host host, DataBuffer data, byte channelId, PacketFlags packetFlags)
        : base(data, channelId, packetFlags)
    {
        this.host = host;
    }

    public NetworkMessageBroadcast(Host host, byte[] data, byte channelId, PacketFlags packetFlags)
    : base(data, channelId, packetFlags)
    {
        this.host = host;
    }

    public override void Send()
    {
        if (data.Size == 0) 
        {
            data.Return();
            return;
        }

        var reliablity = isReliable;
        var packet = CreatePacket();
        if (reliablity)
            packet.SetFreeCallback(NativeFreeCallback);
        host.Broadcast(channelId,ref packet);
        if (!reliablity)
            ReturnData();

    }
}

public class NetworkMessageExclusionaryBroadcast : NetworkMessage 
{
    Host host;
    Peer peer;
    public NetworkMessageExclusionaryBroadcast(Host host, Peer peer, DataBuffer data, byte channelId, PacketFlags packetFlags)
        : base(data, channelId, packetFlags)
    {
        this.host = host;
        this.peer = peer;
    }

    public NetworkMessageExclusionaryBroadcast(Host host, Peer peer, byte[] data, byte channelId, PacketFlags packetFlags)
        : base(data, channelId, packetFlags) 
    {
        this.host = host;
        this.peer = peer;
    }

    public override void Send()
    {
        if (data.Size == 0)
        {
            ReturnData();
            return;
        }

        var reliablity = isReliable;
        var packet = CreatePacket();
        if (reliablity)
            packet.SetFreeCallback(NativeFreeCallback);
        host.Broadcast(channelId, ref packet, peer);
        if (!reliablity) //i think this is fine to do here
            ReturnData();//if ever there is a hard to find bug here i am sorry to my future self
    }
}

public class NetworkMessageSelectiveBroadcast : NetworkMessage
{
    Host host;
    Peer[] peers;
    public NetworkMessageSelectiveBroadcast(Host host, Peer[] peers, DataBuffer data, byte channelId, PacketFlags packetFlags)
        : base(data, channelId, packetFlags)
    {
        this.host = host;
        this.peers = peers;
    }

    public NetworkMessageSelectiveBroadcast(Host host, Peer[] peers, byte[] data, byte channelId, PacketFlags packetFlags)
        : base(data, channelId, packetFlags)
    {
        this.host = host;
        this.peers = peers;
    }

    public override void Send()
    {
        if (data.Size == 0)
        {
            ReturnData();
            return;
        }

        var reliablity = isReliable;
        var packet = CreatePacket();
        if (reliablity)
            packet.SetFreeCallback(NativeFreeCallback);
        host.Broadcast(channelId, ref packet, peers);
        if (!reliablity)
            ReturnData();
    }
}


///// <summary>
///// A message to be sent over the network
///// messages are pooled, the nework handler takes care of the pooling
///// </summary>
//public class NetworkMessageLegacy : INetwrokMessage
//{
//    const int PoolRetainedItems = 255;//subject to change
//    const int InitialPoolSeed = 10;
//    static ObjectPool<NetworkMessageLegacy> pool;

//    static NetworkMessageLegacy() 
//    {
//        pool = new DefaultObjectPool<NetworkMessageLegacy>(ObjectPoolPolicy.Instance, PoolRetainedItems);

//        for (int i = 0; i < InitialPoolSeed; i++)
//        {
//            var item = new NetworkMessageLegacy();
//            pool.Return(item);
//        }

//    }
    
//    DataBuffer data;
//    byte channelID;
//    PacketFlags packetFlags;
//    internal Peer reciever;

//    bool isReliable => (packetFlags & PacketFlags.Reliable) > 0;

//    private NetworkMessageLegacy()
//    {
//        data = new();
//        channelID = 0;
//        packetFlags = PacketFlags.None;
//        reciever = default(Peer);
//    }

//    public void ReturnDataToPool() 
//    {
//        data.Return();
//        data = new();//reset
//    }

//    unsafe Packet CreatePacket() 
//    {
//        var packet = default(Packet);

//        fixed (void* dataPtr = data.Buffer)
//        {
//            packet.Create(new IntPtr(dataPtr), data.Size, packetFlags);
//        }

//        return packet;
//    }

//    public void Send()
//        => Send(SendToPeer);

//    internal void Send(SendAction send) 
//    {
//        if (data.Size == 0) 
//        {
//            ReturnToPool(this);
//            return;
//        }
        
//        var reliablity = isReliable;
//        var packet = CreatePacket();
//        if (reliablity)
//            packet.SetFreeCallback(NativeFreeCallback);
//        send(packet, channelID);
//        if (!reliablity) //i think this is fine to do here
//            ReturnToPool(this);//if ever there is a hard to find bug here i am sorry to my future self
//    }

//    void SendToPeer(Packet p, byte channelID) 
//    {
//        reciever.Send(channelID, ref p);
//    }

//    void NativeFreeCallback(Packet p)
//        => ReturnToPool(this);


//    /// <summary>
//    /// creates empty network message
//    /// object pool is used to reduce garbage collection
//    /// </summary>
//    /// <returns>an empty network message</returns>
//    public static NetworkMessageLegacy Create()
//    {
//        return pool.Get();
//    }

//    /// <summary>
//    /// Creates a NetworkMessage with the given data and flags and channel
//    /// object pool is used to reduce garbage collection
//    /// data will be appropriated by the buffer pool after message is sent
//    /// recommended to already use the buffer pool for data array
//    /// </summary>
//    /// <param name="data">the data</param>
//    /// <param name="channelID">channel to send message over</param>
//    /// <param name="packetFlags">the flags</param>
//    /// <returns>a network message that can be sent</returns>
//    public static NetworkMessageLegacy Create(byte[] data, byte channelID, PacketFlags packetFlags)
//    {
//        var message = pool.Get();
//        message.data = new(data);
//        message.channelID = channelID;
//        message.packetFlags = packetFlags;
//        return message;
//    }

//    /// <summary>
//    /// Creates a NetworkMessage with the given data and flags and channel
//    /// object pool is used to reduce garbage collection
//    /// data will be appropriated by the buffer pool after message is sent
//    /// recommended to already use the buffer pool for data array
//    /// </summary>
//    /// <param name="data">the data</param>
//    /// <param name="channelID">channel to send message over</param>
//    /// <param name="packetFlags">the flags</param>
//    /// <param name="reciever">the peer to send message to</param>
//    /// <returns>a network message that can be sent</returns>
//    public static NetworkMessageLegacy Create(byte[] data, byte channelID, PacketFlags packetFlags, Peer reciever)
//    {
//        var message = pool.Get();
//        message.data = new(data);
//        message.channelID = channelID;
//        message.packetFlags = packetFlags;
//        message.reciever = reciever;
//        return message;
//    }

//    /// <summary>
//    /// Creates a new NetworkMessage with the given data and flags and for a specific channel
//    /// object pool is used to reduce garbage collection
//    /// </summary>
//    /// <typeparam name="T">The type of data</typeparam>
//    /// <param name="data">instance of data</param>
//    /// <param name="channelID">channel to send message over</param>
//    /// <param name="packetFlags">flags of message</param>
//    /// <param name="dataConverter">converts data to byte array to be sent over network</param>
//    /// <returns>a network message</returns>
//    public static NetworkMessageLegacy Create<T>(T data, byte channelID, PacketFlags packetFlags, IDataConverter<T> dataConverter)
//    {
//        var message = pool.Get();
//        message.data = new(dataConverter.SizeInBytes(data), DataPool.poolImpl);
//        dataConverter.WriteInto(data, message.data.Buffer);
//        message.channelID = channelID;
//        message.packetFlags = packetFlags;
//        return message;
//    }

//    /// <summary>
//    /// Creates a new NetworkMessage with the given data and flags and for a specific channel
//    /// object pool is used to reduce garbage collection
//    /// </summary>
//    /// <typeparam name="T">The type of data</typeparam>
//    /// <param name="data">instance of data</param>
//    /// <param name="channelID">channel to send message over</param>
//    /// <param name="packetFlags">flags of message</param>
//    /// <param name="reciever">the peer to send message to</param>
//    /// <param name="dataConverter">converts data to byte array to be sent over network</param>
//    /// <returns>a network message</returns>
//    public static NetworkMessageLegacy Create<T>(T data, byte channelID, PacketFlags packetFlags, Peer reciever, IDataConverter<T> dataConverter)
//    {
//        var message = pool.Get();
//        message.data = new(dataConverter.SizeInBytes(data), DataPool.poolImpl);
//        Console.WriteLine($"data array: {message.data.Buffer.Length}");
//        dataConverter.WriteInto(data, message.data.Buffer);
//        Console.WriteLine("Created message with size: " + message.data.Size);
//        message.channelID = channelID;
//        message.packetFlags = packetFlags;
//        message.reciever = reciever;
//        return message;
//    }

//    /// <summary>
//    /// Creates a new NetworkMessage with the given data and flags and for a specific channel
//    /// object pool is used to reduce garbage collection
//    /// </summary>
//    /// <typeparam name="T">The type of data</typeparam>
//    /// <param name="data">instance of data</param>
//    /// <param name="channelID">channel to send message over</param>
//    /// <param name="packetFlags">flags of message</param>
//    /// <param name="dataConverter">converts data to byte array to be sent over network</param>
//    /// <returns>a network message</returns>
//    public static NetworkMessageLegacy Create(DataBuffer data, byte channelID, PacketFlags packetFlags)
//    {
//        var message = pool.Get();
//        message.data = data;
//        message.channelID = channelID;
//        message.packetFlags = packetFlags;
//        return message;
//    }

//    /// <summary>
//    /// Creates a new NetworkMessage with the given data and flags and for a specific channel
//    /// object pool is used to reduce garbage collection
//    /// </summary>
//    /// <typeparam name="T">The type of data</typeparam>
//    /// <param name="data">instance of data</param>
//    /// <param name="channelID">channel to send message over</param>
//    /// <param name="packetFlags">flags of message</param>
//    /// <param name="reciever">the peer to send message to</param>
//    /// <param name="dataConverter">converts data to byte array to be sent over network</param>
//    /// <returns>a network message</returns>
//    public static NetworkMessageLegacy Create(DataBuffer data, byte channelID, PacketFlags packetFlags, Peer reciever)
//    {
//        var message = pool.Get();
//        message.data = data;
//        message.channelID = channelID;
//        message.packetFlags = packetFlags;
//        message.reciever = reciever;
//        return message;
//    }

//    /// <summary>
//    /// rents a data buffer from the networkmessages data pool
//    /// </summary>
//    /// <param name="size">size of the buffer</param>
//    /// <returns>a data buffer</returns>
//    public static DataBuffer RentBuffer(int size)
//        => new DataBuffer(size, DataPool.poolImpl);

//    /// <summary>
//    /// returns message to pool
//    /// </summary>
//    /// <param name="message"></param>
//    internal static void ReturnToPool(NetworkMessageLegacy message)
//    {
//        pool.Return(message);
//    }

//    internal static class DataPool
//    {
//        internal static ArrayPool<byte> poolImpl;

//        static DataPool()
//        {
//            poolImpl = ArrayPool<byte>.Shared;
//        }

//        public static byte[] Rent(int size)
//        {
//            return poolImpl.Rent(size);
//        }
        
//        public static void Return(byte[] data, bool clear = false)
//        {
//            poolImpl.Return(data, clear);
//        }

//    }



//    class ObjectPoolPolicy : IPooledObjectPolicy<NetworkMessageLegacy>
//    {
//        internal static ObjectPoolPolicy Instance = new();

//        private ObjectPoolPolicy() { }

//        public NetworkMessageLegacy Create()
//        {
//            return new NetworkMessageLegacy();
//        }

//        public bool Return(NetworkMessageLegacy obj)
//        {
//            obj.ReturnDataToPool();
//            obj.reciever = default(Peer);
//            obj.packetFlags = PacketFlags.None;
//            obj.channelID = 0;
//            return true;
//        }
//    }
//}
