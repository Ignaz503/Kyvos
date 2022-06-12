using DefaultEcs;
using DefaultEcs.Command;
using Kyvos.Core;
using System;

namespace Kyvos.ECS.Entities;

public struct EntityCommands : IDisposable
{
    EntityCommandRecorder recorder;
    World world;

    public IApplication App => world.Get<IApplication>(); 

    public EntityCommands(World w)
    {
        this.world = w;
        this.recorder = new EntityCommandRecorder();
    }

    //
    // Summary:
    //     From DefaultECS:
    //     Gives an DefaultEcs.Command.EntityRecord to record action on the given DefaultEcs.Entity.
    //     This command takes 9 bytes.
    //
    // Parameters:
    //   entity:
    //     The DefaultEcs.Command.EntityRecord used to record action on the given DefaultEcs.Entity.
    //
    // Returns:
    //     The DefaultEcs.Command.EntityRecord used to record actions on the given DefaultEcs.Entity.
    //
    // Exceptions:
    //   T:System.InvalidOperationException:
    //     Command buffer is full.
    public EntityRecord Record(in Entity entity)
        => recorder.Record(entity);

    public WorldRecord WorldModification()
        => recorder.Record(world);

    //
    // Summary:
    //     From DefaultECS:
    //     Records the creation of an DefaultEcs.Entity on a DefaultEcs.World and returns
    //     an DefaultEcs.Command.EntityRecord to record action on it. This command takes
    //     9 bytes.
    //
    // Returns:
    //     The DefaultEcs.Command.EntityRecord used to record actions on the later created
    //     DefaultEcs.Entity.
    //
    // Exceptions:
    //   T:System.InvalidOperationException:
    //     Command buffer is full.
    public EntityRecord CreateEntity()
        => WorldModification().CreateEntity();

    //
    // Summary:
    //     From DefaultECS:
    //     Executes all recorded commands and clears those commands.
    internal void Execute()
        => recorder.Execute();

    public void Dispose()
        => recorder.Dispose();

    public void Clear()
        => recorder.Clear();
}


