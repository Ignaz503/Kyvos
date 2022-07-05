using Kyvos.Assets;

namespace Kyvos.Graphics;

public interface ITextureDataFactory<TTex>
{
    TTex Create(AssetLocation location, bool mipmap = true, bool srgb = false);
}
