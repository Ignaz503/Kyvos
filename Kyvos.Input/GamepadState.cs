using System.Diagnostics;
using System.Runtime.CompilerServices;
using Veldrid.Sdl2;

namespace Kyvos.Input;

internal struct GamepadState
{
    internal long advanceCalls;
    internal GamepadStateData previous;
    internal GamepadStateData current;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal bool CheckKeyCurrent(GamepadButton t) => current[t];

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal bool CheckKeyPrevious(GamepadButton t) => previous[t];

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal bool IsPressed(GamepadButton t) => CheckKeyCurrent(t);// && CheckKeyPrevious(t);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal bool IsDown(GamepadButton t) => CheckKeyCurrent(t) && !CheckKeyPrevious(t);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal bool IsReleased(GamepadButton t) => !CheckKeyCurrent(t) && CheckKeyPrevious(t);


    internal void Advance()
    {
        advanceCalls++;
        previous = current;
    }

    internal void Update(SDL_ControllerButtonEvent @event)
    {

        if (@event.button == SDL_GameControllerButton.Invalid)
            return;
            
        current[(GamepadButton)((byte)@event.button)] = @event.state == 1;

    }


    public override string ToString()
    {
        return $"{{{advanceCalls} c: {current} p: {previous}}}";
    }

    internal static GamepadState Default => new GamepadState() { current = GamepadStateData.Default, previous = GamepadStateData.Default };
}


