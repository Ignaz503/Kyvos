namespace Kyvos.WaveFunctionCollapse;

public enum Dimensions 
{
    TwoD,
    ThreeD
}

public static class DimensionsExtensions
{
    public static int MaxNumberDirections(this Dimensions dim)
        => dim switch
        {
            Dimensions.TwoD => Direction.MAX2D + 1,
            Dimensions.ThreeD => Direction.MAX3D + 1,
            _ => throw new InvalidDataException()
        };
}