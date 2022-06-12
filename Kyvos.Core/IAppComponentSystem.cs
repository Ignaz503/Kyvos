namespace Kyvos.Core;

public interface IAppComponentSystem : IDisposable
{
    void Initialize(IApplication ctx);
    void Update(IApplication application);
}
public interface IAppComponentSystem<TComponent> : IAppComponentSystem
{
    void IAppComponentSystem.Update(IApplication application)
    {
        if (application.HasComponent<TComponent>())
            Update(application.GetComponent<TComponent>()!, application);
    }

    void IAppComponentSystem.Initialize(IApplication application)
    {
        if (application.HasComponent<TComponent>())
            Initialize(application.GetComponent<TComponent>()!, application);
    }
    void Initialize(TComponent component, IApplication ctx);
    void Update(TComponent component, IApplication application);
}