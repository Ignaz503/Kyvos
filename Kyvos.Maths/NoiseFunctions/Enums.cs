namespace Kyvos.Maths.NoiseFunctions;
public enum LayeredType
{
    None,
    FBm,
    Ridged,
    PingPong,
    Derivative,
    FbmDerivative
};

public enum TransformType3D
{
    None,
    ImproveXYPlanes,
    ImproveXZPlanes,
    DefaultOpenSimplex2
};

public enum RotationType3D
{
    None,
    ImproveXYPlanes,
    ImproveXZPlanes
};

public enum VornoiDistanceFunction
{
    Euclidean,
    EuclideanSq,
    Manhattan,
    Hybrid
};

public enum VornoiReturnType
{
    CellValue,
    Distance,
    Distance2,
    Distance2Add,
    Distance2Sub,
    Distance2Mul,
    Distance2Div
};

public enum NoiseType
{
    OpenSimplex2,
    OpenSimplex2S,
    Vornoi,
    Perlin,
    ValueCubic,
    Value,
    Cloud
};

