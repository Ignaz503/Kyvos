using DefaultEcs;
using DefaultEcs.System;
using DefaultEcs.Threading;

namespace Kyvos.ImGUI;

public struct UITree 
{
    IUIComponent root;

    public UITree(IUIComponent root)
    {
        this.root = root;
    }

    void Show() 
    {
        root.Show();
    }

    public class System : AComponentSystem<float, UITree>
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

        protected override void Update(float state, ref UITree component)
        {
            component.Show();
        }
    }

}
