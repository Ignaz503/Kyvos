using System;
/// <summary>
/// as mostly seen here
/// http://paulbourke.net/miscellaneous/interpolation/
/// </summary>


namespace Kyvos.Maths;
public static class Interpolation
{
    public static float Linear(float a, float b, float t)
        => (a * (1.0f - t) + b * t);

    public static float Cosine(float a, float b, float t)
    {
        float t2 = (1.0f - MathF.Cos(t * MathF.PI)) / 2.0f;
        return (a * (1.0f - t2)) + b * t2;
    }

    public static float SmoothStep(float a, float b, float t)
        => (b - a) * (3.0f - t * 2.0f) * t * t + a;

    public static float SmootherStep(float a, float b, float t)
        => (b - a) * ((t * (t * 6.0f - 15.0f) + 10.0f) * t * t * t) + a;

    public static float Cubic(float a0, float a1, float a2, float a3, float t)
    {
        float tsquared = t * t;
        float val0 = a3 - a2 - a0 + a1;
        float val1 = a0 - a1 - val0;
        float val2 = a2 - a0;
        return val0 * t * tsquared + val1 * tsquared + val2 * t + a1;
    }
    public static float CatumllRom(float a0, float a1, float a2, float a3, float t)
    {
        float tSquared = t * t;
        float val0 = -0.5f * a0 + 1.5f * a1 - 1.5f * a2 + 0.5f * a3;
        float val1 = a0 - 2.5f * a1 + 2.0f * a2 - 0.5f * a3;
        float val2 = -0.5f * a0 + 0.5f * a2;
        return val0 * t * tSquared + val1 * tSquared + val2 * t + a1;
    }

    public static float Hermite(float a0, float a1, float a2, float a3, float t, float tension, float bias)
    {
        float tSquared = t * t;
        float tCubed = tSquared * t;

        float m0 = (a1 - a0) * (1.0f + bias) * (1.0f - tension) / 2.0f;
        m0 += (a2 - a1) * (1.0f - bias) * (1.0f - tension) / 2.0f;
        float m1 = (a2 - a1) * (1.0f + bias) * (1.0f - tension) / 2.0f;
        m1 += (a3 - a2) * (1.0f - bias) * (1.0f - tension) / 2.0f;
        float val0 = 2.0f * tCubed - 3.0f * tSquared + 1.0f;
        float val1 = tCubed - 2.0f * tSquared + t;
        float val2 = tCubed - tSquared;
        float val3 = -2.0f * tCubed + 3.0f * tSquared;

        return (val0 * a1 + val1 * m0 + val2 * m1 + val3 * a2);

    }
}

