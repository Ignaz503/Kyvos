using Kyvos.ECS.Entities;
using System;

namespace Kyvos.ECS.Systems.Setup;

internal class EntitySetupSystemAction : IEntitySetupSystem
{
    Action<EntityCommands> action;

    public EntitySetupSystemAction(Action<EntityCommands> action)
    {
        this.action = action ?? throw new ArgumentNullException(nameof(action));
    }

    public void Run(EntityCommands commands)
        => action(commands);
}


