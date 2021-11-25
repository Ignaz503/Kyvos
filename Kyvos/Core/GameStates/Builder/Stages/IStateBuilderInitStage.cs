using Kyvos.Core.Applications;
using Veldrid;

namespace Kyvos.Core.GameStates.Builder.Stages
{
    public interface IStateBuilderInitStage 
    {
        public IStateResourceManagerStage WithName( string name );
    }

}
