using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Veldrid;

namespace Kyvos.Graphics.Vertrices;

public interface IVertex 
{
    //TODO make abstract static functions with .net 7
     VertexLayoutDescription LayoutDescription { get; }
     uint SizeInBytes { get; }
}

public struct VertexPositionColor : IVertex, IEquatable<VertexPositionColor>
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

    public VertexLayoutDescription LayoutDescription => Description;

    uint IVertex.SizeInBytes => SizeInBytes;

    public bool Equals(VertexPositionColor other) 
        => Position.Equals(other.Position) && Color.Equals(other.Color);

    public override bool Equals(object? obj) 
        => obj is VertexPositionColor color && Equals(color);

    public static bool operator ==(VertexPositionColor left, VertexPositionColor right)
        => left.Equals(right);

    public static bool operator !=(VertexPositionColor left, VertexPositionColor right) 
        => !(left == right);

    public override int GetHashCode()
    {
        return HashCode.Combine(Position,Color);
    }
}
