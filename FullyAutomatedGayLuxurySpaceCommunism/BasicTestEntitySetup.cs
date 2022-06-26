using System;
using DefaultEcs.Resource;
using Kyvos.Graphics.Materials;
using Kyvos.ECS.Components;
using System.Numerics;
using Kyvos.Maths;
using Kyvos.ECS.Components.Rendering;
using Kyvos.ECS.Entities;
using Kyvos.Core.Logging;

namespace FullyAutomatedGayLuxurySpaceCommunism;

public static class BasicTestEntitySetup 
{
    public static Action<EntityCommands> CreateSetup() 
    {
        return commands =>
        {
            //var entity = commands.CreateEntity();
            //entity.Set(new ManagedResource<string, Material>("pipe"));//just test managed resources
            //entity.Set(new ManagedResource<string, Mesh>("cube"));
            //entity.Set<Transform>(new());

            //TODO set material textureMat
            //TODO set Mesh texCube

            var texturedEntity = commands.CreateEntity();
            texturedEntity.Set(new ManagedResource<string, Material>("textureMat"));
            texturedEntity.Set(new ManagedResource<string, Mesh>("texCube"));
            texturedEntity.Set<Transform>(new());
 

            var worldModification = commands.WorldModification(); //sets a world component and not on entity

            var cam = new Camera(commands.App) {
                Position = -Vector3.UnitZ * 10,
                Yaw = 180 * Mathf.DegToRad
            };

            worldModification.Set(cam);

        };
    }
}
