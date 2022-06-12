using Veldrid;

namespace FullyAutomatedGayLuxurySpaceCommunism
{
    public struct Mesh 
    {
        public readonly DeviceBuffer VertexBuffer;
        public readonly DeviceBuffer IndexBuffer;

        public Mesh(DeviceBuffer vertexBuffer, DeviceBuffer meshBuffer)
        {
            this.VertexBuffer = vertexBuffer;
            this.IndexBuffer = meshBuffer;
        }
    }
}
