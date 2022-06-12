namespace Kyvos.Core;

internal static class MessageHandler<T>
{
    static readonly MessageListeners<T> listeners;

    static MessageHandler()
    {
        listeners = new(2); 
    }

    public static void Publish(T message, bool reverse)
        => listeners.Publish(message, reverse);
    

    public static void Subscribe(ApplicationMessageHandler<T> handler)
        => listeners.Subscribe(handler);
    

    public static void Unsubscribe(ApplicationMessageHandler<T> handler)        
        => listeners.Unsubscribe(handler);
    
}
