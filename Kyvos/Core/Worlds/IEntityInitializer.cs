using DefaultEcs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kyvos.Core.Worlds
{
    public interface IEntityInitializer
    {
        void Setup( Entity entity );

        void Setup( int idx, Entity entity );
    }
}
