using DefaultEcs;
using DefaultEcs.System;
using Kyvos.Core;
using System;

namespace Kyvos.ECS.Systems;

public class FixedUpdateSystem : ISystem<float>
{
    bool isDisposed = false;
    ISystem<float> wrappedSystem;
    World world;

    public bool IsEnabled { get; set ; }

    public FixedUpdateSystem(ISystem<float> fixedUpdate, World world)
    {
        wrappedSystem = fixedUpdate ?? throw new ArgumentNullException(nameof(fixedUpdate));
        this.world = world ?? throw new ArgumentNullException(nameof(world));
        IsEnabled = true;
    }

    public void Dispose()
    {
        if (isDisposed)
            return;

        wrappedSystem.Dispose();

        isDisposed = true;
    }

    public void Update(float state)
    {
        if (!IsEnabled)
            return;

        var timer = world.Get<IApplication>().GetComponent<Core.Timer>()!;

        var numFixedUpdates = timer.NumberOfFixedUpdates;//TODO timer stuff
        for (int i = 0; i < numFixedUpdates; i++) 
        {
            wrappedSystem.Update(timer.FixedDeltaTime);
        }
    }
}
