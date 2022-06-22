using DefaultEcs;
using Kyvos.Core;
using Kyvos.ECS.Components.Management;
using Kyvos.ECS.GameStatesExtension;

namespace Kyvos.ImGUI;

public partial struct ImGuiHandle
{
    public class ComponentManager : IComponentManagementContractEstablisher
    {
        public static readonly ComponentManager Instance = new();
        
        private ComponentManager() { }
        public IDisposable Establish(World w)
        {
            w.Subscribe<DisposeMessage>(OnWorldDisposed);
            return w.SubscribeComponentRemoved<ImGuiHandle>(OnRemoved);
        }

        private void OnRemoved(in Entity entity, in ImGuiHandle data)
        {
            data.Dispose();
        }

        void OnWorldDisposed(in DisposeMessage disposeMessage)
        {
            if (disposeMessage.World.Has<ImGuiHandle>()) 
            {
                ref var imguiWrapper = ref disposeMessage.World.Get<ImGuiHandle>();
                imguiWrapper.Dispose();
            }
        }

    }

}
