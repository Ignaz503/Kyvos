using System;

using NAudio.Wave;

namespace Kyvos.Audio;

public interface ISoundEngine : IDisposable 
{
    void Play(SoundAsset sample);

    void Play(ISampleProvider sample);
}
