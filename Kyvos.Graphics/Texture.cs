using System;
using Veldrid;
using Veldrid.ImageSharp;
using Kyvos.Utility;
using System.IO;
using SixLabors.ImageSharp.PixelFormats;
using Veldrid.Utilities;
using Veldrid.SPIRV;
using System.Numerics;
using Kyvos.Assets;
using Kyvos.Core.Logging;
using System.Diagnostics;

namespace Kyvos.Graphics;

public enum TextureMutationMethod
{
    Staging,
    Update
}

//TODO rename texture handle
//TOODO take TextureData, not textureData2D

public class TextureHandle<TTex> : IDisposable
    where TTex : TextureData
{
    internal static event Action<AssetIdentifier>? OnNoReference;

    public static TextureMutationMethod CreationMethod = TextureMutationMethod.Update;

    //TODO don't use imagesharp texture and make your own thing
    //ImageSharpTexture data;
    public TTex Data { get; init; }
    Veldrid.Texture? deviceTexture;

    public Veldrid.Texture Native
        => deviceTexture ?? throw new InvalidOperationException("Call CreateDeviceTexture first!");
    AssetIdentifier assetId;
    ReferenceCounter refCounter;
    bool isDisposed = false;
    object _lockObject;

    public uint Width => (uint)Data.Width;
    public uint Height => (uint)Data.Height;


#pragma warning disable CS8618       //data will alsways be set by public ctors
    private TextureHandle(AssetIdentifier id) 
    {
        this.assetId = id;
        deviceTexture = null;
        refCounter = new(0);
        _lockObject = new();
    }
#pragma warning restore

    internal TextureHandle(AssetIdentifier id, TTex data) : this(id)
        => this.Data = data;



    public void CreateDeviceTexture(GraphicsDevice gfxDevice, TextureMutationMethod method = TextureMutationMethod.Update) 
    {
        lock(_lockObject)
        {
            if (deviceTexture is not null)
                return;

            deviceTexture = Data.CreateDeviceTexture(gfxDevice, method);
        }


        Log<TextureHandle<TTex>>.Debug("W: {Width} H:{Height} Mips: {Mip}", deviceTexture!.Width, deviceTexture.Height, deviceTexture.MipLevels);
        Log<TextureHandle<TTex>>.Debug("Format: {Form} Usage: {Usg} Layers: {ArrayLayers}", deviceTexture.Format, deviceTexture.Usage, deviceTexture.ArrayLayers);

    }

    public TextureView GetTextureView(GraphicsDevice gfxDevice) 
    {
        if (deviceTexture is null)
            CreateDeviceTexture(gfxDevice, CreationMethod);

        refCounter.Increment();

        
        return gfxDevice.ResourceFactory.CreateTextureView(deviceTexture);
    }

    public unsafe void ApplyChanges(GraphicsDevice gd, bool generateMipMaps = true)
    {
        if (deviceTexture is null)
            return;

        deviceTexture = Data.ApplyChanges(gd, deviceTexture, CreationMethod);
    }

    internal void DisposeInternal()
    {
        if (isDisposed)
            return;

        Log.Debug("Disposing Texture {Obj}", assetId);
        deviceTexture?.Dispose();
        Data?.Dispose();
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
