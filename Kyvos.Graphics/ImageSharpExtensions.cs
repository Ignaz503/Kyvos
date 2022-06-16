using Veldrid.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System.Numerics;
using Kyvos.Maths;
using System.Runtime.InteropServices;

namespace Kyvos.Graphics;

public static class ImageSharpExtensions 
{
    public static void SetPixel(this ImageSharpTexture texture, float u, float v,  Rgba32 rgba32) 
    {
        u = Mathf.Clamp01(u);
        v = Mathf.Clamp01(v);

        var image = texture.Images[0];
        var x = (int)(u * image.Width);
        var y = (int)(v * image.Height);

        image[x, y] = rgba32;
    }

    public static void SetPixel(this ImageSharpTexture texture, float u, float v, byte r, byte g, byte b, byte a = byte.MaxValue)
        => texture.SetPixel(u,v,new Rgba32(r, g, b, a));
    public static void SetPixel(this ImageSharpTexture texture, float u, float v, float r, float g, float b, float a = 1f)
        => texture.SetPixel(u, v, new Rgba32(r, g, b, a));
    public static void SetPixel(this ImageSharpTexture texture, float u, float v, Vector3 rgb)
        => texture.SetPixel(u, v, new Rgba32(rgb));
   
    public static void SetPixel(this ImageSharpTexture texture, float u, float v, Vector4 rgba)
        => texture.SetPixel(u, v, new Rgba32(rgba));

    public static void SetPixel(this ImageSharpTexture texture, float u, float v, uint pixel)
        => texture.SetPixel(u, v, new Rgba32(pixel));

    public static void SetPixel(this ImageSharpTexture texture, int x , int y, Rgba32 rgba32)
    {
        x = Math.Clamp(x, 0, (int)texture.Width - 1);
        y = Math.Clamp(y, 0, (int)texture.Height - 1);

        var image = texture.Images[0];
        
        image[x, y] = rgba32;
    }

    public static void SetPixel(this ImageSharpTexture texture, int x, int y, byte r, byte g, byte b, byte a = byte.MaxValue)
        => texture.SetPixel(x, y, new Rgba32(r, g, b, a));
    public static void SetPixel(this ImageSharpTexture texture, int x, int y, float r, float g, float b, float a = 1f)
        => texture.SetPixel(x, y, new Rgba32(r, g, b, a));
    public static void SetPixel(this ImageSharpTexture texture, int x, int y, Vector3 rgb)
        => texture.SetPixel(x, y, new Rgba32(rgb));

    public static void SetPixel(this ImageSharpTexture texture, int x, int y, Vector4 rgba)
        => texture.SetPixel(x, y, new Rgba32(rgba));

    public static void SetPixel(this ImageSharpTexture texture, int x, int y, uint pixel)
        => texture.SetPixel(x, y, new Rgba32(pixel));

    //TODO maybe forward declare Rgba32 struct instead of direct sixlabor use
    public static void SetPixels(this ImageSharpTexture texture, Span<Rgba32> pixels, int x = 0, int y = 0 ) 
    {
        var image = texture.Images[0];

        if (!image.TryGetSinglePixelSpan(out var pixelSpan))
        {
            throw new TextureUpdateException("Can only modify textures stored contigously in memory.");
        }

        var start = Indexing.TwoDimToOneDim(x, y, image.Width);
        if (start + pixels.Length > pixelSpan.Length)
        {
            throw new TextureUpdateException($"Can't copy more pixels than the texture has x:{x} y:{y} length:{start + pixels.Length} allowed:{pixelSpan.Length - start}");
        }

        pixels.CopyTo(pixelSpan[start..]);
    }

    public static unsafe void SetPixels(this ImageSharpTexture texture, Span<float> pixels, int x = 0, int y = 0)
    {

        fixed (float* native = &MemoryMarshal.GetReference(pixels)) 
        {
            void* ptr = native;
            texture.SetPixels(new Span<Rgba32>(ptr,pixels.Length),x,y);
        }
        
    }

    public static unsafe void SetPixels(this ImageSharpTexture texture, Span<byte> pixels, int x = 0, int y = 0)
    {
        if (pixels.Length % 4 != 0)
            throw new TextureUpdateException("Pixels must be a multiple of 4");

        fixed (byte* native = &MemoryMarshal.GetReference(pixels))
        {
            void* ptr = native;
            texture.SetPixels(new Span<Rgba32>(ptr, pixels.Length / 4), x, y);
        }
        
    }

}