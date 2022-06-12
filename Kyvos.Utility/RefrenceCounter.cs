namespace Kyvos.Utility;
public struct ReferenceCounter
{
    uint refCount;

    public uint Count => refCount;

    public ReferenceCounter()
    {
        refCount = 1;
    }

    public ReferenceCounter(uint initCount)
    {
        refCount = initCount;
    }

    public uint Increment()
        => Interlocked.Increment(ref refCount);

    public uint Decrement()
        => Interlocked.Decrement(ref refCount);
}
