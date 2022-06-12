using Kyvos.Core;
using System;

namespace Kyvos.GameStates;

public abstract partial class GameState<TData>
{
    /*
    a state has 5 major phases
    TODO find better name for this, as well as IsZeroState
    uninitialized/torndown -> when created/or torn down.
          if in this state i can be sure to
          close the application savely as anything
          disposable has been disposed anything that needs saving has been saved.
          Or similarly transitioning to run i need to call setup

    run -> activly running this gamestate
           every application update the states update system is actively called

    sleep -> when another state is the top of the stack, lower states get idled
            in this state the game loop is not actively invoked
            however not necessarily all resources need to be torwn down and rebuilt
            maybe just some, think of pause menu state ontop of gameplay state
            no need to unload all the models etc

    suspended -> another state is pushed ontop ons tack but that state uses the
                world(scene) of the sate below as it's own
*/


    /*
        GameState:
            has a world(scene)
            it's own scene, or the one of the state below on the stack
            is on gamse state stack
            can be uninitialized(towrndown)/running/sleeping/suspended
            suspended means it's not really supposed to be unloaded (the world that is)
            as the state above uses it to be run

            creating a state:
                push ontop
                    option 1: idle state below and build your own scene in your state (e.g: game world after main menu)
                    option 2: suspend state below build entities into world of state below (e.g: pause menu ontop of game world)
     */

    protected internal enum Phase
    {
        Uninitialized,
        Tornwdown,
        Running,
        Sleep,
        Suspended
    }

    public string? Name { get; private set; }

    protected Phase currentPhase;

    protected internal Phase CurrentPhase
    {
        get => currentPhase;
        set
        {
            var prev = currentPhase;
            currentPhase = value;
            FinalizeTransitionFrom(prev);
        }
    }

    public bool IsPotentialyPopulated => CurrentPhase == Phase.Sleep || CurrentPhase == Phase.Running;

    public bool IsRunning => CurrentPhase == Phase.Running;
    public bool IsSleeping => CurrentPhase == Phase.Sleep;

    public bool IsSuspended => CurrentPhase == Phase.Suspended;

    public bool IsEmptyState => CheckForEmptyState(CurrentPhase);
    

    protected TData data;

    protected IApplication application;

    protected ISleepHandler<TData> internalSleepSystem;
    protected ISleepHandler<TData>? sleepSystemToUse;
    
    protected ISuspensionHandler<TData> suspensionHandler;

    protected ITeardownHandler<TData> teardownSystem;

    protected GameState(string? name, TData data, IApplication application, ISleepHandler<TData> internalSleepSystem,  ISuspensionHandler<TData> suspensionHandler, ITeardownHandler<TData> teardownSystem)
    {
        Name = name;
        this.data = data;
        this.application = application ?? throw new ArgumentNullException(nameof(application));
        this.internalSleepSystem = internalSleepSystem ?? throw new ArgumentNullException(nameof(internalSleepSystem));
        this.suspensionHandler = suspensionHandler ?? throw new ArgumentNullException(nameof(suspensionHandler));
        this.teardownSystem = teardownSystem ?? throw new ArgumentNullException(nameof(teardownSystem));
        currentPhase = Phase.Uninitialized;
    }

    bool CheckForEmptyState(Phase p)
        => p == Phase.Uninitialized || p == Phase.Tornwdown;

    public abstract void Update(float deltaTime);


    /// <summary>
    /// Sets phase to running
    /// </summary>
    public void Start()
        => CurrentPhase = Phase.Running;

    /// <summary>
    /// sets phase to Torwndown
    /// </summary>
    public void Stop()
        => CurrentPhase = Phase.Tornwdown;

    /// <summary>
    /// Sets phase to idle
    /// </summary>
    public void Sleep()
    {
        sleepSystemToUse = internalSleepSystem;
        CurrentPhase = Phase.Sleep;
    }

    /// <summary>
    /// sets phase to idle
    /// </summary>
    /// <param name="sleepHandler">custom sleep handler</param>
    /// <exception cref="ArgumentNullException">thrown if custom idle handler is null, use param less version instead</exception>
    public void Sleep(ISleepHandler<TData> sleepHandler)
    {
        sleepSystemToUse = sleepHandler ?? throw new ArgumentNullException(nameof(sleepHandler));
        CurrentPhase = Phase.Sleep;
    }

    /// <summary>
    /// suspends this game state
    /// </summary>
    public void Suspend()
        => CurrentPhase = Phase.Suspended;


    //Called after CurrentPhase has been set
    //possible transitions
    //uninitialized/torndown -> run: invoke StepuSystems.Run(world)
    //uninitialized/torndown -> sleep: setup and sleep right after
    //uninitialized/torndown -> susbepnded: setup and suspend right after
    //run -> sleep: invoke sleepHandler.Sleep(world)
    //run -> uninitialized/torndown: invoke TeardownHandler.TearDown(world)
    //run -> suspended: invoke suspend handler on world
    //sleep -> uninitialized/torndown: invoke TeardownSystem.TearDown(world) ?? run beforehand?
    //sleep -> run: invoke idleHandler.Awake(world)
    //sleep -> suspended: invoke awake(world), invoke suspend(world)
    //suspended -> uninitialize/tornwdown -> invoke TeardownHandler.TearDown(world)
    //suspended -> run: invoke resume(world)
    //suspended -> idle: invoke resum(world) sleep(world)

    void FinalizeTransitionFrom(Phase previousPhase)
    {
        if (previousPhase == CurrentPhase)
            return;

        switch (previousPhase)
        {
            case Phase.Tornwdown:
            case Phase.Uninitialized:
                HandleSwitchFromEmpty();
                break;
            case Phase.Running:
                HandleSwitchFromRunning();
                break;
            case Phase.Sleep:
                HandleSwitchFromSleep();
                break;
            case Phase.Suspended:
                HandleSwitchFromSuspended();
                break;
        }
    }
    void HandleSwitchFromSleep()
    {
        //sleep -> uninitialized/torndown: invoke TeardownSystem.TearDown(world) ?? run beforehand?
        //sleep -> run: invoke sleepHandler.Awake(world)
        //sleep -> suspended: invoke awake(world), invoke suspend(world)

        if (IsEmptyState)
        {
            TearDown();
        }
        else if (IsRunning)
        {
            Awake();
        }
        else if (IsSuspended)
        {
            Awake();
            suspensionHandler.Suspend(data);
        }
    }

    protected abstract void Awake();

    void HandleSwitchFromRunning()
    {
        //run -> sleep: invoke sleepHandler.Sleep(world)
        //run -> uninitialized/torndown: invoke TeardownHandler.TearDown(world)
        //run -> suspended: invoke suspend handler on world
        if (IsEmptyState)
        {
            TearDown();
        }
        else if (IsSleeping)
        {
            sleepSystemToUse?.Sleep(data);
        }
        else if (IsSuspended)
        {
            suspensionHandler.Suspend(data);
        }
    }

    void HandleSwitchFromEmpty()
    {
        //uninitialized/torndown -> run: invoke StepuSystems.Run(world)
        //uninitialized/torndown -> sleep: setup and sleep right after
        //uninitialized/torndown -> susbepnded: setup and suspend right after
        Setup();
        if (IsSleeping) //immediately idle after creation
            sleepSystemToUse?.Sleep(data);
        else if (IsSuspended)
            suspensionHandler.Suspend(data);
    }

    protected abstract void Setup();

    private void HandleSwitchFromSuspended()
    {
        //suspended -> uninitialize/tornwdown -> invoke TeardownHandler.TearDown(world)
        //suspended -> run: invoke resume(world)
        //suspended -> idle: invoke resum(world) sleep(world)
        if (IsEmptyState)
        {
            TearDown();
        }
        else if (IsRunning)
        {
            suspensionHandler.Resume(data);
        }
        else if (IsSleeping)
        {
            suspensionHandler.Resume(data);
            sleepSystemToUse?.Sleep(data);
        }

    }

    protected abstract void TearDown();

}

