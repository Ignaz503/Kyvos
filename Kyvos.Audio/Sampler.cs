//https://markheath.net/post/fire-and-forget-audio-playback-with

using NAudio.Wave;
using Microsoft.Extensions.ObjectPool;

namespace Kyvos.Audio;

internal class Sampler : ISampleProvider
{
    SoundAsset? asset;
    public SoundAsset? Asset => asset;
    long position;
    private Sampler(SoundAsset asset)
    {
        this.asset = asset;
        position = 0;
    }

    private Sampler(SoundAsset asset, long position)
    {
        this.asset = asset;
        this.position = position;
    }

    private Sampler()
    {

    }


    public WaveFormat WaveFormat => asset!.waveFormat;

    public int Read(float[] buffer, int offset, int count)
    {
        if (asset is null)
            return 0;

        var amountToCopy = Math.Min(count, asset.data.Length - position); //don't try to copy more than is actually left in the audio file from this position

        Array.Copy(asset.data, position, buffer, offset, amountToCopy);

        position += amountToCopy;
        return (int)amountToCopy;
    }


    static ObjectPool<Sampler> pool = new DefaultObjectPool<Sampler>(PoolPolicy.Instance,255);
    static object _lockObject = new();

    public static Sampler Get(SoundAsset asset) 
    {
        Sampler sampler;
        lock (_lockObject) 
        {
            sampler = pool.Get();
        }
        sampler.asset = asset;
        return sampler;
    }


    public static Sampler Get(SoundAsset asset, long position)
    {
        Sampler sampler;
        lock (_lockObject)
        {
            sampler = pool.Get();
        }
        sampler.asset = asset;
        sampler.position = position;
        return sampler;
    }

    public static void Return(Sampler sampler) 
    {
        lock (_lockObject)
            pool.Return(sampler);
    }

    class PoolPolicy : IPooledObjectPolicy<Sampler>
    {
        public static readonly PoolPolicy Instance = new();

        private PoolPolicy()
        {

        }

        public Sampler Create()
        {
            return new Sampler();
        }

        public bool Return(Sampler obj)
        {
            obj.position = 0;
            obj.asset = null;
            return true;
        }
    }

}
