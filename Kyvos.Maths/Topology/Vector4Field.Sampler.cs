using System.Numerics;

namespace Kyvos.Maths.Topology;
public partial class Vector4Field
{
    struct Sampler
    {
        Vector4Field map;
        float scale;
        Vector4 defaultValue;
        public Sampler(Vector4Field map, Vector4 defualtValue)
        {
            this.map = map;
            this.scale = 1.0f / map.resolution;
            this.defaultValue = defualtValue;
        }


        bool ValidateAndResolve(ref float x, ref float y, out int xi, out int yi)
        {
            xi = yi = -1;
            if (x < 0f || y < 0f)
                return false;

            x *= scale;
            y *= scale;


            xi = Mathf.FloorToInt(x);
            yi = Mathf.FloorToInt(y);

            if (x >= map.width - 1 || y >= map.height - 1)
                return false;
            return true;
        }

        /// <summary>
        /// samples heightmap
        /// uses bilinear interpolation
        /// </summary>
        public Vector4 Sample(float x, float y)
        {
            if (ValidateAndResolve(ref x, ref y, out int xi, out int yi))
            {
                var xDif = x - xi;
                var yDif = y - yi;

                var valueLT = map.values[map.Idx(xi, yi)]; //left top
                var valueLB = map.values[map.Idx(xi, yi + 1)]; // left bottom

                var valueRT = map.values[map.Idx(xi + 1, yi)]; //right top
                var valueRB = map.values[map.Idx(xi + 1, yi + 1)]; //right bottom

                var valueL = valueLT + (valueLB - valueLT) * yDif;
                var valueR = valueRT + (valueRB - valueRT) * yDif;

                return valueL + (valueR - valueL) * xDif;
            }
            return defaultValue;
        }

        /// <summary>
        /// changes heightmap based on delta
        /// distributes changes via interpolation 
        /// </summary>
        public void Change(float x, float y, Vector4 delta)
        {
            if (ValidateAndResolve(ref x, ref y, out int xi, out int yi))
            {
                var xDif = x - xi;
                var yDif = y - yi;

                map.values[map.Idx(xi, yi)] += xDif * yDif * delta;
                map.values[map.Idx(xi + 1, yi)] += (1.0f - xDif) * yDif * delta;
                map.values[map.Idx(xi, yi + 1)] += xDif * (1.0f - yDif) * delta;
                map.values[map.Idx(xi + 1, yi + 1)] += (1.0f - xDif) * (1.0f - yDif) * delta;
            }
        }

        /// <summary>
        /// sets value of pixel hit, no billinear distribution
        /// </summary>
        /// <param name="x">coord</param>
        /// <param name="y">coord</param>
        /// <param name="value">value</param>
        public void Set(float x, float y, Vector4 value)
        {
            if (ValidateAndResolve(ref x, ref y, out int xi, out int yi))
            {
                map.values[map.Idx(xi, yi)] = value;
            }
        }

        internal void Blur()
        {
            Vector4[] newVals = new Vector4[(map.width - 2) * (map.height - 2)];
            for (int y = 1; y < map.Height - 1; y++)
            {
                for (int x = 1; x < map.width - 1; x++)
                {
                    newVals[x - 1 + (y - 1) * (map.width - 2)] =
                        (
                            map.values[map.Idx(x - 1, y)] +
                            map.values[map.Idx(x, y - 1)] +
                            map.values[map.Idx(x + 1, y)] +
                            map.values[map.Idx(x, y + 1)]
                        ) * 0.125f +
                        (
                        map.values[map.Idx(x - 1, y - 1)] +
                            map.values[map.Idx(x + 1, y - 1)] +
                            map.values[map.Idx(x + 1, y + 1)] +
                            map.values[map.Idx(x - 1, y + 1)]) * 0.0625f +
                        map.values[map.Idx(x, y)] * 0.25f;
                }
            }
        }
    }


}


