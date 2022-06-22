using Kyvos.VeldridIntegration;
using Kyvos.Utility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Veldrid;

namespace Kyvos.Graphics.Materials;

public partial class Material
{
    public partial class PropertySet : IDisposable
    {
        private static event Action<PropertySet>? OnGlobalSetDisposed;

        public enum Ownership 
        {
            Global,
            PerMaterial
        }

        internal Ownership Type { get; private set; }

        Dictionary<string, Property> properties;
        ResourceLayout resourceLayout;
        uint setIndex;

        bool isDisposed = false;
        ReferenceCounter refCounter;
        ResourceSet? resourceSet;

        private PropertySet(PropertySetDescription descripton, CreationContext ctx)
        {
            this.Type = descripton.Type;
            this.setIndex = descripton.SetIndex;
            refCounter = new();


            properties = new(descripton.GetPropeties(ctx));

            resourceLayout = descripton.GetResourceLayout(ctx.GfxDevice);
            BuildResourceSet(ctx.GfxDevice);
        }

        public void Use(CommandList cmdList) 
        {
            cmdList.SetGraphicsResourceSet(setIndex,resourceSet);
        }

        T GetProperty<T>(string propertyName)
            where T : Property
        {
            //todo validation in release build?
#if DEBUG
            if (!properties.ContainsKey(propertyName))
                throw new NonExistentPropertyException(propertyName);
#endif
            var property = properties[propertyName];

#if DEBUG
            if (property is not T)
                throw new WrongUpdateMethodException<T>(propertyName, property.GetType());
#endif
            return (property as T)!; //()! would be unecessary if validation in release build
        }

        internal bool HasProperty(string name)
            => properties.ContainsKey(name);

        public void Update<T>(string propertyName, ref T data, CommandList cmdList, uint byteOffset = 0)
            where T: struct
        {
            var property = GetProperty<BufferProperty>(propertyName);

            property.Update(ref data, cmdList,byteOffset); 
        }

        public void Update<T>(string propertyName, ref T data,  GraphicsDevice graphicsDevice, uint byteOffset = 0)
        where T : struct
        {
            var property = GetProperty<BufferProperty>(propertyName);
            property.Update(ref data, graphicsDevice, byteOffset);
        }

        public void Update(string propertyName, Texture texture, GraphicsDevice gfxDevice) 
        {
            var property = GetProperty<TextureProperty>(propertyName);
            property.Update(texture, gfxDevice);
            BuildResourceSet(gfxDevice);
        }

        public void Update(string propertyName, Sampler sampler, GraphicsDevice gfxDevice) 
        {
            var property = GetProperty<SamplerProperty>(propertyName);
            property.Update(sampler);
            BuildResourceSet(gfxDevice);
        }

        private PropertySet Reference()
        { 
            refCounter.Increment();
            return this;
        }

        private void BuildResourceSet(GraphicsDevice gfxDevice)
        {
            resourceSet = gfxDevice.ResourceFactory.CreateResourceSet(
                new ResourceSetDescription(resourceLayout, GetPropertiesAsBindable()
                ));

            BindableResource[] GetPropertiesAsBindable()
            {
                int count = properties.Keys.Count;
                BindableResource[] resources = new BindableResource[properties.Keys.Count];

                foreach (var elem in properties.Values)
                {
                    resources[elem.Order] = elem.Bindable;
                }
                return resources;
            }
        }

        public void Dispose()
        {
            if (isDisposed)
                return;
            var k = refCounter.Decrement();

            if (k == 1 && Type == Ownership.Global) 
            {
                //only resource manager still references it
                k = refCounter.Decrement();
                //inform resource manager to remove set from dictionary
                OnGlobalSetDisposed?.Invoke(this);
            }

            if (k > 0)
                return;

            resourceSet?.Dispose();

            foreach (var prop in properties.Values)
                prop.Dispose();

            isDisposed = true;

        }

    }
}

