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

public enum TextureMutationMethod
{
    Staging,
    Update
}

public class Texture : IDisposable
{
    internal static event Action<AssetIdentifier>? OnNoReference;


    
    public static TextureMutationMethod CreationMethod = TextureMutationMethod.Update;

    //TODO don't use imagesharp texture and make your own thing
    //ImageSharpTexture data;
    TextureData2D data;
    Veldrid.Texture? deviceTexture;

    public Veldrid.Texture Native
        => deviceTexture ?? throw new InvalidOperationException("Call CreateDeviceTexture first!");
    AssetIdentifier assetId;
    ReferenceCounter refCounter;
    bool isDisposed = false;
    object _lockObject;

    public uint Width => (uint)data.Width;
    public uint Height => (uint)data.Height;


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



    public void CreateDeviceTexture(GraphicsDevice gfxDevice, TextureMutationMethod method = TextureMutationMethod.Update) 
    {
        lock(_lockObject)
        {
            if (deviceTexture is not null)
                return;

            deviceTexture = data.CreateDeviceTexture(gfxDevice, method);
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

        deviceTexture = data.ApplyChanges(gd, deviceTexture, CreationMethod);
    }

    internal void DisposeInternal()
    {
        if (isDisposed)
            return;

        Log<Texture>.Debug("Disposing {Obj}", assetId);
        deviceTexture?.Dispose();
        data?.Dispose();
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


internal abstract class TextureData : IDisposable
{
    protected bool isDisposed = false;


    public Veldrid.Texture CreateDeviceTexture(GraphicsDevice gfxDevice, TextureMutationMethod method)
    => method switch
    {
        TextureMutationMethod.Staging =>
            CreateDeviceTextureViaStaging(gfxDevice),
        TextureMutationMethod.Update =>
            CreateDeviceTextureViaUpdate(gfxDevice),
        _ => throw new InvalidOperationException()
    };

    public Veldrid.Texture UpdateDeviceTexture(GraphicsDevice gfxDevice, Veldrid.Texture texture, TextureMutationMethod method)
        => method switch
        {
            TextureMutationMethod.Staging =>
                WriteIntoViaStaging(gfxDevice, texture),
            TextureMutationMethod.Update =>
                WriteIntoViaUpdate(gfxDevice, texture),
            _ => throw new InvalidOperationException()
        };



    protected abstract Veldrid.Texture CreateDeviceTextureViaStaging(GraphicsDevice gfxDevice);

    protected abstract Veldrid.Texture CreateDeviceTextureViaUpdate(GraphicsDevice gfxDevice);

    protected abstract Veldrid.Texture WriteIntoViaStaging(GraphicsDevice gfxDevice, Veldrid.Texture texture);
    protected  abstract Veldrid.Texture WriteIntoViaUpdate(GraphicsDevice gfxDevice, Veldrid.Texture texture);
    public  void Dispose() 
    {
        if (isDisposed)
            return;
        DisposeInternal();
        isDisposed = true;
    }
    protected abstract void DisposeInternal();

    public abstract Veldrid.Texture ApplyChanges(GraphicsDevice gfxDevice, Veldrid.Texture texture, TextureMutationMethod method);

    protected static uint FormatSizeInBytes(PixelFormat format)
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

internal class TextureData1D<T> : TextureData
    where T : unmanaged//needs to be blitable data
{
    PixelFormat Format { get; init; }
    T[][] data;


    public TextureData1D(PixelFormat format, T[][] data)
    {
        Format = format;
        this.data = data;
    }
    public TextureData1D(PixelFormat format, T[] data)
    {
        Format = format;
        this.data = new T[][]{ data };
    }

    public int Width => data[0].Length;
    public int MipMapLevels => data.Length;

    public void SetPixel(int x, T value) 
        => data[0][x] = value;

    public void SetPixel(float u, T value)
    {
        u = Mathf.Clamp01(u);
        var x = (int)(u * (Width-1));
        data[0][x] = value;
    }

    public void SetPixels(int x, Span<T> values) 
    {
        values.CopyTo(data[0].AsSpan()[x..]);
    }

    public override Veldrid.Texture ApplyChanges(GraphicsDevice gfxDevice, Veldrid.Texture texture, TextureMutationMethod method) 
        => UpdateDeviceTexture(gfxDevice, texture, method); //TODO mipmap recalculate

    protected override Veldrid.Texture CreateDeviceTextureViaStaging(GraphicsDevice gfxDevice)
        => WriteIntoViaStaging(gfxDevice, gfxDevice.ResourceFactory.CreateTexture(TextureDescription.Texture1D((uint)Width, mipLevels: (uint)MipMapLevels, arrayLayers: 1u, Format, TextureUsage.Sampled)));

    protected override Veldrid.Texture CreateDeviceTextureViaUpdate(GraphicsDevice gfxDevice)
        => WriteIntoViaUpdate(gfxDevice, gfxDevice.ResourceFactory.CreateTexture(TextureDescription.Texture1D((uint)Width, mipLevels: (uint)MipMapLevels, arrayLayers: 1u, Format, TextureUsage.Sampled)));

    protected override void DisposeInternal()
    {}

    protected unsafe override Veldrid.Texture WriteIntoViaStaging(GraphicsDevice gfxDevice, Veldrid.Texture texture) 
    {
        var staging = gfxDevice.ResourceFactory.CreateTexture(TextureDescription.Texture1D(texture.Width, texture.MipLevels, texture.ArrayLayers, texture.Format, TextureUsage.Staging));
        staging = WriteIntoViaUpdate(gfxDevice, staging);

        var cl = gfxDevice.ResourceFactory.CreateCommandList();
        cl.Begin();
        cl.CopyTexture(staging, texture);
        cl.End();
        gfxDevice.SubmitCommands(cl);
        return texture;
    }

    protected override Veldrid.Texture WriteIntoViaUpdate(GraphicsDevice gfxDevice, Veldrid.Texture texture)
    {
        for (int mipLevel = 0; mipLevel < MipMapLevels; mipLevel++)
        {
            var mipData = data[mipLevel];

            gfxDevice.UpdateTexture(texture, mipData, 0u, 0u, 0u, (uint)mipData.Length, 1u, 1u, (uint)mipLevel, 0u);

        }
        return texture;
    }
}


internal class TextureDataCubeMap : TextureData
{
    public enum ArrayLayerIdx 
    {
        PositiveX = 0,
        NegativeX = 1,
        PositiveY = 2,
        NegativeY = 3,
        PositiveZ = 4,
        NegativeZ = 5
    }


    public Image<Rgba32>[][] data { get; }

    public int Width => data[0][0].Width;
    public int Height => data[0][0].Height;
    public PixelFormat Format 
        => PixelFormat.R8_G8_B8_A8_UNorm;
    public int MipMapLevels => data[0].Length;

    public TextureDataCubeMap(string positiveXPath, string negativeXPath, string positiveYPath, string negativeYPath, string positiveZPath, string negativeZPath)
    : this(Image.Load<Rgba32>(positiveXPath), Image.Load<Rgba32>(negativeXPath), Image.Load<Rgba32>(positiveYPath), Image.Load<Rgba32>(negativeYPath), Image.Load<Rgba32>(positiveZPath), Image.Load<Rgba32>(negativeZPath))
    { }

    public TextureDataCubeMap(string positiveXPath, string negativeXPath, string positiveYPath, string negativeYPath, string positiveZPath, string negativeZPath, bool mipmap)
        : this(Image.Load<Rgba32>(positiveXPath), Image.Load<Rgba32>(negativeXPath), Image.Load<Rgba32>(positiveYPath), Image.Load<Rgba32>(negativeYPath), Image.Load<Rgba32>(positiveZPath), Image.Load<Rgba32>(negativeZPath), mipmap)
    { }

    public TextureDataCubeMap(Stream positiveXStream, Stream negativeXStream, Stream positiveYStream, Stream negativeYStream, Stream positiveZStream, Stream negativeZStream, bool mipmap)
        : this(Image.Load<Rgba32>(positiveXStream), Image.Load<Rgba32>(negativeXStream), Image.Load<Rgba32>(positiveYStream), Image.Load<Rgba32>(negativeYStream), Image.Load<Rgba32>(positiveZStream), Image.Load<Rgba32>(negativeZStream), mipmap)
    { }

    public TextureDataCubeMap(Image<Rgba32> positiveX, Image<Rgba32> negativeX, Image<Rgba32> positiveY, Image<Rgba32> negativeY, Image<Rgba32> positiveZ, Image<Rgba32> negativeZ, bool mipmap = true)
    {
        data = new Image<Rgba32>[6][];
        if (mipmap)
        {
            data[(int)ArrayLayerIdx.PositiveX] = MipmapHelper.GenerateMipmaps(positiveX);
            data[(int)ArrayLayerIdx.NegativeX] = MipmapHelper.GenerateMipmaps(negativeX);
            data[(int)ArrayLayerIdx.PositiveY] = MipmapHelper.GenerateMipmaps(positiveY);
            data[(int)ArrayLayerIdx.NegativeY] = MipmapHelper.GenerateMipmaps(negativeY);
            data[(int)ArrayLayerIdx.PositiveZ] = MipmapHelper.GenerateMipmaps(positiveZ);
            data[(int)ArrayLayerIdx.NegativeZ] = MipmapHelper.GenerateMipmaps(negativeZ);
            return;
        }

        data[(int)ArrayLayerIdx.PositiveX] = new Image<Rgba32>[1] { positiveX };
        data[(int)ArrayLayerIdx.NegativeX] = new Image<Rgba32>[1] { negativeX };
        data[(int)ArrayLayerIdx.PositiveY] = new Image<Rgba32>[1] { positiveY };
        data[(int)ArrayLayerIdx.NegativeY] = new Image<Rgba32>[1] { negativeY };
        data[(int)ArrayLayerIdx.PositiveZ] = new Image<Rgba32>[1] { positiveZ };
        data[(int)ArrayLayerIdx.NegativeZ] = new Image<Rgba32>[1] { negativeZ };
    }

    public override Veldrid.Texture ApplyChanges(GraphicsDevice gfxDevice, Veldrid.Texture texture, TextureMutationMethod method)
    {
        RecalculateMipMaps();
        UpdateDeviceTexture(gfxDevice,texture,method);
        return texture;
    }

    private void RecalculateMipMaps()
    {
        if (MipMapLevels < 1)
            return;//no mipmaps
        data[(int)ArrayLayerIdx.PositiveX] = MipmapHelper.GenerateMipmaps(data[(int)ArrayLayerIdx.PositiveX][0]);
        data[(int)ArrayLayerIdx.NegativeX] = MipmapHelper.GenerateMipmaps(data[(int)ArrayLayerIdx.PositiveX][0]);
        data[(int)ArrayLayerIdx.PositiveY] = MipmapHelper.GenerateMipmaps(data[(int)ArrayLayerIdx.PositiveX][0]);
        data[(int)ArrayLayerIdx.NegativeY] = MipmapHelper.GenerateMipmaps(data[(int)ArrayLayerIdx.PositiveX][0]);
        data[(int)ArrayLayerIdx.PositiveZ] = MipmapHelper.GenerateMipmaps(data[(int)ArrayLayerIdx.PositiveX][0]);
        data[(int)ArrayLayerIdx.NegativeZ] = MipmapHelper.GenerateMipmaps(data[(int)ArrayLayerIdx.PositiveX][0]);
    }

    protected override Veldrid.Texture CreateDeviceTextureViaStaging(GraphicsDevice gfxDevice)
        => WriteIntoViaStaging(gfxDevice, gfxDevice.ResourceFactory.CreateTexture(TextureDescription.Texture2D((uint)Width, (uint)Height, (uint)MipMapLevels, 1u, Format, TextureUsage.Sampled | TextureUsage.Cubemap)));

    protected override Veldrid.Texture CreateDeviceTextureViaUpdate(GraphicsDevice gfxDevice)
        => WriteIntoViaUpdate(gfxDevice, gfxDevice.ResourceFactory.CreateTexture(TextureDescription.Texture2D((uint)Width, (uint)Height, (uint)MipMapLevels, 1u, Format, TextureUsage.Sampled | TextureUsage.Cubemap)));

    protected override void DisposeInternal()
    {
        foreach(var elem in data)
            foreach(var mipmapLevel in elem)
                mipmapLevel.Dispose();
    }

    protected unsafe override Veldrid.Texture WriteIntoViaStaging(GraphicsDevice gfxDevice, Veldrid.Texture texture)
    {
        var staging = gfxDevice.ResourceFactory.CreateTexture(TextureDescription.Texture2D((uint)Width, (uint)Height, (uint)MipMapLevels, 1u, Format, TextureUsage.Staging | TextureUsage.Cubemap));
        staging = WriteIntoViaUpdate(gfxDevice, staging);
        var cl = gfxDevice.ResourceFactory.CreateCommandList();
        cl.Begin();
        cl.CopyTexture(staging, texture);
        cl.End();
        gfxDevice.SubmitCommands(cl);
        return texture;
    }

    protected unsafe override Veldrid.Texture WriteIntoViaUpdate(GraphicsDevice gfxDevice, Veldrid.Texture texture)
    {       
        Memory<Rgba32> memory = default;
        Memory<Rgba32> memory2 = default;
        Memory<Rgba32> memory3 = default;
        Memory<Rgba32> memory4 = default;
        Memory<Rgba32> memory5 = default;
        Memory<Rgba32> memory6 = default;
        for (int i = 0; i < MipMapLevels; i++)
        {
            var currImage = data[0][i];
            var width = (uint)currImage.Width;
            var height = (uint)currImage.Height;
            var sizeInBytes = width * height * FormatSizeInBytes(Format);

            if (!currImage.DangerousTryGetSinglePixelMemory(out memory))
            {
                throw new VeldridException("Unable to get positive x pixelspan.");
            }

            if (!data[1][i].DangerousTryGetSinglePixelMemory(out memory2))
            {
                throw new VeldridException("Unable to get negatve x pixelspan.");
            }

            if (!data[2][i].DangerousTryGetSinglePixelMemory(out memory3))
            {
                throw new VeldridException("Unable to get positive y pixelspan.");
            }

            if (!data[3][i].DangerousTryGetSinglePixelMemory(out memory4))
            {
                throw new VeldridException("Unable to get negatve y pixelspan.");
            }

            if (!data[4][i].DangerousTryGetSinglePixelMemory(out memory5))
            {
                throw new VeldridException("Unable to get positive z pixelspan.");
            }

            if (!data[5][i].DangerousTryGetSinglePixelMemory(out memory6))
            {
                throw new VeldridException("Unable to get negatve z pixelspan.");
            }

            fixed (Rgba32* ptr = &MemoryMarshal.GetReference(memory.Span))
            {
                fixed (Rgba32* ptr2 = &MemoryMarshal.GetReference(memory2.Span))
                {
                    fixed (Rgba32* ptr3 = &MemoryMarshal.GetReference(memory3.Span))
                    {
                        fixed (Rgba32* ptr4 = &MemoryMarshal.GetReference(memory4.Span))
                        {
                            fixed (Rgba32* ptr5 = &MemoryMarshal.GetReference(memory5.Span))
                            {
                                fixed (Rgba32* ptr6 = &MemoryMarshal.GetReference(memory6.Span))
                                {

                                    gfxDevice.UpdateTexture(texture, (IntPtr)ptr, sizeInBytes, 0u, 0u, 0u, width, height, 1u, (uint)i, 0u);
                                    gfxDevice.UpdateTexture(texture, (IntPtr)ptr2, sizeInBytes, 0u, 0u, 0u, width, height, 1u, (uint)i, 1u);
                                    gfxDevice.UpdateTexture(texture, (IntPtr)ptr3, sizeInBytes, 0u, 0u, 0u, width, height, 1u, (uint)i, 2u);
                                    gfxDevice.UpdateTexture(texture, (IntPtr)ptr4, sizeInBytes, 0u, 0u, 0u, width, height, 1u, (uint)i, 3u);
                                    gfxDevice.UpdateTexture(texture, (IntPtr)ptr5, sizeInBytes, 0u, 0u, 0u, width, height, 1u, (uint)i, 4u);
                                    gfxDevice.UpdateTexture(texture, (IntPtr)ptr6, sizeInBytes, 0u, 0u, 0u, width, height, 1u, (uint)i, 5u);
                                }
                            }
                        }
                    }
                }
            }
        }

        return texture;
    }
}

/// <summary>
/// basically a reimplementing of veldrid  image sharp texture
/// to use the newest version of image sharp to fix a problem with the image not being contigous in memory
/// </summary>
internal class TextureData2D : TextureData
{
    static readonly SixLabors.ImageSharp.Configuration loadConfig;
    static TextureData2D()
    {
        var config = Configuration.Default.Clone();
        config.PreferContiguousImageBuffers = true;
        loadConfig = config;
    }

    public PixelFormat Format { get; init; }
    public int Width => data[0].Width;
    public int Height => data[0].Height;
    public int MipLevels => data.Length;
    internal Image<Rgba32>[] data;

    public TextureData2D(string path, bool mipmap = true, bool srgb=false) 
    {
        Format = srgb ?  PixelFormat.R8_G8_B8_A8_UNorm_SRgb:  PixelFormat.R8_G8_B8_A8_UNorm;
        data = Load(Image.Load<Rgba32>(loadConfig, path), mipmap);
    }

    public TextureData2D(Stream stream, bool mipmap=true, bool srgb=false)
    {
        Format = srgb ? PixelFormat.R8_G8_B8_A8_UNorm_SRgb : PixelFormat.R8_G8_B8_A8_UNorm;
        data = Load(Image.Load<Rgba32>(loadConfig, stream), mipmap);
    }

    private Image<Rgba32>[] Load(Image<Rgba32> image, bool mipmap)
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
