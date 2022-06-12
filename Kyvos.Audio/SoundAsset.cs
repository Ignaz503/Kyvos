//https://markheath.net/post/fire-and-forget-audio-playback-with

using Kyvos.Core.Assets;
using Kyvos.Core;
using NAudio.Wave;

namespace Kyvos.Audio;

public class SoundAsset 
{
    float[] data;
    WaveFormat waveFormat;

    public SoundAsset(AssetIdentifier identifier)
    {
        using var audioFileReader = new AudioFileReader(FileSystem.GetPathToAsset(identifier));

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

    public class Sampler : ISampleProvider
    {
        SoundAsset asset;
        long position;
        public Sampler(SoundAsset asset)
        {
            this.asset = asset;
            position = 0;
        }

        public Sampler(SoundAsset asset, long position)
        {
            this.asset = asset;
            this.position = position;
        }

        public WaveFormat WaveFormat => asset.waveFormat;

        public int Read(float[] buffer, int offset, int count)
        {
            var amountToCopy = Math.Min(count, asset.data.Length-position); //don't try to copy more than is actually left in the audio file from this position

            Array.Copy(asset.data, position,buffer, offset, amountToCopy);

            position += amountToCopy;
            return (int)amountToCopy;
        }
    }

}
