﻿using Veldrid;

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

        public abstract Property Get(CreationContext ctx);

    }

    public readonly struct CreationContext 
    {
        public GraphicsDevice GfxDevice { get; init; }
        public TextureLoader TextureLoader { get; init; }

    }
}

