using Veldrid.Sdl2;
using DefaultEcs.System;
using DefaultEcs;
using DefaultEcs.Threading;

namespace Kyvos.Input;

public partial struct Gamepad
{
    public class System : AComponentSystem<float, Gamepad>
    {
        public System(World world) : base(world)
        { }

        public System(World world, IParallelRunner runner) : base(world, runner)
        { }

        public System(World world, IParallelRunner runner, int minComponentCountByRunnerIndex) : base(world, runner, minComponentCountByRunnerIndex)
        { }

        protected override void Update(float state, ref Gamepad gamepad)
        {
            Sdl2Events.ProcessEvents();
            gamepad.Update();
        }
    }

    public class MultiInputSystem : AComponentSystem<InputSystemContext, Gamepad>
    {
        public MultiInputSystem(World world) : base(world)
        {}

        public MultiInputSystem(World world, IParallelRunner runner) : base(world, runner)
        {}

        public MultiInputSystem(World world, IParallelRunner runner, int minComponentCountByRunnerIndex) : base(world, runner, minComponentCountByRunnerIndex)
        {}

        protected override void Update(InputSystemContext state, ref Gamepad gamepad)
        {
            gamepad.Update();
        }
    }
}


