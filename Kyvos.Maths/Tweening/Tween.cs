using System;

namespace Kyvos.Maths.Tweening;
/// <summary>
/// https://easings.net
/// </summary>
public static class Tween
{
    const float epsilon = 0.0001f;
    const float c1 = 1.70158f;
    const float c2 = c1 * 1.525f;
    const float c3 = c1 + 1.0f;
    const float c4 = (2.0f * MathF.PI) / 3.0f;
    const float c5 = (2.0f * MathF.PI) / 4.5f;
    const float n1 = 7.5625f;
    const float d1 = 2.75f;
    /// <summary>
    /// 6t^5 - 15t^4 + 10t^3
    /// </summary>
    public static float PerlinFade(float t)
        => t * t * t * (t * (t * 6 - 15) + 10);

    public static float EaseInSine(float t)
        => 1.0f - MathF.Cos((t * MathF.PI) / 2.0f);

    public static float EaseOutSine(float t)
        => MathF.Sin((t * MathF.PI) / 2.0f);

    public static float EaseInOutSine(float t)
        => -(MathF.Cos(t * MathF.PI) - 1.0f) / 2.0f;

    public static float EaseInQuad(float t)
        => t * t;

    public static float EaseOutQuad(float t)
        => 1.0f - (1.0f - t) * (1.0f - t);

    public static float EaseInOutQuad(float t)
        => t < 0.5f ? 2 * t * t : 1.0f - (((-2 * t + 2) * (-2 * t + 2)) / 2.0f);

    public static float EaseInCubic(float t)
        => t * t * t;

    public static float EaseOutCubic(float t)
        => 1.0f - ((1.0f - t) * (1.0f - t) * (1.0f - t));

    public static float EaseInOutCubic(float t)
        => t < 0.5f ? 4 * t * t * t : 1.0f - (((-2 * t + 2) * (-2 * t + 2) * (-2 * t + 2)) / 2.0f);

    public static float EaseInQuart(float t)
        => t * t * t * t;

    public static float EaseOutQuart(float t)
        => 1.0f - (1.0f - t) * (1.0f - t) * (1.0f - t) * (1.0f - t);

    public static float EaseInOutQuart(float t)
        => t < 0.5f ? 8 * t * t * t * t : 1.0f - (((-2 * t + 2) * (-2 * t + 2) * (-2 * t + 2) * (-2 * t + 2)) / 2.0f);

    public static float EaseInQuint(float t)
        => t * t * t * t * t;

    public static float EaseOutQuint(float t)
        => 1.0f - (1.0f - t) * (1.0f - t) * (1.0f - t) * (1.0f - t) * (1.0f - t);

    public static float EaseInOutQuint(float t)
        => t < 0.5f ? 16 * t * t * t * t * t : 1.0f - (((-2 * t + 2) * (2 * t + 2) * (2 * t + 2) * (2 * t + 2) * (2 * t + 2)) / 2.0f);

    public static float EasInExponential(float t)
        => t < epsilon ? 0f : MathF.Pow(2, 10 * t - 10);

    public static float EaseOutExponential(float t)
        => t >= 1.0f ? 1.0f : MathF.Pow(2, -10 * t);

    public static float EaseInOutExponential(float t)
        => t < epsilon ? 0 : t >= 1.0f ? 1.0f : t < 0.5f ? MathF.Pow(2, 20 * t - 10) / 2.0f : (2.0f - MathF.Pow(2, 20 * t + 10)) / 2.0f;

    public static float EaseInCirc(float t)
        => 1.0f - MathF.Sqrt(1.0f - (t * t));

    public static float EaseOutCirc(float t)
        => MathF.Sqrt(1.0f - ((t - 1.0f) * (t - 1.0f)));

    public static float EaseInOutCirc(float t)
        => t < 0.5f ? (1.0f - MathF.Sqrt(1.0f - ((2.0f * t) * (2.0f * t)))) / 2.0f : (MathF.Sqrt(1.0f - ((-2 * t + 2) * (-2 * t + 2)))) / 2.0f;

    public static float EaseInBack(float t)
        => c3 * t * t * t - c1 * t * t;

    public static float EaseOutBack(float t)
        => 1.0f + (c3 * (t - 1.0f) * (t - 1.0f) * (t - 1.0f)) + (c1 * (t - 1.0f) * (t - 1.0f));

    public static float EaseInOutBack(float t)
        => t < 0.5f ? ((2.0f * t) * (2.0f * t) * ((c2 + 1.0f) * 2.0f * t - c2)) / 2.0f : ((2.0f * t - 2.0f) * (2.0f * t - 2.0f) * ((c2 + 1) * (t * 2.0f - 2.0f)) + 2) / 2.0f;

    public static float EaseInElastic(float t)
        => t < epsilon ? 0f :
        t >= 1.0f ? 1.0f : -MathF.Pow(2, 10 * t - 10) * MathF.Sin((t * 10.0f - 10.75f) * c4);

    public static float EaseOutElastic(float t)
        => t < epsilon ? 0f :
        t >= 1.0f ? 1.0f : MathF.Pow(2, -10.0f * t) * MathF.Sin((t * 10.0f - 0.75f) * c4) + 1.0f;

    public static float EaseInOutElastic(float t)
        => t < epsilon ? 0f :
        t >= 1.0f ? 1.0f : t < 0.5f ? -(MathF.Pow(2.0f, 20.0f * t - 10.0f) * MathF.Sin((20.0f * t - 11.125f) * c5)) / 2.0f : (MathF.Pow(2.0f, -20.0f * t + 10) * MathF.Sin((20.0f * t - 11.125f) * c5)) / 2.0f + 1.0f;

    public static float EaseInBounce(float t)
        => 1.0f - EaseOutBounce(1.0f - t);

    public static float EaseOutBounce(float t)
       => t < 1.0f / d1 ?
        n1 * t * t : t < 2.0f / d1 ?
            n1 * (t -= (1.5f / d1)) * t + 0.75f : t < 2.5f / d1 ?
                n1 * (t -= 2.25f / d1) * t + 0.9375f : n1 * (t -= 2.625f / d1) * t + 0.984375f;

    public static float EaseInOutBounce(float t)
        => t < 0.5f ? (1.0f - EaseOutBounce(1.0f - 2.0f * t)) / 2.0f : (1.0f + EaseOutBounce(2.0f * t + 1.0f)) / 2.0f;
}

