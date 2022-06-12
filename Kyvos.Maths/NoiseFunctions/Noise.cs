namespace Kyvos.Maths.NoiseFunctions;
public static partial class Noise
{
    /// <summary>
    /// Output between -1 ... 1
    /// </summary>
    public static float Vornoi(float x, float y, int seed = NoiseHelpers.DefaultSeed, float frequency = NoiseHelpers.DefaultFrequency, float celularJitter = VornoiNoiseGenerator.DefaultJitterModifier, VornoiDistanceFunction distanceFunction = VornoiNoiseGenerator.DefaultDistanceFunction, VornoiReturnType returnType = VornoiNoiseGenerator.DefaultReturnType)
    {
        return new VornoiNoiseGenerator(vornoiJitterModifier: celularJitter, distFunction: distanceFunction, returnType: returnType).GetNoise(x, y, seed, frequency);
    }

    /// <summary>
    /// Output between -1 ... 1
    /// </summary>
    public static float Vornoi(float x, float y, float z, int seed = NoiseHelpers.DefaultSeed, float frequency = NoiseHelpers.DefaultFrequency, float celularJitter = VornoiNoiseGenerator.DefaultJitterModifier, VornoiDistanceFunction distanceFunction = VornoiNoiseGenerator.DefaultDistanceFunction, VornoiReturnType returnType = VornoiNoiseGenerator.DefaultReturnType, RotationType3D rotationType = NoiseHelpers.DefaultRotationType)
    {
        return new VornoiNoiseGenerator(vornoiJitterModifier: celularJitter, distFunction: distanceFunction, returnType: returnType).GetNoise(x, y, z, seed, frequency, rotationType);
    }

    /// <summary>
    /// Output between 0 ... 1
    /// </summary>
    public static float Vornoi01(float x, float y, int seed = NoiseHelpers.DefaultSeed, float frequency = NoiseHelpers.DefaultFrequency, float celularJitter = VornoiNoiseGenerator.DefaultJitterModifier, VornoiDistanceFunction distanceFunction = VornoiNoiseGenerator.DefaultDistanceFunction, VornoiReturnType returnType = VornoiNoiseGenerator.DefaultReturnType)
        => (1.0f + Vornoi(x, y, seed, frequency, celularJitter, distanceFunction, returnType)) / 2.0f;


    /// <summary>
    /// Output between 0 ... 1
    /// </summary>
    public static float Vornoi01(float x, float y, float z, int seed = NoiseHelpers.DefaultSeed, float frequency = NoiseHelpers.DefaultFrequency, float celularJitter = VornoiNoiseGenerator.DefaultJitterModifier, VornoiDistanceFunction distanceFunction = VornoiNoiseGenerator.DefaultDistanceFunction, VornoiReturnType returnType = VornoiNoiseGenerator.DefaultReturnType, RotationType3D rotationType = NoiseHelpers.DefaultRotationType)
        => (1.0f + Vornoi(x, y, z, seed, frequency, celularJitter, distanceFunction, returnType, rotationType)) / 2.0f;


    /// <summary>
    /// Output between -1 ... 1
    /// </summary>
    public static float Value(float x, float y, int seed = NoiseHelpers.DefaultSeed, float frequency = NoiseHelpers.DefaultFrequency)
    {
        return new ValueNoiseGenerator().GetNoise(x, y, seed, frequency);
    }

    /// <summary>
    /// Output between -1 ... 1
    /// </summary>
    public static float Value(float x, float y, float z, int seed = NoiseHelpers.DefaultSeed, float frequency = NoiseHelpers.DefaultFrequency, RotationType3D rotationType = NoiseHelpers.DefaultRotationType)
    {
        return new ValueNoiseGenerator().GetNoise(x, y, z, seed, frequency, rotationType);
    }

    /// <summary>
    /// Output between 0 ... 1
    /// </summary>
    public static float Value01(float x, float y, int seed = NoiseHelpers.DefaultSeed, float frequency = NoiseHelpers.DefaultFrequency)
    {
        return (1.0f + Value(x, y, seed, frequency)) / 2.0f;
    }

    /// <summary>
    /// Output between 0 ... 1
    /// </summary>
    public static float Value01(float x, float y, float z, int seed = NoiseHelpers.DefaultSeed, float frequency = NoiseHelpers.DefaultFrequency, RotationType3D rotationType = NoiseHelpers.DefaultRotationType)
    {
        return (1.0f + Value(x, y, z, seed, frequency, rotationType)) / 2.0f;
    }

    /// <summary>
    /// Output between -1 ... 1
    /// </summary>
    public static float ValueCubic(float x, float y, int seed = NoiseHelpers.DefaultSeed, float frequency = NoiseHelpers.DefaultFrequency)
    {
        return new ValueCubicNoiseGenerator().GetNoise(x, y, seed, frequency);
    }

    /// <summary>
    /// Output between -1 ... 1
    /// </summary>
    public static float ValueCubic(float x, float y, float z, int seed = NoiseHelpers.DefaultSeed, float frequency = NoiseHelpers.DefaultFrequency, RotationType3D rotationType = NoiseHelpers.DefaultRotationType)
    {
        return new ValueCubicNoiseGenerator().GetNoise(x, y, z, seed, frequency, rotationType);
    }

    /// <summary>
    /// Output between 0 ... 1
    /// </summary>
    public static float ValueCubic01(float x, float y, int seed = NoiseHelpers.DefaultSeed, float frequency = NoiseHelpers.DefaultFrequency)
    {
        return (1.0f + ValueCubic(x, y, seed, frequency)) / 2.0f;
    }

    /// <summary>
    /// Output between 0 ... 1
    /// </summary>
    public static float ValueCubic01(float x, float y, float z, int seed = NoiseHelpers.DefaultSeed, float frequency = NoiseHelpers.DefaultFrequency, RotationType3D rotationType = NoiseHelpers.DefaultRotationType)
    {
        return (1.0f + ValueCubic(x, y, z, seed, frequency, rotationType)) / 2.0f;
    }


    /// <summary>
    /// Output between -1 ... 1
    /// </summary>
    public static float Perlin(float x, float y, int seed = NoiseHelpers.DefaultSeed, float frequency = NoiseHelpers.DefaultFrequency)
    {
        return new PerlinNoiseGenerator().GetNoise(x, y, seed, frequency);
    }

    /// <summary>
    /// Output between -1 ... 1
    /// </summary>
    public static float Perlin(float x, float y, float z, int seed = NoiseHelpers.DefaultSeed, float frequncy = NoiseHelpers.DefaultFrequency, RotationType3D rotationType = NoiseHelpers.DefaultRotationType)
    {
        return new PerlinNoiseGenerator().GetNoise(x, y, z, seed, frequncy, rotationType);
    }

    /// <summary>
    /// Output between 0 ... 1
    /// </summary>
    public static float Perlin01(float x, float y, int seed = NoiseHelpers.DefaultSeed, float frequency = NoiseHelpers.DefaultFrequency)
    {
        return (1.0f + Perlin(x, y, seed, frequency)) / 2.0f;
    }

    /// <summary>
    /// Output between 0 ... 1
    /// </summary>
    public static float Perlin01(float x, float y, float z, int seed = NoiseHelpers.DefaultSeed, float frequncy = NoiseHelpers.DefaultFrequency, RotationType3D rotationType = NoiseHelpers.DefaultRotationType)
    {
        return (1.0f + Perlin(x, y, z, seed, frequncy, rotationType)) / 2.0f;
    }

    /// <summary>
    /// Output between -1 ... 1
    /// </summary>
    public static float OpenSimplex2S(float x, float y, int seed = NoiseHelpers.DefaultSeed, float frequency = NoiseHelpers.DefaultFrequency)
    {
        return new OpenSimplex2SNoiseGenerator().GetNoise(x, y, seed, frequency);
    }

    /// <summary>
    /// Output between -1 ... 1
    /// </summary>
    public static float OpenSimplex2S(float x, float y, float z, int seed = NoiseHelpers.DefaultSeed, float frequncy = NoiseHelpers.DefaultFrequency, RotationType3D rotationType = NoiseHelpers.DefaultRotationType)
    {
        return new OpenSimplex2SNoiseGenerator().GetNoise(x, y, z, seed, frequncy, rotationType);
    }

    /// <summary>
    /// Output between 0 ... 1
    /// </summary>
    public static float OpenSimplex2S01(float x, float y, int seed = NoiseHelpers.DefaultSeed, float frequency = NoiseHelpers.DefaultFrequency)
    {
        return (1.0f + OpenSimplex2S(x, y, seed, frequency)) / 2.0f;
    }

    /// <summary>
    /// Output between 0 ... 1
    /// </summary>
    public static float OpenSimplex2S01(float x, float y, float z, int seed = NoiseHelpers.DefaultSeed, float frequncy = NoiseHelpers.DefaultFrequency, RotationType3D rotationType = NoiseHelpers.DefaultRotationType)
    {
        return (1.0f + OpenSimplex2S(x, y, z, seed, frequncy, rotationType)) / 2.0f;
    }

    /// <summary>
    /// Output between -1 ... 1
    /// </summary>
    public static float OpenSimplex(float x, float y, int seed = NoiseHelpers.DefaultSeed, float frequency = NoiseHelpers.DefaultFrequency)
    {
        return new OpenSimplexNoiseGenerator().GetNoise(x, y, seed, frequency);
    }

    /// <summary>
    /// Output between -1... 1
    /// </summary>
    public static float OpenSimplex(float x, float y, float z, int seed = NoiseHelpers.DefaultSeed, float frequncy = NoiseHelpers.DefaultFrequency, RotationType3D rotationType = NoiseHelpers.DefaultRotationType)
    {
        return new OpenSimplexNoiseGenerator().GetNoise(x, y, z, seed, frequncy, rotationType);
    }


    /// <summary>
    /// Output between 0 ... 1
    /// </summary>
    public static float OpenSimplex01(float x, float y, int seed = NoiseHelpers.DefaultSeed, float frequency = NoiseHelpers.DefaultFrequency)
    {
        return (1.0f + OpenSimplex(x, y, seed, frequency)) / 2.0f;
    }

    /// <summary>
    /// Output between 0 ... 1
    /// </summary>
    public static float OpenSimplex01(float x, float y, float z, int seed = NoiseHelpers.DefaultSeed, float frequncy = NoiseHelpers.DefaultFrequency, RotationType3D rotationType = NoiseHelpers.DefaultRotationType)
    {
        return (1.0f + OpenSimplex(x, y, z, seed, frequncy, rotationType)) / 2.0f;
    }
}

