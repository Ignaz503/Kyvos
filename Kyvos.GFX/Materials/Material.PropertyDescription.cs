using Veldrid;

namespace Kyvos.Graphics.Materials;

public partial class Material
{
    public abstract class PropertyDescription 
    {
        public string? Name { get; init; }

        public int Order { get; init; }

        public ResourceKind Kind { get; init; }

        public ShaderStages Stages { get; init; }


        public ResourceLayoutElementOptions Options { get; init; }

        public ResourceLayoutElementDescription LayoutElementDescription => new(Name,Kind,Stages,Options);

        public abstract Property Get(GraphicsDevice gfxDevice);

    }
}

