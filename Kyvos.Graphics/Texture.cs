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

namespace Kyvos.Graphics;

public class Texture : IDisposable
{
    internal static event Action<AssetIdentifier>? OnNoReference;

    public enum DeviceTextureCreation
    {
        Staging,
        Update
    }
    
    public static DeviceTextureCreation CreationMethod = DeviceTextureCreation.Update;

    //TODO don't use imagesharp texture and make your own thing
    ImageSharpTexture data;
    Veldrid.Texture? deviceTexture;

    public Veldrid.Texture Native
        => deviceTexture ?? throw new InvalidOperationException("Call CreateDeviceTexture first!");
    AssetIdentifier assetId;
    ReferenceCounter refCounter;
    bool isDisposed = false;
    object _lockObject;

    public uint Width => data.Width;
    public uint Height => data.Height ;

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
        => data = new ImageSharpTexture(path);
    
    public Texture(AssetIdentifier id, string path, bool mipmap) : this(id)
        => data = new ImageSharpTexture(path, mipmap);
    
    public Texture(AssetIdentifier id, string path, bool mipmap, bool srgb) : this(id)
        => data = new ImageSharpTexture(path, mipmap, srgb);
    
    public Texture(AssetIdentifier id, Stream stream) : this(id)
        => data = new ImageSharpTexture(stream);
    
    public Texture(AssetIdentifier id, Stream stream, bool mipmap) : this(id)
       => data = new ImageSharpTexture(stream, mipmap);
    
    public Texture(AssetIdentifier id, Stream stream, bool mipmap, bool srgb) : this(id)
        => data = new ImageSharpTexture(stream, mipmap, srgb);

    public void CreateDeviceTexture(GraphicsDevice gfxDevice, DeviceTextureCreation method = DeviceTextureCreation.Update) 
    {
        lock(_lockObject)
        {
            if (deviceTexture is not null)
                return;

            //TODO create yourself and don't call create device texture but actually use staging or update
            switch (method)
            {
                case DeviceTextureCreation.Staging:
                    //TODO this also uses update
                    deviceTexture = data.CreateDeviceTexture(gfxDevice, gfxDevice.ResourceFactory);
                    break;
                case DeviceTextureCreation.Update:
                    deviceTexture = data.CreateDeviceTexture(gfxDevice, gfxDevice.ResourceFactory);
                    break;
            }
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

        if (data.MipLevels > 1) 
        {
            
            //calculate new mip levels
            MipmapHelper.GenerateMipmaps(data.Images);
        }

        for (int i = 0; i < data.MipLevels; i++)
        {
            Image<Rgba32> image = data.Images[i];
            if (!image.TryGetSinglePixelSpan(out var span))
            {
                throw new TextureUpdateException("Unable to get image pixelspan.");
            }

            fixed (Rgba32* ptr = &MemoryMarshal.GetReference(span))
            {
                void* ptr2 = ptr;
                gd.UpdateTexture(deviceTexture, (IntPtr)ptr2, (uint)(data.PixelSizeInBytes * image.Width * image.Height), 0u, 0u, 0u, (uint)image.Width, (uint)image.Height, 1u, (uint)i, 0u);
            }
        }

        //if (generateMipMaps) 
        //{
        //    var cl = gd.ResourceFactory.CreateCommandList();
        //    cl.Begin();

        //    cl.GenerateMipmaps(deviceTexture);
        //    cl.End();
        //    gd.SubmitCommands(cl);

        //} 
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
