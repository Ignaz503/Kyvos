using DefaultEcs;
using DefaultEcs.Resource;
using DefaultEcs.System;
using Kyvos.Core;
using Kyvos.ECS.Entities;
using Kyvos.ECS.Resources;
using Kyvos.ECS.Systems.Setup;
using Kyvos.GameStates.Exceptions;
using System;

namespace Kyvos.ECS.GameStatesExtension;

public partial class ECSGameState
{
    public class Builder : Builder<ECSGameState.Builder,ECSGameState>
    {
        IResourceManagmentOptOuts managmentOptOuts = NoMangmentOptOuts.Instance;
        WorldConfigureSystems worldConfigSystems = new();
        EntitySetupSystems entitySetupSystems = new();

        ISystemBuilder? updateSystemBuilder;

        int? entityCapacity;
        public Builder() :base()
        {
        }

        public Builder OptOutFromManagment<TManager, TInfo, TResource>()
            where TManager : AResourceManager<TInfo, TResource>
        {
            if (managmentOptOuts is NoMangmentOptOuts)
                managmentOptOuts = new MangmentOptOutSet();

            managmentOptOuts.Add(typeof(TManager));

            return this;
        }
        public Builder SetEntityCapacity(int? value)
        {
            entityCapacity = value;
            return this;
        }

        public Builder WithWorldConfiguration(IWorldConfigureSystem system)
        {
            worldConfigSystems.Add(system);
            return this;
        }

        public Builder WithWorldConfiguration(Action<World> system)
            => WithWorldConfiguration(new WorldCoinfigureSystemAction(system));

        public Builder WithEntitySetup(IEntitySetupSystem system)
        {
            entitySetupSystems.Add(system);
            return this;
        }

        public Builder WithEntitySetup(Action<EntityCommands> setup)
            => WithEntitySetup(new EntitySetupSystemAction(setup));


        public Builder WithSystems(ISystemBuilder builder)
        {
            updateSystemBuilder = builder;
            return this;
        }
        public Builder WithSystems(Func<World, ISystem<float>> builder)
            => WithSystems(new UpdateSystemBuilderFunction(builder));



        public override ECSGameState Build(IApplication app)
        {
            Validate();

            return new ECSGameState(name,
                (entityCapacity,UsesNewWorld), 
                worldConfigSystems,
                managmentOptOuts,
                entitySetupSystems, 
                updateSystemBuilder!, // validate makes sure it is not null
                sleepHandler, 
                suspensionHandler,
                teardownHandler,
                app);
        }

        void Validate()
        {
            if (updateSystemBuilder is null)
                throw new InvalidGameStateDescription("Update System builder cannot be null");
        }
    }
}


