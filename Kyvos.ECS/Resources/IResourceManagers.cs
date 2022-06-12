using DefaultEcs;
using DefaultEcs.Resource;

namespace Kyvos.ECS.Resources;
public interface IResourceManagers
{
    ManagmentContract Manage<TManager, TInfo, TResource>(World world) where TManager : AResourceManager<TInfo, TResource>;
    void Add<TManager, TInfo, TResource>(TManager manager) where TManager : AResourceManager<TInfo, TResource>;

    ManagmentContract[] Manage(World world, IResourceManagmentOptOuts managmentOptOuts);
}
