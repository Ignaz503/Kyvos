using Kyvos.Assets;
using Veldrid;

namespace Kyvos.Graphics;

public abstract class TextureData : IDisposable
{
    protected bool isDisposed = false;

    public abstract uint Width { get; }
    public abstract uint Height { get; }

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
