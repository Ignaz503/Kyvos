using DefaultEcs;
using System;

namespace Kyvos.ECS.Systems.Setup;

internal class WorldCoinfigureSystemAction : IWorldConfigureSystem
{
    Action<World> configureAction;
    public WorldCoinfigureSystemAction(Action<World> configureAction)
        => this.configureAction = configureAction ?? throw new ArgumentNullException(nameof(configureAction));

    public void Configure(World w)
        => configureAction(w);
}


