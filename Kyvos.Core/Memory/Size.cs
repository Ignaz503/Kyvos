using System.Runtime.CompilerServices;

namespace Kyvos.Core.Memory;
public static partial class Size
{
    public static int Of<T>()
    {
        return Unsafe.SizeOf<T>();
    }

    public static uint Of_U<T>()
    {
        return (uint)Of<T>();
    }

    public static int Of<T>(T[] array)
    {
        return array.Length * Of<T>();
    }

    public static uint Of_U<T>(T[] array)
    {
        return (uint)Of(array);
    }
}

