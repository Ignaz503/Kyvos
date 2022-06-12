using NUnit.Framework;
using System.Numerics;
using System;

namespace Kyvos.Maths.Tests
{
    public class QuaternionTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void QuaternionToEuler()
        {
            var euler = Random.RandomVecOnUnitSphere;

            var pitch = 35f * Mathf.DegToRad;
            var yaw = 45f * Mathf.DegToRad;
            var roll = 50f * Mathf.DegToRad;

            var rot = Quaternion.CreateFromYawPitchRoll(yaw, pitch, roll);

            var back = rot.ToEuler();
            Console.WriteLine($"{back * Mathf.RadToDeg}");
            Assert.IsTrue(Mathf.AlmostEquals(pitch, back.X), $"Pitch: {pitch * Mathf.RadToDeg} != {back.X * Mathf.RadToDeg}");
            Assert.IsTrue(Mathf.AlmostEquals(yaw, back.Y), $"Yaw: {yaw * Mathf.RadToDeg} != {back.Y * Mathf.RadToDeg}");
            Assert.IsTrue(Mathf.AlmostEquals(roll, back.Z), $"Roll: {roll * Mathf.RadToDeg} != {back.Z * Mathf.RadToDeg}");
        }

        enum SomeEnum 
        {
            a,b,c,d,e
        }

        [Test]
        public void RandomStuff() 
        {

        }

    }

}