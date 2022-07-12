using Xunit;
using FluentAssertions;
using Kyvos.WaveFunctionCollapse;
using System.Collections;

namespace Kyvos.Tests.WaveCollapse;

public class SocketDirectionTests
{

    [Theory]
    [ClassData(typeof(TransformationTests))]
    public void SocketDirectionTransformations(Direction inDir, Direction expected, Func<Direction, Direction> transformation) 
        => transformation(inDir).Should().BeEquivalentTo(expected);
}

public class TransformationTests : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[] { Direction.UP, Direction.DOWN, (Direction dir) => Direction.Opposite(dir) };
        yield return new object[] { Direction.DOWN, Direction.UP, (Direction dir) => Direction.Opposite(dir) };
        yield return new object[] { Direction.RIGHT, Direction.LEFT, (Direction dir) => Direction.Opposite(dir) };
        yield return new object[] { Direction.LEFT, Direction.RIGHT, (Direction dir) => Direction.Opposite(dir) };
        yield return new object[] { Direction.AWAY, Direction.TOWARDS, (Direction dir) => Direction.Opposite(dir) };
        yield return new object[] { Direction.TOWARDS, Direction.AWAY, (Direction dir) => Direction.Opposite(dir) };


        yield return new object[] { Direction.UP, Direction.RIGHT, (Direction dir) => Direction.RotateClockwiseZAxis(dir) };
        yield return new object[] { Direction.RIGHT, Direction.DOWN, (Direction dir) => Direction.RotateClockwiseZAxis(dir) };
        yield return new object[] { Direction.DOWN, Direction.LEFT, (Direction dir) => Direction.RotateClockwiseZAxis(dir) };
        yield return new object[] { Direction.LEFT, Direction.UP, (Direction dir) => Direction.RotateClockwiseZAxis(dir) };
        yield return new object[] { Direction.AWAY, Direction.AWAY, (Direction dir) => Direction.RotateClockwiseZAxis(dir) };
        yield return new object[] { Direction.TOWARDS, Direction.TOWARDS, (Direction dir) => Direction.RotateClockwiseZAxis(dir) };

        yield return new object[] { Direction.UP, Direction.LEFT, (Direction dir) => Direction.RotateCounterClockwiseZAxis(dir) };
        yield return new object[] { Direction.LEFT, Direction.DOWN, (Direction dir) => Direction.RotateCounterClockwiseZAxis(dir) };
        yield return new object[] { Direction.DOWN, Direction.RIGHT, (Direction dir) => Direction.RotateCounterClockwiseZAxis(dir) };
        yield return new object[] { Direction.RIGHT, Direction.UP, (Direction dir) => Direction.RotateCounterClockwiseZAxis(dir) };
        yield return new object[] { Direction.AWAY, Direction.AWAY, (Direction dir) => Direction.RotateCounterClockwiseZAxis(dir) };
        yield return new object[] { Direction.TOWARDS, Direction.TOWARDS, (Direction dir) => Direction.RotateCounterClockwiseZAxis(dir) };

        yield return new object[] { Direction.UP, Direction.UP, (Direction dir) => Direction.RotateClockwiseYAxis(dir) };
        yield return new object[] { Direction.DOWN, Direction.DOWN, (Direction dir) => Direction.RotateClockwiseYAxis(dir) };
        yield return new object[] { Direction.LEFT, Direction.AWAY, (Direction dir) => Direction.RotateClockwiseYAxis(dir) };
        yield return new object[] { Direction.RIGHT, Direction.TOWARDS, (Direction dir) => Direction.RotateClockwiseYAxis(dir) };
        yield return new object[] { Direction.AWAY, Direction.RIGHT, (Direction dir) => Direction.RotateClockwiseYAxis(dir) };
        yield return new object[] { Direction.TOWARDS, Direction.LEFT, (Direction dir) => Direction.RotateClockwiseYAxis(dir) };

        yield return new object[] { Direction.UP, Direction.UP, (Direction dir) => Direction.RotateCounterClockwiseYAxis(dir) };
        yield return new object[] { Direction.DOWN, Direction.DOWN, (Direction dir) => Direction.RotateCounterClockwiseYAxis(dir) };
        yield return new object[] { Direction.LEFT, Direction.TOWARDS, (Direction dir) => Direction.RotateCounterClockwiseYAxis(dir) };
        yield return new object[] { Direction.RIGHT, Direction.AWAY, (Direction dir) => Direction.RotateCounterClockwiseYAxis(dir) };
        yield return new object[] { Direction.AWAY, Direction.LEFT, (Direction dir) => Direction.RotateCounterClockwiseYAxis(dir) };
        yield return new object[] { Direction.TOWARDS, Direction.RIGHT, (Direction dir) => Direction.RotateCounterClockwiseYAxis(dir) };

        yield return new object[] { Direction.UP, Direction.AWAY, (Direction dir) => Direction.RotateClockwiseXAxis(dir) };
        yield return new object[] { Direction.DOWN, Direction.TOWARDS, (Direction dir) => Direction.RotateClockwiseXAxis(dir) };
        yield return new object[] { Direction.LEFT, Direction.LEFT, (Direction dir) => Direction.RotateClockwiseXAxis(dir) };
        yield return new object[] { Direction.RIGHT, Direction.RIGHT, (Direction dir) => Direction.RotateClockwiseXAxis(dir) };
        yield return new object[] { Direction.AWAY, Direction.DOWN, (Direction dir) => Direction.RotateClockwiseXAxis(dir) };
        yield return new object[] { Direction.TOWARDS, Direction.UP, (Direction dir) => Direction.RotateClockwiseXAxis(dir) };

        yield return new object[] { Direction.UP, Direction.TOWARDS, (Direction dir) => Direction.RotateCounterClockwiseXAxis(dir) };
        yield return new object[] { Direction.DOWN, Direction.AWAY, (Direction dir) => Direction.RotateCounterClockwiseXAxis(dir) };
        yield return new object[] { Direction.LEFT, Direction.LEFT, (Direction dir) => Direction.RotateCounterClockwiseXAxis(dir) };
        yield return new object[] { Direction.RIGHT, Direction.RIGHT, (Direction dir) => Direction.RotateCounterClockwiseXAxis(dir) };
        yield return new object[] { Direction.AWAY, Direction.UP, (Direction dir) => Direction.RotateCounterClockwiseXAxis(dir) };
        yield return new object[] { Direction.TOWARDS, Direction.DOWN, (Direction dir) => Direction.RotateCounterClockwiseXAxis(dir) };

    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
