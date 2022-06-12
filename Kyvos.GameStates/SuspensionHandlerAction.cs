using System;

namespace Kyvos.GameStates;

public class SuspensionHandlerAction<T> : ISuspensionHandler<T> 
{
    Action<T> supendAction;
    Action<T> resumeAction;

    public SuspensionHandlerAction(Action<T> supendAction, Action<T> resumeAction)
    {
        this.supendAction = supendAction ?? throw new ArgumentNullException(nameof(supendAction));
        this.resumeAction = resumeAction ?? throw new ArgumentNullException(nameof(resumeAction));
    }

    public void Suspend(T w)    
        => supendAction(w);

    public void Resume(T w)
        => resumeAction(w);

}
