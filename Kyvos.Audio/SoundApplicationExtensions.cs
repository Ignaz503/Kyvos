//https://markheath.net/post/fire-and-forget-audio-playback-with

using Kyvos.Core;

namespace Kyvos.Audio;

public static class SoundApplicationExtensions
{
    public static IModifyableApplication WithSoundEngine(this IModifyableApplication app, ISoundEngine soundEngine)
    {
        app.AddComponent(soundEngine);
        return app;
    }

    public static IModifyableApplication WithSoundEngine(this IModifyableApplication app)
    {
        app.EnsureExistence<ISoundEngine>((application)=>new DefaultSoundEngine());
        return app;
    }

    public static IModifyableApplication WithSoundLoader(this IModifyableApplication app) 
    {
        app.EnsureExistence<ISoundLoader>((application)=>new SoundLoader(application));
        return app;
    }
    public static IModifyableApplication WithSoundLoader(this IModifyableApplication app, ISoundLoader loader)
    {
        app.EnsureExistence<ISoundLoader>(loader);
        return app;
    }

    public static IModifyableApplication WithAudio(this IModifyableApplication app) 
        => app.WithSoundEngine().WithSoundLoader();

    public static IModifyableApplication WithAudio(this IModifyableApplication app, ISoundEngine engine, ISoundLoader soundLoader)
    => app.WithSoundEngine(engine).WithSoundLoader(soundLoader);

}
