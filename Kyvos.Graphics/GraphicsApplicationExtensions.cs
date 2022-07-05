using Kyvos.Core;
using Kyvos.Graphics.Materials;

namespace Kyvos.Graphics;

public static class GraphicsApplicationExtensions 
{
    public static IModifyableApplication WithKyvosMaterials(this IModifyableApplication application) 
    {
        application.EnsureExistence((app)=>new MaterialLoader(application));

        return application;
    }

    public static IModifyableApplication WithTextureHandling(this IModifyableApplication application)
    {
        application.EnsureExistence<ITextureLoader>((app)=>new TextureLoader(application));
        return application;
    }

    public static IModifyableApplication WithKyvosGraphics(this IModifyableApplication app)
        => app.WithTextureHandling().WithKyvosMaterials();

}