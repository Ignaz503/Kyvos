using System;

namespace Kyvos.ECS.Resources;

public class UnkownResourceManagerException : ResourceManagerException
{
    public UnkownResourceManagerException(Type t) : base($"{t} is not a known resource manager type")
    {

    }
}

