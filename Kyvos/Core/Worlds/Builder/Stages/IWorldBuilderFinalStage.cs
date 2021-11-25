using DefaultEcs;
using Kyvos.Core.GameStates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kyvos.Core.Worlds.Builder.Stages
{
    public interface IWorldBuilderFinalStage
    {
        World Build();

        void InitializeWorld( World w );

    }
}
