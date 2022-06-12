using DefaultEcs;
using System;

namespace Kyvos.ECS.Resources;

public struct ManagerInfo 
{
    public IDisposable ManagerInstance { get; init; }
    public Func<World, ManagmentContract> ContractEstablisher { get; init; }

    public ManagerInfo(IDisposable manager, Func<World, ManagmentContract> contractEstablisher)
    {
        ManagerInstance = manager;
        ContractEstablisher = contractEstablisher;
    }
}

