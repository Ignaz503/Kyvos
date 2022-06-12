using System.Runtime.Serialization;

namespace Kyvos.Utility.Exceptions;
[Serializable]
public class TODO : Exception
{
    public TODO()
    {
    }

    public TODO(string message) : base(message)
    {
    }

    public TODO(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected TODO(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}