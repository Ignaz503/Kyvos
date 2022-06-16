using System;
using System.Runtime.CompilerServices;

namespace Kyvos.Maths;
public static class Mathf
{
    public const float DegToRad = MathF.PI / 180.0f;

    public const float RadToDeg = 180.0f / MathF.PI;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool AlmostEquals(float value1, float value2, float epsilon = 0.0000001f)
    {
        return Math.Abs(value1 - value2) < epsilon;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float ModularClamp(float val, float min, float max, float rangemin, float rangemax)
    {
        var modulus = Math.Abs(rangemax - rangemin);
        if ((val %= modulus) < 0f)
            val += modulus;
        return Math.Clamp(val + Math.Min(rangemin, rangemax), min, max);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float ModularClamp(float val, float min, float max)
        => ModularClamp(val, min, max, min, max);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int RoundToInt(float a)
        => a >= 0 ? (int)(a + 0.5f) : (int)(a - 0.5f);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int FloorToInt(float a)
        => a >= 0 ? (int)a : (int)a - 1;

    public static float Map(float x, float inMin, float inMax, float outMin, float outMax)
    {
        return (x - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
    }

    public static float Random(System.Random r, float min, float max)
    {
        float range = max - min;
        return range * r.NextSingle() + min;
    }

    public static float Clamp01(float val)
    {
        return val < 0f ? 0f
            : val > 1f ? 1f
            : val;
    }
}

