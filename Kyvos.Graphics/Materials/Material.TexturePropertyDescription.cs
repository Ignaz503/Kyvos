using Veldrid;

namespace Kyvos.Graphics.Materials;

public partial class Material
{
    public class TexturePropertyDescription : PropertyDescription
    {
        public string? AssetID { get; init; }

        public bool MipMap { get; init; }

        public override Property Get(CreationContext ctx)
        {
            //TODO default texture fallback
            var texture = ctx.TextureLoader.Load(new(AssetID??""),MipMap);
            return new TextureProperty(texture,Order,ctx.GfxDevice);
        }
    }
}

