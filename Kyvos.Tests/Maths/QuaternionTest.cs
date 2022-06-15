using Kyvos.Maths;
using System.Numerics;

namespace Kyvos.Tests.Maths;
public class QuaternionTest
{

    [Fact]
    public void QuaternionToEuler()
    {
        var euler = Kyvos.Maths.Random.RandomVecOnUnitSphere;

        var pitch = 35f * Mathf.DegToRad;
        var yaw = 45f * Mathf.DegToRad;
        var roll = 50f * Mathf.DegToRad;

        var rot = Quaternion.CreateFromYawPitchRoll(yaw, pitch, roll);

        var back = rot.ToEuler();
        Console.WriteLine($"{back * Mathf.RadToDeg}");
        Assert.True(Mathf.AlmostEquals(pitch, back.X), $"Pitch: {pitch * Mathf.RadToDeg} != {back.X * Mathf.RadToDeg}");
        Assert.True(Mathf.AlmostEquals(yaw, back.Y), $"Yaw: {yaw * Mathf.RadToDeg} != {back.Y * Mathf.RadToDeg}");
        Assert.True(Mathf.AlmostEquals(roll, back.Z), $"Roll: {roll * Mathf.RadToDeg} != {back.Z * Mathf.RadToDeg}");
    }

    enum SomeEnum
    {
        a, b, c, d, e
    }

    [Fact]
    public void RandomStuff()
    {

    }

}

