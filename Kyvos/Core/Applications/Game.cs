using Kyvos.Core.GameStateRunner;
using Kyvos.Core.GameStates;
using Kyvos.Core.GameStates.Builder.Stages;
using Kyvos.Util.Collections;

using System;
using System.Collections.Generic;

namespace Kyvos.Core.Applications
{
    public class Game : Application
    {
        //TODO maybe dictionary
        DisposableList<IDisposable> resourceManagers;

        GameStateStack gamestateStack;

        internal Game(DisposableList<IDisposable> resourceManagers, IEnumerable<IStateBuilderFinalStage> stateBuilders, string initialState) 
        {
            this.resourceManagers = resourceManagers;
            gamestateStack = new( stateBuilders );

            gamestateStack.Push( initialState );
        }


        internal override void Initialize()
        {
            base.Initialize();
        }


        internal sealed override void Run()
        {
            gamestateStack.Top.Run( Time.DeltaTime );
        }

        internal override void Cleanup()
        {
            resourceManagers.Dispose();
            gamestateStack.Dispose();
        }
    }

}
