using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Veldrid;

namespace Kyvos.Graphics.Vertrices;
public struct VertexPositionColor
{
    public Vector3 Position;
    public RgbaFloat Color;

    public VertexPositionColor(Vector3 position, RgbaFloat color)
    {
        Position = position;
        Color = color;
    }

    public const uint SizeInBytes = 28;

    public static readonly VertexLayoutDescription Description = new(
                new VertexElementDescription(nameof(Position), VertexElementFormat.Float3, VertexElementSemantic.TextureCoordinate),
                new VertexElementDescription(nameof(Color), VertexElementFormat.Float4, VertexElementSemantic.TextureCoordinate)
                );
}

