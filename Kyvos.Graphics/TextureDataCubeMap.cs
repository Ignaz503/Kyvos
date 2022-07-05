using Veldrid;
using SixLabors.ImageSharp.PixelFormats;
using System.Runtime.InteropServices;
using SixLabors.ImageSharp;
using Kyvos.Assets;

namespace Kyvos.Graphics;

public class TextureDataCubeMap : TextureData
{
    public enum MapDirection 
    {
        PositiveX = 0,
        NegativeX = 1,
        PositiveY = 2,
        NegativeY = 3,
        PositiveZ = 4,
        NegativeZ = 5
    }

    public Image<Rgba32>[][] data { get; }

    public override uint Width => (uint)data[0][0].Width;
    public override uint Height => (uint)data[0][0].Height;
    public PixelFormat Format;
    public int MipMapLevels => data[0].Length;

    public TextureDataCubeMap(AssetLocation location, bool mipmap, bool srgb)
    {
        Image<Rgba32> positiveX = Image.Load<Rgba32>(location[(int)MapDirection.PositiveX]);
        Image<Rgba32> negativeX = Image.Load<Rgba32>(location[(int)MapDirection.NegativeX]);
        Image<Rgba32> positiveY = Image.Load<Rgba32>(location[(int)MapDirection.PositiveY]);
        Image<Rgba32> negativeY = Image.Load<Rgba32>(location[(int)MapDirection.NegativeY]);
        Image<Rgba32> positiveZ = Image.Load<Rgba32>(location[(int)MapDirection.PositiveZ]);
        Image<Rgba32> negativeZ = Image.Load<Rgba32>(location[(int)MapDirection.NegativeZ]);

        Format = srgb ? PixelFormat.R8_G8_B8_A8_UNorm_SRgb : PixelFormat.R8_G8_B8_A8_UNorm;
        data = new Image<Rgba32>[6][];
        if (mipmap)
        {
            data[(int)MapDirection.PositiveX] = MipmapHelper.GenerateMipmaps(positiveX);
            data[(int)MapDirection.NegativeX] = MipmapHelper.GenerateMipmaps(negativeX);
            data[(int)MapDirection.PositiveY] = MipmapHelper.GenerateMipmaps(positiveY);
            data[(int)MapDirection.NegativeY] = MipmapHelper.GenerateMipmaps(negativeY);
            data[(int)MapDirection.PositiveZ] = MipmapHelper.GenerateMipmaps(positiveZ);
            data[(int)MapDirection.NegativeZ] = MipmapHelper.GenerateMipmaps(negativeZ);
            return;
        }

        data[(int)MapDirection.PositiveX] = new Image<Rgba32>[1] { positiveX };
        data[(int)MapDirection.NegativeX] = new Image<Rgba32>[1] { negativeX };
        data[(int)MapDirection.PositiveY] = new Image<Rgba32>[1] { positiveY };
        data[(int)MapDirection.NegativeY] = new Image<Rgba32>[1] { negativeY };
        data[(int)MapDirection.PositiveZ] = new Image<Rgba32>[1] { positiveZ };
        data[(int)MapDirection.NegativeZ] = new Image<Rgba32>[1] { negativeZ };

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
        data[(int)MapDirection.PositiveX] = MipmapHelper.GenerateMipmaps(data[(int)MapDirection.PositiveX][0]);
        data[(int)MapDirection.NegativeX] = MipmapHelper.GenerateMipmaps(data[(int)MapDirection.PositiveX][0]);
        data[(int)MapDirection.PositiveY] = MipmapHelper.GenerateMipmaps(data[(int)MapDirection.PositiveX][0]);
        data[(int)MapDirection.NegativeY] = MipmapHelper.GenerateMipmaps(data[(int)MapDirection.PositiveX][0]);
        data[(int)MapDirection.PositiveZ] = MipmapHelper.GenerateMipmaps(data[(int)MapDirection.PositiveX][0]);
        data[(int)MapDirection.NegativeZ] = MipmapHelper.GenerateMipmaps(data[(int)MapDirection.PositiveX][0]);
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
