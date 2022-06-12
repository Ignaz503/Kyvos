using System;
using System.Collections.Generic;
/// <summary>
/// as mostly seen here
///https://github.com/Scrawk/Terrain-Topology-Algorithms/blob/master/Assets/TerrainTopology/Scripts/CreateResidualMap.cs
/// </summary>
namespace Kyvos.Maths.Topology;
public static class ResidualAnalysis
{
    public const int DefaultKernel = 3;

    static List<float> CollectData(FloatField map, float x, float y, int kernel)
    {
        var l = new List<float>();
        float localKernel = kernel * map.Resolution;
        for (float i = -localKernel; i <= localKernel; i += map.Resolution)
        {
            for (float j = -localKernel; j <= localKernel; j += map.Resolution)
            {
                var xi = x + i;
                var yj = y + j;
                if (!map.Contains(xi, yj))
                    continue;
                l.Add(map[xi, yj]);
            }
        }
        return l;
    }


    public static float MeanElevation(FloatField map, float x, float y, int kernel = DefaultKernel)
    {
        return MeanFromElevations(CollectData(map, x, y, kernel));
    }

    static float MeanFromElevations(IEnumerable<float> elevations)
        => Statistics.Mean(elevations);

    public static float StdevElevation(FloatField map, float x, float y, int kernel = DefaultKernel)
    {
        var d = CollectData(map, x, y, kernel);

        return CalculateStdev(d);
    }

    static float CalculateStdev(IEnumerable<float> data)
    {
        var mean = MeanFromElevations(data);
        return MathF.Sqrt(Statistics.Variance(mean, data));
    }

    public static float DifferenceFromMeanElevation(FloatField map, float x, float y, int kernel = DefaultKernel)
    {
        return map[x, y] - MeanElevation(map, x, y, kernel);
    }

    public static float DeviationFromMeanElevation(FloatField map, float x, float y, int kernel = DefaultKernel)
    {
        var d = CollectData(map, x, y, kernel);
        var stdev = CalculateStdev(d);
        if (Mathf.AlmostEquals(stdev, 0))
            return 0;
        var diffMeanElev = map[x, y] - MeanFromElevations(d);
        return diffMeanElev / stdev;
    }
    public static float Percentile(FloatField map, float x, float y, int kernel = DefaultKernel)
    {
        float h = map[x, y];
        var d = CollectData(map, x, y, kernel);

        float hits = 0;
        for (int i = 0; i < d.Count; i++)
        {
            if (d[i] < h)
                hits++;
        }
        if (hits == 0)
            return 0;
        return hits / d.Count;
    }
}

