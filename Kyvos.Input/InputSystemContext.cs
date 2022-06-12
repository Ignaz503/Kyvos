using Veldrid;

namespace Kyvos.Input;

public struct InputSystemContext 
{
    public float DeltaTime { get; init; }
    public InputSnapshot InputSnapshot { get; init; } 
}


