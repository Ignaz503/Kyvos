using Kyvos.Core;
using Kyvos.VeldridIntegration.Sdl2;
using System;
using System.Numerics;
using Veldrid;
using Veldrid.Sdl2;
using Veldrid.StartupUtilities;

namespace Kyvos.VeldridIntegration;

public class Window
{
    public const string CONFIG_KEY = "window";
    public enum EventType 
    {
        Resized,
        Closing,
        Closed,
        FocusLost,
        FocusGained,
        Shown,
        Hidden,
        MouseEntered,
        MouseLeft,
        Exposed
    }

    public struct Event
    {
        public EventType Type { get; init; }
        public Window Window { get; init; }

        public Event(EventType type, Window window)
        {
            Type = type;
            Window = window;
        }
    }


    public event Action<Event>? OnWindowEvent;

    private Sdl2Window instance;
    private bool resize;

    internal Sdl2Window Instance => instance; 

    public int Width => Instance.Width;
    public int Height => Instance.Height;

    public bool Exists => instance.Exists;

    public string Title 
    {
        get => Instance.Title;
        set => Instance.Title = value;
    }

    public int DisplayRefreshRate => DisplayInformation.GetDisplayRefreshRate(@default: () => 60);

    public float DisplayRefreshPerSecond => 1f / DisplayRefreshRate;

    public Vector2 MouseDelta => Instance.MouseDelta;

    internal Window(WindowCreateInfo createInfo)
    {
        instance = VeldridStartup.CreateWindow(createInfo);
        instance.Resized += MarkForResize;
        RegisterEvents();
    }

    void MarkForResize()
        => resize = true;

    internal void InvokeResize()
        => OnWindowEvent?.Invoke(new(EventType.Resized,this));

    public bool GetWindowExits()
        => instance.Exists;

    public InputSnapshot PumpEvents()
        => Instance.PumpEvents();

    void RegisterEvents()
    {
        instance.Closing += InvokeClosing;
        instance.Closed += InvokeClosed;
        instance.FocusLost += InvokeFocusLost;
        instance.FocusGained += InvokeFocusGained;
        instance.Shown += InvokeShown;
        instance.Hidden += InvokeHidden;
        instance.MouseLeft += InvokeMouseLeft;
        instance.MouseEntered += InvokeMouseEntered;
        instance.Exposed += InvokeExposed;
    }

    void UnregisterEvents()
    {
        instance.Closing -= InvokeClosing;
        instance.Closed -= InvokeClosed;
        instance.FocusLost -= InvokeFocusLost;
        instance.FocusGained -= InvokeFocusGained;
        instance.Shown -= InvokeShown;
        instance.Hidden -= InvokeHidden;
        instance.MouseLeft -= InvokeMouseLeft;
        instance.MouseEntered -= InvokeMouseEntered;
        instance.Exposed -= InvokeExposed;
    }

    void InvokeClosing()
        => OnWindowEvent?.Invoke(new(EventType.Closing, this));

    void InvokeClosed()
        => OnWindowEvent?.Invoke(new(EventType.Closed, this));

    void InvokeFocusLost()
        => OnWindowEvent?.Invoke(new(EventType.FocusLost, this));

    void InvokeFocusGained()
        => OnWindowEvent?.Invoke(new(EventType.FocusGained, this));

    void InvokeShown()
        => OnWindowEvent?.Invoke(new(EventType.Shown, this));

    void InvokeHidden()
        => OnWindowEvent?.Invoke(new(EventType.Hidden, this));

    void InvokeMouseLeft()
        => OnWindowEvent?.Invoke(new(EventType.MouseLeft, this));

    void InvokeMouseEntered()
        => OnWindowEvent?.Invoke(new(EventType.MouseEntered, this));

    void InvokeExposed()
        => OnWindowEvent?.Invoke(new(EventType.Exposed, this));

    public void RealtiveMouseMode(bool enabled)
        => Sdl2Native.SDL_SetRelativeMouseMode(enabled);

    public void Dispose()
    {
        UnregisterEvents();
        instance.Resized -= MarkForResize;
        Instance.Close();
    }

    void HandleResize(IApplication application)
    {
        resize = false;

        application.Publish(new EarlyWindowResizeEvent(Width, Height));

        InvokeResize();
    }

    public static implicit operator Sdl2Window(Window d)
        => d.Instance;

    public class System : IAppComponentSystem<Window>
    {
        public void Dispose()
        {}

        public void Initialize(Window comp, IApplication ctx)
        {}

        public void Update(Window window, IApplication application)
        {
            if (window.resize)
                window.HandleResize(application);
        }
    }

}
