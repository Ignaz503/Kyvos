using System;

namespace Kyvos.Core.GameStates.Builder.Stages
{
    public interface IStateDisposeSystemSetupStage
    {
        public IStateRunnerStage WithDisposeHandler( Func<IGameStateDisposeHandler> factory );

        public IStateRunnerStage WithNoDisposeStage();
    }

}
