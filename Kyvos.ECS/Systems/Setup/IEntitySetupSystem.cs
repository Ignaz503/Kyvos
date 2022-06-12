using Kyvos.ECS.Entities;

namespace Kyvos.ECS.Systems.Setup;

public interface IEntitySetupSystem
{
    void Run(EntityCommands commands);
}


