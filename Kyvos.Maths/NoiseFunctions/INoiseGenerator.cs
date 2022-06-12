
using System.Numerics;

namespace Kyvos.Maths.NoiseFunctions;
public interface INoiseGenerator2D
{
    NoiseType Type { get; }
    float GetNoise(float x, float y, int seed, float frequency);
    float GetNoise(int seed, float x, float y);

    public Vector2 GetDerivative(float x, float y, int seed, float frequency);

    public Vector2 GetDerivative(int seed, float x, float y);

}
public interface INoiseGenerator3D
{
    NoiseType Type { get; }
    float GetNoise(float x, float y, float z, int seed, float frequency, RotationType3D rotationType);

    float GetNoise(int seed, float x, float y, float z);

    public Vector3 GetDerivative(float x, float y, float z, int seed, float frequency, RotationType3D rotationType);

    public Vector3 GetDerivative(int seed, float x, float y, float z);

}

public interface INoiseGenerator : INoiseGenerator2D, INoiseGenerator3D
{ }

