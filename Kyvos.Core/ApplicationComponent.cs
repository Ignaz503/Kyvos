using Kyvos.Core.Logging;
using System.Reflection;

namespace Kyvos.Core;

struct ApplicationComponent { }

internal static class ApplicationComponent<T>
{
    static bool isSet = false;
    static T? component;

    public static bool IsSet()
     => component != null;


    public static T Set(Func<IApplication, T> factory, IApplication app) 
    {
        return Set(factory(app),app);
    }

    public static ref T SetRef(Func<IApplication, T> factory, IApplication app)
    {
        Set(factory(app), app);   
        return ref component!;
    }


    public static T Set(T comp, IApplication app)
    {
        component = comp;
        if (!isSet)
            SubscribeToAppDispose(app);
        isSet = true;
        return component!;
    }

    public static ref T Set(ref T comp, IApplication app)
    {
        component = comp;
        if (!isSet)
            SubscribeToAppDispose(app);
        isSet = true;
        return ref component!;
    }

    public static T Get()
    {
#if DEBUG
        ArgumentNullException.ThrowIfNull(component, nameof(component));
#endif
        return component!;
    }

    public static ref T GetRef()
    {
#if DEBUG
        ArgumentNullException.ThrowIfNull(component, nameof(component));
#endif
        return ref component!;
    }

    static void SubscribeToAppDispose(IApplication app) 
    {
        if (component is not null && component is IDisposable) 
        {
            Log<ApplicationComponent>.Debug("Registering component {Component} to normal dispose", component.GetType().Name);
            app.Subscribe<AppDisposedMessage>(NormalDispose);

            //var attrib = component.GetType().GetCustomAttribute<AppDisposeStageAttribute>();

            //var stage = attrib?.Stage ?? AppDisopseStage.Normal;

            //switch (stage) 
            //{
            //    case AppDisopseStage.Early:
            //        Log<ApplicationComponent>.Debug("Registering component {Component} to early dispose", component.GetType().Name);
            //        app.Subscribe<EarlyAppDisposedMessage>(EarlyDispose);
            //        break;
            //    case AppDisopseStage.Late:
            //        Log<ApplicationComponent>.Debug("Registering component {Component} to late dispose", component.GetType().Name);
            //        app.Subscribe<LateAppDisposedMessage>(LateDispose);
            //        break;
            //    case AppDisopseStage.Last:
            //        Log<ApplicationComponent>.Debug("Registering component {Component} to last dispose", component.GetType().Name);
            //        app.Subscribe<LastAppDisposedMessage>(LastDispose);
            //        break;
            //    case AppDisopseStage.Normal:
            //    default:
            //        Log<ApplicationComponent>.Debug("Registering component {Component} to normal dispose", component.GetType().Name);
            //        app.Subscribe<AppDisposedMessage>(NormalDispose);
            //        break;
            //}
        }

    }
    
    //static void EarlyDispose(EarlyAppDisposedMessage _)
    //    => (component as IDisposable)?.Dispose();

    static void NormalDispose(AppDisposedMessage _) 
        => (component as IDisposable)?.Dispose();

    //static void LateDispose(LateAppDisposedMessage _)
    //    => (component as IDisposable)?.Dispose();
    //static void LastDispose(LastAppDisposedMessage _)
    //    => (component as IDisposable)?.Dispose();
}
