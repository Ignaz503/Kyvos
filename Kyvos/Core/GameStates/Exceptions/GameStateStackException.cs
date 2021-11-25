using System;
using System.Runtime.Serialization;

namespace Kyvos.Core.GameStates.Exceptions
{
    public abstract class GameStateStackException : Exception
    {
        protected GameStateStackException()
        {
        }

        protected GameStateStackException( string message ) : base( message )
        {
        }

        protected GameStateStackException( SerializationInfo info, StreamingContext context ) : base( info, context )
        {
        }

        protected GameStateStackException( string message, Exception innerException ) : base( message, innerException )
        {
        }
    }

}
