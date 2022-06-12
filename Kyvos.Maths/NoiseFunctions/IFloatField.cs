
namespace Kyvos.Maths.NoiseFunctions;
public interface IFloatField
{
    public int Size { get; }

    (float min, float max) MinMax();

    float this[int x, int y]
    {
        get; set;
    }
}