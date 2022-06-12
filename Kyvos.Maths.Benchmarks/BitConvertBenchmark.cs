using BenchmarkDotNet.Attributes;
using Kyvos.Maths;
using Kyvos.Core.Memory;
using System;
using System.Numerics;

[MemoryDiagnoser]
public class BitConvertBenchmark<T>
    where T : unmanaged
{

    T val;
    Type t = typeof(int);
    public BitConvertBenchmark()
    {
               

        val = (T)Enum.GetValues(typeof(T)).GetValue(0);
    }


    [Benchmark]
    public int ChangeType()
        => (int)Convert.ChangeType(val, t);

    [Benchmark]
    public int Unsafe()
    {
        unsafe 
        {
            fixed(T* ptr = &val) 
            {
                return *(int*)ptr;
            }
        }
    }
}
