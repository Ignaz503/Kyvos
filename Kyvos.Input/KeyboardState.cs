using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Kyvos.Input;
internal struct KeyboardState
{
    KeyboardStateData previous;
    KeyboardStateData current;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal bool CheckKeyCurrent(Key t) => current[t];

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal bool CheckKeyPrevious(Key t) => previous[t];

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal bool IsPressed(Key t) => CheckKeyCurrent(t);// && CheckKeyPrevious(t);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal bool IsDown(Key t) => CheckKeyCurrent(t) && !CheckKeyPrevious(t);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal bool IsReleased(Key t) => !CheckKeyCurrent(t) && CheckKeyPrevious(t);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal bool CheckButtonCurrent(MouseButton t) => current[t];

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal bool CheckButtonPrevious(MouseButton t) => previous[t];

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal bool IsPressed(MouseButton t) => CheckButtonCurrent(t);// && CheckButtonPrevious(t);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal bool IsDown(MouseButton t) => CheckButtonCurrent(t) && !CheckButtonPrevious(t);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal bool IsReleased(MouseButton t) => !CheckButtonCurrent(t) && CheckButtonPrevious(t);

    internal void Advance()
    {
        previous = current;
    }

    internal void Update(Veldrid.KeyEvent @event)
    {
        current[(Key)(int)@event.Key] = @event.Down;
    }

    internal void Update(Veldrid.MouseEvent @event)
    {
        current[(MouseButton)(int)@event.MouseButton] = @event.Down;
    }

    internal string Stringify()
    {
        return $"current State: {current}\nprevious State: {previous}";
    }

    internal static KeyboardState Default => new KeyboardState() { current = KeyboardStateData.Default, previous = KeyboardStateData.Default };
}



