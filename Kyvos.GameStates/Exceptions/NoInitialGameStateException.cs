namespace Kyvos.GameStates.Exceptions;

public class NoInitialGameStateException : GameStateException 
{
    public NoInitialGameStateException() : base("No game state was marked as initial state. Can't initialize stack.")
    {

    }
}
