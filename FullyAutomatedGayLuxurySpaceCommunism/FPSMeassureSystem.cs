using DefaultEcs;
using DefaultEcs.System;
using Kyvos.Core;
using Veldrid.StartupUtilities;
using System.Diagnostics;
using Kyvos.Core.Configuration;
using Kyvos.VeldridIntegration;

namespace FullyAutomatedGayLuxurySpaceCommunism
{
    public class FPSMeassureSystem : ISystem<float>
    {
        public bool IsEnabled { get; set; }

        World world;
        Window window;

        string baseTitle;

        public FPSMeassureSystem(World world)
        {
            this.world = world;
            var app = world.Get<IApplication>();
            Debug.Assert(app.HasComponent<IConfig>(), "Application has no config component");
            this.baseTitle = app.GetComponent<IConfig>()!.ReadValue<WindowCreateInfo>(Window.CONFIG_KEY).WindowTitle;
            Debug.Assert(app.HasComponent<Window>(), "Application has no window component");
            this.window =app.GetComponent<Window>()!;
            IsEnabled = true;
        }

        public void Dispose()
        { }

        public void Update(float deltaTime)
        {
            if (!IsEnabled)
                return;



            window.Title = $"{baseTitle} ({(1.0f / deltaTime):F2}FPS)";

        }
    }

}
