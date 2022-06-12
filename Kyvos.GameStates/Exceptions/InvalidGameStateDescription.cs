namespace Kyvos.GameStates.Exceptions;

public class InvalidGameStateDescription : GameStateBuilderException
{
    public InvalidGameStateDescription(string message) : base(message)
    { }
}


