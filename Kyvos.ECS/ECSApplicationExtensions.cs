using DefaultEcs;
using DefaultEcs.Resource;

using Kyvos.Core;
using Kyvos.ECS.Resources;
using Kyvos.ECS.Components.Management;
using Kyvos.ECS.GameStatesExtension;
using Kyvos.GameStates;

namespace Kyvos.ECS;
public static class ECSApplicationExtensions
{
    public static IModifyableApplication With(this IModifyableApplication app, IResourceManagers resourceManagers)
    {
        app.AddComponent(resourceManagers);
        return app;
    }
    public static IModifyableApplication With(this IModifyableApplication app, Func<IApplication, IResourceManagers> factory)
    {
        app.AddComponent(factory);
        return app;
    }

    public static IModifyableApplication With<TManager, TInfo, TResource>(this IModifyableApplication app, Func<IApplication, TManager> factory)
        where TManager : AResourceManager<TInfo, TResource>
    {
        var manager = app.EnsureExistence<IResourceManagers>(ResourceManagers.Default);

        manager.Add<TManager, TInfo, TResource>(factory(app));
        return app;
    }

    public static IModifyableApplication With<TManager, TInfo, TResource>(this IModifyableApplication app, IResourceManagerFactory<TManager, TInfo, TResource> factory)
    where TManager : AResourceManager<TInfo, TResource>
    {
        var manager = app.EnsureExistence<IResourceManagers>(ResourceManagers.Default);

        manager.Add<TManager, TInfo, TResource>(factory.Create(app));
        return app;
    }

    public static IModifyableApplication With(this IModifyableApplication app, IComponentManager componentManager)
    {
        app.AddComponent(componentManager);
        return app;
    }
    public static IModifyableApplication With(this IModifyableApplication app, Func<IApplication, IComponentManager> factory)
    {
        app.AddComponent(factory);
        return app;
    }

    public static IModifyableApplication With(this IModifyableApplication app, IComponentManagementContractEstablisher contractEstablisher)
    {
        var manager = app.EnsureExistence<IComponentManager>(ComponentManager.Default);
        manager.AddContractEstablisher(contractEstablisher);
        return app;
    }

    public static IModifyableApplication With(this IModifyableApplication app, Func<World, IDisposable> componentManager)
    {
        var manager = app.EnsureExistence<IComponentManager>(ComponentManager.Default);
        manager.AddContractEstablisher(new ComponentContractEstablishingFunction(componentManager));
        return app;
    }

    public static IModifyableApplication With<TBuilder, TState, TData>(this IModifyableApplication app, string name, ISomeStateDescription<ECSGameState.Builder, ECSGameState, World> stateFactory, bool isInitial = false)
        => GameStateApplicationExtensions.With<ECSGameState.Builder, ECSGameState, World>(app, name, stateFactory, isInitial);

    public static IModifyableApplication With(this IModifyableApplication app, string name, Action<ECSGameState.Builder> factory, bool isInitial = false)
        => GameStateApplicationExtensions.With<ECSGameState.Builder, ECSGameState, World>(app, name, factory, isInitial);

    public static IModifyableApplication WithStackCapacity(this IModifyableApplication app, int stackCapacity)
        => GameStateApplicationExtensions.WithStackCapacity<ECSGameState.Builder, ECSGameState, World>(app, stackCapacity);

    public static IModifyableApplication WithGameStateStack(this IModifyableApplication app)
        => GameStateApplicationExtensions.WithGameStateStack<ECSGameState.Builder, ECSGameState, World>(app);

    public static IModifyableApplication With(this IModifyableApplication app, GameStateStack<ECSGameState.Builder, ECSGameState, World> stack)
        => GameStateApplicationExtensions.With<ECSGameState.Builder, ECSGameState, World>(app, stack);

}