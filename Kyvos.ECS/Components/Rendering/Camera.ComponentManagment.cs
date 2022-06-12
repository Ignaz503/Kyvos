using DefaultEcs;
using Kyvos.Core;
using Kyvos.ECS.Components.Management;
using Kyvos.Graphics.Materials;
using System;

namespace Kyvos.ECS.Components.Rendering;

public partial struct Camera
{
    //TODO shader camera info

    public class ComponentManagment : IComponentManagementContractEstablisher
    {
        public static readonly ComponentManagment Instance = new();


        private ComponentManagment()
        { }

        public IDisposable Establish(World w)
        {
            return w.SubscribeComponentRemoved<Camera>(OnRemoved);
        }

        void OnRemoved(in Entity ent, in Camera cam)
        {
            cam.Cleanup(ent.World.Get<IApplication>());
        }

    }

}