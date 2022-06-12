using System;

namespace Kyvos.GameStates;

internal class TearDownHandlerAction<T> : ITeardownHandler<T>
{
    Action<T> teardownAction;

    public TearDownHandlerAction(Action<T> teardownAction)
    {
        this.teardownAction = teardownAction ?? throw new ArgumentNullException(nameof(teardownAction));
    }

    public void TearDown(T w)
        => teardownAction(w);
}
