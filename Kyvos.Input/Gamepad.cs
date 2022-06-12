using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using Veldrid.Sdl2;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Collections.Generic;
using Kyvos.Core.Logging;
using Kyvos.Core;

namespace Kyvos.Input;

public partial class Gamepad : IDisposable
{
    //TODO move events and processint to a Gamepad Registry class that
    //maps gamepad ids to respective event lists
    //handle events looks up this list from regitry and handles that
    //make gamepad into struct again to alow for inlining

    GamepadState state;

    int controllerIndex;
    SDL_GameController controller;
    public bool IsValid { get; private set; }
    public int ControllerIndex => controllerIndex;
    public string? ControllerName { get; }

    float[] axes;

    public Gamepad() : this(GetFirstController())
    {}

    public unsafe Gamepad(int idx)
    {
        axes = new float[6];
        if (idx == -1 || idx >= Sdl2Native.SDL_NumJoysticks())
        {
            //invalid
            state = GamepadState.Default;
            controller = new();//nullptr
            IsValid = false;
            controllerIndex = -1;
            ControllerName = string.Empty;

            return;
        }

        controller = Sdl2Native.SDL_GameControllerOpen(idx);
        state = GamepadState.Default;

        var joystick = Sdl2Native.SDL_GameControllerGetJoystick(controller);

        controllerIndex = Sdl2Native.SDL_JoystickInstanceID(joystick);

        ControllerName = Marshal.PtrToStringUTF8((IntPtr)Sdl2Native.SDL_GameControllerName(controller));

        GamepadRegistry.RegisterGamepad(this);

        IsValid = true;
    }

    public Vector2 GetAxis(GamepadAxis axis)
    {
        var idx = (int)axis;
        //todo maybe use vector2(ReadonlySpan<float>)
        return new Vector2(axes[idx], axes[idx + 1]);
    }


    /// <summary>
    /// pressed this frame
    /// </summary>
    public bool IsDown(GamepadButton k) => state.IsDown(k);


    /// <summary>
    /// released this frame
    /// </summary>
    public bool IsReleased(GamepadButton k) => state.IsReleased(k);


    /// <summary>
    /// held down
    /// also true on first frame
    /// </summary>
    public bool IsPressed(GamepadButton k) => state.IsPressed(k);

    public void Update() 
    {
        HandleEvents(GamepadRegistry.PumpEvents(this));
    }

    private void HandleEvents(IList<SDL_Event> events) 
    {
        state.Advance();
        for (int i = 0; i < events.Count; i++)
        {
            SDL_Event ev = events[i];
            switch (ev.type)
            {
                case SDL_EventType.ControllerButtonDown:
                case SDL_EventType.ControllerButtonUp:
                    var bEvent = Unsafe.As<SDL_Event, SDL_ControllerButtonEvent>(ref ev);
                    if (bEvent.which == controllerIndex)
                    {

                        state.Update(bEvent);
   
                    }
                    break;
                case SDL_EventType.ControllerAxisMotion:
                    var aEvent = Unsafe.As<SDL_Event, SDL_ControllerAxisEvent>(ref ev);
                    if (aEvent.which == controllerIndex)
                    {
                        if (aEvent.axis == SDL_GameControllerAxis.Invalid || aEvent.axis == SDL_GameControllerAxis.Max)
                            break;

                        axes[(byte)aEvent.axis] = NormalizeAxisValue(aEvent.value);
                    }
                    break;
            }
        }
        events.Clear();
    }


    float NormalizeAxisValue(short value)
    {
        return value < 0 ? -(value / (float)short.MinValue) : (value / (float)short.MaxValue);
    }

    public void Dispose()
    {
        if (!IsValid)
            return;

        Log<Gamepad>.Debug("Disposing gamepad");

        GamepadRegistry.Unregister(this);
        Sdl2Native.SDL_GameControllerClose(controller);
        IsValid = false;

        
    }

    private static int GetFirstController()
    {
        Sdl2Native.SDL_Init(SDLInitFlags.GameController);
        int count = Sdl2Native.SDL_NumJoysticks();
        for (int i = 0; i < count; i++)
        {
            if (Sdl2Native.SDL_IsGameController(i))
                return i;
        }
        return -1;
    }
}

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


    public static void RegisterGamepad(Gamepad gamePad) 
    {
        lock (lockObj) 
        {
            if (!registry.ContainsKey(gamePad.ControllerIndex))
            {
                registry.Add(gamePad.ControllerIndex, new());
            }
        }
    }
    public static void Unregister(Gamepad gamepad) 
    {
        lock (lockObj) 
        {
            registry.Remove(gamepad.ControllerIndex);
        }
    }

    public static IList<SDL_Event> PumpEvents(Gamepad gamepad) 
    {
        if (registry.TryGetValue(gamepad.ControllerIndex, out var list))
            return list;
        return Array.Empty<SDL_Event>();
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
