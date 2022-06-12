using System;

namespace Kyvos.GameStates.Exceptions;

public class UnknownGameStateException : GameStateException 
{
    public UnknownGameStateException(ReadOnlySpan<char> name):base($"No state of name '{name}' exists.")
    {

    }
}
