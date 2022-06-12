

using System;
/// <summary>
/// as mostly seen here
/// https://github.com/Scrawk/Terrain-Topology-Algorithms/blob/master/Assets/TerrainTopology/Scripts/CreateLandformMap.cs
/// </summary>
namespace Kyvos.Maths.Topology;
public static class Landform
{
    public enum Classification
    {
        Dome = 100,
        ConvexSaddle = 75,
        Plane = 50,
        ConcaveSaddle = 25,
        Basin = 0,
    }

    public enum FlowClassification
    {
        Dissperse = 100,
        TransitiveConvex = 75,
        Flat = 50,
        TransitiveConcave = 25,
        Accumulate = 0
    }

    public static Classification Guassian(float dx, float dy, float dxx, float dyy, float dxy)
    {
        float gauss = Curvature.Gaussian(dx, dy, dxx, dyy, dxy);
        float mean = Curvature.Mean(dx, dy, dxx, dyy, dxy);

        if (gauss > 0 && mean > 0)
            return Classification.Dome;
        if (gauss < 0 && mean > 0)
            return Classification.ConvexSaddle;
        if (gauss == 0 && mean == 0)
            return Classification.Plane;
        if (gauss < 0 && mean < 0)
            return Classification.ConcaveSaddle;
        if (gauss > 0 && mean < 0)
            return Classification.Basin;
        throw new Exception($"Unknown landform {{Gaussian curvature:{gauss}, Mean Curvature {mean}}}");
    }

    /// <summary>
    /// 0..1
    /// > 0.5 convex
    /// < 0.5 concave
    /// </summary>
    public static float ShapeIndex(float dx, float dy, float dxx, float dyy, float dxy)
    {
        float gauss = Curvature.Gaussian(dx, dy, dxx, dyy, dxy);
        float mean = Curvature.Mean(dx, dy, dxx, dyy, dxy);

        float val = mean * mean - gauss;
        float d = 0;
        if (!(val <= 0)) //no complex sqrt
            d = MathF.Sqrt(val);

        if (Mathf.AlmostEquals(d, 0)) //don't divide by d if d 0
            val = 0;
        else
            val = mean / d;

        float s = 2.0f / MathF.PI * MathF.Atan(val);
        return s * 0.5f + 0.5f;
    }

    public static FlowClassification Accumulation(float dx, float dy, float dxx, float dyy, float dxy)
    {
        float h = Curvature.Horizontal(dx, dy, dxx, dyy, dxy);
        float v = Curvature.Vertical(dx, dy, dxx, dyy, dxy);
        if (h > 0 && v > 0)
            return FlowClassification.Dissperse;
        if (h > 0 && v < 0)
            return FlowClassification.TransitiveConvex;
        if (h == 0 && v == 0)
            return FlowClassification.Flat;
        if (h < 0 && v > 0)
            return FlowClassification.TransitiveConcave;
        if (h < 0 && v < 0)
            return FlowClassification.Accumulate;
        throw new Exception($"Unknown flow. {{ horizontal curvature: {h}, vertical curvature {v}}}");
    }

}

public static class LandformExtensions
{
    public static float Continuous(this Landform.Classification @class)
    {
        return ((int)@class) / (float)100f;
    }

    public static float Continuous(this Landform.FlowClassification @class)
    {
        return ((int)@class) / (float)100f;
    }
}

