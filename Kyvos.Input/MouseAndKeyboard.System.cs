using DefaultEcs;
using DefaultEcs.System;
using DefaultEcs.Threading;
using Kyvos.Core;
using Kyvos.VeldridIntegration;
using System.Diagnostics;

namespace Kyvos.Input;



public partial struct MouseAndKeyboard
{
    //TODO decide if even left in code base
    //maybe force multi input system use and rename it to just input system
    public class System : AComponentSystem<float, MouseAndKeyboard>
    {
        Window window;

        public System(World world) : base(world)
        {
            window = GetWindow();
        }

        public System(World world, IParallelRunner runner) : base(world, runner)
        {
            window = GetWindow();
        }

        public System(World world, IParallelRunner runner, int minComponentCountByRunnerIndex) : base(world, runner, minComponentCountByRunnerIndex)
        {
            window = GetWindow();
        }

        Window GetWindow()
        {
            Debug.Assert(World.Get<IApplication>().HasComponent<Window>());
            return World.Get<IApplication>().GetComponent<Window>()!;
        }

        protected override void Update(float state, ref MouseAndKeyboard component)
        {
            //Sdl2Events.ProcessEvents(); //called by window pump events
            var snapshot = window.PumpEvents();

            component.Update(snapshot, window);
        }
    }

    public class MultiInputSystem : AComponentSystem<InputSystemContext, MouseAndKeyboard>
    {
        Window window;

        public MultiInputSystem(World world) : base(world)
        {
            window = GetWindow();
        }

        public MultiInputSystem(World world, IParallelRunner runner) : base(world, runner)
        {

            window = GetWindow();
        }

        public MultiInputSystem(World world, IParallelRunner runner, int minComponentCountByRunnerIndex) : base(world, runner, minComponentCountByRunnerIndex)
        {
            window = GetWindow();
        }

        Window GetWindow() 
        {
            Debug.Assert(World.Get<IApplication>().HasComponent<Window>());
            return World.Get<IApplication>().GetComponent<Window>()!;
        }

        protected override void Update(InputSystemContext state, ref MouseAndKeyboard component)
        {
            component.Update(state.InputSnapshot, window);
        }
    }

}


