using Kyvos.Core.Worlds.Builder.Stages;
using System;

namespace Kyvos.Core.GameStates.Builder.Stages
{
    public interface IStateECSWorldSetupStage
    {
        IStateDisposeSystemSetupStage WithECSWorld( Func<IWorldBuilderInitStage, IWorldBuilderFinalStage> build );
    }
}
