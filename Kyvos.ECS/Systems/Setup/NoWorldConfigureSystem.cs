using DefaultEcs;

namespace Kyvos.ECS.Systems.Setup;

internal sealed class NoWorldConfigureSystem : IWorldConfigureSystem
{
    internal static readonly NoWorldConfigureSystem Instance = new();

    private NoWorldConfigureSystem() { }

    public void Configure(World w)
    {}
}

