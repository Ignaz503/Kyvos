#pragma warning disable CA1416

using System;
using System.Drawing;
using System.Drawing.Imaging;

public class NoiseToPng 
{
    public static void CreateNoise( int width, int height, string absoluteFileName, Func<float,float,float> sampler ) 
    {
        using Bitmap b = new(width,height);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var xCoord = (float)x / (float)width;
                var yCoord = (float)y / (float)height;

                var val = sampler(xCoord,yCoord);
                int colVal = (int)MathF.Round(255*val);
                colVal = Math.Clamp( colVal, 0, 255 );

                b.SetPixel( x, y, Color.FromArgb(255, colVal, colVal, colVal ) );
            }
        }
        b.Save( absoluteFileName, ImageFormat.Png );
    }

    public static void CreateNoise( int width, int height, string absoluteFileName, Func<float, float, float> sampler, ColorMap colMap )
    {
        using Bitmap b = new(width,height);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var xCoord = (float)x / (float)width;
                var yCoord = (float)y / (float)height;

                var val = sampler(xCoord,yCoord);

                var colVal = colMap[val];


                b.SetPixel( x, y, colVal );
            }
        }
        b.Save( absoluteFileName, ImageFormat.Png );
    }
}


#pragma warning restore CA1416