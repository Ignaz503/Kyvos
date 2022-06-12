using DefaultEcs;
using Kyvos.Utility.Collections;

namespace Kyvos.ECS.Components.Management;

public class ComponentManager : IComponentManager
{
    public static readonly ComponentManager Default = new ComponentManager();

    //todo instead of world dictionary
    //do same as DefualtECS static world array
    // index by worldID (also fork git project and make it public)
    Dictionary<World, IDisposable> worlds = new();
    List<IComponentManagementContractEstablisher> contractEstablishers = new();
    bool isDisposed = false;

    public ComponentManager()
    {}

    public void Dispose()
    {
        if (isDisposed)
            return;

        foreach(var contract in worlds.Values)
            contract.Dispose();

        isDisposed = true;
    }

    public void AddContractEstablisher(IComponentManagementContractEstablisher toAdd) 
    {
        contractEstablishers.Add(toAdd);
    }

    public void SeutpManagement(World w) 
    {
        if (worlds.ContainsKey(w))
            return;
        worlds.Add(w,GetContracts(w));

        w.SubscribeWorldDisposed(OnWorldDisposed);
    }

    IDisposable GetContracts(World w) 
    {
        DisposableList<IDisposable> contracts = new(contractEstablishers.Count);
        foreach (var contractEstablisher in contractEstablishers)
            contracts.Add(contractEstablisher.Establish(w));
        return contracts;
    }

    void OnWorldDisposed(World w) 
    {
        if (!worlds.ContainsKey(w))
            return;

        worlds[w].Dispose();
        worlds.Remove(w);
    }
}


