using DefaultEcs;
using Veldrid;
using System.Numerics;
using Kyvos.Core.Memory;
using Kyvos.ECS.Resources;

namespace FullyAutomatedGayLuxurySpaceCommunism
{
    public class MeshResourceManager : GraphicsResourceManager<string, Mesh>
    {
        static readonly VertexPositionColor[] cubeVertices = new VertexPositionColor[]
            {
                // Top
                new VertexPositionColor(new Vector3(-0.5f, +0.5f, -0.5f), RgbaFloat.Red),
                new VertexPositionColor(new Vector3(+0.5f, +0.5f, -0.5f), RgbaFloat.Red),
                new VertexPositionColor(new Vector3(+0.5f, +0.5f, +0.5f), RgbaFloat.Red),
                new VertexPositionColor(new Vector3(-0.5f, +0.5f, +0.5f), RgbaFloat.Red),
                // Bottom                                                             
                new VertexPositionColor(new Vector3(-0.5f,-0.5f, +0.5f),  RgbaFloat.Red),
                new VertexPositionColor(new Vector3(+0.5f,-0.5f, +0.5f),  RgbaFloat.Red),
                new VertexPositionColor(new Vector3(+0.5f,-0.5f, -0.5f),  RgbaFloat.Red),
                new VertexPositionColor(new Vector3(-0.5f,-0.5f, -0.5f),  RgbaFloat.Red),
                // Left                                                               
                new VertexPositionColor(new Vector3(-0.5f, +0.5f, -0.5f), RgbaFloat.Green),
                new VertexPositionColor(new Vector3(-0.5f, +0.5f, +0.5f), RgbaFloat.Green),
                new VertexPositionColor(new Vector3(-0.5f, -0.5f, +0.5f), RgbaFloat.Green),
                new VertexPositionColor(new Vector3(-0.5f, -0.5f, -0.5f), RgbaFloat.Green),
                // Right                                                              
                new VertexPositionColor(new Vector3(+0.5f, +0.5f, +0.5f), RgbaFloat.Green),
                new VertexPositionColor(new Vector3(+0.5f, +0.5f, -0.5f), RgbaFloat.Green),
                new VertexPositionColor(new Vector3(+0.5f, -0.5f, -0.5f), RgbaFloat.Green),
                new VertexPositionColor(new Vector3(+0.5f, -0.5f, +0.5f), RgbaFloat.Green),
                // Back                                                               
                new VertexPositionColor(new Vector3(+0.5f, +0.5f, -0.5f), RgbaFloat.Blue),
                new VertexPositionColor(new Vector3(-0.5f, +0.5f, -0.5f), RgbaFloat.Blue),
                new VertexPositionColor(new Vector3(-0.5f, -0.5f, -0.5f), RgbaFloat.Blue),
                new VertexPositionColor(new Vector3(+0.5f, -0.5f, -0.5f), RgbaFloat.Blue),
                // Front                                                              
                new VertexPositionColor(new Vector3(-0.5f, +0.5f, +0.5f), RgbaFloat.Blue),
                new VertexPositionColor(new Vector3(+0.5f, +0.5f, +0.5f), RgbaFloat.Blue),
                new VertexPositionColor(new Vector3(+0.5f, -0.5f, +0.5f), RgbaFloat.Blue),
                new VertexPositionColor(new Vector3(-0.5f, -0.5f, +0.5f), RgbaFloat.Blue),
            };

        static readonly ushort[] cubeIndices =             {
                0,1,2, 0,2,3,
                4,5,6, 4,6,7,
                8,9,10, 8,10,11,
                12,13,14, 12,14,15,
                16,17,18, 16,18,19,
                20,21,22, 20,22,23,
            };

        public MeshResourceManager(GraphicsDevice gfxD) : base(gfxD)
        { }


        protected override Mesh Load(string info)
        {
            var vertBuff = resourceFactory.CreateBuffer(new BufferDescription(((uint)cubeVertices.Length) * VertexPositionColor.SizeInBytes, BufferUsage.VertexBuffer));
            var indexBuff = resourceFactory.CreateBuffer(new BufferDescription(((uint)cubeIndices.Length) * Size.Of_U<ushort>(), BufferUsage.IndexBuffer));

            gfxDevice.UpdateBuffer(vertBuff, 0, cubeVertices);
            gfxDevice.UpdateBuffer(indexBuff,0, cubeIndices);

            return new(vertBuff, indexBuff);
        }

        protected override void OnResourceLoaded(in Entity entity, string info, Mesh resource)
        {
            entity.Set(resource);
        }
    }

}
