namespace Kyvos.Core;

internal class MessageListeners<T> : List<ApplicationMessageHandler<T>>
{
    readonly object lockObj = new();

    public MessageListeners():base()
    {}

    public MessageListeners(int capacity) : base(capacity) 
    { }

    public MessageListeners(IEnumerable<ApplicationMessageHandler<T>> collection) : base(collection)
    { }


    public void Publish(T message, bool reverse)
    {
        if (!reverse)
            Invoke(message);
        else
            InvokeReverse(message);
    }

    public void Invoke(T message) 
    {
        //Do i lock this here?
        foreach (var listener in this)
        {
            listener(message);
        }
    }

    public void InvokeReverse(T message) 
    {
        for (int i = this.Count-1; i >= 0; i--)
        {
            this[i].Invoke(message);
        }
    }

    public void Subscribe(ApplicationMessageHandler<T> handler)
    {
        lock (@lockObj)
        {
            Add(handler); 
        }
    }


    public void Unsubscribe(ApplicationMessageHandler<T> handler)
    {
        lock (lockObj) 
        {
            Remove(handler);
        } 
    }
}
