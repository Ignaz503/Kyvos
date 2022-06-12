using DefaultEcs.Resource;
using Kyvos.Core;

namespace Kyvos.ECS.Resources;

public interface IResourceManagerFactory<TManager, TInfo, TResouce>
        where TManager : AResourceManager<TInfo, TResouce>
{
    TManager Create(IApplication application);
}


