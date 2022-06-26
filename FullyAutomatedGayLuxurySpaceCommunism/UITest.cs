using System;
using Kyvos.Core;
using DefaultEcs.System;
using DefaultEcs.Threading;
using Kyvos.Graphics.Materials;
using Kyvos.ECS.Systems.Rendering;
using Kyvos.Input;
using Kyvos.GameStates;
using Kyvos.Audio;
using Kyvos.ECS;
using Kyvos.VeldridIntegration;
using Kyvos.ECS.Components.Rendering;
using Kyvos.Core.Configuration;
using Kyvos.ImGUI;
using DefaultEcs;
using Kyvos.Assets;
using Kyvos.Graphics;

namespace FullyAutomatedGayLuxurySpaceCommunism;

[EngineTest]
public class UITest : ConfigurableEngineTest
{
    public UITest() : base()
    { }

    public UITest(IConfig config) : base(config)
    { }

    protected override IModifyableApplication ApplySetup(IModifyableApplication app)
    {
        return app.WithAudio()
            .WithKyvosGraphics()
            .WithStackCapacity(1)
            .With<MaterialManager, string, Material>(app => new MaterialManager(app))
            .With<MeshResourceManager, string, Mesh>(app => new MeshResourceManager(app.GetComponent<GraphicsDeviceHandle>()!.GfxDevice))
            .With(Camera.ComponentManagment.Instance)
            .WithUIComponentManagment()
            .With("main", stateBuilder => {
                stateBuilder.UseNewWorld()
                            .WithWorldConfiguration(w => {
                                w.Set<MouseAndKeyboard>(new());
                                w.Set<MessageBoard>(new(messagesShownForSeconds:10f));
                                w.Set<LogToMessageBoardForwarder>(new(w));
                                UIBlueprint.AddUI(new MyUI(w),w);
                            })
                            //without call would use world of state that is below on stack
                            .WithEntitySetup(BasicTestEntitySetup.CreateSetup())
                            .WithSystems(
                            w => {
                                return new SequentialSystem<float>(
                                    new MouseAndKeyboard.System(w),
                                    new ToggleMouseGrab(w),
                                    //new RandomColorSystem(w),
                                    new SimpleMoveSystem(w),
                                    new CameraMoveSystem(w),
                                    new PlaySoundTest(w),
                                    new MessageBoard.System(w),
                                    UIBlueprint.GetSystem(w), //this systems informas imgui on what to show
                                    new ParallelSystem<float>(
                                        new DefaultParallelRunner(Environment.ProcessorCount),
                                        new FPSMeassureSystem(w),
                                        new RenderSystem(false, w,
                                        new RenderSystemProvider(new BasicRenderStage(w), new UIRenderSystem(w)),//this UI render system actually renderes it
                                        new RenderSystemProvider(new SetFrameBufferAndClear(),new CameraBufferUpdateSystem(w)))
                                        )
                                    );
                            });
            }, isInitial: true);

    }
}