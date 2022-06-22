using Kyvos.Core.Logging;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;

namespace Kyvos.Audio;

internal class DefaultSoundEngine : ISoundEngine
{ 

    readonly IWavePlayer output;
    MixingSampleProvider mixer;
    bool isDisposed = false;

    public DefaultSoundEngine(int sampleRate = 48000, int channelCount = 2)
    {
        this.output = new WaveOutEvent();
        mixer = new MixingSampleProvider(WaveFormat.CreateIeeeFloatWaveFormat(sampleRate, channelCount));

        mixer.MixerInputEnded += OnMixerInputEnded;
        mixer.ReadFully = true;
        output.Init(mixer);
        output.Play();
    }

    void OnMixerInputEnded(object? sender, SampleProviderEventArgs args) 
    {
        if (args.SampleProvider is Sampler sampler) 
        {
            Log<DefaultSoundEngine>.Debug("Finisehd playing {AssetId}", sampler.Asset?.ID ?? "undefined");
            Sampler.Return(sampler);
        }
    }

    public void Play(ISampleProvider sample) 
        => AddSampleToMixer(sample);

    public void Play(SoundAsset sample) 
        => AddSampleToMixer(Sampler.Get(sample));

    ISampleProvider ConvertToCorrectSampleCount(ISampleProvider sample) 
    {
        if (sample.WaveFormat.Channels == mixer.WaveFormat.Channels)
            return sample;

        if (sample.WaveFormat.Channels == 1 && mixer.WaveFormat.Channels == 2)
            return new MonoToStereoSampleProvider(sample);

        if (sample.WaveFormat.Channels == 2 && mixer.WaveFormat.Channels == 1)
            return new StereoToMonoSampleProvider(sample);

        throw new NotImplementedException($"Unkown channel conversion: From: {sample.WaveFormat.Channels}, To: {mixer.WaveFormat.Channels}");
    }

    void AddSampleToMixer(ISampleProvider sample) 
    {
        mixer.AddMixerInput(ConvertToCorrectSampleCount(sample));
    }

    public void Dispose()
    {
        if (isDisposed)
            return;

        Log<ISoundEngine>.Information("Disposing Audio Engine");

        output.Dispose();

        isDisposed = true;
    }

}
