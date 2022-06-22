using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace Kyvos.Graphics;

/// <summary>
/// see Veldrid.ImageSharp MipMapHelper
/// sadly it's internal
/// </summary>
internal static class MipmapHelper
{
    public static int ComputeMipLevels(int width, int height)
    {
        return 1 + (int)Math.Floor(Math.Log(Math.Max(width, height), 2.0));
    }

    public static int GetDimension(int largestLevelDimension, int mipLevel)
    {
        int num = largestLevelDimension;
        for (int i = 0; i < mipLevel; i++)
        {
            num /= 2;
        }

        return Math.Max(1, num);
    }

    internal static Image<Rgba32>[] GenerateMipmaps(Image<Rgba32> baseImage)
    {
        Image<Rgba32>[] array = new Image<Rgba32>[ComputeMipLevels(baseImage.Width, baseImage.Height)];
        array[0] = baseImage;
        int num = 1;
        int num2 = baseImage.Width;
        int num3 = baseImage.Height;
        while (num2 != 1 || num3 != 1)
        {
            int newWidth = Math.Max(1, num2 / 2);
            int newHeight = Math.Max(1, num3 / 2);
            array[num] = baseImage.Clone(delegate (IImageProcessingContext context)
            {
                context.Resize(newWidth, newHeight, KnownResamplers.Lanczos3);
            });
            num++;
            num2 = newWidth;
            num3 = newHeight;
        }

        return array;
    }

    internal static void GenerateMipmaps(Image<Rgba32>[] images)
    {
        var baseImage = images[0];
        int num = 1;
        int num2 = baseImage.Width;
        int num3 = baseImage.Height;
        while (num2 != 1 || num3 != 1)
        {
            int newWidth = Math.Max(1, num2 / 2);
            int newHeight = Math.Max(1, num3 / 2);

            
            images[num] = baseImage.Clone(delegate (IImageProcessingContext context)
            {
                context.Resize(newWidth, newHeight, KnownResamplers.Lanczos3);
            });
            num++;
            num2 = newWidth;
            num3 = newHeight;
        }
    }

}