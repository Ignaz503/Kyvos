using Kyvos.Input;
using DefaultEcs.System;
using DefaultEcs;
using System;
using System.Diagnostics;
using Kyvos.Core.Logging;

namespace FullyAutomatedGayLuxurySpaceCommunism;

public class KeyboardDebugSystem : AComponentSystem<float,MouseAndKeyboard>
{

    Key[] values;
    int[] counts;
    
    public KeyboardDebugSystem(World w) :base(w)
    {
        values =  Enum.GetValues<Key>();
        counts = new int[values.Length];
    }



    protected override void Update(float state, ref MouseAndKeyboard input)
    {

        foreach (var key in values)
        { 
            if (input.IsDown(key))
            {
                Log<KeyboardDebugSystem>.Debug("{Key} is down: {IsDown}",key,input.IsDown(key));
            }
            if (input.IsPressed(key)) 
            {
                counts[(int)key]++;
            }
            if (input.IsReleased(key))
            {
                Log<KeyboardDebugSystem>.Debug("{Key} was pressed for {Count} frames", key, counts[(int)key]);
                counts[(int)key] = 0;
                Log<KeyboardDebugSystem>.Debug("{Key} is released: {IsReleased}", key, input.IsReleased(key));
            }
        }
    }
}
