using DefaultEcs;
using Kyvos.Core;
using Kyvos.VeldridIntegration;
using Kyvos.ECS;

namespace Kyvos.ImGUI;

public static class UIApplicationExtensions
{
    public static IModifyableApplication WithUIComponentManagment(this IModifyableApplication app)
     => app.With(ImGuiHandle.ComponentManager.Instance);


}
