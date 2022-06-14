using DefaultEcs;
using Kyvos.GameStates;

namespace Kyvos.ECS.GameStatesExtension;

public class DefaultTeardownHandler : ITeardownHandler<World> 
{

    public void TearDown(World world)
    {
        world.Publish(new DisposeMessage() { World = world });

        world.Dispose();
    }
}


