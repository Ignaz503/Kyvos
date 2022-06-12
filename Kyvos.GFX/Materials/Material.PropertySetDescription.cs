using System;
using System.Collections.Generic;
using System.Linq;
using Veldrid;

namespace Kyvos.Graphics.Materials;

public partial class Material
{
    public struct PropertySetDescription
    {
        public PropertySet.Ownership Type { get; init; }
        public uint SetIndex { get; init; }

        ResourceLayout? layout;

        //TODO build resourceLayoutDescription from propertyDescriptions no need to be explicitly told
        PropertyDescription[] properties;


        public PropertySetDescription(PropertySet.Ownership type, uint setIdx,  params PropertyDescription[] properties)
        {
            this.Type = type;
            this.SetIndex = setIdx;
            this.properties = properties.OrderBy(elem => elem.Order).ToArray();
            layout = null;
        }
        
        public ResourceLayout GetResourceLayout(GraphicsDevice gfxDevice) 
        {
            if (layout is null) 
            {
                layout = gfxDevice.ResourceFactory.CreateResourceLayout(CreateLayoutDescription());       
            }

            return layout;
        }

        private ResourceLayoutDescription CreateLayoutDescription() 
        {
            return new ResourceLayoutDescription(GetElementDescriptions());
        }

        private ResourceLayoutElementDescription[] GetElementDescriptions()
        {
            var descriptions = new ResourceLayoutElementDescription[properties.Length];

            for (int i = 0; i < properties.Length; i++)
            {
                descriptions[i] = properties[i].LayoutElementDescription;                      
            }
            return descriptions;
        }

        public IEnumerable<KeyValuePair<string, Property>> GetPropeties(GraphicsDevice gfxDevice) 
        {
            foreach (var propDescription in properties) 
            {
                yield return new KeyValuePair<string, Property>(propDescription.Name ?? string.Empty,propDescription.Get(gfxDevice));
            }
        }


        //TODO serialization handling

    }

}

