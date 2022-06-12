using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Veldrid.Utilities;

namespace Kyvos.VeldridIntegration.RayCasting;
public  struct RayHitInfo
{
    public int TiangleIndex;
    public float Distance;

    public RayHitInfo(int triIdx, float distance)
    {
        this.TiangleIndex = triIdx;
        this.Distance = distance;
    }

    public Vector3 Point(Ray r)
        => r.Origin + r.Direction * Distance;

    public Vector3 Point(ref Ray r)
        => r.Origin + r.Direction * Distance;

    public static RayHitInfo NoHit =>
        new(-1, float.NaN);

}

