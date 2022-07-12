namespace Kyvos.WaveFunctionCollapse;

public interface IRngProvider 
{
    int GetInteger(int max);
    int GetInteger(int min, int max);
}

