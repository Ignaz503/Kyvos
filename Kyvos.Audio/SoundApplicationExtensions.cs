//https://markheath.net/post/fire-and-forget-audio-playback-with

using Kyvos.Core;

namespace Kyvos.Audio;

public static class SoundApplicationExtensions
{
    public static IModifyableApplication With(this IModifyableApplication app, ISoundEngine soundEngine)
    {
        app.AddComponent(soundEngine);
        return app;
    }

    public static IModifyableApplication WithSoundEngine(this IModifyableApplication app)
    {
        app.EnsureExistence<ISoundEngine>(new DefaultSoundEngine());
        return app;
    }
}
