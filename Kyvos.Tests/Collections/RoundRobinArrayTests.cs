using Xunit.Abstractions;

using Kyvos.Utility.Collections;
using Kyvos.Maths;

namespace Kyvos.Tests.Collections;
public class RoundRobinArrayTests
{
    ITestOutputHelper output;

    public RoundRobinArrayTests(ITestOutputHelper output)
    {
        this.output = output;
    }

    [Fact]
    public void CorrectSize()
    {
        var rr = new RoundRobinArray<int>(10);
        Assert.True(rr.Size == 10);
    }

    [Fact]
    public void SingleAdd()
    {
        var rr = new RoundRobinArray<int>(5);

        rr.Add(4);
        output.WriteLine(rr.ToString());
        Assert.True(rr[0] == 4);
    }

    [Fact]
    public void RoundRobinAdd()
    {
        var rr = new RoundRobinArray<int>(5);

        const int addAmount = 6;

        var expectedOutput = "[ 5, 1, 2, 3, 4 ]";

        for (int i = 0; i < addAmount; i++)
        {
            rr.Add(i);
        }
        var stringified = rr.ToString();
        output.WriteLine(stringified);
        Assert.Equal(expectedOutput, stringified);

    }

    [Fact]
    public void Initlizer()
    {
        var rr = new RoundRobinArray<int>(5, new IndexInitialier());
        output.WriteLine(rr.ToString());
        Assert.True(rr.Head == 0, $"Head: {rr.Head}");
    }

    [Fact]
    public void Loop()
    {
        var init = new RandomValue(0, 5);

        var rr = new RoundRobinArray<int>(5, init);

        var randomAdd = MathF.Min(init.GetForIdx(12),3);

        for (int i = 0; i < randomAdd; i++)
        {
            rr.Add(init.GetForIdx(i));
        }
        output.WriteLine($"Head is at: {rr.Head}");
        output.WriteLine(rr.ToString());

        var res = "";
        for (int i = 0; i < rr.Size; i++)
        {
            res += $"{rr[rr.Head + i]}, ";
        }
        output.WriteLine(res);
        Assert.True(true);
    }

    public struct IndexInitialier : IArrayInitializer<int>
    {
        public int GetForIdx(int idx)
        {
            return idx;
        }
    }

    public struct RandomValue : IArrayInitializer<int> 
    {
        int min;
        int max;

        public RandomValue(int min, int max)
        {
            this.min = min;
            this.max = max;
        }

        public int GetForIdx(int idx)
        {
            var rVal = Kyvos.Maths.Random.Value;
            return Kyvos.Maths.Mathf.RoundToInt(Kyvos.Maths.Mathf.Map(rVal, 0, 1, min, max));
        }
    }
}

