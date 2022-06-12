using System.Runtime.Serialization;

namespace Kyvos.Core;

public abstract class AppComponentException : Exception
{
    protected AppComponentException()
    {
    }

    protected AppComponentException(string? message) : base(message)
    {
    }

    protected AppComponentException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    protected AppComponentException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
