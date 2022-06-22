using DefaultEcs;
using DefaultEcs.System;
using Kyvos.Input;
using Kyvos.Audio;
using Kyvos.Assets;
using Kyvos.Core;
using System.Diagnostics;
using Kyvos.Core.Logging;
using System;

namespace FullyAutomatedGayLuxurySpaceCommunism
{
    public class PlaySoundTest : ISystem<float>
    {
        readonly World world;
        readonly ISoundEngine engine;
        
        readonly SoundAsset shot;
        readonly Key playKey;

        public bool IsEnabled { get; set; } = true;
        public PlaySoundTest(World w, Key k = Key.Space)
        {
            world = w;
            var app = w.Get<IApplication>();

            Debug.Assert(app.HasComponent<ISoundEngine>(), "No sound engine found");
            
            engine = app.GetComponent<ISoundEngine>()!;
            Debug.Assert(app.HasComponent<ISoundLoader>(), "No sound loader found");
            var soundLoader = app.GetComponent<ISoundLoader>()!;

            shot = soundLoader.Load((AssetIdentifier)"simple_shot");
            playKey = k;
        }
        public void Dispose()
        {
            shot.Dispose();
            GC.SuppressFinalize(this);
        }

        public void Update(float state)
        {
            ref var input = ref world.Get<MouseAndKeyboard>();
            if (input.IsDown(playKey)) 
            {
                engine.Play(shot);
                Log.Information("Played sound simple_shot.wav");
            }
        }
    }
}
