using DefaultEcs;
using DefaultEcs.System;

namespace Kyvos.ECS.Systems.Setup;

public interface ISystemBuilder
{
    ISystem<float> Build(World w);
}
