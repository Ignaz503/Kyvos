using System;
using Kyvos.Maths.Topology;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing;
using SixLabors.ImageSharp.PixelFormats;
using System.Linq;
using System.Collections.Generic;
using System.Numerics;

public static class FloatFieldVisualizer<T>
    where T : unmanaged, IPixel<T>
{
    public static Image<T> Visualize(FloatField map, Func<int, int, float,Color> mapping)

    {

        var img = new Image<T>(map.Width, map.Height);

        for (int x = 0; x < map.Width; x++)
        {
            for (int y = 0; y < map.Height; y++)
            {
                img[x, y] = mapping(x, y, map[x, y]).ToPixel<T>();
            }
        }
        return img;
    }

    public static Image<T> VisualizeDerivative(FloatField map,Func<(Vector2 firstOder, Vector3 secondOrder),float> derivGetter, Gradient gradient)
    {
        return Visualize(map, (int x, int y, float value) =>{

            var fx = ((float)x / map.Width) * map.ResolutionDependendWidth();
            var fy = ((float)y / map.Height) * map.ResolutionDependendHeight();

            map.GetDerivatives(fx, fy, out Vector2 first, out Vector3 second);
            return gradient.Evaluate(derivGetter((firstOder: first, secondOrder:second)));
        });
    }
    public static Image<T> Visualize(FloatField map, Func<int, int, float, (Vector2 firstOder, Vector3 secondOrder), Color> colorPicker)
    {
        return Visualize(map, (int x, int y, float value) => {

            var fx = ((float)x / map.Width) * map.ResolutionDependendWidth();
            var fy = ((float)y / map.Height) * map.ResolutionDependendHeight();

            map.GetDerivatives(fx, fy, out Vector2 first, out Vector3 second);
            return colorPicker(x, y, value, (firstOder: first, secondOrder: second));
        });
    }

}


public class Gradient
{
    public struct Key
    {
        public Color Color { get; init; }
        public float Ratio { get; init; }
    }
    public enum RepetitionMode 
    {
        None,
        Repeat,
        Reflect,
        Transparent
    }

    private IEnumerable<Key> keys;

    public IEnumerable<Key> Keys 
    { 
        get => keys;
        set => keys = value.OrderBy(key => key.Ratio);
    }
    public RepetitionMode RepeatMode { get; set; }

    public Gradient(RepetitionMode mode)
    {
        keys = new[] { new Key() { Color = Color.White,Ratio = 1f } };
        this.RepeatMode = mode;
    }

    public Gradient(IEnumerable<Key> tempKeys, RepetitionMode mode)
    {
        this.Keys = tempKeys;
        this.RepeatMode = mode;
    }

    public Color Evaluate(float x)
    {
        if (RepeatMode == RepetitionMode.Transparent)
            if (x < 0f || x > 1f)
                return Color.Transparent;

        x = HandleRepeat(x);

        var (start, stop) = GetSegment(x);


        if (start.Color.Equals(stop.Color))
            return start.Color;

        var local = (x - start.Ratio) / (stop.Ratio - start.Ratio);

        return new Color(Vector4.Lerp((Vector4)start.Color, (Vector4)stop.Color, local));
    }

    (Key start, Key stop) GetSegment(float x) 
    {
        Key from = keys.First();
        Key to = default;

        foreach (var key in keys) 
        {
            to = key;
            if (key.Ratio > x)
                break;
            from = to;
        }
        return (to, from);
    }

    float HandleRepeat(float inp) 
    {
        switch (RepeatMode) 
        {
            case RepetitionMode.None:
                break;
            case RepetitionMode.Repeat:
                inp %= 1f;
                break;
            case RepetitionMode.Reflect:
                inp %= 2f;
                if (inp > 1) 
                {
                    inp = 2.0f - inp;
                }
                break;
            default:
                break;
        }
        return inp;
    }
}