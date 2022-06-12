﻿using NUnit.Framework;
using Kyvos.Maths.Graphs.Noise;
using Kyvos.Maths.Graphs.Noise.Nodes;
using Kyvos.Maths.Graphs.Noise.Nodes.Mathematics;
using System.Numerics;

namespace Kyvos.Maths.Tests
{
    public class NoiseGraphTest
    {
        [SetUp]
        public void Setup()
        {
        }


        [Test]
        public void SomeNodes() 
        {
            var g = NoiseGraph.Builder<string>.Get()
                .CreateNode("sum", () => new AddNode<string>())
                .MarkRoot()
                .CreateNode("2", () => new ConstValueNode<string>(2))
                .InputOf("sum")
                .CreateNode("4", () => new ConstValueNode<string>(4))
                .InputOf("sum")
                .Build();

            var res = g.Evaluate(Vector2.Zero);
            Assert.IsTrue( Mathf.AlmostEquals( res, 6f ) );
        }

        public enum Labels 
        {
            SUM,
            TWO,
            FOUR
        }

        [Test]
        public void SomeNodesEnum()
        {
            var g = NoiseGraph.Builder<Labels>.Get()
                .CreateNode(Labels.SUM, () => new AddNode<Labels>())
                .MarkRoot()
                .CreateNode(Labels.TWO, () => new ConstValueNode<Labels>(2))
                .InputOf(Labels.SUM)
                .CreateNode(Labels.FOUR, () => new ConstValueNode<Labels>(4))
                .InputOf(Labels.SUM)
                .Build();

            var res = g.Evaluate(Vector2.Zero);
            Assert.IsTrue( Mathf.AlmostEquals( res, 6f ) );
        }

    }
}