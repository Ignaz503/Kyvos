using Veldrid;
using System.Numerics;

namespace FullyAutomatedGayLuxurySpaceCommunism
{
    struct VertexPositionColor 
    {
        public Vector3 Position;
        public RgbaFloat Color;

        public VertexPositionColor(Vector3 position, RgbaFloat color)
        {
            Position = position;
            Color = color;
        }

        public const uint SizeInBytes = 28;
    }
}
