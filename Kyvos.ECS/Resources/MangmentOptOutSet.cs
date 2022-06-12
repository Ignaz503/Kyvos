using System;
using System.Collections.Generic;

namespace Kyvos.ECS.Resources;

public class MangmentOptOutSet : HashSet<Type>, IResourceManagmentOptOuts
{
    public bool HasOptedOut(Type t)
    {
        return this.Contains(t);
    }

    void IResourceManagmentOptOuts.Add(Type t)
    {
        Add(t);
    }
}

