using DefaultEcs;
using Veldrid;
using DefaultEcs.System;
using Kyvos.Graphics.Materials;
using Kyvos.Input;
using Kyvos.VeldridIntegration;
using Kyvos.Core;
using Kyvos.Core.Configuration;
using Kyvos.Core.Logging;

namespace FullyAutomatedGayLuxurySpaceCommunism
{
    [With(typeof(Material))]
    public class RandomColorSystem : AEntitySetSystem<float> 
    {
        GraphicsDevice gfxDevice;

        public RandomColorSystem(World world) : base(world)
        {
            this.gfxDevice = World.Get<GraphicsDeviceHandle>().GfxDevice;
        }
        protected override void Update(float state, in Entity entity)
        {
            var inp = World.Get<MouseAndKeyboard>();
            if (inp.IsDown(Kyvos.Input.Key.Space)) 
            {
                ref var mat = ref entity.Get<Material>();
                var col = Kyvos.Maths.Random.RandomVecOnHyperShpere;
                mat.Update("ColorBuffer", ref col , gfxDevice);

                //Log.Information(World.Get<IApplication>()!.GetComponent<IConfig>()!.ToString() ?? "no config");

            }
        }
    }
}
