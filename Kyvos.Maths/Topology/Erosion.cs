using MathNet.Numerics.Random;
using System;
using System.Threading.Tasks;

namespace Kyvos.Maths.Topology;
public partial class Erosion
{
    public static class Hydraulic
    {
        internal const int DefaultSeed = 1337;
        internal const float DefaultDroptletRadius = 0.8f;
        internal const int MaxTraceIterations = 80;
        internal const float DefaultDepositionRate = 0.03f;
        internal const float DefualtErosionRate = 0.04f;
        internal const float DefaultIterationScale = 0.04f;
        internal const float DefaultFriction = 0.7f;
        internal const float DefaultSpeed = 0.15f;
        internal const float DefaultDropsPerCel = 0.4f;

        public static void Apply(FloatField toErode, Action<int> log, int seed = DefaultSeed, float dropsPerCel = DefaultDropsPerCel, float dropletRadius = DefaultDroptletRadius, int maxTraceIterations = MaxTraceIterations, float depositionRate = DefaultDepositionRate, float erosionRate = DefualtErosionRate, float iterationScale = DefaultIterationScale, float friction = DefaultFriction, float speed = DefaultSpeed)
        {

            var numDrops = dropsPerCel * toErode.Width * toErode.Height;

            //Random rng = new(seed);
            SystemRandomSource rng = new(seed, true);
            for (int i = 0; i < numDrops; i++)
            {
                log(i);
                float x = (float)rng.NextSingle() * toErode.ResolutionDependendWidth();
                float y = (float)rng.NextSingle() * toErode.ResolutionDependendHeight();

                TraceDroplet(toErode,
                    x,
                    y,
                    rng,
                    dropletRadius,
                    maxTraceIterations,
                    depositionRate,
                    erosionRate,
                    iterationScale,
                    friction,
                    speed);
            }
            toErode.Blur();
        }
        public static void ApplyThreaded(FloatField toErode, Action<int> log, int seed = DefaultSeed, float dropsPerCel = DefaultDropsPerCel, float dropletRadius = DefaultDroptletRadius, int maxTraceIterations = MaxTraceIterations, float depositionRate = DefaultDepositionRate, float erosionRate = DefualtErosionRate, float iterationScale = DefaultIterationScale, float friction = DefaultFriction, float speed = DefaultSpeed)
        {

            var numDrops = Mathf.RoundToInt(dropsPerCel * toErode.Width * toErode.Height);

            //Random rng = new(seed);
            SystemRandomSource rng = new(seed, true);
            //SemaphoreSlim sm = new(1, 1);
            Parallel.For(0, numDrops, (i) =>
            {
                //float x = 0;
                //float y = 0;
                //sm.Wait();
                //try
                //{
                //    x = (float)rng.NextDouble();
                //    y = (float)rng.NextDouble();
                //}
                //finally
                //{
                //    sm.Release();
                //}
                float x = rng.NextSingle();
                float y = rng.NextSingle();
                x *= toErode.ResolutionDependendWidth();
                y *= toErode.ResolutionDependendHeight();
                log(i);
                TraceDroplet(toErode,
                    x,
                    y,
                    rng,
                    dropletRadius,
                    maxTraceIterations,
                    depositionRate,
                    erosionRate,
                    iterationScale,
                    friction,
                    speed);
            });
            toErode.Blur();
        }

        static void TraceDroplet(FloatField map, float x, float y, System.Random rng, float dropletRadius, int maxIterations, float depositionRate, float erosionRate, float iterationScale, float friction, float speed)
        {
            float offsetX = ((float)rng.NextDouble() * 2.0f - 1f) * dropletRadius * map.Resolution;
            float offsetY = ((float)rng.NextDouble() * 2.0f - 1f) * dropletRadius * map.Resolution;

            var sediment = 0f;
            var prevX = x;
            var prevY = y;
            var velX = 0f;
            var velY = 0f;

            for (int i = 0; i < maxIterations; i++)
            {

                var normal = map.NormalAtPoint(x + offsetX, y + offsetY);

                if (Mathf.AlmostEquals(normal.Y, 1f))
                {
                    //map.Change( x, y, sediment );
                    break;
                }//landet in flat surfce

                var deposit = sediment * depositionRate * normal.Y;
                var erosion = erosionRate * (1.0f - normal.Y) * MathF.Min(1f, i * iterationScale);

                map.Change(prevX, prevY, deposit - erosion);

                velX = friction * velX + normal.X * speed * map.Resolution;
                velY = friction * velY + normal.Z * speed * map.Resolution;

                prevX = x;
                prevY = y;
                x += velX;
                y += velY;
                sediment += erosion - deposit;
            }


        }


    }

    public static class Aeolian
    {
        public static void Apply(FloatField toErode)
        {
            throw new NotImplementedException(); 
        }
    }

}

