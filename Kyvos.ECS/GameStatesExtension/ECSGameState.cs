using DefaultEcs;
using DefaultEcs.System;
using Kyvos.Core;
using Kyvos.ECS.Systems.Setup;
using Kyvos.ECS.Resources;
using System;
using System.Diagnostics;
using Kyvos.ECS;
using Kyvos.ECS.Components.Management;
using Kyvos.GameStates;
using Kyvos.VeldridIntegration;
using Kyvos.Core.Logging;

namespace Kyvos.ECS.GameStatesExtension;

public partial class ECSGameState : GameState<World>
{
    int? entityCapacity;
    bool usesNewWorld;
    Func<World> worldInitializer;
    IWorldConfigureSystem worldConfigure;
    EntitySetupSystems setupSystems;
    ISystemBuilder updateSystemBuilder;
    
    IResourceManagmentOptOuts resourceManagmentOptOuts;
    ManagmentContract[] resourceManagmentContracts;

    ISystem<float> updateSystem;

    internal ECSGameState(string name, (int? entityCapacity, bool useNewWorld) worldInit, IWorldConfigureSystem worldConfigure, IResourceManagmentOptOuts managmentOptOuts, EntitySetupSystems setupSystems, ISystemBuilder updateSystemBuilder, ISleepHandler<World> internalSleepSystem, ISuspensionHandler<World> suspensionHandler, ITeardownHandler<World> teardownSystem, IApplication app)
        : base(name, ECSExtensions.EmptyWorld, app, internalSleepSystem, suspensionHandler, teardownSystem)
    {

        this.entityCapacity = worldInit.entityCapacity;
        this.usesNewWorld = worldInit.useNewWorld;
        if (worldInit.useNewWorld)
            this.worldInitializer = GetNewWorld;
        else
            this.worldInitializer = GetPreviousStatesWorld;
        
        this.worldConfigure = worldConfigure ?? throw new ArgumentNullException(nameof(worldConfigure));
        
        this.resourceManagmentOptOuts = managmentOptOuts ?? throw new ArgumentNullException(nameof(managmentOptOuts)); ;
        
        resourceManagmentContracts = Array.Empty<ManagmentContract>();
        
        this.setupSystems = setupSystems ?? throw new ArgumentNullException(nameof(setupSystems));
        
        this.updateSystemBuilder = updateSystemBuilder ?? throw new ArgumentNullException(nameof(updateSystemBuilder));
        

        this.updateSystem = ECSExtensions.EmptySystem;

    }

    World GetNewWorld()
        => entityCapacity is null ? new World() : new World(entityCapacity.Value);

    World GetPreviousStatesWorld()
        => application.GetComponent<GameStateStack<Builder,ECSGameState,World>>()?.Peek(1)?.data ?? ECSExtensions.EmptyWorld;


    public override void Update(float deltaTime)
    {
        #if DEBUG
                if (!IsRunning)
                {
                    Log<ECSGameState>.Debug("Trying to update non running state: {Name}", Name);
                    return;
                }
        #endif
        updateSystem.Update(deltaTime);
    }

    protected override void Awake()
    {
        sleepSystemToUse?.Awake(data);
        sleepSystemToUse = null; //one time use idle system
    }

    protected override void TearDown()
    {
        teardownSystem.TearDown(data);
        for (int i = 0; i < resourceManagmentContracts.Length; i++)
        {
            resourceManagmentContracts[i].End();
        }

        if (usesNewWorld) 
        {
            StopWindowEventPublishing();
            //component manager listens to world dispose to stop managing world
        }

        data = ECSExtensions.EmptyWorld;
        updateSystem?.Dispose();
        updateSystem = ECSExtensions.EmptySystem;
    }

    protected override void Setup()
    {
        data = worldInitializer();

        //////only do this if new world///////
        if (usesNewWorld)
        {
            SetSingleton(data, application);
            SetSingleton(data, application.GetComponent<GraphicsDeviceHandle>());
            //already managed
            EstablishResourceMangament(data);
            SetupWindowEventPublishing();
            application.GetComponent<IComponentManager>()!.SeutpManagement(data);
        }
        ////////////////////////////

        worldConfigure.Configure(data);

        setupSystems.Execute(data);
        updateSystem = updateSystemBuilder.Build(data);
    }

    void EstablishResourceMangament(World w)
    {
        resourceManagmentContracts = application.GetComponent<IResourceManagers>()!.Manage(w, resourceManagmentOptOuts);
    }

    void SetSingleton<T>(World world, T obj)
    {
        world.SetMaxCapacity<T>(1);
        world.Set<T>(obj);
    }

    void SetupWindowEventPublishing()
    {
        application.GetComponent<Window>()!.OnWindowEvent += PublishWindowEvent;
        
    }

    private void PublishWindowEvent(Window.Event obj)
    {
        data.Publish(in obj);
    }

    public void StopWindowEventPublishing() 
    {
        application.GetComponent<Window>()!.OnWindowEvent -= PublishWindowEvent;
    }
}

