using System.Numerics;
using System.Runtime.CompilerServices;

namespace Kyvos.Maths;

public static class VectorExtensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3 XYZ(this Vector4 vec4)
        => new(vec4.X, vec4.Y, vec4.Z);


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3 YZW(this Vector4 vec4)
        => new(vec4.Y, vec4.Z, vec4.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2 XY(this Vector4 vec4)
        => new(vec4.X, vec4.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2 YZ(this Vector4 vec4)
        => new(vec4.Y, vec4.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2 ZW(this Vector4 vec4)
        => new(vec4.Z, vec4.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2 XY(this Vector3 vec4)
    => new(vec4.X, vec4.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2 YZ(this Vector3 vec4)
        => new(vec4.Y, vec4.Z);

}
