using System;
using System.Diagnostics;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Kyvos.Maths.Topology;
public partial class FloatField : BaseField
{

    LockedArray<float> values;

    Sampler sampler;

    public float this[float x, float y]
    {
        get => sampler.Sample(x, y);
        set => sampler.Set(x, y, value);
    }

    public float this[int x, int y]
    {
        get => values[Idx(x, y)];
        set => values[Idx(x, y)] = value;
    }

    public float this[int idx]
    {
        get => values[idx];
        set => values[idx] = value;
    }

    public FloatField(int width, int height, float resolution = DefaultResolution, Func<int, int, float> initializer = null, float defaultValue = 0f)
        : base(width, height, resolution)
    {

        values = new(width * height);
        sampler = new(this, defaultValue);

        if (initializer is not null)
            Initialize(initializer);
        //if not initializer and not 0 default value initialize to that
        else if (!Mathf.AlmostEquals(defaultValue, 0f))
        {
            Parallel.For(0, width * height, i =>
            {
                values[i] = defaultValue;
            });
            //for (int i = 0; i < values.Length; i++)
            //{
            //    values[i] = defaultValue;
            //}
        }

    }

    public void Initialize(Func<int, int, float> initializer)
    {
        Parallel.For(0, width * height, i =>
            {
                var (x, y) = Indexing.OneDimToTwoDim(i, width);
                values[i] = initializer(x, y);
            });

        //for (int x = 0; x < width; x++)
        //{
        //    for (int y = 0; y < height; y++)
        //    {
        //        var idx = Idx(x,y);
        //        values[idx] = initializer( x, y );
        //    }
        //}

    }

    public void Change(float x, float y, float delta)
        => sampler.Change(x, y, delta);

    public Vector3 NormalAtPoint(float x, float y)
    {
        var rDoubled = -(resolution + resolution);
        var left = sampler.Sample(x - resolution, y);
        var top = sampler.Sample(x, y - resolution);
        var right = sampler.Sample(x + resolution, y);
        var bottom = sampler.Sample(x, y + resolution);

        return Vector3.Normalize(new(rDoubled * (right - left), rDoubled * rDoubled, rDoubled * (bottom - top)));
    }


    public Vector2 GetFirstOrderDerivative(float x, float y)
    {
        var rDoubled = 1;
        float z1 = sampler.Sample(x - resolution, y + resolution);
        float z2 = sampler.Sample(x, y + resolution);
        float z3 = sampler.Sample(x + resolution, y + resolution);
        float z4 = sampler.Sample(x - resolution, y);
        //float z5 = sampler.Sample(x , y );
        float z6 = sampler.Sample(x + resolution, y);
        float z7 = sampler.Sample(x - resolution, y - resolution);
        float z8 = sampler.Sample(x, y - resolution);
        float z9 = sampler.Sample(x + resolution, y - resolution);

        return CalcFirstOrderGradient(rDoubled, z1, z2, z3, z4, z6, z7, z8, z9);
    }

    private Vector2 CalcFirstOrderGradient(float rDoubled, float z1, float z2, float z3, float z4, float z6, float z7, float z8, float z9)
    {
        return new(
            -((z3 + z6 + z9 - z1 - z4 - z7) / (6.0f * rDoubled)),
            -((z1 + z2 + z3 - z7 - z8 - z9) / (6.0f * rDoubled)));
    }

    public Vector3 GetSecondOrderDerivative(float x, float y)
    {
        var rDoubled = 1;
        float z1 = sampler.Sample(x - resolution, y + resolution);
        float z2 = sampler.Sample(x, y + resolution);
        float z3 = sampler.Sample(x + resolution, y + resolution);
        float z4 = sampler.Sample(x - resolution, y);
        float z5 = sampler.Sample(x, y);
        float z6 = sampler.Sample(x + resolution, y);
        float z7 = sampler.Sample(x - resolution, y - resolution);
        float z8 = sampler.Sample(x, y - resolution);
        float z9 = sampler.Sample(x + resolution, y - resolution);

        return CalcSecondOrderGradient(rDoubled, z1, z2, z3, z4, z5, z6, z7, z8, z9);
    }

    public Vector3 CalcSecondOrderGradient(float rDoubled, float z1, float z2, float z3, float z4, float z5, float z6, float z7, float z8, float z9)
    {
        return new(
            -((z1 + z3 + z4 + z6 + z7 + z9 - 2.0f * (z2 + z5 + z8)) / (3.0f * rDoubled)),
            -((z1 + z2 + z3 + z7 + z8 + z9 - 2.0f * (z4 + z5 + z6)) / (3.0f * rDoubled)),
            -((z3 + z7 - z1 - z9) / (4.0f * rDoubled))
            );
    }

    public void GetDerivatives(float x, float y, out Vector2 firstOder, out Vector3 secondOrder)
    {
        var rDoubled = 1;
        float z1 = sampler.Sample(x - resolution, y + resolution);
        float z2 = sampler.Sample(x, y + resolution);
        float z3 = sampler.Sample(x + resolution, y + resolution);
        float z4 = sampler.Sample(x - resolution, y);
        float z5 = sampler.Sample(x, y);
        float z6 = sampler.Sample(x + resolution, y);
        float z7 = sampler.Sample(x - resolution, y - resolution);
        float z8 = sampler.Sample(x, y - resolution);
        float z9 = sampler.Sample(x + resolution, y - resolution);


        firstOder = CalcFirstOrderGradient(rDoubled, z1, z2, z3, z4, z6, z7, z8, z9);
        secondOrder = CalcSecondOrderGradient(rDoubled, z1, z2, z3, z4, z5, z6, z7, z8, z9);

    }

    public void Blur()
        => sampler.Blur();

    public override string ToString()
    {
        StringBuilder b = new();

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (y == height - 1)
                    b.AppendLine($"{this[x, y]}");
                else
                    b.Append($"{this[x, y]}").Append(", ");
            }
        }
        return b.ToString();
    }

    public static FloatField AbsoluteDiff(FloatField lhs, FloatField rhs)
    {
        Debug.Assert(lhs.Width == rhs.Width && lhs.Height == rhs.Height);
        FloatField absDiff = new(lhs.width, lhs.height, lhs.resolution);

        Parallel.For(0, lhs.Width * lhs.Height, (i) =>
        {
            var (x, y) = Indexing.OneDimToTwoDim(i, lhs.Width);
            absDiff[x, y] = MathF.Abs(lhs[x, y] - rhs[x, y]);
        });

        return absDiff;
    }
}


