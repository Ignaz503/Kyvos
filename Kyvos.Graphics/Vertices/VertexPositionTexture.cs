using System.Numerics;
using Veldrid;

namespace Kyvos.Graphics.Vertrices;

public struct VertexPositionTexture : IVertex, IEquatable<VertexPositionTexture>
{
    public Vector3 Position;
    public Vector2 TexCoords;

    public VertexPositionTexture(Vector3 position, Vector2 uV)
    {
        Position = position;
        TexCoords = uV;
    }

    public const uint SizeInBytes = 20;

    public static readonly VertexLayoutDescription Description = new(
            new VertexElementDescription(nameof(Position), VertexElementFormat.Float3, VertexElementSemantic.TextureCoordinate),
            new VertexElementDescription(nameof(TexCoords), VertexElementFormat.Float2, VertexElementSemantic.TextureCoordinate)
            );

    public VertexLayoutDescription LayoutDescription => Description;

    uint IVertex.SizeInBytes => SizeInBytes;

    public bool Equals(VertexPositionTexture other) 
        => Position.Equals(other.Position) && TexCoords.Equals(other.TexCoords);

    public override bool Equals(object? obj) 
        => obj is VertexPositionTexture texture && Equals(texture);

    public static bool operator ==(VertexPositionTexture left, VertexPositionTexture right) 
        => left.Equals(right);

    public static bool operator !=(VertexPositionTexture left, VertexPositionTexture right) 
        => !(left == right);

    public override int GetHashCode()
        => HashCode.Combine(Position, TexCoords); //, nameof(VertexPositionTexture));
}