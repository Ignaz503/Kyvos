using Veldrid;

namespace Kyvos.Graphics.Materials;

public partial class Material
{
    public class TexturePropertyDescription : PropertyDescription
    {
        public string? LocalFilePath { get; init; }

        public override Property Get(GraphicsDevice gfxDevice)
        {
            throw new System.NotImplementedException();
        }
    }


}

