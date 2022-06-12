using System;
using System.IO;
using System.Linq;
using BenchmarkDotNet;
using BenchmarkDotNet.Running;
using Kyvos.Maths.NoiseFunctions;
using Kyvos.Maths.Graphs;
using System.Threading;
using System.Runtime.CompilerServices;
using System.Diagnostics;
using SixLabors.ImageSharp.PixelFormats;
using System.Numerics;
using SixLabors.ImageSharp;
using System.Collections.Generic;
using Kyvos.Maths.Topology;

namespace Kyvos.Maths.Benchmarks
{

    public enum TestEnum
    {
        a, b, c, d, e, f, g, h, i, j, k, l
    }
    class Program
    {
        static void Main( string[] args )
        {
            //BenchmarkRunner.Run<InterpolationBenchmarks>();
            //BenchmarkRunner.Run<PerlinNoiseBenchMark>();
            //BenchmarkRunner.Run<NoiseBenchmarks>();

            BenchmarkRunner.Run<BitConvertBenchmark<TestEnum>>();

            //NonBenchmarks();

            Console.WriteLine( "Done" );
            Console.ReadKey();
        }

        static void NonBenchmarks() 
        {
            CreateDirs();

            //AllVornoiTypes();
            //AllPerlin();
            //AllValue();
            //AllValueCubic();
            //AllOpenSimplex2S();
            //AllOpenSimplex();
            //DomainWarpPerlin();

            //CloudTest();

            //TimingTests();

            TopologyTests();
            //DiffTesting();
            //GradientTests();
            //Massive();
            //Indexing2D(25,25);
            //Console.ReadKey();
            //Indexing3D(25,25,25);

            //AnalTest();
        }

        static Gradient CreateCoolToWarmGradient() 
        {
            return new Gradient(new Gradient.Key[]
            {
                new Gradient.Key(){ Ratio = 0f, Color = new Color(new Argb32(80, 80, 230))},
                new Gradient.Key(){ Ratio = 1f/9f, Color = new Color(new Argb32(80, 180, 230))},
                new Gradient.Key(){ Ratio = 2f/9f, Color = new Color(new Argb32(80, 230, 230))},
                new Gradient.Key(){ Ratio = 3f/9f, Color = new Color(new Argb32(80, 230, 180))},
                new Gradient.Key(){ Ratio = 4f/9f, Color = new Color(new Argb32(80, 230, 80))},
                new Gradient.Key(){ Ratio = 5f/9f, Color =new Color(new Argb32(180, 230, 80))},
                new Gradient.Key(){ Ratio = 6f/9f, Color = new Color(new Argb32(230, 230, 80))},
                new Gradient.Key(){ Ratio = 7f/9f, Color = new Color(new Argb32(230, 180, 80))},
                new Gradient.Key(){ Ratio = 1f, Color =new Color(new Argb32(230, 80, 80))},

            }, Gradient.RepetitionMode.None);

        }

        static Color Colorize(float v, float exponent, bool nonNegative, Gradient g) 
        {
            if (exponent > 0f)
            {
                float sign = MathF.Sign(v);
                float pow = MathF.Pow(10, exponent);
                float log = MathF.Log(1.0f + pow * MathF.Abs(v));
                v = sign * log;
            }

            if (nonNegative)
                return g.Evaluate(v);
            else 
            {
                if (v > 0f)
                    return g.Evaluate(v);
                else
                    return g.Evaluate(-v);
            }

        }

        static void Massive() 
        {
            var eroT = new ErosionTesting();
            eroT.DoThreadedMassive((x, y) => Noise.OpenSimplex01(x, y));
        }

        static void TimingTests() 
        {
            var eroTest = new ErosionTesting();

            Stopwatch stopwatch1 = Stopwatch.StartNew();
            eroTest.Do((x, y) =>
            {
                return Noise.OpenSimplex01(x, y);
                //DomainWarp.FractalIndependent(ref x, ref y,
                //    warpType: DomainWarp.Type.OpenSimplex2Reduced,
                //    warpAmp: 50f,
                //    freq: 0.015f,
                //    octaves: 10,
                //    lacunarity: 2.5f,
                //    gain: 0.5f);

                //return Noise.Layered.Derivative01(x, y, new PerlinNoiseGenerator());
            });
            stopwatch1.Stop();

            Stopwatch stopwatch2 = Stopwatch.StartNew();
            eroTest.DoThreaded((x, y) =>
            {
                return Noise.OpenSimplex01(x, y);
                //DomainWarp.FractalIndependent(ref x, ref y,
                //    warpType: DomainWarp.Type.OpenSimplex2Reduced,
                //    warpAmp: 50f,
                //    freq: 0.015f,
                //    octaves: 10,
                //    lacunarity: 2.5f,
                //    gain: 0.5f);

                //return Noise.Layered.Derivative01(x, y, new PerlinNoiseGenerator());
            });
            stopwatch2.Stop();
            Console.WriteLine($"Non-Threaded time: {stopwatch1.Elapsed}");
            Console.WriteLine($"Threaded time: {stopwatch2.Elapsed}");
        }

        static void TopologyTests()
        {
            var eroTest = new ErosionTesting();
            var map = eroTest.GenerateSomeHeightmapThreaded((x, y) =>
            {
                //return Noise.OpenSimplex01(x, y);
                DomainWarp.FractalIndependent(ref x, ref y,
                    warpType: DomainWarp.Type.OpenSimplex2Reduced,
                    warpAmp: 200f,
                    freq: 0.010f,
                    octaves: 10,
                    lacunarity: 1.5f,
                    gain: 0.5f);

                return Noise.Layered.Derivative01(x, y, new OpenSimplex2SNoiseGenerator());
            });
            var bw = new Gradient(new Gradient.Key[]
{
                            new Gradient.Key(){Ratio = 0f, Color = Color.Black },
                            new Gradient.Key(){ Ratio = 1f, Color = Color.White}
}, Gradient.RepetitionMode.Repeat);
            var img = FloatFieldVisualizer<Argb32>.Visualize(map,
            (x, y, value) =>
            {
                return bw.Evaluate(value);
            });
            img.SaveAsPng(MakePath("BasicHeightMap.png"));
            TopologyTests(map);

            LandformTests(map);
        }

        static void DiffTesting() 
        {
            var eroTest = new ErosionTesting();
            var map = eroTest.GenerateSomeHeightmap((x, y) =>
            {
                return Noise.OpenSimplex01(x, y);
                //DomainWarp.FractalIndependent(ref x, ref y,
                //    warpType: DomainWarp.Type.OpenSimplex2Reduced,
                //    warpAmp: 50f,
                //    freq: 0.015f,
                //    octaves: 10,
                //    lacunarity: 2.5f,
                //    gain: 0.5f);

                //return Noise.Layered.Derivative01(x, y, new PerlinNoiseGenerator());
            });
            var bw = new Gradient(new Gradient.Key[]{
                new Gradient.Key(){Ratio = 0f, Color = Color.Black },
                new Gradient.Key(){ Ratio = 1f, Color = Color.White}
            }, Gradient.RepetitionMode.Repeat);
            var img = FloatFieldVisualizer<Argb32>.Visualize(map,
            (x, y, value) =>
            {
                return bw.Evaluate(value);
            });
            img.SaveAsPng(MakePath("BasicHeightNotThreaded.png"));
            var mapThreaded = eroTest.GenerateSomeHeightmapThreaded((x, y) =>
            {
                return Noise.OpenSimplex01(x, y);
                //DomainWarp.FractalIndependent(ref x, ref y,
                //    warpType: DomainWarp.Type.OpenSimplex2Reduced,
                //    warpAmp: 50f,
                //    freq: 0.015f,
                //    octaves: 10,
                //    lacunarity: 2.5f,
                //    gain: 0.5f);

                //return Noise.Layered.Derivative01(x, y, new PerlinNoiseGenerator());
            });
            img = FloatFieldVisualizer<Argb32>.Visualize(mapThreaded,
            (x, y, value) =>
            {
                return bw.Evaluate(value);
            });
            img.SaveAsPng(MakePath("BasicHeightMapThreaded.png"));

            var diff = FloatField.AbsoluteDiff(map, mapThreaded);
            img = FloatFieldVisualizer<Argb32>.Visualize(diff,(x,y,v)=> bw.Evaluate(v));
            img.SaveAsPng(MakePath("DiffFromHeightmaps.png"));
        }

        static void GradientTests() 
        {
            var eroTest = new ErosionTesting();
            var map = eroTest.GenerateSomeHeightmapThreaded((x, y) => {
                return Noise.OpenSimplex01(x, y);
                //DomainWarp.FractalIndependent(ref x, ref y,
                //    warpType: DomainWarp.Type.OpenSimplex2Reduced,
                //    warpAmp: 50f,
                //    freq: 0.015f,
                //    octaves: 10,
                //    lacunarity: 2.5f,
                //    gain: 0.5f);

                //return Noise.Layered.Derivative01(x, y, new PerlinNoiseGenerator());
            });

            var bw = new Gradient(new Gradient.Key[]
            {
                new Gradient.Key(){Ratio = 0f, Color = Color.Black },
                new Gradient.Key(){ Ratio = 1f, Color = Color.White}
            }, Gradient.RepetitionMode.Repeat);
            var res = FloatFieldVisualizer<Argb32>.Visualize(map,
                (x, y, value) => 
                {
                    return bw.Evaluate(value);
                });
            res.SaveAsPng(MakePath("BasicHeightMap.png"));

            Console.WriteLine("Done basic");
            Gradient g = new Gradient(
                new Gradient.Key[]
                {
                    new Gradient.Key(){Ratio = 0f, Color = Color.Blue},
                    new Gradient.Key(){ Ratio = 0.5f, Color = Color.DarkGray},
                    new Gradient.Key(){Ratio=  1f, Color = Color.Red}
                }, Gradient.RepetitionMode.Repeat);

            Console.WriteLine("first order x");
            res = FloatFieldVisualizer<Argb32>.VisualizeDerivative(map,
                (data) =>
                {
                    var x = data.firstOder.X + data.firstOder.Y;
                    x = Mathf.Map(x, -1, 1, 0, 1);
                    return x;
                },g);
            res.SaveAsPng(MakePath("GradientFirstOrder.png"));


            Console.WriteLine("second order x");
            res = FloatFieldVisualizer<Argb32>.VisualizeDerivative(map,
                (data) => { 
                    var x = data.secondOrder.X + data.secondOrder.Y + data.secondOrder.Z;

                    x = Mathf.Map(x, -1, 1, 0, 1);
                    return x;
                }, g);
            res.SaveAsPng(MakePath("GradientScondOrder.png"));
        }

        static void LandformTests(FloatField map) 
        {

            var bw = new Gradient(new Gradient.Key[]{
                new Gradient.Key(){Ratio = 0f, Color = Color.Black },
                new Gradient.Key(){ Ratio = 1f, Color = Color.White}}, Gradient.RepetitionMode.Repeat);

            Gradient g = new Gradient(
                new Gradient.Key[]
                {
                    new Gradient.Key(){Ratio = 0f, Color = Color.Blue},
                    new Gradient.Key(){ Ratio = 0.5f, Color = Color.DarkGray},
                    new Gradient.Key(){Ratio=  1f, Color = Color.Red}
                }, Gradient.RepetitionMode.Repeat);

            Dictionary<Landform.Classification, Color> colorMap = new()
            {
                { Landform.Classification.Basin, Color.Blue },
                { Landform.Classification.Dome, Color.Red },
                { Landform.Classification.ConvexSaddle, Color.Orange },
                { Landform.Classification.ConcaveSaddle, Color.Turquoise },
                { Landform.Classification.Plane, Color.DarkGray },

            };     
            Dictionary<Landform.FlowClassification, Color> flowColorMap = new()
            {
                { Landform.FlowClassification.Accumulate, Color.Blue },
                { Landform.FlowClassification.Dissperse, Color.Red },
                { Landform.FlowClassification.TransitiveConvex, Color.Orange },
                { Landform.FlowClassification.TransitiveConcave, Color.Turquoise },
                { Landform.FlowClassification.Flat, Color.DarkGray },

            };
            var img = FloatFieldVisualizer<Argb32>.Visualize(map,
                (x, y, val, data) =>{
                    var classification = Landform.Guassian(data.firstOder.X, data.firstOder.Y, data.secondOrder.X, data.secondOrder.Y, data.secondOrder.Z);
                    return colorMap[classification];
                });
            img.SaveAsPng(MakePath("GaussLandformClassification.png"));
            
            img = FloatFieldVisualizer<Argb32>.Visualize(map,
                (x, y, val, data) => {
                    var classification = Landform.ShapeIndex(data.firstOder.X, data.firstOder.Y, data.secondOrder.X, data.secondOrder.Y, data.secondOrder.Z);
                    return g.Evaluate(classification);
                });
            img.SaveAsPng(MakePath("ShapeIndexLandformClassification.png"));

            img = FloatFieldVisualizer<Argb32>.Visualize(map,
                (x, y, val, data) => {
                    var classification = Landform.Accumulation(data.firstOder.X, data.firstOder.Y, data.secondOrder.X, data.secondOrder.Y, data.secondOrder.Z);
                    return flowColorMap[classification];
                });
            img.SaveAsPng(MakePath("AccumulationLandformClassification.png"));
        }

        static void TopologyTests(FloatField map) 
        {

            var bw = new Gradient(new Gradient.Key[]
{
                new Gradient.Key(){Ratio = 0f, Color = Color.Black },
                new Gradient.Key(){ Ratio = 1f, Color = Color.White}
}, Gradient.RepetitionMode.Repeat);

            Console.WriteLine("Surface normal tests");
            var img = FloatFieldVisualizer<Argb32>.Visualize(map, (x, y, v) =>
            {
                var (sampleX, sampleY) = map.DiscretePositionToSamplePosition(x, y);
                var normal = map.NormalAtPoint(sampleX, sampleY);
                return new Color(new Argb32(normal.X, normal.Y, normal.Z));
            });
            img.SaveAsPng(MakePath("SurfaceNormalMap.png"));
            Console.WriteLine("Flow Map");
            var flowMap = FlowMap.Calculate(map);
            img = FloatFieldVisualizer<Argb32>.Visualize(flowMap, (x, y, v) =>
            {
                return bw.Evaluate(v);
            });
            img.SaveAsPng(MakePath("FlowMap.png"));

            var gradient = CreateCoolToWarmGradient();
            Console.WriteLine("Residual Mean Elevation");
            img = FloatFieldVisualizer<Argb32>.Visualize(map, (x, y, v) =>
            {
                var (xS, yS) = map.DiscretePositionToSamplePosition(x, y);
                var res = ResidualAnalysis.MeanElevation(map, xS, yS);
                return Colorize(res,0,true, gradient);
            });
            img.SaveAsPng(MakePath("ResMeanElevation.png"));

            Console.WriteLine("Residual Difference from Mean elevation");
            img = FloatFieldVisualizer<Argb32>.Visualize(map, (x, y, v) =>
            {
                var (xS, yS) = map.DiscretePositionToSamplePosition(x, y);
                var res = ResidualAnalysis.DifferenceFromMeanElevation(map, xS, yS);
                return Colorize(res, 4, false, gradient);
            });
            img.SaveAsPng(MakePath("ResDiffFromMeanElevation.png"));
            Console.WriteLine("Residual Std dev mean elev");
            img = FloatFieldVisualizer<Argb32>.Visualize(map, (x, y, v) =>
            {
                var (xS, yS) = map.DiscretePositionToSamplePosition(x, y);
                var res = ResidualAnalysis.DeviationFromMeanElevation(map, xS, yS);
                return Colorize(res, 0.6f, true, gradient);
            });
            img.SaveAsPng(MakePath("ResStdDevFromMeanElevation.png"));
            Console.WriteLine("Residual Percentile");
            img = FloatFieldVisualizer<Argb32>.Visualize(map, (x, y, v) =>
            {
                var (xS, yS) = map.DiscretePositionToSamplePosition(x, y);
                var res = ResidualAnalysis.Percentile(map, xS, yS);
                return Colorize(res, 0.3f, true, gradient);
            });
            img.SaveAsPng(MakePath("ResPercentile.png"));
            Console.WriteLine("Slope Visualization");
            List<float> results = new(map.Width*map.Height);
            img = FloatFieldVisualizer<Argb32>.Visualize(map, (x, y, v) =>
            {
                var (xS, yS) = map.DiscretePositionToSamplePosition(x, y);
                var res = Derivative.Slope(map, xS, yS);
                results.Add(res);
                return Colorize(res, 0.4f, true, bw);
            });
            img.SaveAsPng(MakePath("SlopeMap.png"));
            PrintSomeInfo(results);


            Console.WriteLine("Aspect Visualization");
            results.Clear();
            img = FloatFieldVisualizer<Argb32>.Visualize(map, (x, y, v) =>
            {
                var (xS, yS) = map.DiscretePositionToSamplePosition(x, y);
                var res = Derivative.Aspect(map, xS, yS);
                results.Add(res);
                return Colorize(res, 0, true, gradient);
            });
            PrintSomeInfo(results);
            img.SaveAsPng(MakePath("AspectMap.png"));

            void PrintSomeInfo(List<float> aList) 
            {

                Console.WriteLine($"Max: {aList.Max()}");
                Console.WriteLine($"Min: {aList.Min()}");
                Console.WriteLine($"Avg: {aList.Average()}");
            }

        }

        static void AllVornoiTypes() 
        {
            Console.WriteLine( "Vornoi" );
            foreach (var distFunc in Enum.GetValues(typeof(VornoiDistanceFunction)))
            {
                foreach (var returnFunc in Enum.GetValues( typeof( VornoiReturnType ) ))
                {
                    NoiseToPng.CreateNoise( 512, 512, MakePath($"Vornoi-{distFunc}-{returnFunc}.png",NoiseType.Vornoi), ( x, y ) =>
                        Noise.Vornoi01( x * 512, y * 512, distanceFunction: (VornoiDistanceFunction)distFunc, returnType: (VornoiReturnType)returnFunc ) );
                    
                    var generator = new VornoiNoiseGenerator(returnType: (VornoiReturnType)returnFunc, distFunction: (VornoiDistanceFunction)distFunc);
                    NoiseToPng.CreateNoise( 512, 512, MakePath($"Vornoi-FractalBm-{distFunc}-{returnFunc}.png",NoiseType.Vornoi), ( x, y ) =>
                        Noise.Layered.FBm01( x * 512, y * 512, generator ) );
                    NoiseToPng.CreateNoise( 512, 512, MakePath( $"Vornoi-FractalBmDerivative-{distFunc}-{returnFunc}.png", NoiseType.Vornoi ), ( x, y ) =>
                               Noise.Layered.FBmDerivative01( x * 512, y * 512, generator ) );

                    NoiseToPng.CreateNoise( 512, 512, MakePath($"Vornoi-Ridged-{distFunc}-{returnFunc}.png",NoiseType.Vornoi), ( x, y ) =>
                        Noise.Layered.Ridged01( x * 512, y * 512, generator ) );
                    NoiseToPng.CreateNoise( 512, 512, MakePath($"Vornoi-PingPong-{distFunc}-{returnFunc}.png",NoiseType.Vornoi), ( x, y ) =>
                        Noise.Layered.PingPong01( x * 512, y * 512, generator ) );

                }
            }
        }

        static void AllPerlin() 
        {
            Console.WriteLine( "Perlin" );
            NoiseToPng.CreateNoise( 512, 512, MakePath("Perlin.png",NoiseType.Perlin), ( x, y ) =>
                Noise.Perlin01( x * 512, y * 512) );

            PerlinNoiseGenerator generator = new();

            NoiseToPng.CreateNoise( 512, 512, MakePath($@"Perlin-FractalBm.png",NoiseType.Perlin), ( x, y ) =>
                Noise.Layered.FBm01( x * 512, y * 512,generator ));   
            NoiseToPng.CreateNoise( 512, 512, MakePath($@"Perlin-FractalBmDerivative.png",NoiseType.Perlin), ( x, y ) =>
                Noise.Layered.FBmDerivative01( x * 512, y * 512,generator ));
            NoiseToPng.CreateNoise( 512, 512, MakePath("Perlin-Ridged.png",NoiseType.Perlin), ( x, y ) =>
                Noise.Layered.Ridged01( x * 512, y * 512, generator ) );
            NoiseToPng.CreateNoise( 512, 512, MakePath("Perlin-PingPong.png",NoiseType.Perlin), ( x, y ) =>
                Noise.Layered.PingPong01( x * 512, y * 512, generator ) );

            NoiseToPng.CreateNoise( 512, 512, MakePath("Perlin-AnalDeriv.png",NoiseType.Perlin), ( x, y ) => Noise.Layered.Derivative01( x * 512, y * 512, generator ) );

        }

        static void DomainWarpPerlin() 
        {
            Console.WriteLine( "Perlin Domain Warp" );
            NoiseToPng.CreateNoise( 512, 512, MakePath( "PerlinNormal.png", NoiseType.Perlin ), ( x, y ) => Noise.Perlin01( x * 512, y * 512 ) );


            NoiseToPng.CreateNoise( 512, 512, MakePath( "PerlinWarped-FractalIndepent.png", NoiseType.Perlin ), ( x, y ) => {
                x *= 512;
                y *= 512;
                DomainWarp.FractalIndependent( ref x, ref y, 
                    warpType: DomainWarp.Type.OpenSimplex2Reduced,
                    warpAmp: 50f,
                    freq: 0.015f,
                    octaves: 10,
                    lacunarity: 2.5f,
                    gain: 0.5f );
                return Noise.Perlin01( x, y);
            });

            NoiseToPng.CreateNoise( 512, 512, MakePath( "PerlinWarped-FractalProgressive.png", NoiseType.Perlin ), ( x, y ) => {
                x *= 512;
                y *= 512;
                DomainWarp.FractalProgressive( ref x, ref y,
                    warpType: DomainWarp.Type.OpenSimplex2Reduced,
                    warpAmp:50f,
                    freq:0.015f,
                    octaves:10,
                    lacunarity:2.5f,
                    gain:0.5f);
                return Noise.Perlin01( x, y );
            } );
            NoiseToPng.CreateNoise( 512, 512, MakePath( "PerlinWarped-Single.png", NoiseType.Perlin ), ( x, y ) => {
                x *= 512;
                y *= 512;
                DomainWarp.Single( ref x, ref y, 
                    warpType: DomainWarp.Type.OpenSimplex2Reduced,
                    warpAmp: 50f,
                    freq: 0.015f);
                return Noise.Perlin01( x, y );
            } );

            NoiseToPng.CreateNoise( 512, 512, MakePath( "PerlinDerivedWarped-Independent.png", NoiseType.Perlin ), ( x, y ) => {
                x *= 512;
                y *= 512;
                DomainWarp.FractalIndependent( ref x, ref y,
                    warpType: DomainWarp.Type.OpenSimplex2Reduced,
                    warpAmp: 50f,
                    freq: 0.015f,
                    octaves: 10,
                    lacunarity: 2.5f,
                    gain: 0.5f );

                return Noise.Layered.Derivative01( x, y, new PerlinNoiseGenerator() );
            }, ColorMap.DefaultTerrainMap );
            NoiseToPng.CreateNoise( 512, 512, MakePath( "PerlinDerivedWarped-Progressive.png", NoiseType.Perlin ), ( x, y ) => {
                x *= 512;
                y *= 512;
                DomainWarp.FractalProgressive( ref x, ref y,
                    warpType: DomainWarp.Type.OpenSimplex2Reduced,
                    warpAmp: 50f,
                    freq: 0.015f,
                    octaves: 10,
                    lacunarity: 2.5f,
                    gain: 0.5f );

                return Noise.Layered.Derivative01( x, y, new PerlinNoiseGenerator() );
            }, ColorMap.DefaultTerrainMap );
        }

        static void AllValue()
        {
            Console.WriteLine( "Value" );
            NoiseToPng.CreateNoise( 512, 512, MakePath("Value.png",NoiseType.Value), ( x, y ) =>
                Noise.Value01( x * 512, y * 512 ) );

            ValueNoiseGenerator generator = new();

            NoiseToPng.CreateNoise( 512, 512, MakePath("Value-FractalBm.png",NoiseType.Value), ( x, y ) =>
                Noise.Layered.FBm01( x * 512, y * 512, generator ));
            NoiseToPng.CreateNoise( 512, 512, MakePath("Value-FractalBmDerivative.png",NoiseType.Value), ( x, y ) =>
                Noise.Layered.FBmDerivative01( x * 512, y * 512, generator ));
            NoiseToPng.CreateNoise( 512, 512, MakePath("Value-Ridged.png",NoiseType.Value), ( x, y ) =>
                Noise.Layered.Ridged01( x * 512, y * 512, generator ) );
            NoiseToPng.CreateNoise( 512, 512, MakePath("Value-PingPong.png",NoiseType.Value), ( x, y ) =>
                Noise.Layered.PingPong01( x * 512, y * 512, generator ) );
        }

        static void AllValueCubic()
        {
            Console.WriteLine( "Value Cubic" );
            NoiseToPng.CreateNoise( 512, 512, MakePath("ValueCubic.png",NoiseType.ValueCubic), ( x, y ) =>
                Noise.ValueCubic01( x * 512, y * 512 ) );

            ValueCubicNoiseGenerator generator = new();

            NoiseToPng.CreateNoise( 512, 512, MakePath("ValueCubic-FractalBm.png",NoiseType.ValueCubic), ( x, y ) =>
                Noise.Layered.FBm01( x * 512, y * 512, generator ) );
            NoiseToPng.CreateNoise( 512, 512, MakePath("ValueCubic-FractalBmDerivative.png",NoiseType.ValueCubic), ( x, y ) =>
                Noise.Layered.FBmDerivative01( x * 512, y * 512, generator ) );
            NoiseToPng.CreateNoise( 512, 512, MakePath("ValueCubic-Ridged.png",NoiseType.ValueCubic), ( x, y ) =>
                Noise.Layered.Ridged01( x * 512, y * 512, generator ) );
            NoiseToPng.CreateNoise( 512, 512, MakePath("ValueCubic-PingPong.png",NoiseType.ValueCubic), ( x, y ) =>
                Noise.Layered.PingPong01( x * 512, y * 512, generator ) );
        }

        static void AllOpenSimplex2S()
        {
            Console.WriteLine( "Open Simplex 2s" );
            NoiseToPng.CreateNoise( 512, 512, MakePath("OpenSimplex2S.png",NoiseType.OpenSimplex2S), ( x, y ) =>
                Noise.OpenSimplex2S01( x * 512, y * 512 ) );

            OpenSimplex2SNoiseGenerator generator = new();

            NoiseToPng.CreateNoise( 512, 512, MakePath("OpenSimplex2S-FractalBm.png",NoiseType.OpenSimplex2S), ( x, y ) =>
                Noise.Layered.FBm01( x * 512, y * 512, generator ) );
            NoiseToPng.CreateNoise( 512, 512, MakePath("OpenSimplex2S-FractalBmDerivative.png",NoiseType.OpenSimplex2S), ( x, y ) =>
                Noise.Layered.FBmDerivative01( x * 512, y * 512, generator ) );
            NoiseToPng.CreateNoise( 512, 512, MakePath("OpenSimplex2S-Ridged.png",NoiseType.OpenSimplex2S), ( x, y ) =>
                Noise.Layered.Ridged01( x * 512, y * 512, generator ) );
            NoiseToPng.CreateNoise( 512, 512, MakePath("OpenSimplex2S-PingPong.png",NoiseType.OpenSimplex2S), ( x, y ) =>
                Noise.Layered.PingPong01( x * 512, y * 512, generator ) );
        }

        static void AllOpenSimplex()
        {
            Console.WriteLine( "OpenSimplex" );
            NoiseToPng.CreateNoise( 512, 512, MakePath("OpenSimplex.png",NoiseType.OpenSimplex2), ( x, y ) =>
                Noise.OpenSimplex01( x * 512, y * 512 ) );

            OpenSimplexNoiseGenerator generator = new();

            NoiseToPng.CreateNoise( 512, 512, MakePath("OpenSimplex-FractalBm.png",NoiseType.OpenSimplex2), ( x, y ) =>
                Noise.Layered.FBm01( x * 512, y * 512, generator ) );
            NoiseToPng.CreateNoise( 512, 512, MakePath("OpenSimplex-FractalBmDerivative.png",NoiseType.OpenSimplex2), ( x, y ) =>
                Noise.Layered.FBmDerivative01( x * 512, y * 512, generator ) );
            NoiseToPng.CreateNoise( 512, 512, MakePath("OpenSimplex-Ridged.png",NoiseType.OpenSimplex2), ( x, y ) =>
                Noise.Layered.Ridged01( x * 512, y * 512, generator ) );
            NoiseToPng.CreateNoise( 512, 512, MakePath("OpenSimplex-PingPong.png",NoiseType.OpenSimplex2), ( x, y ) =>
                Noise.Layered.PingPong01( x * 512, y * 512, generator ) );
        }

        static void CloudTest() 
        {
            var f = new MinMaxTrackingFloatField(513);

            Noise.Layered.Cloud( f, roughness: 32, outMax: 1, outMin: 0 );
            f.Save( MakePath("Cloud.png",NoiseType.Cloud) ); 
        }

        static void CreateDirs() 
        {
            foreach (var noiseType in Enum.GetValues( typeof( NoiseType ) ))
            {
                if (!Directory.Exists( $@"C:\Users\lukas\Documents\PerlinTest\{noiseType}" ))
                    Directory.CreateDirectory( $@"C:\Users\lukas\Documents\PerlinTest\{noiseType}" );
            }
        }

        static string MakePath( string fileName, NoiseType type )
            => $@"C:\Users\lukas\Documents\PerlinTest\{type}\{fileName}";
        
        static string MakePath( string fileName )
            => $@"C:\Users\lukas\Documents\PerlinTest\{fileName}";

        static void AnalTest()
        {
            Console.WriteLine( "Perlin" );
            NoiseToPng.CreateNoise( 512, 512, MakePath($"Perlin.png",NoiseType.Perlin), ( x, y ) =>
                Noise.Perlin01( x * 512, y * 512 ) );
            PerlinNoiseGenerator perlinGenerator = new();
            NoiseToPng.CreateNoise( 512, 512, MakePath( $"Perlin-AnalDeriv.png", NoiseType.Perlin ), ( x, y ) => Noise.Layered.Derivative01( x * 512, y * 512, perlinGenerator ) );

            Console.WriteLine( "Value" );
            NoiseToPng.CreateNoise( 512, 512, MakePath("Value.png",NoiseType.Value), ( x, y ) =>
                Noise.Value01( x * 512, y * 512 ) );
            ValueNoiseGenerator valueGenerator = new();
            NoiseToPng.CreateNoise( 512, 512, MakePath("Value-AnalDeriv.png",NoiseType.Value), ( x, y ) => Noise.Layered.Derivative01( x * 512, y * 512, valueGenerator ) );

            Console.WriteLine( "Value Cubic" );
            NoiseToPng.CreateNoise( 512, 512,MakePath("ValueCubic.png",NoiseType.ValueCubic), ( x, y ) =>
                Noise.ValueCubic01( x * 512, y * 512 ) );
            ValueCubicNoiseGenerator valueCubicGenerator = new();
            NoiseToPng.CreateNoise( 512, 512, MakePath("ValueCubic-AnalDeriv.png",NoiseType.ValueCubic), ( x, y ) => Noise.Layered.Derivative01( x * 512, y * 512, valueCubicGenerator ) );

            Console.WriteLine( "OpenSimplex" );
            NoiseToPng.CreateNoise( 512, 512, MakePath("OpenSimplex.png",NoiseType.OpenSimplex2), ( x, y ) =>
                Noise.OpenSimplex01( x * 512, y * 512 ) );

            OpenSimplexNoiseGenerator simplexGenerator = new();
            NoiseToPng.CreateNoise( 512, 512, MakePath("OpenSimplex-AnalDeriv.png",NoiseType.OpenSimplex2), ( x, y ) => Noise.Layered.Derivative01( x * 512, y * 512, simplexGenerator ) );
            
            Console.WriteLine( "Open Simplex 2s" );
            NoiseToPng.CreateNoise( 512, 512, MakePath("OpenSimplex2S.png",NoiseType.OpenSimplex2S), ( x, y ) =>
                Noise.OpenSimplex2S01( x * 512, y * 512 ) );

            OpenSimplex2SNoiseGenerator simplex2SGenerator = new();
            NoiseToPng.CreateNoise( 512, 512, MakePath("OpenSimplex2S-AnalDeriv.png",NoiseType.OpenSimplex2S), ( x, y ) => Noise.Layered.Derivative01( x * 512, y * 512, simplex2SGenerator ) );

            VornoiAnalDeriv();

        }
        static void VornoiAnalDeriv() 
        {
            Console.WriteLine( "Vornoi" );
            foreach (var distFunc in Enum.GetValues(typeof(VornoiDistanceFunction)))
            {
                foreach (var returnFunc in Enum.GetValues( typeof(VornoiReturnType ) ))
                {
                    NoiseToPng.CreateNoise( 512, 512, MakePath($"Vornoi-{distFunc}-{returnFunc}.png",NoiseType.Vornoi), (x, y) =>
                        Noise.Vornoi01(x* 512, y* 512, distanceFunction: (VornoiDistanceFunction)distFunc, returnType: (VornoiReturnType)returnFunc ) );

                    var generator = new VornoiNoiseGenerator(returnType: (VornoiReturnType)returnFunc, distFunction: (VornoiDistanceFunction)distFunc);
                    NoiseToPng.CreateNoise( 512, 512, MakePath($"Vornoi-{distFunc}-{returnFunc}-AnalDeriv.png",NoiseType.Vornoi), ( x, y ) => Noise.Layered.Derivative01( x * 512, y * 512, generator ) );
                }
            }
        }

        static void Indexing2D(int width, int height) 
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    var oneDim = Indexing.TwoDimToOneDim(x, y,width);
                    var back = Indexing.OneDimToTwoDim(oneDim, width);
                    Console.WriteLine((x, y) == back);
                    //Console.WriteLine($"{(x, y)} 1D: {oneDim} Back: {back}");
                }
            }
        }
        static void Indexing3D(int width, int height, int depth)
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    for (int z = 0; z < depth; z++)
                    {
                        var oneDim = Indexing.ThreeDimToOneDim(x, y,z, width, depth);
                        var back = Indexing.OneDimToThreeDim(oneDim, width, depth);
                        Console.WriteLine((x, y, z) == back);
                        //Console.WriteLine($"{(x, y, z)} 1D: {oneDim} Back: {back}");
                    }

                }
            }
        }

    }
}
