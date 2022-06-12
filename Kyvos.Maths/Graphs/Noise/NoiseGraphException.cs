using System;
using System.Runtime.Serialization;

namespace Kyvos.Maths.Graphs.Noise;

public abstract class NoiseGraphException : Exception
{
    protected NoiseGraphException()
    {
    }

    protected NoiseGraphException(string message) : base(message)
    {
    }

    protected NoiseGraphException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    protected NoiseGraphException(string message, Exception innerException) : base(message, innerException)
    {
    }
}

public class OrphanedNodeException<TLabel> : NoiseGraphException
{
    public OrphanedNodeException(TLabel node) : base($"Node '{node}' found without parent, won't ever be called")
    { }
}

public class MultiLabelUseException<TLabel> : NoiseGraphException
{
    public MultiLabelUseException(TLabel lable) : base($"Label '{lable}' already in use")
    {

    }
}

public class UnusedLableException<TLabel> : NoiseGraphException
{
    public UnusedLableException(TLabel label) : base($"Label '{label}' is not used by any node")
    {

    }
}

public class AppendChildToLeafException<TLabel> : NoiseGraphException
{
    public AppendChildToLeafException(TLabel leafLable, TLabel lable) : base($"Trying to append node '{lable}' to leaf node '{leafLable}'")
    {

    }
}

public class InvalidGraphException : NoiseGraphException
{
    public InvalidGraphException(string str) : base(str)
    {

    }
}
