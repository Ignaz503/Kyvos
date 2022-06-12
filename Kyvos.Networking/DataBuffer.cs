using System.Buffers;

namespace Kyvos.Networking;

public struct DataBuffer
{
    byte[] array;
    bool IsRented => associatedPool != null;
    readonly ArrayPool<byte>? associatedPool;

    public Span<byte> Buffer => new(array, 0, Size);
    public int Size { get; private set; }

    public DataBuffer()
    {
        array = Array.Empty<byte>();
        associatedPool = null;
        Size = 0;
    }

    public DataBuffer(byte[] data)
    {
        this.array = data;
        Size = data.Length;
        associatedPool = null;
    }

    public DataBuffer(int size, ArrayPool<byte> poolRentedFrom)
    {
        associatedPool = poolRentedFrom ?? throw new ArgumentNullException(nameof(poolRentedFrom));
        Size = size;
        this.array = poolRentedFrom.Rent(size);
    }

    public void Return()
    {
        if (IsRented)
            associatedPool!.Return(array);
        array = Array.Empty<byte>();
        Size = 0;
    }

    public static implicit operator Span<byte>(DataBuffer buffer) => buffer.Buffer;
}
