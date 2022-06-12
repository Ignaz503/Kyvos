using DefaultEcs;
using Kyvos.ECS.Entities;
using System;
using System.Collections.Generic;

namespace Kyvos.ECS.Systems.Setup;

internal class EntitySetupSystems : List<IEntitySetupSystem>
{
    public void Execute(World w)
    {
        //system building of what can be done in parallell etc
        foreach (var system in this)
        {
            var commands = new EntityCommands(w);
            system.Run(commands);
            commands.Execute();
        }
    }

}


