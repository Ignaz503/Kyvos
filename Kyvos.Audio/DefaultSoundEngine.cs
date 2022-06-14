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


        mixer.ReadFully = true;
        output.Init(mixer);
        output.Play();
    }

    public void Play(ISampleProvider sample)
    {
        AddSampleToMixer(sample);
    }

    public void Play(SoundAsset sample)
    {
        var sampler = new SoundAsset.Sampler(sample);

        AddSampleToMixer(sampler);
    }

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
