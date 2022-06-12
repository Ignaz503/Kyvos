using Kyvos.Input;
using DefaultEcs.System;
using System;
using System.Diagnostics;
using DefaultEcs;
using Kyvos.Core.Logging;

namespace FullyAutomatedGayLuxurySpaceCommunism;

public class GamepadDebugSystem : AComponentSystem<float,Gamepad>
{

    GamepadButton[] values;
    GamepadAxis[] axes;
    int[] count;

    public GamepadDebugSystem(World w):base(w)
    {
        values = Enum.GetValues<GamepadButton>();
        axes = Enum.GetValues<GamepadAxis>();
        count = new int[values.Length];
    }

    protected override void Update(float state, ref Gamepad gamepad)
    {

        foreach (GamepadButton button in values)
        {
            if (gamepad.IsDown(button))
            {
                Log<GamepadDebugSystem>.Information("{Button}: is down? {IsDown}", button, gamepad.IsDown(button));
            }
            if (gamepad.IsPressed(button))
            {
                count[(int)button]++;
            }
            if (gamepad.IsReleased(button))
            {
                Log<GamepadDebugSystem>.Information("{Button}: was pressed for {Count} frames", button, count[(int)button]);
                count[(int)button] = 0;
                Log<GamepadDebugSystem>.Information("{Button}: is released? {IsReleased}", button, gamepad.IsReleased(button));
            }
        }

        foreach (var axis in axes) 
        {
            var readout = gamepad.GetAxis(axis);
            if (readout.LengthSquared() > 0.01) 
            {
                Log<GamepadDebugSystem>.Information("{Axis}: {Value}", axis, readout);
            }
        }

    }
}
