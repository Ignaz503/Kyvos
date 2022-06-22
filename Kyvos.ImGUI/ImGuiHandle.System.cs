using DefaultEcs;
using DefaultEcs.System;
using DefaultEcs.Threading;
using Kyvos.Input;

namespace Kyvos.ImGUI;

public partial struct ImGuiHandle
{
    public class System : AComponentSystem<float, ImGuiHandle>
    {
        public System(World world) : base(world)
        {
        }

        public System(World world, IParallelRunner runner) : base(world, runner)
        {
        }

        public System(World world, IParallelRunner runner, int minComponentCountByRunnerIndex) : base(world, runner, minComponentCountByRunnerIndex)
        {
        }

        protected override void Update(float state, ref ImGuiHandle component)
        {
            if (World.Has<MouseAndKeyboard>()) 
            {
                ref var input = ref World.Get<MouseAndKeyboard>();
                component.Update(state, input);
            }
        }

    }

}
