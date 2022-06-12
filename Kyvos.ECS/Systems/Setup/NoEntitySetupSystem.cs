using Kyvos.ECS.Entities;

namespace Kyvos.ECS.Systems.Setup;

public sealed class NoEntitySetupSystem : IEntitySetupSystem
{
    internal static readonly NoEntitySetupSystem Instance = new();

    private NoEntitySetupSystem() { }

    public void Run(EntityCommands commands)
    {
    }
}