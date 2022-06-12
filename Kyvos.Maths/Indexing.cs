using System.Runtime.CompilerServices;
namespace Kyvos.Maths;
public static class Indexing
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int TwoDimToOneDim(int x, int y, int width)
        => x + y * width;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static (int x, int y) OneDimToTwoDim(int i, int width)
        => (i % width, i / width);

    public static int ThreeDimToOneDim(int x, int y, int z, int width, int depth)
        => x + y * width + z * width * depth;

    public static (int x, int y, int z) OneDimToThreeDim(int i, int width, int depth)
    {
        int wd = width * depth;
        int z = i / (wd);
        i -= (z * wd);
        int x = i % width;
        int y = i / width;

        return (x, y, z);
    }

}



