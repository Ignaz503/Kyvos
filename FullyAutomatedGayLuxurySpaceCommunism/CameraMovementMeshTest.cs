﻿using System;
using Kyvos.Core;
using DefaultEcs.Resource;
using DefaultEcs.System;
using DefaultEcs.Threading;
using Kyvos.Graphics.Materials;
using Kyvos.ECS.Components;
using Kyvos.ECS.Systems.Rendering;
using System.Numerics;
using Kyvos.Maths;
using Kyvos.Input;
using Kyvos.GameStates;
using Kyvos.Audio;
using Kyvos.ECS;
using Kyvos.VeldridIntegration;
using Kyvos.ECS.Components.Rendering;
using Kyvos.Core.Configuration;

namespace FullyAutomatedGayLuxurySpaceCommunism;

[EngineTest]
public class CameraMovementMeshTest : ConfigurableEngineTest
{
    public CameraMovementMeshTest() : base()
    { }

    public CameraMovementMeshTest(IConfig config) : base(config)
    {}

    protected override IModifyableApplication ApplySetup(IModifyableApplication app) 
    {
        return app.WithSoundEngine()
            .WithStackCapacity(1)
            .With<MaterialManager, string, Material>(app => new MaterialManager(app.GetComponent<GraphicsDeviceHandle>()!.GfxDevice))
            .With<MeshResourceManager, string, Mesh>(app => new MeshResourceManager(app.GetComponent<GraphicsDeviceHandle>()!.GfxDevice))
            .With(Camera.ComponentManagment.Instance)
            .With("main", stateBuilder => {
                stateBuilder.UseNewWorld()
                            .WithWorldConfiguration(w => w.Set<MouseAndKeyboard>(new()))
                            //without call would use world of state that is below on stack
                            .WithEntitySetup(commands => {
                                var entity = commands.CreateEntity();
                                entity.Set(new ManagedResource<string, Material>("pipe"));//just test managed resources
                                entity.Set(new ManagedResource<string, Mesh>("cube"));
                                entity.Set<Transform>(new());

                                var worldModification = commands.WorldModification(); //sets a world component and not on entity

                                var cam = new Camera(commands.App);
                                cam.Position = -Vector3.UnitZ * 10;
                                cam.Yaw = 180 * Mathf.DegToRad;

                                worldModification.Set(cam);

                            })
                            .WithSystems(
                            w => {
                                return new SequentialSystem<float>(
                                    new MouseAndKeyboard.System(w),
                                    new ToggleMouseGrab(w),
                                    new RandomColorSystem(w),
                                    new SimpleMoveSystem(w),
                                    new CameraMoveSystem(w),
                                    new PlaySoundTest(w),
                                    new ParallelSystem<float>(
                                        new DefaultParallelRunner(Environment.ProcessorCount),
                                        new FPSMeassureSystem(w),
                                        new RenderSystem(false, w,
                                        new RenderSystemProvider(new BasicRenderStage(w)),
                                        new RenderSystemProvider(new CameraBufferUpdateSystem(w)))
                                        )
                                    );
                            });
            }, isInitial: true);

    }

}
