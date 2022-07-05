using System;
using System.Collections.Generic;
using System.Linq;
using Veldrid;
using Kyvos.Utility;
using Kyvos.VeldridIntegration;
using Kyvos.Core.Logging;
using Kyvos.Assets;

namespace Kyvos.Graphics.Materials;

//TODO The descriptor set number 0 will be used for engine-global resources, and bound once per frame. The descriptor set number 1 will be used for per-pass resources, and bound once per pass. The descriptor set number 2 will be used for material resources, and the number 3 will be used for per-object resources. This way, the inner render loops will only be binding descriptor sets 2 and 3, and performance will be high.

//can remove global local idx


public partial class Material : IDisposable
{
    internal static event Action<AssetIdentifier>? OnNoReference;

    internal AssetIdentifier Name;
    Dictionary<uint,PropertySet> propertySets;
    Pipeline piplineStateObject;
    ReferenceCounter refCounter;

    bool isCleanedUp = false;

    internal Material(MaterialDescription description, CreationContext ctx)
    {
        refCounter = new ReferenceCounter(0);
        Name = description.Name;
        var gfxDevice = ctx.GfxDevice;
        propertySets = description.BuildPropertieSets(ctx);

        var shaderSet = new Veldrid.ShaderSetDescription(
            description.VertexLayouts,
            description.ShaderSetDescription.GetShaderSet(gfxDevice)
            );

        piplineStateObject = gfxDevice.ResourceFactory.CreateGraphicsPipeline(
            new GraphicsPipelineDescription(
                description.BlendStateDescription,
                description.DepthStencilStateDescription,
                description.RasterizerStateDescription,
                description.PrimitiveTopology,
                shaderSet,
                description.GetResourceLayouts(gfxDevice),
                description.OutputDescription));
        
    }

    PropertySet GetSet(uint idx) 
    {
#if DEBUG
        if (!propertySets.ContainsKey(idx))
            throw new NonExistentPropertySetIdx(Name, idx);
#endif
        return propertySets[idx];
    }

    public void Use(CommandList cmdList)
    {
        cmdList.SetPipeline(piplineStateObject);
        foreach(var set in propertySets.Values)
            set.Use(cmdList);
    }

    public void Update<T>(uint idx, string propertyName, ref T Data, CommandList cmdList, uint byteOffset = 0)
        where T : struct
    {
        GetSet(idx).Update(propertyName, ref Data, cmdList, byteOffset);
    }
    public void Update<T>(uint idx, string propertyName, ref T Data, GraphicsDevice gfxDevie, uint byteOffset = 0)
    where T : struct
    {
        GetSet(idx).Update(propertyName, ref Data, gfxDevie, byteOffset);
    }

    public void Update<T>(uint idx, string propertyName, T Data, CommandList cmdList, uint byteOffset = 0)
    where T : struct
    {
        GetSet(idx).Update(propertyName, ref Data, cmdList, byteOffset);
    }
    public void Update<T>(uint idx, string propertyName, T Data, GraphicsDevice gfxDevie, uint byteOffset = 0)
    where T : struct
    {
        GetSet(idx).Update(propertyName, ref Data, gfxDevie, byteOffset);
    }

    public void Update<T>(uint idx, string propertyName, TextureHandle<T> texture, GraphicsDevice gfxDevice)
        where T : TextureData
    {
        GetSet(idx).Update(propertyName, texture, gfxDevice);
    }

    public void Update(uint idx, string propertyName, Sampler sampler, GraphicsDevice gfxDevice)
    {
        GetSet(idx).Update(propertyName, sampler, gfxDevice);
    }
    PropertySet FindSetForProperty(string name)
    {
        foreach (var property in propertySets.Values)
            if (property.HasProperty(name))
                return property;

        throw new NonExistentPropertyException(name);
    }
    public void Update<T>(string propertyName, ref T Data, CommandList cmdList, uint byteOffset = 0)
        where T : struct
    {
        FindSetForProperty(propertyName).Update(propertyName, ref Data, cmdList, byteOffset);
    }
    public void Update<T>(string propertyName, ref T Data, GraphicsDevice gfxDevie, uint byteOffset = 0)
        where T : struct
    {
        FindSetForProperty(propertyName).Update(propertyName, ref Data, gfxDevie, byteOffset);
    }

    public void Update<T>(string propertyName, TextureHandle<T> texture, GraphicsDevice gfxDevice)
        where T : TextureData
    {
        FindSetForProperty(propertyName).Update(propertyName, texture, gfxDevice);
    }

    public void Update(string propertyName, Sampler sampler, GraphicsDevice gfxDevice)
    {
        FindSetForProperty(propertyName).Update(propertyName, sampler, gfxDevice);
    }
    internal Material Reference()
    {
        refCounter.Increment();
        return this;
    }

    internal void DisposeInternal()
    {
        if (isCleanedUp)
            return;

        Log<Material>.Debug("Disposing material {Name}", Name);

        foreach (var set in propertySets.Values)
            set.Dispose();
        piplineStateObject.Dispose();

        isCleanedUp = true;
    }

    public void Dispose()
    {
        var c = refCounter.Decrement();
        if (c > 0)
            return;
        OnNoReference?.Invoke(Name);
    }
}