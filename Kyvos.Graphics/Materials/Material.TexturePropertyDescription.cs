using Veldrid;

namespace Kyvos.Graphics.Materials;

public partial class Material
{
    public class Texture2DPropertyDescription : PropertyDescription
    {
        public string? AssetID { get; init; }

        public bool MipMap { get; init; }

        public override Property Get(CreationContext ctx)
        {
            //TODO default texture fallback
            var texture = ctx.TextureLoader.Load<TextureData2D>(new(AssetID??""),MipMap);
            return new TextureProperty<TextureData2D>(texture,Order,ctx.GfxDevice);
        }
    }
}

