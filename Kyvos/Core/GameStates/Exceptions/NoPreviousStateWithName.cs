namespace Kyvos.Core.GameStates.Exceptions
{
    public class NoPreviousStateWithName : GameStateStackException
    {
        public NoPreviousStateWithName(string name) : base($"No previous gamestate has the name: {name}")
        {

        }
    }

}
