using DefaultEcs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veldrid;

namespace Kyvos.ECS.Components.Rendering;
internal interface IOpaqueRenderable
{
    void Render(CommandList cmdList, in Entity entity);

}

