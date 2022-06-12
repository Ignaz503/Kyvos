using BenchmarkDotNet.Attributes;
using Kyvos.Maths;
using System;

public class InterpolationBenchmarks 
{
    float smaller;
    float larger;
    System.Random rng;
    public InterpolationBenchmarks()
    {
        rng = new System.Random( (int)DateTime.Now.Ticks );
        smaller = rng.Next( 1, 100 );
        larger = rng.Next( 100, 200 );
    }

    [Benchmark]
    public float LinearInterpolation() 
    {
        return Interpolation.Linear( smaller, larger, (float)rng.NextDouble() );
    }
}
