using System;

namespace Kyvos.GameStates;

internal class SleepHandlerActions<T> : ISleepHandler<T>
{
    Action<T> awakeAction;
    Action<T> sleepAction;

    public SleepHandlerActions(Action<T> awakeAction, Action<T> sleepAction)
    {
        this.awakeAction = awakeAction ?? throw new ArgumentNullException(nameof(awakeAction));
        this.sleepAction = sleepAction ?? throw new ArgumentNullException(nameof(sleepAction));
    }

    public void Awake(T w)
        => awakeAction(w);

    public void Sleep(T w)
        => sleepAction(w);
}


