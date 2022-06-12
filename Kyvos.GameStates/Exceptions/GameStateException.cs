using System;
using System.Runtime.Serialization;

namespace Kyvos.GameStates.Exceptions;

public abstract class GameStateException : Exception
{
    protected GameStateException()
    {
    }

    protected GameStateException(string message) : base(message)
    {
    }

    protected GameStateException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    protected GameStateException(string message, Exception innerException) : base(message, innerException)
    {
    }
}


