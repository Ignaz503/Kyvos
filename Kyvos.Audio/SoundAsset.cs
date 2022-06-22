//https://markheath.net/post/fire-and-forget-audio-playback-with

using Kyvos.Assets;
using Kyvos.Core;
using Kyvos.Core.Logging;
using Kyvos.Utility;
using NAudio.Wave;

namespace Kyvos.Audio;

public class SoundAsset : IDisposable
{
    internal static event Action<AssetIdentifier>? OnNoReference;

    AssetIdentifier id;
    public AssetIdentifier ID => id;
    ReferenceCounter refCounter;
    bool isCleanup = false;
    internal float[] data;
    internal WaveFormat waveFormat;

    internal SoundAsset(AssetIdentifier identifier, string path)
    {
        id= identifier;
        refCounter = new(0);
        using var audioFileReader = new AudioFileReader(path);

        waveFormat = audioFileReader.WaveFormat;

        var readFile = new List<float>((int)(audioFileReader.Length / 4));

        var buffer = new float[audioFileReader.WaveFormat.SampleRate * audioFileReader.WaveFormat.Channels];

        int samplesRead;
        while ((samplesRead = audioFileReader.Read(buffer, 0, buffer.Length)) > 0)
        {
            readFile.AddRange(buffer.Take(samplesRead));
        }
        data = readFile.ToArray();
    }

    internal void Reference()
        => refCounter.Increment();

    internal void DisposeInternal()
    {
        if (isCleanup)
            return;

        Log<SoundAsset>.Debug("unloading {Id}", id);

        isCleanup = true;
    }

    public void Dispose()
    {
        var c = refCounter.Decrement();
        if (c > 0)
            return;
        OnNoReference?.Invoke(id);
    }
}
