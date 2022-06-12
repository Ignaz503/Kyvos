using System;
using DefaultEcs;
using Kyvos.ECS.Components.Management;
using Kyvos.Utility.Collections;

namespace Kyvos.Input;

public partial class Gamepad 
{
    public class ComponentManager : IComponentManagementContractEstablisher
    {
        public static readonly ComponentManager Instance = new ComponentManager();


        private ComponentManager()
        {}

        public IDisposable Establish(World w)
        {
            return new DisposableList<IDisposable>(
                w.SubscribeComponentRemoved<Gamepad>(OnRemove),
                w.SubscribeWorldDisposed(OnWorldDisposed)
            );
                    
        }

        public void OnRemove(in Entity ent, in Gamepad gamepad) 
        {
            gamepad.Dispose();
        }

        public void OnWorldDisposed(World w) 
        {
            if (w.Has<Gamepad>()) 
            {
                w.Get<Gamepad>().Dispose();
            }
        }

    }
}


