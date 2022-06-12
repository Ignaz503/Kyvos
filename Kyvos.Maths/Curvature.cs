

using System;
/// <summary>
/// as mostly seen here
/// https://github.com/Scrawk/Terrain-Topology-Algorithms/blob/master/Assets/TerrainTopology/Scripts/CreateCurvatureMap.cs
/// </summary>
namespace Kyvos.Maths;
public static class Curvature
{
    public static float Plan(float dx, float dy, float dxx, float dyy, float dxy)
    {
        float dxSquared = dx * dx;
        float dySquared = dy * dy;
        float p = dxSquared + dySquared;
        float n = dySquared * dxx - 2.0f * dxy * dx * dy + dxSquared * dyy;
        float d = MathF.Pow(p, 1.5f);
        if (Mathf.AlmostEquals(d, 0))//avoid divison by zero
            return 0;
        return n / d;
    }

    public static float Horizontal(float dx, float dy, float dxx, float dyy, float dxy)
    {
        float dxSquared = dx * dx;
        float dySquared = dy * dy;
        float p = dxSquared + dySquared;
        float n = dySquared * dxx - 2.0f * dxy * dx * dy + dxSquared * dyy;
        float d = p * MathF.Pow(p + 1, 0.5f);
        if (Mathf.AlmostEquals(d, 0))//avoid divison by zero
            return 0;
        return n / d;
    }

    public static float Vertical(float dx, float dy, float dxx, float dyy, float dxy)
    {
        float dxSquared = dx * dx;
        float dySquared = dy * dy;
        float p = dxSquared + dySquared;
        float n = dxSquared * dxx - 2.0f * dxy * dx * dy + dySquared * dyy;
        float d = p * MathF.Pow(p + 1, 1.5f);
        if (Mathf.AlmostEquals(d, 0))//avoid divison by zero
            return 0;
        return n / d;
    }

    public static float Mean(float dx, float dy, float dxx, float dyy, float dxy)
    {
        float dxSquared = dx * dx;
        float dySquared = dy * dy;
        float p = dxSquared + dySquared;

        float n = (1f + dySquared) * dxx - 2.0f * dxy * dx * dy + (1 + dxSquared) * dyy;
        float d = 2 * MathF.Pow(p + 1, 1.5f);

        if (Mathf.AlmostEquals(d, 0))//avoid divison by zero
            return 0;
        return n / d;
    }


    public static float Gaussian(float dx, float dy, float dxx, float dyy, float dxy)
    {
        float dxSquared = dx * dx;
        float dySquared = dy * dy;
        float p = dxSquared + dySquared;
        float n = dxx * dyy - dxy * dxy;
        float d = MathF.Pow(p + 1, 2f);

        if (Mathf.AlmostEquals(d, 0))//avoid divison by zero
            return 0;
        return n / d;
    }

    public static float Minimal(float dx, float dy, float dxx, float dyy, float dxy)
    {
        float h = Mean(dx, dy, dxx, dyy, dxy);
        float k = Gaussian(dx, dy, dxx, dyy, dxy);
        float val = h * h - k;
        if (val <= 0)//avoid sqrt complexity
            return h;
        return h - MathF.Sqrt(val);
    }

    public static float Unsphericity(float dx, float dy, float dxx, float dyy, float dxy)
    {
        float h = Mean(dx, dy, dxx, dyy, dxy);
        float k = Gaussian(dx, dy, dxx, dyy, dxy);
        float val = h * h - k;
        if (val <= 0)//avoid sqrt complexity
            return 0;
        return MathF.Sqrt(val);
    }

    public static float Maximal(float dx, float dy, float dxx, float dyy, float dxy)
    {
        float h = Mean(dx, dy, dxx, dyy, dxy);
        float k = Gaussian(dx, dy, dxx, dyy, dxy);
        float val = h * h - k;
        if (val <= 0)//avoid sqrt complexity
            return h;
        return h + MathF.Sqrt(val);
    }

    public static float Rotor(float dx, float dy, float dxx, float dyy, float dxy)
    {
        float dxSquared = dx * dx;
        float dySquared = dy * dy;
        float p = dxSquared + dySquared;
        float n = (dxSquared * dySquared) * dxy - dx * dy * (dxx - dyy);
        float d = MathF.Pow(p, 1.5f);
        if (Mathf.AlmostEquals(d, 0))//avoid divison by zero
            return 0;
        return n / d;
    }

    public static float Difference(float dx, float dy, float dxx, float dyy, float dxy)
    {
        float v = Vertical(dx, dy, dxx, dyy, dxy);
        float h = Horizontal(dx, dy, dxx, dyy, dxy);
        return 0.5f * (v - h);
    }

    public static float HorizontalExcess(float dx, float dy, float dxx, float dyy, float dxy)
    {
        float h = Horizontal(dx, dy, dxx, dyy, dxy);
        float min = Minimal(dx, dy, dxx, dyy, dxy);
        return h - min;
    }

    public static float VerticalExcess(float dx, float dy, float dxx, float dyy, float dxy)
    {
        float v = Vertical(dx, dy, dxx, dyy, dxy);
        float min = Minimal(dx, dy, dxx, dyy, dxy);
        return v - min;
    }

    public static float Ring(float dx, float dy, float dxx, float dyy, float dxy)
    {
        float mean = Mean(dx, dy, dxx, dyy, dxy);
        float gauss = Gaussian(dx, dy, dxx, dyy, dxy);
        float hori = Horizontal(dx, dy, dxx, dyy, dxy);
        return 2f * mean * hori - hori * hori - gauss;
    }

    public static float Accumulation(float dx, float dy, float dxx, float dyy, float dxy)
    {
        float hori = Horizontal(dx, dy, dxx, dyy, dxy);
        float vert = Vertical(dx, dy, dxx, dyy, dxy);
        return hori * vert;
    }

}