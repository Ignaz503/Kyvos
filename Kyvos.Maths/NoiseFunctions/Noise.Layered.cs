//Adapted From: 
// MIT License
//
// Copyright(c) 2020 Jordan Peck (jordan.me2@gmail.com)
// Copyright(c) 2020 Contributors
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files(the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and / or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions :
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
//
// .'',;:cldxkO00KKXXNNWWWNNXKOkxdollcc::::::;:::ccllloooolllllllllooollc:,'...        ...........',;cldxkO000Okxdlc::;;;,,;;;::cclllllll
// ..',;:ldxO0KXXNNNNNNNNXXK0kxdolcc::::::;;;,,,,,,;;;;;;;;;;:::cclllllc:;'....       ...........',;:ldxO0KXXXK0Okxdolc::;;;;::cllodddddo
// ...',:loxO0KXNNNNNXXKK0Okxdolc::;::::::::;;;,,'''''.....''',;:clllllc:;,'............''''''''',;:loxO0KXNNNNNXK0Okxdollccccllodxxxxxxd
// ....';:ldkO0KXXXKK00Okxdolcc:;;;;;::cclllcc:;;,''..... ....',;clooddolcc:;;;;,,;;;;;::::;;;;;;:cloxk0KXNWWWWWWNXKK0Okxddoooddxxkkkkkxx
// .....';:ldxkOOOOOkxxdolcc:;;;,,,;;:cllooooolcc:;'...      ..,:codxkkkxddooollloooooooollcc:::::clodkO0KXNWWWWWWNNXK00Okxxxxxxxxkkkkxxx
// . ....';:cloddddo___________,,,,;;:clooddddoolc:,...      ..,:ldx__00OOOkkk___kkkkkkxxdollc::::cclodkO0KXXNNNNNNXXK0OOkxxxxxxxxxxxxddd
// .......',;:cccc:|           |,,,;;:cclooddddoll:;'..     ..';cox|  \KKK000|   |KK00OOkxdocc___;::clldxxkO0KKKKK00Okkxdddddddddddddddoo
// .......'',,,,,''|   ________|',,;;::cclloooooolc:;'......___:ldk|   \KK000|   |XKKK0Okxolc|   |;;::cclodxxkkkkxxdoolllcclllooodddooooo
// ''......''''....|   |  ....'',,,,;;;::cclloooollc:;,''.'|   |oxk|    \OOO0|   |KKK00Oxdoll|___|;;;;;::ccllllllcc::;;,,;;;:cclloooooooo
// ;;,''.......... |   |_____',,;;;____:___cllo________.___|   |___|     \xkk|   |KK_______ool___:::;________;;;_______...'',;;:ccclllloo
// c:;,''......... |         |:::/     '   |lo/        |           |      \dx|   |0/       \d|   |cc/        |'/       \......',,;;:ccllo
// ol:;,'..........|    _____|ll/    __    |o/   ______|____    ___|   |   \o|   |/   ___   \|   |o/   ______|/   ___   \ .......'',;:clo
// dlc;,...........|   |::clooo|    /  |   |x\___   \KXKKK0|   |dol|   |\   \|   |   |   |   |   |d\___   \..|   |  /   /       ....',:cl
// xoc;'...  .....'|   |llodddd|    \__|   |_____\   \KKK0O|   |lc:|   |'\       |   |___|   |   |_____\   \.|   |_/___/...      ...',;:c
// dlc;'... ....',;|   |oddddddo\          |          |Okkx|   |::;|   |..\      |\         /|   |          | \         |...    ....',;:c
// ol:,'.......',:c|___|xxxddollc\_____,___|_________/ddoll|___|,,,|___|...\_____|:\ ______/l|___|_________/...\________|'........',;::cc
// c:;'.......';:codxxkkkkxxolc::;::clodxkOO0OOkkxdollc::;;,,''''',,,,''''''''''',,'''''',;:loxkkOOkxol:;,'''',,;:ccllcc:;,'''''',;::ccll
// ;,'.......',:codxkOO0OOkxdlc:;,,;;:cldxxkkxxdolc:;;,,''.....'',;;:::;;,,,'''''........,;cldkO0KK0Okdoc::;;::cloodddoolc:;;;;;::ccllooo
// .........',;:lodxOO0000Okdoc:,,',,;:clloddoolc:;,''.......'',;:clooollc:;;,,''.......',:ldkOKXNNXX0Oxdolllloddxxxxxxdolccccccllooodddd
// .    .....';:cldxkO0000Okxol:;,''',,;::cccc:;,,'.......'',;:cldxxkkxxdolc:;;,'.......';coxOKXNWWWNXKOkxddddxxkkkkkkxdoollllooddxxxxkkk
//       ....',;:codxkO000OOxdoc:;,''',,,;;;;,''.......',,;:clodkO00000Okxolc::;,,''..',;:ldxOKXNWWWNNK0OkkkkkkkkkkkxxddooooodxxkOOOOO000
//       ....',;;clodxkkOOOkkdolc:;,,,,,,,,'..........,;:clodxkO0KKXKK0Okxdolcc::;;,,,;;:codkO0XXNNNNXKK0OOOOOkkkkxxdoollloodxkO0KKKXXXXX
//
// VERSION: 1.0.1
// https://github.com/Auburn/FastNoise

using System;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Kyvos.Maths.NoiseFunctions;
public static partial class Noise
{

    public static class Layered
    {

        //analytic derivative
        public static float Derivative(float x, float y, INoiseGenerator2D noiseGen, int seed = NoiseHelpers.DefaultSeed, int octaves = NoiseHelpers.DefaultOctaves, float frequency = NoiseHelpers.DefaultFrequency, float gain = NoiseHelpers.DefaultGain, float lacunarity = NoiseHelpers.DefaultLacunarity, float weightedStrength = NoiseHelpers.DefaultWeightedStrengh)
        {
            TransformNoiseCoordinate(ref x, ref y, frequency, noiseGen.Type);
            float sum = 0f;
            Vector2 dSum = Vector2.Zero;
            float amp = 1.0f;

            for (int i = 0; i < octaves; i++)
            {
                Vector2 n = noiseGen.GetDerivative(seed, x, y);
                float noise = noiseGen.GetNoise(seed, x, y);
                dSum += n;
                sum += amp * noise / (1f + Vector2.Dot(dSum, dSum));
                amp *= NoiseHelpers.Lerp(1.0f, MathF.Min(noise + 1, 2) * 0.5f, weightedStrength);
                x *= lacunarity;
                y *= lacunarity;
                amp *= gain;

            }
            return sum;
        }


        public static float Derivative01(float x, float y, INoiseGenerator2D noiseGen, int seed = NoiseHelpers.DefaultSeed, int octaves = NoiseHelpers.DefaultOctaves, float frequency = NoiseHelpers.DefaultFrequency, float gain = NoiseHelpers.DefaultGain, float lacunarity = NoiseHelpers.DefaultLacunarity, float weightedStrength = NoiseHelpers.DefaultWeightedStrengh)
            => (1.0f + Derivative(x, y, noiseGen, seed, octaves, frequency, gain, lacunarity, weightedStrength)) / 2.0f;

        public static float Derivative(float x, float y, float z, INoiseGenerator3D noiseGen, int seed = NoiseHelpers.DefaultSeed, int octaves = NoiseHelpers.DefaultOctaves, float frequency = NoiseHelpers.DefaultFrequency, float gain = NoiseHelpers.DefaultGain, float lacunarity = NoiseHelpers.DefaultLacunarity, float weightedStrength = NoiseHelpers.DefaultWeightedStrengh, RotationType3D rotationType = NoiseHelpers.DefaultRotationType)
        {
            NoiseHelpers.TransformNoiseCoordinate(ref x, ref y, ref z, frequency, NoiseHelpers.GetTransformType(rotationType, noiseGen.Type));
            float sum = 0f;
            Vector3 dSum = Vector3.Zero;
            float amp = 1.0f;

            for (int i = 0; i < octaves; i++)
            {
                Vector3 n = noiseGen.GetDerivative(seed, x, y, z);
                float noise = noiseGen.GetNoise(seed, x, y, z);
                dSum += n;
                sum += amp * noise / (1f + Vector3.Dot(dSum, dSum));
                amp *= NoiseHelpers.Lerp(1.0f, MathF.Min(noise + 1, 2) * 0.5f, weightedStrength);
                x *= lacunarity;
                y *= lacunarity;
                amp *= gain;

            }
            return sum;
        }


        public static float Derivative01(float x, float y, float z, INoiseGenerator3D noiseGen, int seed = NoiseHelpers.DefaultSeed, int octaves = NoiseHelpers.DefaultOctaves, float frequency = NoiseHelpers.DefaultFrequency, float gain = NoiseHelpers.DefaultGain, float lacunarity = NoiseHelpers.DefaultLacunarity, float weightedStrength = NoiseHelpers.DefaultWeightedStrengh, RotationType3D rotationType = NoiseHelpers.DefaultRotationType)
            => (1.0f + Derivative(x, y, z, noiseGen, seed, octaves, frequency, gain, lacunarity, weightedStrength, rotationType)) / 2.0f;

        // Fractal FBm

        public static float FBm(float x, float y, INoiseGenerator2D noiseGen, int seed = NoiseHelpers.DefaultSeed, int octaves = NoiseHelpers.DefaultOctaves, float gain = NoiseHelpers.DefaultGain, float lacunarity = NoiseHelpers.DefaultLacunarity, float weightedStrength = NoiseHelpers.DefaultWeightedStrengh, float frequency = NoiseHelpers.DefaultFrequency)
        {
            TransformNoiseCoordinate(ref x, ref y, frequency, noiseGen.Type);

            float sum = 0;
            float amp = NoiseHelpers.CalculateFractalBounding(gain, octaves);

            for (int i = 0; i < octaves; i++)
            {
                float noise = noiseGen.GetNoise(seed++, x, y);
                sum += noise * amp;
                amp *= NoiseHelpers.Lerp(1.0f, MathF.Min(noise + 1, 2) * 0.5f, weightedStrength);

                x *= lacunarity;
                y *= lacunarity;
                amp *= gain;
            }

            return sum;
        }


        public static float FBm01(float x, float y, INoiseGenerator2D noiseGen, int seed = NoiseHelpers.DefaultSeed, int octaves = NoiseHelpers.DefaultOctaves, float gain = NoiseHelpers.DefaultGain, float lacunarity = NoiseHelpers.DefaultLacunarity, float weightedStrength = NoiseHelpers.DefaultWeightedStrengh, float frequency = NoiseHelpers.DefaultFrequency)
            => (1.0f + FBm(x, y, noiseGen, seed, octaves, gain, lacunarity, weightedStrength, frequency)) / 2.0f;

        public static float FBm(float x, float y, float z, INoiseGenerator3D noiseGen, int seed = NoiseHelpers.DefaultSeed, int octaves = NoiseHelpers.DefaultOctaves, float gain = NoiseHelpers.DefaultGain, float lacunarity = NoiseHelpers.DefaultLacunarity, float weightedStrength = NoiseHelpers.DefaultWeightedStrengh, float frequency = NoiseHelpers.DefaultFrequency, RotationType3D rotationType = NoiseHelpers.DefaultRotationType)
        {
            NoiseHelpers.TransformNoiseCoordinate(ref x, ref y, ref z, frequency, NoiseHelpers.GetTransformType(rotationType, noiseGen.Type));

            float sum = 0;
            float amp = NoiseHelpers.CalculateFractalBounding(gain, octaves);

            for (int i = 0; i < octaves; i++)
            {
                float noise = noiseGen.GetNoise(seed++, x, y, z);
                sum += noise * amp;
                amp *= NoiseHelpers.Lerp(1.0f, (noise + 1) * 0.5f, weightedStrength);

                x *= lacunarity;
                y *= lacunarity;
                z *= lacunarity;
                amp *= gain;
            }

            return sum;
        }

        public static float FBm01(float x, float y, float z, INoiseGenerator3D noiseGen, int seed = NoiseHelpers.DefaultSeed, int octaves = NoiseHelpers.DefaultOctaves, float gain = NoiseHelpers.DefaultGain, float lacunarity = NoiseHelpers.DefaultLacunarity, float weightedStrength = NoiseHelpers.DefaultWeightedStrengh, float frequency = NoiseHelpers.DefaultFrequency, RotationType3D rotationType = NoiseHelpers.DefaultRotationType)
            => (1.0f + FBm(x, y, z, noiseGen, seed, octaves, gain, lacunarity, weightedStrength, frequency, rotationType)) / 2.0f;

        public static float FBmDerivative(float x, float y, INoiseGenerator2D noiseGen, int seed = NoiseHelpers.DefaultSeed, int octaves = NoiseHelpers.DefaultOctaves, float gain = NoiseHelpers.DefaultGain, float lacunarity = NoiseHelpers.DefaultLacunarity, float weightedStrength = NoiseHelpers.DefaultWeightedStrengh, float frequency = NoiseHelpers.DefaultFrequency)
        {
            TransformNoiseCoordinate(ref x, ref y, frequency, noiseGen.Type);

            float sum = 0;
            float amp = NoiseHelpers.CalculateFractalBounding(gain, octaves);

            Vector2 vec = new(0, 0);

            for (int i = 0; i < octaves; i++)
            {
                float noise = noiseGen.GetNoise(seed++, x, y);
                vec += amp * noiseGen.GetDerivative(seed - 1, x, y) * lacunarity;

                sum += noise * amp / (1f + Vector2.Dot(vec, vec));
                amp *= NoiseHelpers.Lerp(1.0f, MathF.Min(noise + 1, 2) * 0.5f, weightedStrength);

                //seed -1 to counteract the above seed++

                x *= lacunarity;
                y *= lacunarity;
                amp *= gain;
            }

            return sum;
        }

        public static float FBmDerivative01(float x, float y, INoiseGenerator2D noiseGen, int seed = NoiseHelpers.DefaultSeed, int octaves = NoiseHelpers.DefaultOctaves, float gain = NoiseHelpers.DefaultGain, float lacunarity = NoiseHelpers.DefaultLacunarity, float weightedStrength = NoiseHelpers.DefaultWeightedStrengh, float frequency = NoiseHelpers.DefaultFrequency)
            => (1.0f + FBmDerivative(x, y, noiseGen, seed, octaves, gain, lacunarity, weightedStrength, frequency)) / 2.0f;

        public static float FBmDerivative(float x, float y, float z, INoiseGenerator3D noiseGen, int seed = NoiseHelpers.DefaultSeed, int octaves = NoiseHelpers.DefaultOctaves, float gain = NoiseHelpers.DefaultGain, float lacunarity = NoiseHelpers.DefaultLacunarity, float weightedStrength = NoiseHelpers.DefaultWeightedStrengh, float frequency = NoiseHelpers.DefaultFrequency, RotationType3D rotationType = NoiseHelpers.DefaultRotationType)
        {
            NoiseHelpers.TransformNoiseCoordinate(ref x, ref y, ref z, frequency, NoiseHelpers.GetTransformType(rotationType, noiseGen.Type));

            float sum = 0;
            float amp = NoiseHelpers.CalculateFractalBounding(gain, octaves);
            Vector3 vec = new(0, 0, 0);


            for (int i = 0; i < octaves; i++)
            {
                float noise = noiseGen.GetNoise(seed++, x, y, z);
                vec += amp * noiseGen.GetDerivative(seed - 1, x, y, z) * lacunarity;


                sum += noise * amp / (1f + Vector3.Dot(vec, vec));
                amp *= NoiseHelpers.Lerp(1.0f, (noise + 1) * 0.5f, weightedStrength);

                //seed -1 to counteract the above seed++

                x *= lacunarity;
                y *= lacunarity;
                z *= lacunarity;
                amp *= gain;
            }

            return sum;
        }

        public static float FBmDerivative01(float x, float y, float z, INoiseGenerator3D noiseGen, int seed = NoiseHelpers.DefaultSeed, int octaves = NoiseHelpers.DefaultOctaves, float gain = NoiseHelpers.DefaultGain, float lacunarity = NoiseHelpers.DefaultLacunarity, float weightedStrength = NoiseHelpers.DefaultWeightedStrengh, float frequency = NoiseHelpers.DefaultFrequency, RotationType3D rotationType = NoiseHelpers.DefaultRotationType)
            => (1.0f + FBmDerivative(x, y, z, noiseGen, seed, octaves, gain, lacunarity, weightedStrength, frequency, rotationType)) / 2.0f;

        // Fractal Ridged
        public static float Ridged(float x, float y, INoiseGenerator2D noiseGen, int seed = NoiseHelpers.DefaultSeed, int octaves = NoiseHelpers.DefaultOctaves, float gain = NoiseHelpers.DefaultGain, float lacunarity = NoiseHelpers.DefaultLacunarity, float weightedStrength = NoiseHelpers.DefaultWeightedStrengh, float frequency = NoiseHelpers.DefaultFrequency)
        {
            TransformNoiseCoordinate(ref x, ref y, frequency, noiseGen.Type);

            float sum = 0;
            float amp = NoiseHelpers.CalculateFractalBounding(gain, octaves);

            for (int i = 0; i < octaves; i++)
            {
                float noise = MathF.Abs(noiseGen.GetNoise(seed++, x, y));
                sum += (noise * -2 + 1) * amp;
                amp *= NoiseHelpers.Lerp(1.0f, 1 - noise, weightedStrength);

                x *= lacunarity;
                y *= lacunarity;
                amp *= gain;
            }

            return sum;
        }

        public static float Ridged01(float x, float y, INoiseGenerator2D noiseGen, int seed = NoiseHelpers.DefaultSeed, int octaves = NoiseHelpers.DefaultOctaves, float gain = NoiseHelpers.DefaultGain, float lacunarity = NoiseHelpers.DefaultLacunarity, float weightedStrength = NoiseHelpers.DefaultWeightedStrengh, float frequency = NoiseHelpers.DefaultFrequency)
            => (1.0f + Ridged(x, y, noiseGen, seed, octaves, gain, lacunarity, weightedStrength, frequency)) / 2.0f;

        public static float Ridged(float x, float y, float z, INoiseGenerator3D noiseGen, int seed = NoiseHelpers.DefaultSeed, int octaves = NoiseHelpers.DefaultOctaves, float gain = NoiseHelpers.DefaultGain, float lacunarity = NoiseHelpers.DefaultLacunarity, float weightedStrength = NoiseHelpers.DefaultWeightedStrengh, float frequency = NoiseHelpers.DefaultFrequency, RotationType3D rotationType = NoiseHelpers.DefaultRotationType)
        {
            NoiseHelpers.TransformNoiseCoordinate(ref x, ref y, ref z, frequency, NoiseHelpers.GetTransformType(rotationType, noiseGen.Type));

            float sum = 0;
            float amp = NoiseHelpers.CalculateFractalBounding(gain, octaves);

            for (int i = 0; i < octaves; i++)
            {
                float noise = MathF.Abs(noiseGen.GetNoise(seed++, x, y, z));
                sum += (noise * -2 + 1) * amp;
                amp *= NoiseHelpers.Lerp(1.0f, 1 - noise, weightedStrength);

                x *= lacunarity;
                y *= lacunarity;
                z *= lacunarity;
                amp *= gain;
            }

            return sum;
        }

        public static float Ridged01(float x, float y, float z, INoiseGenerator3D noiseGen, int seed = NoiseHelpers.DefaultSeed, int octaves = NoiseHelpers.DefaultOctaves, float gain = NoiseHelpers.DefaultGain, float lacunarity = NoiseHelpers.DefaultLacunarity, float weightedStrength = NoiseHelpers.DefaultWeightedStrengh, float frequency = NoiseHelpers.DefaultFrequency, RotationType3D rotationType = NoiseHelpers.DefaultRotationType)
            => (1.0f + Ridged(x, y, z, noiseGen, seed, octaves, gain, lacunarity, weightedStrength, frequency, rotationType)) / 2.0f;


        // Fractal PingPong 

        public static float PingPong(float x, float y, INoiseGenerator2D noiseGen, int seed = NoiseHelpers.DefaultSeed, int octaves = NoiseHelpers.DefaultOctaves, float gain = NoiseHelpers.DefaultGain, float pingPongStrength = NoiseHelpers.DefaultPingPongStrength, float lacunarity = NoiseHelpers.DefaultLacunarity, float weightedStrength = NoiseHelpers.DefaultWeightedStrengh, float frequency = NoiseHelpers.DefaultFrequency)
        {
            TransformNoiseCoordinate(ref x, ref y, frequency, noiseGen.Type);

            float sum = 0;
            float amp = NoiseHelpers.CalculateFractalBounding(gain, octaves);

            for (int i = 0; i < octaves; i++)
            {
                float noise = NoiseHelpers.PingPong((noiseGen.GetNoise(seed++, x, y) + 1) * pingPongStrength);
                sum += (noise - 0.5f) * 2 * amp;
                amp *= NoiseHelpers.Lerp(1.0f, noise, weightedStrength);

                x *= lacunarity;
                y *= lacunarity;
                amp *= gain;
            }

            return sum;
        }

        public static float PingPong01(float x, float y, INoiseGenerator2D noiseGen, int seed = NoiseHelpers.DefaultSeed, int octaves = NoiseHelpers.DefaultOctaves, float gain = NoiseHelpers.DefaultGain, float pingPongStrength = NoiseHelpers.DefaultPingPongStrength, float lacunarity = NoiseHelpers.DefaultLacunarity, float weightedStrength = NoiseHelpers.DefaultWeightedStrengh, float frequency = NoiseHelpers.DefaultFrequency)
            => (1.0f + PingPong(x, y, noiseGen, seed, octaves, gain, pingPongStrength, lacunarity, weightedStrength, frequency)) / 2.0f;
        public static float PingPong(float x, float y, float z, INoiseGenerator3D noiseGen, int seed = NoiseHelpers.DefaultSeed, int octaves = NoiseHelpers.DefaultOctaves, float gain = NoiseHelpers.DefaultGain, float pingPongStrength = NoiseHelpers.DefaultPingPongStrength, float lacunarity = NoiseHelpers.DefaultLacunarity, float weightedStrength = NoiseHelpers.DefaultWeightedStrengh, float frequency = NoiseHelpers.DefaultFrequency, RotationType3D rotationType = NoiseHelpers.DefaultRotationType)
        {
            NoiseHelpers.TransformNoiseCoordinate(ref x, ref y, ref z, frequency, NoiseHelpers.GetTransformType(rotationType, noiseGen.Type));

            float sum = 0;
            float amp = NoiseHelpers.CalculateFractalBounding(gain, octaves);

            for (int i = 0; i < octaves; i++)
            {
                float noise = NoiseHelpers.PingPong((noiseGen.GetNoise(seed++, x, y, z) + 1) * pingPongStrength);
                sum += (noise - 0.5f) * 2 * amp;
                amp *= NoiseHelpers.Lerp(1.0f, noise, weightedStrength);

                x *= lacunarity;
                y *= lacunarity;
                z *= lacunarity;
                amp *= gain;
            }

            return sum;
        }

        public static float PingPong01(float x, float y, float z, INoiseGenerator3D noiseGen, int seed = NoiseHelpers.DefaultSeed, int octaves = NoiseHelpers.DefaultOctaves, float gain = NoiseHelpers.DefaultGain, float pingPongStrength = NoiseHelpers.DefaultPingPongStrength, float lacunarity = NoiseHelpers.DefaultLacunarity, float weightedStrength = NoiseHelpers.DefaultWeightedStrengh, float frequency = NoiseHelpers.DefaultFrequency, RotationType3D rotationType = NoiseHelpers.DefaultRotationType)
            => (1.0f + PingPong(x, y, z, noiseGen, seed, octaves, gain, pingPongStrength, lacunarity, weightedStrength, frequency, rotationType)) / 2.0f;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void TransformNoiseCoordinate(ref float x, ref float y, float frequency, NoiseType noiseType)
        {
            x *= frequency;
            y *= frequency;

            switch (noiseType)
            {
                case NoiseType.OpenSimplex2:
                case NoiseType.OpenSimplex2S:
                    {
                        const float SQRT3 = (float)1.7320508075688772935274463415059;
                        const float F2 = 0.5f * (SQRT3 - 1);
                        float t = (x + y) * F2;
                        x += t;
                        y += t;
                    }
                    break;
                default:
                    break;
            }
        }


        public static void Cloud(IFloatField floatField, int seed = NoiseHelpers.DefaultSeed, float roughness = NoiseHelpers.DefaultRoughness, bool initialize = true, float outMin = NoiseHelpers.OutputMin, float outMax = NoiseHelpers.OutputMax)
        {
            System.Random rng = new(seed);

            if (floatField.Size % 2 == 0)
                throw new ArgumentException("Size needs to be of form 2^n + 1.");
            if (initialize)
            {
                floatField[0, 0] = Mathf.Random(rng, 0, 1);
                floatField[0, floatField.Size - 1] = Mathf.Random(rng, 0, 1);
                floatField[floatField.Size - 1, 0] = Mathf.Random(rng, 0, 1);
                floatField[floatField.Size - 1, floatField.Size - 1] = Mathf.Random(rng, 0, 1);
            }


            int step = floatField.Size;

            while (step > 0)
            {
                SquareStep(floatField, rng, step, roughness);
                DiamondStep(floatField, rng, step, roughness);
                step /= 2;
                roughness /= 2.0f;
            }

            (float min, float max) = floatField.MinMax();

            for (int x = 0; x < floatField.Size; x++)
            {
                for (int y = 0; y < floatField.Size; y++)
                {
                    floatField[x, y] = Mathf.Map(floatField[x, y], min, max, outMin, outMax);
                }
            }

        }

        private static void SquareStep(IFloatField field, System.Random rng, int stepSize, float roughness)
        {
            int halfStep = stepSize / 2;

            for (int y = halfStep; y < field.Size; y += stepSize)
            {
                for (int x = halfStep; x < field.Size; x += stepSize)
                {
                    float a = field[x - halfStep, y - halfStep];//top left
                    float b = field[x + halfStep, y - halfStep];//top right
                    float c = field[x - halfStep, y + halfStep];//bottom left
                    float d = field[x + halfStep, y + halfStep];//bottom right
                    float mid = (a + b + c + d) / 4.0f;
                    mid += Mathf.Random(rng, -roughness, roughness);

                    field[x, y] = mid;
                }
            }

        }

        private static void DiamondStep(IFloatField field, System.Random rng, int stepSize, float roughness)
        {
            int halfStep = stepSize / 2;
            if (halfStep == 0)
                return;

            //x-offset
            for (int y = 0; y < field.Size; y += stepSize)
            {
                for (int x = halfStep; x < field.Size; x += stepSize)
                {
                    float n = 4.0f;
                    float a, b, c, d;

                    //make sure in bounds
                    if (x - halfStep >= 0)
                        a = field[x - halfStep, y];
                    else
                    {
                        a = 0;
                        n -= 1;
                    }

                    if (x + halfStep < field.Size)
                        b = field[x + halfStep, y];
                    else
                    {
                        b = 0;
                        n -= 1;
                    }
                    if (y - halfStep >= 0)
                        c = field[x, y - halfStep];
                    else
                    {
                        c = 0;
                        n -= 1;
                    }
                    if (y + halfStep < field.Size)
                        d = field[x, y + halfStep];
                    else
                    {
                        d = 0;
                        n -= 1;
                    }
                    float p = (a + b + c + d) / n;
                    p += Mathf.Random(rng, -roughness, roughness);
                    field[x, y] = p;
                }
            }

            //y offset
            for (int y = halfStep; y < field.Size; y += stepSize)
            {
                for (int x = 0; x < field.Size; x += stepSize)
                {
                    float n = 4.0f;
                    float a, b, c, d;

                    //make sure in bounds
                    if (x - halfStep >= 0)
                        a = field[x - halfStep, y];
                    else
                    {
                        a = 0;
                        n -= 1;
                    }

                    if (x + halfStep < field.Size)
                        b = field[x + halfStep, y];
                    else
                    {
                        b = 0;
                        n -= 1;
                    }
                    if (y - halfStep >= 0)
                        c = field[x, y - halfStep];
                    else
                    {
                        c = 0;
                        n -= 1;
                    }
                    if (y + halfStep < field.Size)
                        d = field[x, y + halfStep];
                    else
                    {
                        d = 0;
                        n -= 1;
                    }
                    float p = (a + b + c + d) / n;
                    p += Mathf.Random(rng, -roughness, roughness);
                    field[x, y] = p;
                }
            }
        }



    }
}


