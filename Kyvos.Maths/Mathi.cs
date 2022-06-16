using System;
using System.Runtime.CompilerServices;

namespace Kyvos.Maths;
public static class Mathi 
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Digits( int n )
             => n == 0 ? 1 : (int)Math.Floor( Math.Log10( Math.Abs( n ) ) + 1 );
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int ModularClamp(int val, int min, int max, int rangemin, int rangemax)
    {
        var modulus = Math.Abs(rangemax - rangemin);
        if ((val %= modulus) < 0f)
            val += modulus;
        return Math.Clamp(val + Math.Min(rangemin, rangemax), min, max);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int ModularClamp(int val, int min, int max)
        => ModularClamp(val, min, max, min, max);
}

