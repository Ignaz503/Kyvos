using System;
using System.Runtime.CompilerServices;
using Veldrid;
using System.Text;
using System.Runtime.InteropServices;

namespace Kyvos.Input;


[StructLayout(LayoutKind.Sequential, Pack = 1)]
internal struct KeyboardStateData : IEquatable<KeyboardStateData>
{
    const int bitsOfShort = sizeof(short) * 8;

    int s0, s1, s2, s3;
    short s4;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static (int, int) CalcOffsetAndIndex(int val)
    {
        var offset = val / bitsOfShort;//(Size.Of<short>() * 8);
        var idx = val - (offset * bitsOfShort);// (Size.Of<short>() * 8));
        return (offset, idx);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    unsafe bool Get(int global_idx)
    {
        fixed (KeyboardStateData* addr = &this)
        {
            short* s_addr = (short*)addr;

            (var offset, var idx) = CalcOffsetAndIndex(global_idx);

            return Convert.ToBoolean(
                (*(s_addr + offset)) & (1 << idx)
                );
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    unsafe void Set(int global_idx, bool value)
    {
        //var val = Convert.ToInt32(value);?????

        fixed (KeyboardStateData* addr = &this)
        {
            short* s_addr = (short*)addr;

            (var offset, var idx) = CalcOffsetAndIndex(global_idx);

            if (value)
            {
                *(s_addr + offset) |= (short)(1 << idx);

            }
            else
            {
                *(s_addr + offset) &= (short)~(1 << idx);
            }
        }
    }

    public bool this[Key k]
    {
        get => Get((int)k);

        internal set => Set((int)k, value);
    }

    public bool this[MouseButton b]
    {
        get => Get((int)Veldrid.Key.LastKey + (int)b);
        set => Set((int)Veldrid.Key.LastKey + (int)b, value);
    }

    public void Clear()
    {
        s0 = s1 = s2 = s3 = s4 = 0;
    }

    public override string ToString()
    {
        return
            ToBitStringInt(s0) + "\n" +
            ToBitStringInt(s1) + "\n" +
            ToBitStringInt(s2) + "\n" +
            ToBitStringInt(s3) + "\n" +
            ToBitStringShort(s4) + "\n";

        //what fucking abominations are those to bit functions
        string ToBitStringShort(short b)
        {
            StringBuilder builder = new();

            for (int i = 0; i < 16; i++)
            {
                builder.Insert(0, ((b % 2) == 0 ? "0" : "1"));
                b = (short)(b >> 1);
            }

            return builder.ToString();
        }

        string ToBitStringInt(Int32 b)
        {
            StringBuilder builder = new();

            for (int i = 0; i < 32; i++)
            {
                builder.Insert(0, ((b % 2) == 0 ? "0" : "1"));
                b >>= 1;
            }

            return builder.ToString();
        }
    }

    public bool Equals(KeyboardStateData other)
    {
        return other.s0 == s0 && other.s1 == s1 && other.s2 == s2 && other.s3 == s3 && other.s4 == s4;
    }

    public static KeyboardStateData Default => new KeyboardStateData();

    public override bool Equals(object? obj)
    {
        return obj is KeyboardStateData && Equals((KeyboardStateData)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(s0, s1, s2, s3, s4);
    }
}


