using Kyvos.Core.Logging;
using System;
using Veldrid;

namespace FullyAutomatedGayLuxurySpaceCommunism
{
    public struct Mesh : IDisposable
    {
        public readonly DeviceBuffer VertexBuffer;
        public readonly DeviceBuffer IndexBuffer;

        public Mesh(DeviceBuffer vertexBuffer, DeviceBuffer meshBuffer)
        {
            this.VertexBuffer = vertexBuffer;
            this.IndexBuffer = meshBuffer;
        }

        public void Dispose()
        {
            Log<Mesh>.Debug("Disposing {Obj}", nameof(Mesh));
            VertexBuffer.Dispose();
            IndexBuffer.Dispose();
        }
    }
}
