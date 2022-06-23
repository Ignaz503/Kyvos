using Veldrid;

namespace Kyvos.Graphics.Materials;

public partial class Material
{
    public class TexturePropertyDescription : PropertyDescription
    {
        public string? AssetID { get; init; }

        public override Property Get(CreationContext ctx)
        {
            var texture = ctx.TextureLoader.Load(new(AssetID??""),true);
            return new TextureProperty(texture,Order,ctx.GfxDevice);
        }
    }
}

