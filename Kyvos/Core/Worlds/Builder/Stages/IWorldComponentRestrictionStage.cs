using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kyvos.Core.Worlds.Builder.Stages
{
    public interface IWorldBuilderComponentRestrictionStage<TStage>
    {
        TStage WithComponentRestriction<T>( int maxCapacity );
    }
}
