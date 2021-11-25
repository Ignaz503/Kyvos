using DefaultEcs;
using Kyvos.Core.Applications;
using System;
using System.Collections.Generic;
using Veldrid;

namespace Kyvos.Core.GameStates.Builder.Stages
{
    public interface IStateBuilderFinalStage
    {
        internal string Name { get; }
        
        internal GameState Build(WindowData windowData, GraphicsDevice graphicsDevice);


        internal IStateBuilderFinalStage AddApplicationResourceManagers( List<Action<World>> callbacks );

    }

}
