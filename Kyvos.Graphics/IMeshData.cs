using Kyvos.Graphics.RayCasting;
using Veldrid;
using Veldrid.Utilities;

namespace Kyvos.Graphics;

public interface IMeshData 
{
    DeviceBuffer CreateVertexBuffer(GraphicsDevice gfxDevice);
    DeviceBuffer CreateIndexBuffer(GraphicsDevice gfxDevice);

    DeviceBuffer CreateVertexBuffer(ResourceFactory factory, CommandList cmdList);
    DeviceBuffer CreateIndexBuffer(ResourceFactory factory, CommandList cmdList);

    BoundingBox GetBoundingBox();

    bool Raycast(Ray ray, out RayHitInfo hit);
    int RayCast(Ray ray, ICollection<RayHitInfo> hits);

    public int TriangleCount { get; }

}

