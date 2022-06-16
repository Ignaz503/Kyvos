using System.Runtime.Serialization;

namespace Kyvos.Graphics;

public abstract class TextureException : Exception
{
    protected TextureException()
    {
    }

    protected TextureException(string? message) : base(message)
    {
    }

    protected TextureException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    protected TextureException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
