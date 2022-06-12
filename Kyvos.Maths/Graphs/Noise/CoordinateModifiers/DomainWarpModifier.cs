using System.Numerics;

namespace Kyvos.Maths.Graphs.Noise.CoordinateModifiers;
public abstract class DomainWarpModifier<T> : ICoordinateModifier where T : DomainWarpSettings
{
    protected T settings;

    public DomainWarpModifier(T settings)
    {
        this.settings = settings;
    }

    public Vector3 Modify(Vector3 coords)
    {
        float x = coords.X;
        float y = coords.Y;
        float z = coords.Z;

        Warp(ref x, ref y, ref z);

        return new(x, y, z);
    }

    public Vector2 Modify(Vector2 coords)
    {
        float x = coords.X;
        float y = coords.Y;

        Warp(ref x, ref y);

        return new(x, y);
    }

    protected abstract void Warp(ref float x, ref float y, ref float z);
    protected abstract void Warp(ref float x, ref float y);

}

