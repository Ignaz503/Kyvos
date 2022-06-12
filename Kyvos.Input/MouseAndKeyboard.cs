using Kyvos.Core;
using Kyvos.VeldridIntegration;
using System.Numerics;
using Veldrid;

namespace Kyvos.Input;


public partial struct MouseAndKeyboard
{
    KeyboardState state;

    float scrollWheelDelta;
    public float ScrollWheelDelta => scrollWheelDelta;

    Vector2 mousePosition;
    public Vector2 MousePosition => mousePosition;

    Vector2 mouseDelta;
    public Vector2 MouseDelta => mouseDelta;

    public MouseAndKeyboard()
    {
        state = KeyboardState.Default;
        scrollWheelDelta = 0;
        mousePosition = Vector2.Zero;
        mouseDelta = Vector2.Zero;
    }

    /// <summary>
    /// pressed this frame
    /// </summary>
    public bool IsDown(Key k) => state.IsDown(k);

    /// <summary>
    /// pressed this frame
    /// </summary>
    public bool IsDown(MouseButton b) => state.IsDown(b);

    /// <summary>
    /// released this frame
    /// </summary>
    public bool IsReleased(Key k) => state.IsReleased(k);

    /// <summary>
    /// released this frame
    /// </summary>
    public bool IsReleased(MouseButton b) => state.IsReleased(b);

    /// <summary>
    /// held down
    /// also true on first frame
    /// </summary>
    public bool IsPressed(Key k) => state.IsPressed(k);

    /// <summary>
    /// held down
    /// also true on first frame
    /// </summary>
    public bool IsPressed(MouseButton b) => state.IsPressed(b);


    private void Update(InputSnapshot snapshot, Window window)
    {

        state.Advance();

        mouseDelta = window.MouseDelta;

        var keyEvents = snapshot.KeyEvents;

        scrollWheelDelta = snapshot.WheelDelta;

        mousePosition = snapshot.MousePosition;


        for (int i = 0; i < keyEvents.Count; i++)
        {
            state.Update(keyEvents[i]);
        }

        var mouseEvents = snapshot.MouseEvents;

        for (int i = 0; i < mouseEvents.Count; i++)
        {
            state.Update(mouseEvents[i]);
        }

    }

}


