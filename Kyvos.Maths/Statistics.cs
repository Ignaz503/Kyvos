using System.Collections.Generic;
namespace Kyvos.Maths;
public static class Statistics
{
    public static float Mean(IEnumerable<float> data)
    {
        int count = 0;
        float sum = 0f;
        foreach (var f in data)
        {
            sum += f;
            count++;
        }
        if (count == 0)
            return 0f;
        return sum / count;
    }

    public static float Variance(float mean, IEnumerable<float> data)
    {
        int count = 0;
        float variance = 0f;
        foreach (var f in data)
        {
            float diff = f - mean;
            variance = diff * diff;
            count++;
        }
        if (count == 0)
            return 0f;
        return variance / count;
    }

    public static float Variance(IEnumerable<float> data)
        => Variance(Mean(data), data);

}

