using Kyvos.Maths;
using System;

namespace Kyvos.Tests.Maths;
public class InterpolationTests
{

    [Fact]
    public void LinearInterpolation()
    {
        float x = 12f;
        float y = 24f;
        float t = 0.5f;

        float expected = 18f;

        var res = Interpolation.Linear(x, y, t);

        Assert.True(Mathf.AlmostEquals(res, expected, .00001f));
    }
}
