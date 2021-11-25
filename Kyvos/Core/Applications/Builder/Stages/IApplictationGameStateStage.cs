using DefaultEcs;
using Kyvos.Core.GameStates.Builder;
using Kyvos.Core.GameStates.Builder.Stages;
using Veldrid;
using System;

namespace Kyvos.Core.Applications.Builder.Stages
{
    public interface IApplictationGameStateStage : IApplicationBuildStage
    {
        public IApplictationGameStateStage WithGameState(Func<IStateBuilderInitStage,IStateBuilderFinalStage> setup, bool isInitialState = false, bool resourceManagedByApplication = true);
    }

}
