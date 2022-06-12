using Kyvos.Graphics.Vertrices;
using Kyvos.Memory;
using Kyvos.GFX.RayCasting;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Veldrid;
using Veldrid.Utilities;

namespace Kyvos.GFX;

public class MeshPositionColor : IMeshData
{

    private VertexPositionColor[] vertices;
    public VertexPositionColor[] Vertices
    {
        get => vertices;
        set
        {
            vertices = value;
            CalculateBoundingBox();
        }
    }

    public uint[] Triangles { get; set; }

    public int TriangleCount => Triangles.Length;

    BoundingBox box;

    public MeshPositionColor(uint[] triangles, VertexPositionColor[] vertices)
    {
        Triangles = triangles;
        this.vertices = vertices;
        CalculateBoundingBox();
    }

    void CalculateBoundingBox()
    {
        unsafe
        {
            fixed (void* ptr = Vertices)
            {
                box = BoundingBox.CreateFromPoints(
                    (Vector3*)ptr,
                    Vertices.Length,
                    (int)VertexPositionColor.SizeInBytes,
                    Quaternion.Identity,
                    Vector3.Zero,
                    Vector3.One
                    );
            }
        }
    }

    public DeviceBuffer CreateIndexBuffer(GraphicsDevice gfxDevice)
    {
        var buff = gfxDevice.ResourceFactory.CreateBuffer(new BufferDescription((uint)Triangles.Length * Size.Of_U<uint>(), BufferUsage.IndexBuffer));
        gfxDevice.UpdateBuffer(buff, 0, Triangles);
        return buff;
    }

    public DeviceBuffer CreateIndexBuffer(ResourceFactory factory, CommandList cmdList)
    {
        var buff = factory.CreateBuffer(new BufferDescription((uint)Triangles.Length * Size.Of_U<uint>(), BufferUsage.IndexBuffer));
        cmdList.UpdateBuffer(buff, 0, Triangles);
        return buff;
    }

    public DeviceBuffer CreateVertexBuffer(GraphicsDevice gfxDevice)
    {
        var buff = gfxDevice.ResourceFactory.CreateBuffer(new BufferDescription((uint)Vertices.Length * Size.Of_U<VertexPositionColor>(), BufferUsage.VertexBuffer));
        gfxDevice.UpdateBuffer(buff, 0, Vertices);
        return buff;
    }

    public DeviceBuffer CreateVertexBuffer(ResourceFactory factory, CommandList cmdList)
    {
        var buff = factory.CreateBuffer(new BufferDescription((uint)Vertices.Length * Size.Of_U<VertexPositionColor>(), BufferUsage.VertexBuffer));
        cmdList.UpdateBuffer(buff, 0, Vertices);
        return buff;
    }

    public BoundingBox GetBoundingBox()
        => box;

    public bool Raycast(Ray ray, out RayHitInfo hit)
    {
        float dist = float.MaxValue;
        int triangleIdx = -1;
        bool hasHit = false;

        for (int i = 0; i < TriangleCount - 2; i += 3)
        {
            Vector3 v0 = vertices[Triangles[i]].Position;
            Vector3 v1 = vertices[Triangles[i + 1]].Position;
            Vector3 v2 = vertices[Triangles[i + 2]].Position;

            if (ray.Intersects(ref v0, ref v1, ref v2, out float hitDist))
            {
                hasHit = true;
                if (hitDist < dist)
                {
                    triangleIdx = i;
                    dist = hitDist;
                }
            }

        }
        if (hasHit)
            hit = new(triangleIdx, dist);
        else
            hit = RayHitInfo.NoHit;

        return hasHit;
    }

    public int RayCast(Ray ray, ICollection<RayHitInfo> hits)
    {
        int hitCount = 0;

        for (int i = 0; i < TriangleCount - 2; i += 3)
        {
            Vector3 v0 = vertices[Triangles[i]].Position;
            Vector3 v1 = vertices[Triangles[i + 1]].Position;
            Vector3 v2 = vertices[Triangles[i + 2]].Position;

            if (ray.Intersects(ref v0, ref v1, ref v2, out float hitDist))
            {
                hitCount++;

                hits.Add(new(i, hitDist));
            }
        }
        return hitCount;
    }


}
