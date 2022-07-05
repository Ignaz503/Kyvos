using Kyvos.Assets;

namespace Kyvos.Graphics;

public class GenericTextureFactory : ITextureDataFactory<TextureData2D>, ITextureDataFactory<TextureData1D>, ITextureDataFactory<TextureDataCubeMap>
{
    public static GenericTextureFactory Instance { get; } = new();

    private GenericTextureFactory()
    {

    }

    public TextureData2D Create(AssetLocation location, bool mipmap = true, bool srgb = false)
    {
        return new TextureData2D(location, mipmap, srgb);
    }

    TextureData1D ITextureDataFactory<TextureData1D>.Create(AssetLocation location, bool mipmap, bool srgb)
    {
        return new TextureData1D(location, mipmap, srgb);
    }

    TextureDataCubeMap ITextureDataFactory<TextureDataCubeMap>.Create(AssetLocation location, bool mipmap, bool srgb)
    {
        return new TextureDataCubeMap(location, mipmap, srgb);
    }
}