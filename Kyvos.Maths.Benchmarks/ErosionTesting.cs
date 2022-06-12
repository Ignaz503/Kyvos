using System;
using Kyvos.Maths.NoiseFunctions;
using Kyvos.Maths.Topology;
using System.Drawing;

namespace Kyvos.Maths.Benchmarks
{
    public class ErosionTesting 
    {
        public void Do(Func<float,float,float> noiseSampler) 
        {
            const int size = 512;
            FloatField map = new(size,size,initializer: (x,y) => 
            {
                float modX = (float)x / (float)size * 2f - 1f;
                float modY = (float)y / (float)size * 2f - 1f;

                float value = Evaluate(MathF.Max (MathF.Abs (modX), MathF.Abs (modY)));


                var n = noiseSampler(x,y);  
                return Math.Clamp(n-value,0,1);
            } );
            WriteToFile( $@"C:\Users\lukas\Documents\PerlinTest\", map, -1 );
            Erosion.Hydraulic.Apply( map,i =>{
                if (i % 10000 == 0)
                    Console.WriteLine(i);
            
            },
            dropsPerCel:5);
            WriteToFile( $@"C:\Users\lukas\Documents\PerlinTest\", map, int.MaxValue);
        }

        public void DoThreaded(Func<float, float, float> noiseSampler)
        {
            const int size = 512;
            FloatField map = new(size, size, initializer: (x, y) =>
            {
                float modX = (float)x / (float)size * 2f - 1f;
                float modY = (float)y / (float)size * 2f - 1f;

                float value = Evaluate(MathF.Max(MathF.Abs(modX), MathF.Abs(modY)));


                var n = noiseSampler(x, y);
                return Math.Clamp(n - value, 0, 1);
            });
            WriteToFile($@"C:\Users\lukas\Documents\PerlinTest\Threaded-", map, -1);
            Erosion.Hydraulic.ApplyThreaded(map, i => {
                if (i % 10000 == 0)
                    Console.WriteLine(i);

            }, dropsPerCel: 5);
            WriteToFile($@"C:\Users\lukas\Documents\PerlinTest\Threaded-", map, int.MaxValue);
        }

        public void DoThreadedMassive(Func<float, float, float> noiseSampler)
        {
            const int size = 2048;
            FloatField map = new(size, size, initializer: (x, y) =>
            {
                float modX = (float)x / (float)size * 2f - 1f;
                float modY = (float)y / (float)size * 2f - 1f;

                float value = Evaluate(MathF.Max(MathF.Abs(modX), MathF.Abs(modY)));


                var n = noiseSampler(x, y);
                return Math.Clamp(n - value, 0, 1);
            });
            WriteToFile($@"C:\Users\lukas\Documents\PerlinTest\Threaded-", map, -1);
            Erosion.Hydraulic.ApplyThreaded(map, i => {
                if (i % 10000 == 0)
                    Console.WriteLine(i);

            }, dropsPerCel: 5);
            WriteToFile($@"C:\Users\lukas\Documents\PerlinTest\Threaded-", map, int.MaxValue);
        }

        public FloatField GenerateSomeHeightmapThreaded(Func<float, float, float> noiseSampler)
        {
            const int size = 512;
            FloatField map = new(size, size, initializer: (x, y) =>
            {
                float modX = (float)x / (float)size * 2f - 1f;
                float modY = (float)y / (float)size * 2f - 1f;

                float value = Evaluate(MathF.Max(MathF.Abs(modX), MathF.Abs(modY)));


                var n = noiseSampler(x, y);
                return Math.Clamp(n - value, 0, 1);
            });
            
            Erosion.Hydraulic.ApplyThreaded(map, i => {
                if (i % 10000 == 0) 
                    Console.WriteLine(i);


            }, dropsPerCel: 5);
            return map;
        }


        public FloatField GenerateSomeHeightmap(Func<float, float, float> noiseSampler)
        {
            const int size = 512;
            FloatField map = new(size, size, initializer: (x, y) =>
            {
                float modX = (float)x / (float)size * 2f - 1f;
                float modY = (float)y / (float)size * 2f - 1f;

                float value = Evaluate(MathF.Max(MathF.Abs(modX), MathF.Abs(modY)));


                var n = noiseSampler(x, y);
                return Math.Clamp(n - value, 0, 1);
            });

            Erosion.Hydraulic.Apply(map, i => {
                if (i % 10000 == 0)
                    Console.WriteLine(i);


            }, dropsPerCel: 5);
            return map;
        }


#pragma warning disable CA1416

        void WriteToFile(string filePath, FloatField map, int i) 
        {
            using Bitmap b = new(map.Width,map.Height);

            for (int x = 0; x < map.Width; x++)
            {
                for (int y = 0; y < map.Height; y++)
                {
                    var val = map[x,y];
                    int colVal = (int)MathF.Round(255*val);
                    colVal = Math.Clamp( colVal, 0, 255 );

                    b.SetPixel( x, y, Color.FromArgb( 255, colVal, colVal, colVal ) );
                }
            }
            b.Save( filePath + $"HydraulicErosion-Iteration-{i}.png", System.Drawing.Imaging.ImageFormat.Png );
        }
        
        void WriteToFile(string filePath, FloatField map, int i, ColorMap colMap) 
        {
            using Bitmap b = new(map.Width,map.Height);

            for (int x = 0; x < map.Width; x++)
            {
                for (int y = 0; y < map.Height; y++)
                {
                    var val = map[x,y];

                    var colVal = colMap[val];


                    b.SetPixel( x, y, colVal );
                }
            }
            b.Save( filePath + $"HydraulicErosion-Final.png", System.Drawing.Imaging.ImageFormat.Png );
        }
#pragma warning restore CA1416

        static float Evaluate( float value )
        {
            float a = 3;
            float b = 2.2f;

            return MathF.Pow( value, a ) / (MathF.Pow( value, a ) + MathF.Pow( b - b * value, a ));
        }
    }
}
