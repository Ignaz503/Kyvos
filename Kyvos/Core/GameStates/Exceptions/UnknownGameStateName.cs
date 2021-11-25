namespace Kyvos.Core.GameStates.Exceptions
{
    public class UnknownGameStateName : GameStateStackException
    {
        public UnknownGameStateName( string name) : base($"{name} is not a known game state")
        {

        }
    }

}
