using Kyvos.Core.Configuration;

using System;
using System.Collections.Generic;
using System.Linq;

namespace Kyvos.Core;

public interface IApplication : IDisposable
{
    T? GetComponent<T>();

    ref T GetComponentRef<T>();

    bool HasComponent<T>();

    bool IsAlive { get; }

    void Publish<T>(T message, bool reverse = false);
    void Subscribe<T>(ApplicationMessageHandler<T> handler);

    void Unsubscribe<T>(ApplicationMessageHandler<T> handler);

    void Run();

}

public delegate bool LifetimeQuery();

public delegate void ApplicationMessageHandler<T>(T message);

public class MissingComponentException : AppComponentException
{
    public MissingComponentException(string msg) : base(msg)
    {}
}