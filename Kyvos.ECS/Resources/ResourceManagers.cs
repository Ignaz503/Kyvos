using DefaultEcs;
using DefaultEcs.Resource;
using System;
using System.Collections.Generic;

namespace Kyvos.ECS.Resources;

public class ResourceManagers : IResourceManagers, IDisposable
{
    public static readonly ResourceManagers Default = new ResourceManagers();

    bool isDisposed = false;
    Dictionary<Type, ManagerInfo> managers;

    public ResourceManagers(IEnumerable<ManagerInfo> entries)
    {
        managers = new(Expand(entries));
        IEnumerable<KeyValuePair<Type, ManagerInfo>> Expand(IEnumerable<ManagerInfo> list)
        {
            foreach (var item in list)
                yield return new KeyValuePair<Type, ManagerInfo>(item.ManagerInstance.GetType(),item);
        }
    }

    public ResourceManagers()
    {
        managers = new();
    }

    public ManagmentContract Manage<TManager, TInfo, TResource>(World world)
        where TManager : AResourceManager<TInfo, TResource>
    {
        var t = typeof(TManager);
        if (!managers.ContainsKey(t))
            throw new UnkownResourceManagerException(t);

        var eventDisposables = (managers[t] as TManager)?.Manage(world);

        return eventDisposables is not null ? new(eventDisposables) : ManagmentContract.Empty;
    }

    public void Dispose()
    {
        if (isDisposed)
            return;

        isDisposed = true;
        foreach (var manager in managers)
            manager.Value.ManagerInstance.Dispose();
    }

    public void Add<TManager, TInfo, TResource>(TManager manager) where TManager : AResourceManager<TInfo, TResource>
    {
        managers.Add(typeof(TManager), new ManagerInfo(manager, (w) => new(manager.Manage(w))));
    }

    public ManagmentContract[] Manage(World world, IResourceManagmentOptOuts managmentOptOuts)
    {
        ManagmentContract[] contracts = new ManagmentContract[managers.Count - managmentOptOuts.Count];

        int i = 0;
        foreach (var managerEntry in managers) 
        {
            if (managmentOptOuts.HasOptedOut(managerEntry.Key))
                continue; //opted out

            contracts[i++] = managerEntry.Value.ContractEstablisher(world);
        }

        return contracts;
    }
}

