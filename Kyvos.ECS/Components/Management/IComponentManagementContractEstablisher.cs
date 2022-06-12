using DefaultEcs;
using System;

namespace Kyvos.ECS.Components.Management;
public interface IComponentManagementContractEstablisher
{
    IDisposable Establish(World w);
}

public class ComponentContractEstablishingFunction : IComponentManagementContractEstablisher
{
    Func<World, IDisposable> establishFactory;

    public ComponentContractEstablishingFunction(Func<World, IDisposable> establishFactory)
    {
        this.establishFactory = establishFactory ?? throw new ArgumentNullException(nameof(establishFactory));
    }

    public IDisposable Establish(World w)
        => establishFactory(w);
}
