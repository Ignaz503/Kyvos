using DefaultEcs;
using DefaultEcs.System;
using Kyvos.ECS.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kyvos.ECS;
internal static class ECSExtensions
{
    public static readonly World EmptyWorld = new();

    public static readonly ISystem<float> EmptySystem = new EmptyFloatSytem();
}
