using System;

namespace Kyvos.GameStates.Exceptions;

public class NonUniqueNameException : GameStateException
{
    public NonUniqueNameException(ReadOnlySpan<char> name) : base($"{name} is already used as a name for a state")
    {

    }
}
