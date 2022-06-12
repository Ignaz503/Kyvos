using Kyvos.Core;
using Kyvos.Input;
using DefaultEcs.System;
using DefaultEcs;
using Kyvos.GameStates;
using Kyvos.ECS;
using Kyvos.Core.Configuration;
using DefaultEcs.Threading;
using System;

namespace FullyAutomatedGayLuxurySpaceCommunism;

[EngineTest]
public class MixedInputTest : ConfigurableEngineTest 
{
    public MixedInputTest() : base()
    {
    }

    public MixedInputTest(IConfig config) : base(config)
    { }

    protected override IModifyableApplication ApplySetup(IModifyableApplication app)
    {
        return app.WithStackCapacity(1)
            .With(Gamepad.ComponentManager.Instance)
            .With("main", stateBuilder => {
                stateBuilder.UseNewWorld()//without call would use world of state that is below on stack
                    .WithWorldConfiguration(w => w.Set<Gamepad>(new()))
                    .WithWorldConfiguration(w => w.Set<MouseAndKeyboard>(new()))
                    .WithSystems(world => {
                        return new SequentialSystem<float>(
                            new Gamepad.System(world),
                            new MouseAndKeyboard.System(world),
                            new ParallelSystem<float>(
                                        new DefaultParallelRunner(Environment.ProcessorCount),
                                        new GamepadDebugSystem(world),
                                        new KeyboardDebugSystem(world),
                                        new MouseDebugSystem(world))
                            );
                    });
            }, isInitial: true);
    }
}
