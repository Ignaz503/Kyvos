using System;

namespace Kyvos.Maths.Topology;

public class Heightmap : FloatField
{
    public Heightmap(int width, int height, float resolution = 0.1F, Func<int, int, float> initializer = null, float defaultValue = 0) : base(width, height, resolution, initializer, defaultValue)
    {
    }
}


