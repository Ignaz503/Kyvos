using System.Runtime.CompilerServices;
using Veldrid.Sdl2;
using Kyvos.Utility.Collections;

namespace Kyvos.Input;

internal static class GamepadRegistry
{
    static object lockObj = new();
    //TODO maybe instead of using a Dictionary use an array of list wrapper objects, as there are only so many gamepads
    //ensure length on registry and on unregister call remove on warpper objects
    //wrapper object allows access IList<T> 
    static Dictionary<int, List<SDL_Event>> registry = new();

    static GamepadRegistry()
    {
        Sdl2Events.Subscribe(ProcessEvent);    
    }


    public static void RegisterGamepad(int controllerIndex) 
    {
        lock (lockObj) 
        {
            if (!registry.ContainsKey(controllerIndex))
            {
                registry.Add(controllerIndex, new());
            }
        }
    }
    public static void Unregister(int controllerIndex) 
    {
        lock (lockObj) 
        {
            registry.Remove(controllerIndex);
        }
    }

    public static IList<SDL_Event> PumpEvents(int controllerIndex) 
    {
        if (registry.TryGetValue(controllerIndex, out var list))
            return list;
        return FakeList<SDL_Event>.Instance;
    }

    static void ProcessEvent(ref SDL_Event ev)
    {
        int idx = -1;
        switch (ev.type)
        {
            case SDL_EventType.ControllerButtonDown:
            case SDL_EventType.ControllerButtonUp:
                var bEvent = Unsafe.As<SDL_Event, SDL_ControllerButtonEvent>(ref ev);
                idx = bEvent.which;
                break;
            case SDL_EventType.ControllerAxisMotion:
                var motionEvent = Unsafe.As<SDL_Event, SDL_ControllerAxisEvent>(ref ev);
                idx = motionEvent.which;
                break;
        }

        if (idx != -1 && registry.TryGetValue(idx,out var events) )
        {
            events.Add(ev);
        }
    }

}
