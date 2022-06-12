using System;
using System.Numerics;
using MathNet.Numerics.Random;

namespace Kyvos.Maths;

public static class Random
{
    static SystemRandomSource rngSource;

    static Random() 
    {
        rngSource = new SystemRandomSource((int)DateTime.Now.Ticks, true); 
    }

    /// <summary>
    /// [0..1[
    /// </summary>
    public static float Value 
    {
        get => rngSource.NextSingle();
    }

    public static Vector2 RandomVecOnUnitCircle 
    {
        get { return Vector2.Normalize(new(Value,Value)); }
    }

    public static Vector3 RandomVecOnUnitSphere 
    {
        get { return Vector3.Normalize(new(Value, Value, Value)); }
    }

    public static Vector4 RandomVecOnHyperShpere
    {
        get { return Vector4.Normalize(new(Value, Value, Value, Value)); }
    }

    public static void Seed(int seed) 
    {
        rngSource = new(seed, true);
    }

}