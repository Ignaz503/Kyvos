using System;
using System.Numerics;

namespace Kyvos.Maths.Topology;
public partial class Vector4Field : BaseField
{

    LockedArray<Vector4> values;


    Sampler sampler;

    public Vector4 this[float x, float y]
    {
        get => sampler.Sample(x, y);
        set => sampler.Set(x, y, value);
    }

    public Vector4 this[int x, int y]
    {
        get => values[Idx(x, y)];
        set => values[Idx(x, y)] = value;
    }

    public Vector4Field(int width, int height, Vector4 defaultValue, float resolution = DefaultResolution, Func<int, int, Vector4> initializer = null)
    : base(width, height, resolution)
    {
        values = new(width * height);
        sampler = new(this, defaultValue);

        if (initializer is not null)
            Initialize(initializer);
        //if not initializer and not 0 default value initialize to that
        else
        {
            for (int i = 0; i < values.Length; i++)
            {
                values[i] = defaultValue;
            }
        }

    }

    public Vector4Field(int width, int height, float resolution = DefaultResolution, Func<int, int, Vector4> initializer = null)
    : base(width, height, resolution)
    {
        values = new(width * height);
        sampler = new(this, Vector4.Zero);

        if (initializer is not null)
            Initialize(initializer);
    }

    public void Initialize(Func<int, int, Vector4> initializer)
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var idx = Idx(x, y);
                values[idx] = initializer(x, y);
            }
        }

    }

    public void Change(float x, float y, Vector4 delta)
        => sampler.Change(x, y, delta);


    public void Blur()
        => sampler.Blur();

}


