using DefaultEcs;

namespace Kyvos.Core.GameStates
{
    public interface IGameStateDisposeHandler
    {
        void Cleanup(World world);

        internal static IGameStateDisposeHandler NoCleanup =>
            new NoCleanupStage();

    }

    internal class NoCleanupStage : IGameStateDisposeHandler
    {
        public void Cleanup( World world )
        {}
    }

}
