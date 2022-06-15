using DefaultEcs;
using DefaultEcs.System;
using Kyvos.Input;
using Kyvos.Audio;
using Kyvos.Core.Assets;
using Kyvos.Core;
using System.Diagnostics;
using Kyvos.Core.Logging;

namespace FullyAutomatedGayLuxurySpaceCommunism
{
    public class PlaySoundTest : ISystem<float>
    {
        World world;
        ISoundEngine engine;
        SoundAsset shot;
        Key playKey;

        public bool IsEnabled { get; set; } = true;
        public PlaySoundTest(World w, Key k = Key.Space)
        {
            world = w;
            var app = w.Get<IApplication>();

            Debug.Assert(app.HasComponent<ISoundEngine>(), "No sound engine found");
            
            engine = app.GetComponent<ISoundEngine>()!;

            shot = new((AssetIdentifier)"simple_shot.wav");
            playKey = k;
        }
        public void Dispose()
        {}

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
