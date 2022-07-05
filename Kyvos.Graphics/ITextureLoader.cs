using Kyvos.Assets;

namespace Kyvos.Graphics;

public interface ITextureLoader : IDisposable
{
    TextureHandle<T> Load<T>(AssetIdentifier id, bool mipmap = true, bool srgb=false) where T : TextureData;
}

public interface ITextureLoader<T> : ITextureLoader
    where T : TextureData
{
    static Type type = typeof(T);
    TextureHandle<K> ITextureLoader.Load<K>(AssetIdentifier id, bool mipmap, bool srgb)
    {
        if (!typeof(K).Equals(type))
            throw new InvalidOperationException($"{typeof(K)} needs to be same type as {typeof(T)}");
        return (Load(id, mipmap,srgb) as TextureHandle<K>)!;
    }

    TextureHandle<T> Load(AssetIdentifier id, bool mipmap = true, bool srgb= false);
}
