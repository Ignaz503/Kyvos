using System;

namespace Kyvos.Core.Memory;

public struct ByteReinterpret<TFrom>
    where TFrom: unmanaged
{
    TFrom from;

    public ByteReinterpret(TFrom from)
    {
        this.from = from;
    }

    public ReadOnlySpan<byte> To 
    {
        get => Get();
    }

    ReadOnlySpan<byte> Get() 
    {
        unsafe
        {
            fixed (void* ptr = &from) 
            {
                var size = Size.Of<TFrom>();
                return new ReadOnlySpan<byte>(ptr, size);
            }
        }
    }
}
