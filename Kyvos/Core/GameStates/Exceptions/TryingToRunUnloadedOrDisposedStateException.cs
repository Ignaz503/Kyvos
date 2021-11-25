namespace Kyvos.Core.GameStates.Exceptions
{
    public class TryingToRunUnloadedOrDisposedStateException: GameStateException
    {
        public TryingToRunUnloadedOrDisposedStateException():base("Trying to run game state that is unloaded or disposed")
        {

        }
    }

}
