using Kyvos.Core;
using Kyvos.Input;
using DefaultEcs.System;
using DefaultEcs;
using Kyvos.GameStates;
using Kyvos.ECS;
using Kyvos.Core.Configuration;

namespace FullyAutomatedGayLuxurySpaceCommunism;

[EngineTest]
public class GamePadInputTest : ConfigurableEngineTest
{
    public GamePadInputTest() :base()
    {
    }

    public GamePadInputTest(IConfig config) : base(config)
    { }

    protected override IModifyableApplication ApplySetup(IModifyableApplication app)
    {
        return app.WithStackCapacity(1)
            .With(Gamepad.ComponentManager.Instance)
            .With("main", stateBuilder => {
                stateBuilder.UseNewWorld()//without call would use world of state that is below on stack
                    .WithWorldConfiguration(w => w.Set<Gamepad>(new()))
                    .WithSystems(world => {
                        return new SequentialSystem<float>(
                            new Gamepad.System(world),
                            new GamepadDebugSystem(world));
                    });
            }, isInitial: true);
    }
}
