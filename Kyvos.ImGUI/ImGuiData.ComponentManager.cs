using DefaultEcs;
using Kyvos.Core;
using Kyvos.ECS.Components.Management;
using Kyvos.ECS.GameStatesExtension;

namespace Kyvos.ImGUI;

public partial struct ImGuiData
{
    public class ComponentManager : IComponentManagementContractEstablisher
    {
        public static readonly ComponentManager Instance = new();
        
        private ComponentManager() { }
        public IDisposable Establish(World w)
        {
            w.Subscribe<DisposeMessage>(OnWorldDisposed);
            return w.SubscribeComponentRemoved<ImGuiData>(OnRemoved);
        }

        private void OnRemoved(in Entity entity, in ImGuiData data)
        {
            data.UnregisterWindowEvent(entity.World.Get<IApplication>());
        }

        void OnWorldDisposed(in DisposeMessage disposeMessage)
        {
            if (disposeMessage.World.Has<ImGuiData>()) 
            {
                ref var imguiWrapper = ref disposeMessage.World.Get<ImGuiData>();
                imguiWrapper.Dispose();
            }
        }

    }

}
