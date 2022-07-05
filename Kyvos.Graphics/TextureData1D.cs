using Veldrid;
using Kyvos.Maths;
using SixLabors.ImageSharp.PixelFormats;
using Kyvos.Assets;
using SixLabors.ImageSharp;
using Kyvos.Core.Logging;
using System.Runtime.InteropServices;

namespace Kyvos.Graphics;

public class TextureData1D : TextureData
{
    PixelFormat Format { get; init; }
    Image<Rgba32>[] data;

    public override uint Width => (uint)data[0].Width;
    public override uint Height => 1;

    public TextureData1D(AssetLocation location, bool mipmap, bool srgb)
    {
        Format = srgb ? PixelFormat.R8_G8_B8_A8_UNorm_SRgb : PixelFormat.R8_G8_B8_A8_UNorm;
        var image = Image.Load<Rgba32>(location.First);
        if (image.Height != 1)
            throw new InvalidOperationException("TextureData 1D can only be 1pixel high.");
        if (mipmap)
        {
            data = MipmapHelper.GenerateMipmaps(image);
        }
        else 
        {
            data = new Image<Rgba32>[] { image };
        }

    }

    public int MipMapLevels => data.Length;

    public void SetPixel(int x, Rgba32 value) 
        => data[0][x,0] = value;

    public void SetPixel(float u, Rgba32 value)
    {
        u = Mathf.Clamp01(u);
        var x = (int)(u * (Width-1));
        data[0][x,0] = value;
    }

    public void SetPixels(int x, Span<Rgba32> values) 
    {
        if (!data[0].DangerousTryGetSinglePixelMemory(out var memory))
            throw new NonContigousPixelMemoryException();

        if (values.Length > memory.Length) 
        {
            Log<TextureData1D>.Debug("Trunkating values from {LengthOriginal} to {LengthNew}", values.Length, memory.Length);
            values = values[..memory.Length];
        }

        values.CopyTo(memory.Span);
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

    protected unsafe override Veldrid.Texture WriteIntoViaUpdate(GraphicsDevice gfxDevice, Veldrid.Texture texture)
    {
        for (int mipLevel = 0; mipLevel < MipMapLevels; mipLevel++)
        {
            var mipData = data[mipLevel];

            if (!mipData.DangerousTryGetSinglePixelMemory(out var memory))
                throw new NonContigousPixelMemoryException();


            var size = Width*FormatSizeInBytes(Format);

            fixed (Rgba32* ptr = &MemoryMarshal.GetReference(memory.Span))
            {
                void* dataPtr = ptr;
                gfxDevice.UpdateTexture(texture, (IntPtr)dataPtr, size, 0u, 0u, 0u, Width, 1, depth: 1u, mipLevel: (uint)mipLevel, arrayLayer: 0u);
            }

        }
        return texture;
    }
}


