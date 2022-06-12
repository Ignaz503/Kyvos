using DefaultEcs;
using DefaultEcs.System;
using System;

namespace Kyvos.ECS.Systems.Setup;

internal class UpdateSystemBuilderFunction : ISystemBuilder
{
    Func<World, ISystem<float>> factory;

    public UpdateSystemBuilderFunction(Func<World, ISystem<float>> factory)
    {
        this.factory = factory ?? throw new ArgumentNullException(nameof(factory));
    }

    public ISystem<float> Build(World w)
        => factory(w);
}


