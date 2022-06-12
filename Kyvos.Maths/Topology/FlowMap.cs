using System;
using System.Numerics;
using System.Threading.Tasks;
/// <summary>
/// as mostly seen here
///https://github.com/Scrawk/Terrain-Topology-Algorithms/blob/master/Assets/TerrainTopology/Scripts/CreateFlowMap.cs
/// </summary>
namespace Kyvos.Maths.Topology;
public static class FlowMap
{
    private const float DefaultTimeStep = 0.2f;
    private const int DefaultIterations = 5;
    const float DefautlWaterAmount = 0.0001f;

    public static FloatField Calculate(FloatField map, int iterations = DefaultIterations, float timeStep = DefaultTimeStep)
    {
        FloatField watermap = new FloatField(map.Width, map.Height, map.Resolution);
        FillWatermap(watermap);
        Vector4Field outFlow = new Vector4Field(map.Width, map.Height, map.Resolution);

        for (int i = 0; i < iterations; i++)
        {
            ComputeOutFlow(watermap, outFlow, map, timeStep);
            UpdateWaterMap(watermap, outFlow, timeStep);
        }

        FloatField velocityMap = new(map.Width, map.Height, map.Resolution);

        CalculateVelocityMap(velocityMap, outFlow);
        NormalizeVelocityMap(velocityMap);

        return velocityMap;
    }

    private static void NormalizeVelocityMap(FloatField velocityMap)
    {
        const float epsilon = 1e-12f;
        float min = float.MaxValue;
        float max = float.MinValue;
        for (int x = 0; x < velocityMap.Width; x++)
        {
            for (int y = 0; y < velocityMap.Height; y++)
            {
                float value = velocityMap[x, y];
                if (value < min)
                    min = value;
                if (value > max)
                    max = value;
            }
        }
        float size = max - min;
        Parallel.For(0, velocityMap.Width * velocityMap.Height, i =>
        {
            var value = velocityMap[i];
            if (size < epsilon)
                value = 0;
            else
                value = (value - min) / size;
            velocityMap[i] = value;
        });
    }

    private static void CalculateVelocityMap(FloatField velocityMap, Vector4Field outFlow)
    {

        Parallel.For(0, velocityMap.Width * velocityMap.Height, i =>
        {
            var (x, y) = Indexing.OneDimToTwoDim(i, velocityMap.Width);
            float dl = (x == 0) ? 0f : outFlow[x - 1, y].Y - outFlow[x, y].X;
            float dr = (x == velocityMap.Width - 1) ? 0f : outFlow[x, y].Y - outFlow[x + 1, y].X;
            float dt = (y == velocityMap.Height - 1) ? 0f : outFlow[x, y + 1].Z - outFlow[x, y].W;
            float db = (y == 0) ? 0f : outFlow[x, y].Z - outFlow[x, y - 1].W;
            float vx = (dl + dr) * 0.5f;
            float vy = (db + dt) * 0.5f;
            velocityMap[x, y] = MathF.Sqrt(vx * vx + vy * vy);
        });
    }

    private static void UpdateWaterMap(FloatField watermap, Vector4Field outFlow, float timeStep)
    {

        Parallel.For(0, watermap.Width * watermap.Height, i =>
        {
            var (x, y) = Indexing.OneDimToTwoDim(i, watermap.Width);

            var flow = outFlow[x, y];
            var flowOut = flow.X + flow.Y + flow.Z + flow.W;
            var flowIn = (x == 0) ? 0.0f : outFlow[x - 1, y].Y;
            flowIn += (x == watermap.Width - 1) ? 0.0f : outFlow[x + 1, y].X;
            flowIn += (y == 0) ? 0f : outFlow[x, y - 1].W;
            flowIn += (y == watermap.Height - 1) ? 0f : outFlow[x, y + 1].Z;


            float newWaterHeight = watermap[x, y] + (flowIn - flowOut) * timeStep;
            if (newWaterHeight < 0f)
                newWaterHeight = 0f;
            watermap[x, y] = newWaterHeight;
        });
    }



    private static void ComputeOutFlow(FloatField watermap, Vector4Field outFlow, FloatField map, float timeStep)
    {
        Parallel.For(0, watermap.Width * watermap.Height, i =>
        {
            var (x, y) = Indexing.OneDimToTwoDim(i, map.Width);

            int xPRev = (x == 0) ? 0 : x - 1;
            int xNext = (x == map.Width - 1) ? map.Width - 1 : x + 1;
            int yPrev = (y == 0) ? 0 : y - 1;
            int yNext = (y == map.Height - 1) ? map.Height - 1 : y + 1;

            float waterHeight = watermap[x, y];
            float waterHeight0 = watermap[xPRev, y];
            float waterHeight1 = watermap[xNext, y];
            float waterHeight2 = watermap[x, yPrev];
            float waterHeight3 = watermap[x, yNext];

            float landHeight = map[x, y];
            float landHeight0 = map[xPRev, y];
            float landHeight1 = map[xNext, y];
            float landHeight2 = map[x, yPrev];
            float landHeight3 = map[x, yNext];

            float diff0 = (waterHeight + landHeight) - (waterHeight0 + landHeight0);
            float diff1 = (waterHeight + landHeight1) - (waterHeight1 + landHeight1);
            float diff2 = (waterHeight + landHeight1) - (waterHeight2 + landHeight2);
            float diff3 = (waterHeight + landHeight1) - (waterHeight3 + landHeight3);

            var curFlow = outFlow[x, y];
            float flow0 = MathF.Max(0, curFlow.X + diff0);
            float flow1 = MathF.Max(0, curFlow.Y + diff1);
            float flow2 = MathF.Max(0, curFlow.Z + diff2);
            float flow3 = MathF.Max(0, curFlow.W + diff3);

            float sum = flow0 + flow1 + flow2 + flow3;
            var res = Vector4.Zero;
            if (sum > 0f)
            {
                var K = waterHeight / (sum * timeStep);
                if (K > 1f)
                    K = 1f;
                if (K < 0f)
                    K = 0f;

                res = new(
                 flow0 * K,
                 flow1 * K,
                 flow2 * K,
                 flow3 * K);
            }

            outFlow[x, y] = res;

        });

    }

    static void FillWatermap(FloatField map, float amount = DefautlWaterAmount)
    {
        Parallel.For(0, map.Width * map.Height, i =>
        {
            map[i] = amount;
        });

    }


}

