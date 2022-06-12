using System;
using System.Collections.Generic;
using System.Linq;
using Veldrid;
using Kyvos.Utility;
using Kyvos.VeldridIntegration;
using Kyvos.Core.Logging;

namespace Kyvos.Graphics.Materials;

public partial class Material : IDisposable
{
    private static event Action<Material>? OnDispose;

    private string name;
    Dictionary<uint,PropertySet> propertySets;
    Pipeline piplineStateObject;
    ReferenceCounter refCounter;

    bool isDisposed = false;

    private Material(MaterialDescription description, GraphicsDevice gfxDevice)
    {
        refCounter = new ReferenceCounter();
        name = description.Name;

        propertySets = description.BuildPropertieSets(gfxDevice);

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
            throw new NonExistentPropertySetIdx(name, idx);
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

    public void Update(uint idx, string propertyName, Texture texture, GraphicsDevice gfxDevice)
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

    public void Update(string propertyName, Texture texture, GraphicsDevice gfxDevice)
    {
        FindSetForProperty(propertyName).Update(propertyName, texture, gfxDevice);
    }

    public void Update(string propertyName, Sampler sampler, GraphicsDevice gfxDevice)
    {
        FindSetForProperty(propertyName).Update(propertyName, sampler, gfxDevice);
    }
    Material Reference()
    {
        refCounter.Increment();
        return this;
    }

    public void Dispose()
    {
        if (isDisposed)
            return;

        if (refCounter.Decrement() == 1)
        {
            //only material manager still references
            refCounter.Decrement();
            OnDispose?.Invoke(this);
        }

        if (refCounter.Count > 0)
            return;

        Log<Material>.Debug("Disposing material {Name}", name);

        foreach (var set in propertySets.Values)
            set.Dispose();
        piplineStateObject.Dispose();

        isDisposed = true;
    }
}