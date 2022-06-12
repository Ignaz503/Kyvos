using System;

namespace Kyvos.ECS.Resources;

public interface IResourceManagmentOptOuts 
{
    bool HasOptedOut(Type t);

    void Add(Type t);

    int Count { get; }

}