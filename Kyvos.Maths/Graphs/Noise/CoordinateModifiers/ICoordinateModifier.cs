using System.Numerics;

namespace Kyvos.Maths.Graphs.Noise.CoordinateModifiers;
public interface ICoordinateModifier
{
    Vector3 Modify(Vector3 coords);
    Vector2 Modify(Vector2 coords);
}

