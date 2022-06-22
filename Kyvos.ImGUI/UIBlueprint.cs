using DefaultEcs;
using DefaultEcs.System;
using DefaultEcs.Threading;
using Kyvos.Core;
using System.Diagnostics;

namespace Kyvos.ImGUI;

public class UIBlueprint 
{
    public static void AddUI(IUIComponent component, World world)
    {
        Debug.Assert(world.Get<IApplication>() != null, "Application must be set");
        world.SetMaxCapacity<ImGuiHandle>(1);
        world.Set<ImGuiHandle>(new(world.Get<IApplication>()));

        world.SetMaxCapacity<UITree>(1);
        world.Set(new UITree(component));
    }

    public static ISystem<float> GetSystem(World w) 
    {
        return new SequentialSystem<float>(
            new ImGuiHandle.System(w),
            new UITree.System(w)
            );
    }
    public static ISystem<float> GetSystem(World w, IParallelRunner runner)
    {
        return new SequentialSystem<float>(
            new ImGuiHandle.System(w, runner),
            new UITree.System(w, runner)
            );
    }


    public static ISystem<float> GetSystem(World w, IParallelRunner runner, int minComponentCountByRunnerIndex)
    {
        return new SequentialSystem<float>(
            new ImGuiHandle.System(w, runner, minComponentCountByRunnerIndex),
            new UITree.System(w, runner, minComponentCountByRunnerIndex)
            );
    }
}
