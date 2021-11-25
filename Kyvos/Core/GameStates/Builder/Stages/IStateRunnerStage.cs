using DefaultEcs;
using DefaultEcs.System;
using Kyvos.Core.GameStateRunner;
using System;

namespace Kyvos.Core.GameStates.Builder.Stages
{
    public interface IStateRunnerStage
    {
        IStateStackBehaviourSatge WithRunner( Func<GameStateRunnerBuilder, GameStateRunnerBuilder> setup );

        IStateStackBehaviourSatge WithRunner( IGameStateRunnerSetup setupHandler );

    }

    public interface IStateStackBehaviourSatge 
    {
        IStateBuilderFinalStage WithStackBehaviour( Func<GameState.IStackBehaviour> behaviourFactory );
    }

}
