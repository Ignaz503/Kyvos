#pragma warning disable CA1416

using Kyvos.Maths.NoiseFunctions;
using System;
using System.Drawing;
using System.Drawing.Imaging;

public class MinMaxTrackingFloatField : IFloatField
{
    float[,] b;
    float min,max;

    public MinMaxTrackingFloatField( int size )
    {
        b = new float[size, size];
        min = float.MaxValue;
        max = float.MinValue;
    }

    public float this[int x, int y]
    {
        get => b[x, y];
        set
        {
            b[x, y] = value;
            UpdateMinMax( value );
        }
    }

    public int Size => b.GetLength( 0 );

    public NoiseType Type => NoiseType.Value;

    public void PrintMinMax()
    {
        Console.WriteLine( $"Min: {min}, Max {max}" );
    }


    void UpdateMinMax( float value )
    {
        if (value < min)
            min = value;
        if (value > max)
            max = value;
    }

    public (float min, float max) MinMax()
        => (min, max);


    public void ForEach(Func<int, int, float, float> action)
    {
        for (int x = 0; x < Size; x++)
        {
            for (int y = 0; y < Size; y++)
            {
                this[x, y] = action( x, y, this[x, y] );
            }
        }
    }


    public void Save( string path )
    {
        using Bitmap bit = new(Size,Size);
        for (int x = 0; x < Size; x++)
        {
            for (int y = 0; y < Size; y++)
            {
                var val = b[x,y];
                int colVal = (int)MathF.Round(255*val);
                colVal = Math.Clamp( colVal, 0, 255 );

                bit.SetPixel( x, y, Color.FromArgb( 255, colVal, colVal, colVal ) );
            }
        }

        bit.Save( path, ImageFormat.Png );
    }


}


#pragma warning restore CA1416