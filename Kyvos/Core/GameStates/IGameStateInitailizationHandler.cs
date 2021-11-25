using DefaultEcs;


namespace Kyvos.Core.GameStates
{
    public interface IGameStateInitailizationHandler
    {
        void Setup( World world );
    }


}
