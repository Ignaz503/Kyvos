using DefaultEcs;
using DefaultEcs.System;
using Kyvos.Core;
using Kyvos.Core.Logging;
using Kyvos.Input;
using Kyvos.VeldridIntegration;
using System.Diagnostics;
using Veldrid.Sdl2;

namespace FullyAutomatedGayLuxurySpaceCommunism
{
    public class ToggleMouseGrab : ISystem<float>
    {
        public bool IsEnabled { get; set; } = true;

        public bool isGrabbed;
        public Key toggleKey;
        World world;
        Window window;

        public ToggleMouseGrab(World world, Key toggleKey = Key.Escape, bool initialState = true)
        {
            isGrabbed = initialState;
            this.toggleKey = toggleKey;
            this.world = world;
            Debug.Assert(world.Get<IApplication>().HasComponent<Window>(), "Window component is missing");
            window = world.Get<IApplication>().GetComponent<Window>()!;
            window.RealtiveMouseMode(isGrabbed);
        }

        public void Dispose()
        {}

        public void Update(float state)
        {
            ref var input = ref world.Get<MouseAndKeyboard>();
            if (input.IsDown(toggleKey)) 
            {
                isGrabbed = !isGrabbed;
                Log<ToggleMouseGrab>.Debug("Toggled mouse grab to {isGrabbed}", isGrabbed);
                window.RealtiveMouseMode(isGrabbed);
            }
        }
    }
}
