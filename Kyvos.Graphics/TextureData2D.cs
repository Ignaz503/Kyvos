using Veldrid;
using SixLabors.ImageSharp.PixelFormats;
using System.Runtime.InteropServices;
using SixLabors.ImageSharp;
using System.Numerics;
using Kyvos.Maths;
using Kyvos.Assets;

namespace Kyvos.Graphics;

/// <summary>
/// basically a reimplementing of veldrid  image sharp texture
/// to use the newest version of image sharp to fix a problem with the image not being contigous in memory
/// </summary>
public class TextureData2D : TextureData
{
    static readonly SixLabors.ImageSharp.Configuration loadConfig;
    static TextureData2D()
    {
        var config = Configuration.Default.Clone();
        config.PreferContiguousImageBuffers = true;
        loadConfig = config;
    }

    public PixelFormat Format { get; init; }
    public override uint Width => (uint)data[0].Width;
    public override uint Height => (uint)data[0].Height;
    public int MipLevels => data.Length;
    internal Image<Rgba32>[] data;

    public TextureData2D(AssetLocation location,  bool mipmap = true, bool srgb=false)
    {
        Format = srgb ?  PixelFormat.R8_G8_B8_A8_UNorm_SRgb:  PixelFormat.R8_G8_B8_A8_UNorm;
        data = Load(Image.Load<Rgba32>(loadConfig, location.First), mipmap);
    }

    private static Image<Rgba32>[] Load(Image<Rgba32> image, bool mipmap)
    {
        if (mipmap)
        {
            return MipmapHelper.GenerateMipmaps(image);
        }
         return new Image<Rgba32>[] { image };     
    }


    protected override Veldrid.Texture CreateDeviceTextureViaStaging(GraphicsDevice gfxDevice) 
        => WriteIntoViaStaging(gfxDevice, gfxDevice.ResourceFactory.CreateTexture(TextureDescription.Texture2D((uint)Width, (uint)Height, (uint)MipLevels, arrayLayers: 1u, Format, TextureUsage.Sampled)));
        
    protected override unsafe Veldrid.Texture WriteIntoViaStaging(GraphicsDevice gfxDevice, Veldrid.Texture texture)
    {
        var resourceFactory = gfxDevice.ResourceFactory;
        var staging = resourceFactory.CreateTexture(TextureDescription.Texture2D(texture.Width, texture.Height, texture.MipLevels, arrayLayers: 1u, Format, TextureUsage.Staging));

        staging = WriteIntoViaUpdate(gfxDevice, staging);
        var cl = resourceFactory.CreateCommandList();
        cl.Begin();
        cl.CopyTexture(staging, texture);
        cl.End();
        gfxDevice.SubmitCommands(cl);

        return texture;
    }

    protected override Veldrid.Texture CreateDeviceTextureViaUpdate(GraphicsDevice gfxDevice) 
        => WriteIntoViaUpdate(gfxDevice,gfxDevice.ResourceFactory.CreateTexture(TextureDescription.Texture2D((uint)Width, (uint)Height, (uint)MipLevels, arrayLayers: 1u, Format, TextureUsage.Sampled)));

    protected override unsafe Veldrid.Texture WriteIntoViaUpdate(GraphicsDevice gfxDevice, Veldrid.Texture texture)
    {
        var memory = default(Memory<Rgba32>);
        uint width = 0;
        uint height = 0;
        for (int i = 0; i < MipLevels; i++)
        {
            Image<Rgba32> image = data[i];
            width = (uint)image.Width;
            height = (uint)image.Height;
            var size = width * height * FormatSizeInBytes(Format);

            if (!image.DangerousTryGetSinglePixelMemory(out memory))
                throw new NonContigousPixelMemoryException();

            fixed (Rgba32* ptr = &MemoryMarshal.GetReference(memory.Span))
            {
                void* dataPtr = ptr;
                gfxDevice.UpdateTexture(texture, (IntPtr)dataPtr, size, 0u, 0u, 0u, width, height, depth: 1u, mipLevel: (uint)i, arrayLayer: 0u);
            }
        }

        return texture;
    }

    public void SetPixel(float u, float v, Rgba32 rgba)
    {
        u = Mathf.Clamp01(u);
        v = Mathf.Clamp01(v);

        var x = (int)(u * (Width-1));
        var y = (int)(v * (Height-1));

        data[0][x,y] = rgba;
    }

    public void SetPixel(float u, float v, byte r, byte g, byte b, byte a = byte.MaxValue)
        => SetPixel(u, v, new Rgba32(r, g, b, a));
    public void SetPixel(float u, float v, float r, float g, float b, float a = 1f)
        => SetPixel(u, v, new Rgba32(r, g, b, a));
    public void SetPixel(float u, float v, Vector3 rgb)
        => SetPixel(u, v, new Rgba32(rgb));

    public void SetPixel(float u, float v, Vector4 rgba)
        => SetPixel(u, v, new Rgba32(rgba));

    public void SetPixel(float u, float v, uint pixel)
        => SetPixel(u, v, new Rgba32(pixel));

    public void SetPixel(int x, int y, Rgba32 rgba)
    {
        data[0][x,y] = rgba;
    }

    public void SetPixel(int x, int y, byte r, byte g, byte b, byte a = byte.MaxValue)
        => SetPixel(x, y, new Rgba32(r, g, b, a));
    public void SetPixel(int x, int y, float r, float g, float b, float a = 1f)
        => SetPixel(x, y, new Rgba32(r, g, b, a));
    public void SetPixel(int x, int y, Vector3 rgb)
        => SetPixel(x, y, new Rgba32(rgb));

    public void SetPixel(int x, int y, Vector4 rgba)
        => SetPixel(x, y, new Rgba32(rgba));

    public void SetPixel(int x, int y, uint pixel)
        => SetPixel(x, y, new Rgba32(pixel));

    //TODO maybe forward declare Rgba32 struct instead of direct sixlabor use
    public void SetPixels(Span<Rgba32> pixels, int x = 0, int y = 0)
    {
        var idx = Indexing.TwoDimToOneDim(x,y,(int)Width);

        if (!data[0].DangerousTryGetSinglePixelMemory(out var memory))
            throw new NonContigousPixelMemoryException();

        pixels.CopyTo(memory.Span[idx..]);

    }

    //this assumes correctly packed floats
    public void SetPixels(Span<float> pixels, int x = 0, int y = 0)
        => SetPixels(MemoryMarshal.Cast<float,Rgba32>(pixels), x, y);

    public void SetPixels(Span<byte> pixels, int x = 0, int y = 0)
        => SetPixels(MemoryMarshal.Cast<byte, Rgba32>(pixels), x, y);

    protected override void DisposeInternal()
    {
        foreach (var image in data)
            image.Dispose();
    }

    internal void RecalculateMipMaps()
    {
        if (MipLevels > 1) 
        {
            data = MipmapHelper.GenerateMipmaps(data[0]);
        }
    }

    public override Veldrid.Texture ApplyChanges(GraphicsDevice gfxDevice, Veldrid.Texture deviceTexture, TextureMutationMethod method) 
    {
        RecalculateMipMaps();
        return UpdateDeviceTexture(gfxDevice, deviceTexture, method);
    }

}
