using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kyvos.Core.Worlds.Builder.Stages
{
    public interface IWorldBuilderInitStage
    {
        IWorldBuilderPopulateStage WithMaxCapacity( int? maxCapacity );

    }
}
