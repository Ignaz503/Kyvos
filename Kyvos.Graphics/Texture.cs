using System;
using Veldrid;
using Veldrid.ImageSharp;
using Kyvos.Utility;
using System.IO;
using SixLabors.ImageSharp.PixelFormats;
using System.Runtime.InteropServices;
using SixLabors.ImageSharp;
using Veldrid.Utilities;
using Veldrid.SPIRV;
using System.Numerics;
using Kyvos.Assets;
using Kyvos.Core.Logging;
using System.Diagnostics;
using Kyvos.Maths;

namespace Kyvos.Graphics;

public enum DeviceTextureCreation
{
    Staging,
    Update
}

public class Texture : IDisposable
{
    internal static event Action<AssetIdentifier>? OnNoReference;


    
    public static DeviceTextureCreation CreationMethod = DeviceTextureCreation.Staging;

    //TODO don't use imagesharp texture and make your own thing
    //ImageSharpTexture data;
    TextureData data;
    Veldrid.Texture? deviceTexture;

    public Veldrid.Texture Native
        => deviceTexture ?? throw new InvalidOperationException("Call CreateDeviceTexture first!");
    AssetIdentifier assetId;
    ReferenceCounter refCounter;
    bool isDisposed = false;
    object _lockObject;

    public uint Width => (uint)data.Width;
    public uint Height => (uint)data.Height ;

#pragma warning disable CS8618       //data will alsways be set by public ctors
    private Texture(AssetIdentifier id) 
    {
        this.assetId = id;
        deviceTexture = null;
        refCounter = new(0);
        _lockObject = new();
    }
#pragma warning restore

    public Texture(AssetIdentifier id, string path) :this(id)
        => data = new(path);

    public Texture(AssetIdentifier id, string path, bool mipmap) : this(id)
    {
        data = new(path, mipmap);
    }

    public Texture(AssetIdentifier id, string path, bool mipmap, bool srgb) : this(id)
        => data = new(path, mipmap, srgb);
    
    public Texture(AssetIdentifier id, Stream stream) : this(id)
        => data = new(stream);
    
    public Texture(AssetIdentifier id, Stream stream, bool mipmap) : this(id)
       => data = new(stream, mipmap);
    
    public Texture(AssetIdentifier id, Stream stream, bool mipmap, bool srgb) : this(id)
        => data = new(stream, mipmap, srgb);



    public void CreateDeviceTexture(GraphicsDevice gfxDevice, DeviceTextureCreation method = DeviceTextureCreation.Update) 
    {
        lock(_lockObject)
        {
            if (deviceTexture is not null)
                return;

            deviceTexture = data.CreateDeviceTexture(gfxDevice,TextureUsage.Sampled,method);
        }


        Log<Texture>.Debug("W: {Width} H:{Height} Mips: {Mip}", deviceTexture!.Width, deviceTexture.Height, deviceTexture.MipLevels);
        Log<Texture>.Debug("Format: {Form} Usage: {Usg} Layers: {ArrayLayers}", deviceTexture.Format, deviceTexture.Usage, deviceTexture.ArrayLayers);

    }

    public TextureView GetTextureView(GraphicsDevice gfxDevice) 
    {
        if (deviceTexture is null)
            CreateDeviceTexture(gfxDevice, CreationMethod);

        refCounter.Increment();

        
        return gfxDevice.ResourceFactory.CreateTextureView(deviceTexture);
    }

    public void SetPixel( float u, float v, Rgba32 rgba)
        => data.SetPixel(u, v, rgba);

    public void SetPixel( float u, float v, byte r, byte g, byte b, byte a = byte.MaxValue)
        => data.SetPixel(u, v, r,g,b,a);
    public void SetPixel( float u, float v, float r, float g, float b, float a = 1f)
        => data.SetPixel(u, v, r, g, b, a);
    public void SetPixel( float u, float v, Vector3 rgb)
        => data.SetPixel(u, v, rgb);

    public void SetPixel( float u, float v, Vector4 rgba)
        => data.SetPixel(u, v, rgba);

    public void SetPixel( float u, float v, uint pixel)
        => data.SetPixel(u, v, pixel);

    public   void SetPixel( int x, int y, Rgba32 rgba)
        => data.SetPixel(x, y, rgba);

    public void SetPixel( int x, int y, byte r, byte g, byte b, byte a = byte.MaxValue)
        => data.SetPixel(x, y,r,g,b,a);
    public void SetPixel( int x, int y, float r, float g, float b, float a = 1f)
        => data.SetPixel(x, y, r, g, b, a);
    public void SetPixel( int x, int y, Vector3 rgb)
        => data.SetPixel(x, y, rgb);

    public void SetPixel( int x, int y, Vector4 rgba)
        => data.SetPixel(x, y, rgba);

    public void SetPixel( int x, int y, uint pixel)
        => data.SetPixel(x, y, pixel);

    //TODO maybe forward declare Rgba32 struct instead of direct sixlabor use
    public void SetPixels( Span<Rgba32> pixels, int x = 0, int y = 0)
        => data.SetPixels(pixels, x, y);
    
    public void SetPixels( Span<float> pixels, int x = 0, int y = 0)
        => data.SetPixels(pixels, x, y);

    public void SetPixels( Span<byte> pixels, int x = 0, int y = 0)
        => data.SetPixels(pixels, x, y);


    public unsafe void ApplyChanges(GraphicsDevice gd, bool generateMipMaps = true)
    {
        if (data is null)
            return;
        if (deviceTexture is null)
            return;

        throw new NotImplementedException();
    }

    internal void DisposeInternal()
    {
        if (isDisposed)
            return;

        Log<Texture>.Debug("Disposing {Obj}", assetId);
        deviceTexture?.Dispose();
        
        isDisposed = true;
    }

    public void Dispose()
    { 
        var c = refCounter.Decrement();
        if (c > 0)
            return;
        OnNoReference?.Invoke(assetId);
    }
}

internal class TextureData
{
    static readonly SixLabors.ImageSharp.Configuration loadConfig;
    static TextureData()
    {
        var config = Configuration.Default.Clone();
        config.PreferContiguousImageBuffers = true;
        loadConfig = config;
    }


    public PixelFormat Format { get; init; }
    public TextureType Type { get; init; }

    public int Width { get; private set; }
    public int Height { get; private set; }
    public int Depth { get; private set; }
    public int MipLevels { get; private set; }

    public int ArrayLayers { get; private set; }

    public Rgba32[] Data { get; private set; }

    public TextureData(PixelFormat format, TextureType type, int width, int height, int depth, int mipLevels, int arrayLayers, Rgba32[] bytes)
    {
        Format = format;
        Type = type;
        Width = width;
        Height = height;
        Depth = depth;
        MipLevels = mipLevels;
        Debug.Assert(MipLevels > 0, "Min mip level 1");
        ArrayLayers = arrayLayers;
        Data = bytes;
    }

#pragma warning disable CS8618 //load sets all values
    public TextureData(string path, bool mipmap = true, bool srgb=false) 
    {
        Format = srgb ?  PixelFormat.R8_G8_B8_A8_UNorm_SRgb:  PixelFormat.R8_G8_B8_A8_UNorm;
        Type = TextureType.Texture2D;

         Load(Image.Load<Rgba32>(loadConfig, path), mipmap);

    }


    public TextureData(Stream stream, bool mipmap=true, bool srgb=false)
    {
        Format = srgb ? PixelFormat.R8_G8_B8_A8_UNorm_SRgb : PixelFormat.R8_G8_B8_A8_UNorm;
        Type = TextureType.Texture2D;
        Load(Image.Load<Rgba32>(loadConfig, stream), mipmap);
    }
# pragma warning restore

    private void Load(Image<Rgba32> image, bool mipmap)
    {
        if (mipmap)
            Log<TextureData>.Information("Mipmapping not supported yet");

        Width = image.Width;
        Height = image.Height;
        Depth = 1;
        ArrayLayers = 1;
        MipLevels = 1;

        Data = new Rgba32[Width * Height];


        int c = 0;
        for (int i = 0; i < Height; i++)
        {       
            for (int j = 0; j < Width; j++)
            {
                var item = image[j,i];
                if (item.R == 0 && item.G == 0 && item.B == 0 && item.A == 0)
                    c++;
            }
        }


        if (!image.DangerousTryGetSinglePixelMemory(out var span))
            throw new NonContigousPixelMemoryException();
        Log.Debug("Empty Count {Count} out of {Length}", c, span.Length);

        Debug.Assert(span.Length == Data.Length, "Data and pixel span need to be of same size");
        //image.CopyPixelDataTo(Data.AsSpan());


        span.CopyTo(Data.AsMemory());
    }

    public Veldrid.Texture CreateDeviceTexture(GraphicsDevice gfxDevice, TextureUsage usage, DeviceTextureCreation method) 
        => method switch {
            DeviceTextureCreation.Staging =>
                ViaStaging(gfxDevice, usage),
            DeviceTextureCreation.Update =>
                ViaUpdate(gfxDevice, usage),
            _ => throw new InvalidOperationException() };
    

     unsafe Veldrid.Texture ViaStaging(GraphicsDevice gfxDevice, TextureUsage usage) 
    {
        var resourceFactory = gfxDevice.ResourceFactory;
        var texture = resourceFactory.CreateTexture(new TextureDescription((uint)Width, (uint)Height, (uint)Depth, (uint)MipLevels, (uint)ArrayLayers, Format, usage, Type));

        var staging = resourceFactory.CreateTexture(new((uint)Width, (uint)Height, (uint)Depth, (uint)MipLevels, (uint)ArrayLayers,Format,TextureUsage.Staging, Type));

        ulong dataOffset = 0;

        var span = Data.AsSpan();
        var byteSpan = MemoryMarshal.Cast<Rgba32,byte>(span);

        var uWidth = (uint)Width;
        var uHeight = (uint)Height;
        var uDepth = (uint)Depth;

        fixed (byte* dataPtr = &MemoryMarshal.GetReference(byteSpan)) 
        {
            for (uint currentLevel = 0; currentLevel < MipLevels; currentLevel++)
            {
                var lWidth = GetDim(uWidth, currentLevel);
                var lHeight = GetDim(uHeight, currentLevel);
                var lDepth = GetDim(uDepth, currentLevel);
                var size = lWidth*lHeight*lDepth * FormatSizeInBytes(Format);

                for (uint layer = 0; layer < ArrayLayers; layer ++)
                {
                    gfxDevice.UpdateTexture(staging, (IntPtr)(dataPtr + dataOffset), size, 0, 0, 0, lWidth, lHeight, lDepth, currentLevel, layer);
                    dataOffset += size;
                }
            }
        }

        var cl = resourceFactory.CreateCommandList();
        cl.Begin();
        cl.CopyTexture(staging, texture);
        cl.End();
        gfxDevice.SubmitCommands(cl);

        return texture;
    }
    unsafe Veldrid.Texture ViaUpdate(GraphicsDevice gfxDevice, TextureUsage usage) 
    {
        throw new NotImplementedException();
    }
    public void SetPixel(float u, float v, Rgba32 rgba)
    {
        u = Mathf.Clamp01(u);
        v = Mathf.Clamp01(v);

        var x = (int)(u * Width);
        var y = (int)(v * Height);

        Data[Indexing.TwoDimToOneDim(x,y,(int)Width)] = rgba;
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
        Data[Indexing.TwoDimToOneDim(x, y, (int)Width)] = rgba;
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

        pixels.CopyTo(Data.AsSpan()[idx..]);

    }

    //this assumes correctly packed floats
    public void SetPixels(Span<float> pixels, int x = 0, int y = 0)
        => SetPixels(MemoryMarshal.Cast<float,Rgba32>(pixels), x, y);

    public void SetPixels(Span<byte> pixels, int x = 0, int y = 0)
        => SetPixels(MemoryMarshal.Cast<byte, Rgba32>(pixels), x, y);


    static uint GetDim(uint largestDim, uint mipLevel)
    {
        uint ret = largestDim;

        //ret = ret >>> (mipLevel - 1);
        for (int i = 0; i < mipLevel; i++)
        {
            ret /= 2;
        }
        return Math.Max(1u, ret);
    }
    static uint FormatSizeInBytes(PixelFormat format)
    {
        switch (format) 
        {
            //
            // Summary:
            //     RGBA component order. Each component is an 8-bit unsigned normalized integer.
            case PixelFormat.R8_G8_B8_A8_UNorm:
                return 4;
            //
            // Summary:
            //     BGRA component order. Each component is an 8-bit unsigned normalized integer.
            case PixelFormat.B8_G8_R8_A8_UNorm:
                return 4;
            //
            // Summary:
            //     Single-channel: 8-bit unsigned normalized integer.
            case PixelFormat.R8_UNorm:
                return 1;
            //
            // Summary:
            //     Single-channel: 16-bit unsigned normalized integer. Can be used as a depth format.
            case PixelFormat.R16_UNorm:
                return 2;
            //
            // Summary:
            //     RGBA component order. Each component is a 32-bit signed floating-point value.
            case PixelFormat.R32_G32_B32_A32_Float:
                return 16;
            //
            // Summary:
            //     Single-channel: 32-bit signed floating-point value. Can be used as a depth format.
            case PixelFormat.R32_Float:
                return 4;
            //
            // Summary:
            //     BC3 block compressed format.
            case PixelFormat.BC3_UNorm:
                return 1;
            //
            // Summary:
            //     A depth-stencil format where the depth is stored in a 24-bit unsigned normalized
            //     integer: and the stencil is stored in an 8-bit unsigned integer.
            case PixelFormat.D24_UNorm_S8_UInt:
                return 4;
            //
            // Summary:
            //     A depth-stencil format where the depth is stored in a 32-bit signed floating-point
            //     value: and the stencil is stored in an 8-bit unsigned integer.
            case PixelFormat.D32_Float_S8_UInt:
                return 5;
            //
            // Summary:
            //     RGBA component order. Each component is a 32-bit unsigned integer.
            case PixelFormat.R32_G32_B32_A32_UInt:
                return 16;
            //
            // Summary:
            //     RG component order. Each component is an 8-bit signed normalized integer.
            case PixelFormat.R8_G8_SNorm:
                return 2;

            //
            // Summary:
            //     A 32-bit packed format. The 10-bit R component occupies bits 0..9: the 10-bit
            //     G component occupies bits 10..19: the 10-bit A component occupies 20..29: and
            //     the 2-bit A component occupies bits 30..31. Each value is an unsigned: normalized
            //     integer.
            case PixelFormat.R10_G10_B10_A2_UNorm:
                return 4;
            //
            // Summary:
            //     A 32-bit packed format. The 10-bit R component occupies bits 0..9: the 10-bit
            //     G component occupies bits 10..19: the 10-bit A component occupies 20..29: and
            //     the 2-bit A component occupies bits 30..31. Each value is an unsigned integer.
            case PixelFormat.R10_G10_B10_A2_UInt:
                return 4;
            //
            // Summary:
            //     A 32-bit packed format. The 11-bit R componnent occupies bits 0..10: the 11-bit
            //     G component occupies bits 11..21: and the 10-bit B component occupies bits 22..31.
            //     Each value is an unsigned floating point value.
            case PixelFormat.R11_G11_B10_Float:
                return 4;
            //
            // Summary:
            //     Single-channel: 8-bit signed normalized integer.
            case PixelFormat.R8_SNorm:
                return 1;
            //
            // Summary:
            //     Single-channel: 8-bit unsigned integer.
            case PixelFormat.R8_UInt:
                return 1;
            //
            // Summary:
            //     Single-channel: 8-bit signed integer.
            case PixelFormat.R8_SInt:
                return 1;
            //
            // Summary:
            //     Single-channel: 16-bit signed normalized integer.
           case PixelFormat.R16_SNorm:
                return 2;
            //
            // Summary:
            //     Single-channel: 16-bit unsigned integer.
            case PixelFormat.R16_UInt:
                return 2;
            //
            // Summary:
            //     Single-channel: 16-bit signed integer.
            case PixelFormat.R16_SInt:
                return 2;
            //
            // Summary:
            //     Single-channel: 16-bit signed floating-point value.
            case PixelFormat.R16_Float:
                return 2;
            //
            // Summary:
            //     Single-channel: 32-bit unsigned integer
            case PixelFormat.R32_UInt:
                return 4;
            //
            // Summary:
            //     Single-channel: 32-bit signed integer
            case PixelFormat.R32_SInt:
                return 4;
            //
            // Summary:
            //     RG component order. Each component is an 8-bit unsigned normalized integer.
            case PixelFormat.R8_G8_UNorm:
                return 2;
            //
            // Summary:
            //     RG component order. Each component is an 8-bit unsigned integer.
            case PixelFormat.R8_G8_UInt:
                return 2;
            //
            // Summary:
            //     RG component order. Each component is an 8-bit signed integer.
            case PixelFormat.R8_G8_SInt:
                return 2;
            //
            // Summary:
            //     RG component order. Each component is a 16-bit unsigned normalized integer.
            case PixelFormat.R16_G16_UNorm:
                return 4;
            //
            // Summary:
            //     RG component order. Each component is a 16-bit signed normalized integer.
            case PixelFormat.R16_G16_SNorm:
                return 4;
            //
            // Summary:
            //     RG component order. Each component is a 16-bit unsigned integer.
            case PixelFormat.R16_G16_UInt:
                return 4;
            //
            // Summary:
            //     RG component order. Each component is a 16-bit signed integer.
            case PixelFormat.R16_G16_SInt:
                return 4;
            //
            // Summary:
            //     RG component order. Each component is a 16-bit signed floating-point value.
            case PixelFormat.R16_G16_Float:
                return 4;
            //
            // Summary:
            //     RG component order. Each component is a 32-bit unsigned integer.
            case PixelFormat.R32_G32_UInt:
                return 8;
            //
            // Summary:
            //     RG component order. Each component is a 32-bit signed integer.
            case PixelFormat.R32_G32_SInt:
                return 8;
            //
            // Summary:
            //     RG component order. Each component is a 32-bit signed floating-point value.
            case PixelFormat.R32_G32_Float:
                return 8;
            //
            // Summary:
            //     RGBA component order. Each component is an 8-bit signed normalized integer.
            case PixelFormat.R8_G8_B8_A8_SNorm:
                return 4;
            //
            // Summary:
            //     RGBA component order. Each component is an 8-bit unsigned integer.
            case PixelFormat.R8_G8_B8_A8_UInt:
                return 4;
            //
            // Summary:
            //     RGBA component order. Each component is an 8-bit signed integer.
            case PixelFormat.R8_G8_B8_A8_SInt:
                return 4;
            //
            // Summary:
            //     RGBA component order. Each component is a 16-bit unsigned normalized integer.
            case PixelFormat.R16_G16_B16_A16_UNorm:
                return 8;
            //
            // Summary:
            //     RGBA component order. Each component is a 16-bit signed normalized integer.
            case PixelFormat.R16_G16_B16_A16_SNorm:
                return 8;
            //
            // Summary:
            //     RGBA component order. Each component is a 16-bit unsigned integer.
            case PixelFormat.R16_G16_B16_A16_UInt:
                return 8;
            //
            // Summary:
            //     RGBA component order. Each component is a 16-bit signed integer.
            case PixelFormat.R16_G16_B16_A16_SInt:
                return 8;
            //
            // Summary:
            //     RGBA component order. Each component is a 16-bit floating-point value.
            case PixelFormat.R16_G16_B16_A16_Float:
                return 8;
            //
            // Summary:
            //     RGBA component order. Each component is a 32-bit signed integer.
            case PixelFormat.R32_G32_B32_A32_SInt:
                return 16;

            //
            // Summary:
            //     RGBA component order. Each component is an 8-bit unsigned normalized integer.
            //     This is an sRGB format.
            case PixelFormat.R8_G8_B8_A8_UNorm_SRgb:
                return 4;
            //
            // Summary:
            //     BGRA component order. Each component is an 8-bit unsigned normalized integer.
            //     This is an sRGB format.
            case PixelFormat.B8_G8_R8_A8_UNorm_SRgb:
                return 4;

            default:
                throw new NotImplementedException();
        }

    }


}
