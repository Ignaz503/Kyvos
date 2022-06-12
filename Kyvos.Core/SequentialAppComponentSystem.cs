namespace Kyvos.Core;

public class SequentialAppComponentSystem : IAppComponentSystem
{
    public static readonly SequentialAppComponentSystem Empty = new();

    IAppComponentSystem[] componentSystems;

    private SequentialAppComponentSystem()
    {
        componentSystems = Array.Empty<IAppComponentSystem>();
    }

    public SequentialAppComponentSystem(IEnumerable<IAppComponentSystem> systems)
    {
        componentSystems = systems.ToArray();
    }
    public SequentialAppComponentSystem(params IAppComponentSystem[] systems)
    {
        componentSystems = systems;
    }
    
    public void Initialize(IApplication ctx)
    {
        var systems = componentSystems;
        for (int i = 0; i < systems.Length; i++)
        {
            systems[i].Initialize(ctx);
        }
    }

    public void Update(IApplication application)
    {
        var systems = componentSystems;
        for (int i = 0; i < systems.Length; i++)
        {
            systems[i].Update(application);
        }
    }

    public void Dispose()
    {
        var systems = componentSystems;
        for (int i = 0; i < systems.Length; i++)
        {
            systems[i].Dispose();
        }
    }
}
