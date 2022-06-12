using System;
using Veldrid;
using Veldrid.ImageSharp;
using Kyvos.Utility;
using System.IO;

namespace Kyvos.VeldridIntegration;

public class Texture : IDisposable
{

    public enum DeviceTextureCreation
    {
        Staging,
        Update
    }

    public static DeviceTextureCreation CreationMethod = DeviceTextureCreation.Staging;

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


