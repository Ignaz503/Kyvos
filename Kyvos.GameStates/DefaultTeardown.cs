using System;

namespace Kyvos.GameStates;

public sealed class DefaultTeardown<T> : ITeardownHandler<T>
{
    internal static DefaultTeardown<T> Instance = new();

    private DefaultTeardown() { }

    public void TearDown(T w)
    {
        (w as IDisposable)?.Dispose();
    }
}