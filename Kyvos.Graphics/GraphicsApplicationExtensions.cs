using Kyvos.Core;
using Kyvos.Graphics.Materials;

namespace Kyvos.Graphics;

public static class GraphicsApplicationExtensions 
{
    public static IModifyableApplication WithKyvosMaterials(this IModifyableApplication application) 
    {
        application.EnsureExistence(new MaterialLoader(application));

        return application;
    }

    public static IModifyableApplication WithTextureHandling(this IModifyableApplication application)
    {
        application.EnsureExistence(new TextureLoader(application));
        return application;
    }

    public static IModifyableApplication WithKyvosGraphics(this IModifyableApplication app)
        => app.WithTextureHandling().WithKyvosGraphics();

}