namespace Kyvos.Core;

public static class IModifyableApplicationExtensions 
{
    public static T EnsureExistence<T>(this IModifyableApplication app)
        where T : new()
    {
        if (!app.HasComponent<T>())
            return app.AddComponent(new T());
        return app.GetComponent<T>()!;
    }

    public static T EnsureExistence<T>(this IModifyableApplication app, T component)
    {
        if (!app.HasComponent<T>())
            return app.AddComponent(component);
        return app.GetComponent<T>()!;
    }

    public static T EnsureExistence<T>(this IModifyableApplication app, Func<IApplication, T> factory)
    {
        if (!app.HasComponent<T>())
            return app.AddComponent(factory);
        return app.GetComponent<T>()!;
    }
}
