using DefaultEcs;
using Veldrid;

namespace Kyvos.ECS.Systems.Rendering;
public struct RenderContext
{
    public World World { get; init; }

    public GraphicsDevice GfxDevice { get; init; }

    public float DeltaTime { get; init; }

    public CommandList CmdList { get; init; } 
}


