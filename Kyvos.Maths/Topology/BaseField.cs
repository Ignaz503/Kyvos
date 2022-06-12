using System.Numerics;

namespace Kyvos.Maths.Topology;
public abstract class BaseField
{
    internal const float DefaultResolution = 0.1f;

    protected float resolution;
    public float Resolution => resolution;

    protected int width;
    public int Width => width;

    protected int height;
    public int Height => height;

    float scale => 1.0f / resolution;
    public float ResolutionDependendWidth()
        => width * resolution;

    public float ResolutionDependendHeight()
        => height * resolution;

    public (float, float) ResolutionDependentExtens()
        => (ResolutionDependendWidth(), ResolutionDependendHeight());

    protected int Idx(int x, int y)
    {
        return Indexing.TwoDimToOneDim(x, y, width);
    }

    public BaseField(int width, int height, float resolution = DefaultResolution)
    {
        this.width = width;
        this.height = height;
        this.resolution = resolution;
    }

    public void Map01ToLocalSpace(ref float x, ref float y)
    {
        x *= ResolutionDependendWidth();
        y *= ResolutionDependendHeight();
    }

    public void Map01ToLocalSpace(ref Vector2 coords)
    {
        coords.X *= ResolutionDependendWidth();
        coords.Y *= ResolutionDependendHeight();
    }

    public (float x, float y) DiscretePositionToSamplePosition(int x, int y)
    {
        float xT = (float)x / width;
        float yT = (float)y / height;
        Map01ToLocalSpace(ref xT, ref yT);
        return (xT, yT);
    }

    public bool Contains(float x, float y)
    {
        if (x < 0f || y < 0f)
            return false;

        x *= scale;
        y *= scale;


        int xi = Mathf.FloorToInt(x);
        int yi = Mathf.FloorToInt(y);

        if (x >= width - 1 || y >= height - 1)
            return false;
        return true;
    }


}


