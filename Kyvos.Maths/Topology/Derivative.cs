using System;
/// <summary>
/// as mostly seen here
///https://github.com/Scrawk/Terrain-Topology-Algorithms/blob/master/Assets/TerrainTopology/Scripts/CreateResidualMap.cs
/// </summary>
namespace Kyvos.Maths.Topology;
public static class Derivative
{
    public static float Slope(FloatField map, float x, float y)
    {
        var deriv = map.GetFirstOrderDerivative(x, y);
        return MathF.Atan(deriv.Length()) * Mathf.RadToDeg / 90.0f;
    }

    public static float Aspect(FloatField map, float x, float y)
    {
        var deriv = map.GetFirstOrderDerivative(x, y);

        float gyx = 0f;
        float gxx = 0f;
        if (!Mathf.AlmostEquals(deriv.X, 0f))
        {
            gyx = deriv.Y / deriv.X;
            gxx = deriv.X / MathF.Abs(deriv.X);
        }

        return (180f - MathF.Atan(gyx) * Mathf.RadToDeg + 90f * gxx) / 360f;
    }
}

