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

namespace Kyvos.Graphics;

public class Texture : IDisposable
{
    public enum DeviceTextureCreation
    {
        Staging,
        Update
    }
    
    public static DeviceTextureCreation CreationMethod = DeviceTextureCreation.Staging;

    //TODO don't use imagesharp texture and make your own thing
    ImageSharpTexture? data;
    Veldrid.Texture? deviceTexture;
    ReferenceCounter refCounter;
    bool isDisposed = false;

    private Texture() 
    {
        deviceTexture = null;
        refCounter = new();
    }

    public Texture(string path) :this()
        => data = new ImageSharpTexture(path);
    
    public Texture(string path, bool mipmap) : this()
        => data = new ImageSharpTexture(path, mipmap);
    
    public Texture(string path, bool mipmap, bool srgb) : this()
        => data = new ImageSharpTexture(path, mipmap, srgb);
    
    public Texture(Stream stream) : this()
        => data = new ImageSharpTexture(stream);
    
    public Texture(Stream stream, bool mipmap) : this()
       => data = new ImageSharpTexture(stream, mipmap);
    
    public Texture(Stream stream, bool mipmap, bool srgb) : this()
        => data = new ImageSharpTexture(stream, mipmap, srgb);

    private void CreateDeviceTexture(GraphicsDevice gfxDevice, DeviceTextureCreation method) 
    {
        //TODO create yourself and don't call create device texture but actually use staging or update
        switch (method) 
        {
            case DeviceTextureCreation.Staging:
                //TODO this also uses update
                deviceTexture = data?.CreateDeviceTexture(gfxDevice, gfxDevice.ResourceFactory);
                break;
            case DeviceTextureCreation.Update:
                deviceTexture = data?.CreateDeviceTexture(gfxDevice, gfxDevice.ResourceFactory);
                break;
        }
    }

    public TextureView GetTextureView(GraphicsDevice gfxDevice) 
    {
        if (deviceTexture is null)
            CreateDeviceTexture(gfxDevice, CreationMethod);
        refCounter.Increment();
        return gfxDevice.ResourceFactory.CreateTextureView(deviceTexture);
    }

    public void SetPixel( float u, float v, Rgba32 rgba)
        => data?.SetPixel(u, v, rgba);

    public void SetPixel( float u, float v, byte r, byte g, byte b, byte a = byte.MaxValue)
        => data?.SetPixel(u, v, r,g,b,a);
    public void SetPixel( float u, float v, float r, float g, float b, float a = 1f)
        => data?.SetPixel(u, v, r, g, b, a);
    public void SetPixel( float u, float v, Vector3 rgb)
        => data?.SetPixel(u, v, rgb);

    public void SetPixel( float u, float v, Vector4 rgba)
        => data?.SetPixel(u, v, rgba);

    public void SetPixel( float u, float v, uint pixel)
        => data?.SetPixel(u, v, pixel);

    public   void SetPixel( int x, int y, Rgba32 rgba)
        => data?.SetPixel(x, y, rgba);

    public void SetPixel( int x, int y, byte r, byte g, byte b, byte a = byte.MaxValue)
        => data?.SetPixel(x, y,r,g,b,a);
    public void SetPixel( int x, int y, float r, float g, float b, float a = 1f)
        => data?.SetPixel(x, y, r, g, b, a);
    public void SetPixel( int x, int y, Vector3 rgb)
        => data?.SetPixel(x, y, rgb);

    public void SetPixel( int x, int y, Vector4 rgba)
        => data?.SetPixel(x, y, rgba);

    public void SetPixel( int x, int y, uint pixel)
        => data?.SetPixel(x, y, pixel);

    //TODO maybe forward declare Rgba32 struct instead of direct sixlabor use
    public void SetPixels( Span<Rgba32> pixels, int x = 0, int y = 0)
        => data?.SetPixels(pixels, x, y);
    
    public void SetPixels( Span<float> pixels, int x = 0, int y = 0)
        => data?.SetPixels(pixels, x, y);

    public void SetPixels( Span<byte> pixels, int x = 0, int y = 0)
        => data?.SetPixels(pixels, x, y);


    public unsafe void ApplyChanges(GraphicsDevice gd, bool generateMipMaps = true)
    {
        if (data is null)
            return;
        if (deviceTexture is null)
            return;
        
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

        if (generateMipMaps) 
        {
            var cl = gd.ResourceFactory.CreateCommandList();
            cl.Begin();

            cl.GenerateMipmaps(deviceTexture);
            cl.End();
            gd.SubmitCommands(cl);

        } 
    }

    public void Dispose()
    {
        if (isDisposed)
            return;

        var cnt = refCounter.Decrement();

        if (cnt != 0)
            return;

        deviceTexture?.Dispose();
        data = null;
        isDisposed = true;
    }

}
