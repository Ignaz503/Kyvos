using System.Collections.Generic;
using Veldrid;

namespace Kyvos.VeldridIntegration.Materials;

public struct MaterialDescription
{
    public string Name { get; init; }
    public ShaderProgramDescription ShaderSetDescription { get; init; }
    public VertexLayoutDescription[] VertexLayouts { get; init; }
    public SpecializationConstant[] SpecializationConstants { get; init; }
    public BlendStateDescription BlendStateDescription { get; init; }
    public DepthStencilStateDescription DepthStencilStateDescription { get; init; }
    public RasterizerStateDescription RasterizerStateDescription { get; init; }
    public PrimitiveTopology PrimitiveTopology { get; init; }
    public Material.PropertySetDescription[] PropertySetDescription { get; init; }
    public OutputDescription OutputDescription { get; init; }

    internal Dictionary<uint, Material.PropertySet> BuildPropertieSets(GraphicsDevice gfxDevice) 
    {
        if (PropertySetDescription == null)
            return new();

        var sets = new Dictionary<uint, Material.PropertySet>(PropertySetDescription.Length);
        foreach (var description in PropertySetDescription)
            sets.Add(description.SetIndex, Material.PropertySet.Manager.Get(description, gfxDevice));
        return sets;
    }

    internal ResourceLayout[] GetResourceLayouts(GraphicsDevice gfxDevice) 
    {
        var layouts = new ResourceLayout[PropertySetDescription.Length];
        int idx = 0;
        foreach(var description in PropertySetDescription)//?? do they need to be in order?
            layouts[idx++] = description.GetResourceLayout(gfxDevice);

        return layouts;

    }

}
