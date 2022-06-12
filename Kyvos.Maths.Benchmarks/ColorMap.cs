#pragma warning disable CA1416

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
public class ColorMap
{
    public struct ColMapEntry
    {
        public float threshold;
        public Color color;

        public override string ToString()
        {
            return $"{{t: {threshold}, color: {color}}}";
        }
    }

    List<ColMapEntry> entries = new();
    public Color ErrorColor { get; set; } = Color.Magenta;
    public ColorMap(List<ColMapEntry> entries)
    {
        this.entries = entries.OrderBy( e => e.threshold ).ToList();
        
    }
    public void AddEntry( float threshold, Color color )
    {
        entries.Add( new() { threshold = threshold, color = color } );
    }

    public void Print(Action<string> log) 
    {
        if (log == null)
            return;
        foreach (var entry in entries)
            log( entry.ToString() );

    }

    public Color this[float t]
    {
        get
        {
            foreach (var entry in entries)
            {
                if (t < entry.threshold)
                    return entry.color;
            }
            return ErrorColor;
        }
    }

    public static ColorMap DefaultTerrainMap = new( new()
    {
        new(){ threshold = 0.2f, color = Color.DarkBlue},
        new(){ threshold = 0.35f, color = Color.Blue},
        new(){ threshold = 0.40f, color = Color.LightYellow},
        new(){ threshold = 0.475f, color = Color.Yellow},
        new(){ threshold = 0.55f, color = Color.Green},
        new(){ threshold = 0.65f, color = Color.DarkGreen},
        new(){ threshold = 0.7f, color = Color.Brown},
        new(){ threshold = 0.85f, color = Color.DarkGray},
        new(){ threshold = 0.775f, color = Color.LightGray},
        new(){ threshold = 1f, color = Color.White},
    } );

}


#pragma warning restore CA1416