using DefaultEcs;
using System.Collections.Generic;

namespace Kyvos.ECS.Systems.Setup;

internal class WorldConfigureSystems : List<IWorldConfigureSystem>, IWorldConfigureSystem
{
    public void Configure(World w)
    {
        foreach (var configureDescription in this)
            configureDescription.Configure(w);
    }
}
