using Veldrid;
using Veldrid.SPIRV;

namespace Kyvos.VeldridIntegration.Materials;

public struct ShaderProgramDescription
{
    public ShaderStageDescription VertexShaderDescription { get; init; }
    public ShaderStageDescription FragmentShaderDescription { get; init; } 

    //TODO tesselationa and geometry shader support
    public Shader[] GetShaderSet(GraphicsDevice gfxDevice)
    {
        return gfxDevice.ResourceFactory.CreateFromSpirv(
            new Veldrid.ShaderDescription(ShaderStages.Vertex,VertexShaderDescription.GetShaderCode(),VertexShaderDescription.EntryPoint),
            new Veldrid.ShaderDescription(ShaderStages.Fragment,FragmentShaderDescription.GetShaderCode(),FragmentShaderDescription.EntryPoint));
    }
}
