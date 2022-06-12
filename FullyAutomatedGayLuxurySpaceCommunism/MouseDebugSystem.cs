using Kyvos.Input;
using DefaultEcs.System;
using DefaultEcs;
using System;
using Kyvos.Core.Logging;
using Kyvos.Maths;

namespace FullyAutomatedGayLuxurySpaceCommunism;

public class MouseDebugSystem : AComponentSystem<float, MouseAndKeyboard> 
{
    MouseButton[] mouseButtons;
    int[] counts;

    public MouseDebugSystem(World w):base(w)
    {
        mouseButtons = Enum.GetValues<MouseButton>();
        counts = new int[mouseButtons.Length];
    }

    protected override void Update(float state, ref MouseAndKeyboard input)
    {
        foreach (var button in mouseButtons) 
        {
            if (input.IsDown(button)) 
            {
                Log<MouseDebugSystem>.Debug("{Button}: is down {IsDown}", button, input.IsDown(button));
            }
            if (input.IsPressed(button)) 
            {
                counts[(int)button]++;
            }
            if (input.IsReleased(button)) 
            {
                Log<MouseDebugSystem>.Debug("{Button}: was pressed for {Count}", button, counts[(int)button]);
                counts[(int)button] = 0;
                Log<MouseDebugSystem>.Debug("{Button}: is released {IsReleased}", button, input.IsReleased(button));
            }
        }

        if (input.IsPressed(MouseButton.Left)) 
        {
            Log<MouseDebugSystem>.Debug("Mouse Position: {Position}", input.MousePosition);
            Log<MouseDebugSystem>.Debug("Mouse Delta: {Delta}", input.MouseDelta);
        }
        if (!Mathf.AlmostEquals(input.ScrollWheelDelta,0)) 
        {            
            Log<MouseDebugSystem>.Debug("Mouse Scroll Delta: {Delta}", input.ScrollWheelDelta);
        }
        
    }

}