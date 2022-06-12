using DefaultEcs.System;
using Kyvos.Core;
using Kyvos.VeldridIntegration;
using System.Diagnostics;

namespace Kyvos.Input;

public class MultiInputSystem : ISystem<float>
{
    ISystem<InputSystemContext> mainSystem;
    Window window;

    public MultiInputSystem(InputSystemProvider inpSystemProvider, IApplication application)
    {
        //TODO maybe a window shouldn't be reqiured lol but that would mean re implementing the window.process events function
        //for input and create a system for window updates
        Debug.Assert(application.HasComponent<Window>(),"Window is required for input system");
        window = application.GetComponent<Window>()!;
        mainSystem = inpSystemProvider.GetSystem();
        IsEnabled = true;
    }

    public bool IsEnabled { get; set; }

    public void Dispose()
    {
        mainSystem.Dispose();
    }

    public void Update(float state)
    {
        if (!IsEnabled)
            return;

        var snapshot = window.PumpEvents();
        mainSystem.Update(new InputSystemContext { DeltaTime = state, InputSnapshot = snapshot });
    }
}


