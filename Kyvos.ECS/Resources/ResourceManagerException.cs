using System;
using System.Runtime.Serialization;

namespace Kyvos.ECS.Resources;

public abstract class ResourceManagerException : Exception
{
    protected ResourceManagerException()
    {
    }

    protected ResourceManagerException(string message) : base(message)
    {
    }

    protected ResourceManagerException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    protected ResourceManagerException(string message, Exception innerException) : base(message, innerException)
    {
    }
}

