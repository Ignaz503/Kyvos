namespace Kyvos.Core;

public interface IModifyableApplication : IApplication
{
    void RegisterToAppLifetimeQuery(LifetimeQuery toConsider);
    void UnregisterFromAppLifetimeQuery(LifetimeQuery toConsider);

    T AddComponent<T>(T component);
    T AddComponent<T>(Func<IApplication, T> factory);

    ref T AddComponent<T>(ref T component);
    ref T AddComponentRef<T>(Func<IApplication, T> factory);

    void With(IAppComponentSystem system);

    void With(Func<IModifyableApplication, IAppComponentSystem> systemFactory);
}
