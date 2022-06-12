using System;
using System.Runtime.Serialization;

namespace Kyvos.GameStates.Exceptions;

public abstract class GameStateBuilderException : GameStateException
{
    protected GameStateBuilderException()
    {
    }

    protected GameStateBuilderException(string message) : base(message)
    {
    }

    protected GameStateBuilderException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    protected GameStateBuilderException(string message, Exception innerException) : base(message, innerException)
    {
    }
}


