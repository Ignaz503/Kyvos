using System.Diagnostics;
using Kyvos.Core.Logging;

namespace Kyvos.Core;

public class Application : IModifyableApplication
{
    bool isDisposed = false;
    bool exists  = true;
    public bool IsAlive => exists;
    List<LifetimeQuery> lifetimeQueries;
    IAppComponentSystem mainLoop;

    public Application()
    {
        lifetimeQueries = new List<LifetimeQuery>();
        mainLoop = SequentialAppComponentSystem.Empty;
        Thread.CurrentThread.Name = "Main";
    }

    public Application With(IAppComponentSystem mainLoop)
    {
        this.mainLoop = mainLoop;
        return this;
    }
    
    public T AddComponent<T>(T component)
        => ApplicationComponent<T>.Set(component, this);

    public T AddComponent<T>(Func<IApplication, T> factory)
        => ApplicationComponent<T>.Set(factory, this);

    public ref T AddComponent<T>(ref T component)
    => ref ApplicationComponent<T>.Set(ref component, this);

    public ref T AddComponentRef<T>(Func<IApplication, T> factory)
        => ref ApplicationComponent<T>.SetRef(factory, this);
    
    public T GetComponent<T>()
        => ApplicationComponent<T>.Get();

    public ref T GetComponentRef<T>()
        => ref ApplicationComponent<T>.GetRef();

    public bool HasComponent<T>()
        => ApplicationComponent<T>.IsSet();

    public void Publish<T>(T message, bool reverse=false)
        => MessageHandler<T>.Publish(message, reverse);

    public void RegisterToAppLifetimeQuery(LifetimeQuery toConsider)
    {
        lifetimeQueries.Add(toConsider);
        Log<Application>.Debug("Lifetime Query Count: {Count}", lifetimeQueries.Count);
    }
       
    
    public void UnregisterFromAppLifetimeQuery(LifetimeQuery toConsider)
        => lifetimeQueries.Remove(toConsider);

    public void Subscribe<T>(ApplicationMessageHandler<T> handler)
        => MessageHandler<T>.Subscribe(handler);   
    
    public void Unsubscribe<T>(ApplicationMessageHandler<T> handler)
        => MessageHandler<T>.Unsubscribe(handler);
    
    public void Run()
    {
        try
        {
            mainLoop.Initialize(this);
            while (ExecuteExistenceQuery())
            {
                mainLoop.Update(this);
            }
        }
        catch (Exception ex)
        {
            ReportException(ex);
        }
        finally
        {
            HandleClose();
        }
    }
    private void ReportException(Exception ex)
    {
        //TODO maybe better message 
        Log<Application>.Error(ex, "An exception was thrown in the application");
    }

    bool ExecuteExistenceQuery() 
    {
        for (int i = 0; i < lifetimeQueries.Count; i++)
        {
            exists = exists && lifetimeQueries[i]();
        }
        return exists;
    }

    public void Dispose()
    {
        if (isDisposed)
            return;

        HandleClose();
    }

    private void HandleClose()
    {
        Log<Application>.Information("Shutting down");
        Publish(new AppDisposedMessage(), reverse: true);
        mainLoop.Dispose();
        isDisposed = true;
    }

    void IModifyableApplication.With(IAppComponentSystem system)
    {
        mainLoop = system;
    }

    public void With(Func<IModifyableApplication, IAppComponentSystem> systemFactory)
    {
        mainLoop = systemFactory(this);
    }
}
