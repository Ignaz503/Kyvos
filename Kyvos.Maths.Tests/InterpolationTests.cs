using NUnit.Framework;
using Kyvos.Maths;
using System;

namespace Kyvos.Maths.Tests
{
    public class InterpolationTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void LinearInterpolation()
        {
            float x = 12f;
            float y = 24f;
            float t = 0.5f;

            float expected = 18f;

            var res = Interpolation.Linear(x,y,t);


            Assert.That( res, Is.EqualTo( expected ).Within( .00001 ).Ulps );
        }

        enum TestEnum :byte
        {
            A,
            B
        }

        [Test]
        public void EnumTest()
        {
            byte C = 2;
            TestEnum val = (TestEnum)C;
            Console.WriteLine(val);
        }

    }
}