using BenchmarkDotNet.Attributes;
using Kyvos.Maths.Graphs;
using Kyvos.Maths.NoiseFunctions;
using System;
using System.Numerics;

[MemoryDiagnoser]
public class NoiseBenchmarks
{
    Random rng;
    public NoiseBenchmarks()
    {
        rng = new System.Random( (int)DateTime.Now.Ticks );
    }

    [Benchmark]
    public float SpeedTest() 
    {
        return Noise.Vornoi((float) rng.NextDouble() * 512, (float)rng.NextDouble() * 512 );
    }

    [Benchmark]
    public float FractalTest() 
    {
        return Noise.Layered.FBm( (float)rng.NextDouble() * 512, (float)rng.NextDouble() * 512, VornoiNoiseGenerator.Default );
    }

}


