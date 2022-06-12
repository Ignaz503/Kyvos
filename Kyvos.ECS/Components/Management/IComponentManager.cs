using DefaultEcs;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kyvos.ECS.Components.Management
{
    public interface IComponentManager : IDisposable
    {
        public void SeutpManagement(World w);
        public void AddContractEstablisher(IComponentManagementContractEstablisher toAdd);
    }
}
