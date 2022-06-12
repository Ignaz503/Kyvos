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

using System.Numerics;
using System.Runtime.CompilerServices;

// Switch between using floats or doubles for input position


namespace Kyvos.Maths.NoiseFunctions;
public struct OpenSimplex2SNoiseGenerator : INoiseGenerator
{
    public NoiseType Type => NoiseType.OpenSimplex2S;

    public Vector2 GetDerivative(float x, float y, int seed = NoiseHelpers.DefaultSeed, float frequency = NoiseHelpers.DefaultFrequency)
    {
        NoiseHelpers.TransformNoiseCoordinate(ref x, ref y, frequency);
        return GetDerivative(seed, x, y);
    }

    public Vector2 GetDerivative(int seed, float x, float y)
    {
        var eps = 0.0001f;

        //Find rate of change in X direction
        var n1 = GetNoise(seed, x + eps, y);
        var n2 = GetNoise(seed, x - eps, y);

        //Average to find approximate derivative
        var a = (n1 - n2) / (2f * eps);

        //Find rate of change in Y direction
        n1 = GetNoise(seed, x, y + eps);
        n2 = GetNoise(seed, x, y - eps);

        var b = (n1 - n2) / (2f * eps);


        return new Vector2(a, b);
    }

    public Vector3 GetDerivative(float x, float y, float z, int seed = NoiseHelpers.DefaultSeed, float frequency = NoiseHelpers.DefaultFrequency, RotationType3D rotationType = NoiseHelpers.DefaultRotationType)
    {
        NoiseHelpers.TransformNoiseCoordinate(ref x, ref y, ref z, frequency, NoiseHelpers.GetTransformType(rotationType, NoiseType.OpenSimplex2S));
        return GetDerivative(seed, x, y, z);
    }

    public Vector3 GetDerivative(int seed, float x, float y, float z)
    {
        var eps = 0.0001f;

        //Find rate of change in X direction
        var n1 = GetNoise(seed, x + eps, y, z);
        var n2 = GetNoise(seed, x - eps, y, z);

        //Average to find approximate derivative
        var a = (n1 - n2) / (2f * eps);

        //Find rate of change in Y direction
        n1 = GetNoise(seed, x, y + eps, z);
        n2 = GetNoise(seed, x, y - eps, z);

        var b = (n1 - n2) / (2f * eps);

        //Find rate of change in Z direction
        n1 = GetNoise(seed, x, y, z + eps);
        n2 = GetNoise(seed, x, y, z - eps);

        var c = (n1 - n2) / (2f * eps);


        return new Vector3(a, b, c);
    }

    /// <summary>
    /// 2D noise at given position using current settings, with frequency coord transformation
    /// </summary>
    /// <returns>
    /// Noise output bounded between -1...1
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public float GetNoise(float x, float y, int seed = NoiseHelpers.DefaultSeed, float frequency = NoiseHelpers.DefaultFrequency)
    {
        NoiseHelpers.TransformNoiseCoordinateSimplex(ref x, ref y, frequency);

        return GetNoise(seed, x, y);
    }

    /// <summary>
    /// 3D noise at given position using current settings, with frequency coord trnasformation
    /// </summary>
    /// <returns>
    /// Noise output bounded between -1...1
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public float GetNoise(float x, float y, float z, int seed = NoiseHelpers.DefaultSeed, float frequency = NoiseHelpers.DefaultFrequency, RotationType3D rotationType = NoiseHelpers.DefaultRotationType)
    {
        NoiseHelpers.TransformNoiseCoordinate(ref x, ref y, ref z, frequency, NoiseHelpers.GetTransformType(rotationType, NoiseType.OpenSimplex2S));

        return GetNoise(seed, x, y, z);
    }

    /// <summary>
    /// 2D noise at given position using current settings, without noise coord transformation
    /// </summary>
    /// <returns>
    /// Noise output bounded between -1...1
    /// </returns>
    public float GetNoise(int seed, float x, float y)
    {
        // 2D OpenSimplex2S case is a modified 2D simplex noise.

        const float SQRT3 = (float)1.7320508075688772935274463415059;
        const float G2 = (3 - SQRT3) / 6;

        /*
         * --- Skew moved to TransformNoiseCoordinate method ---
         * const FNfloat F2 = 0.5f * (SQRT3 - 1);
         * FNfloat s = (x + y) * F2;
         * x += s; y += s;
        */

        int i = Mathf.FloorToInt(x);
        int j = Mathf.FloorToInt(y);
        float xi = (float)(x - i);
        float yi = (float)(y - j);

        i *= NoiseHelpers.PrimeX;
        j *= NoiseHelpers.PrimeY;
        int i1 = i + NoiseHelpers.PrimeX;
        int j1 = j + NoiseHelpers.PrimeY;

        float t = (xi + yi) * (float)G2;
        float x0 = xi - t;
        float y0 = yi - t;

        float a0 = (2.0f / 3.0f) - x0 * x0 - y0 * y0;
        float value = (a0 * a0) * (a0 * a0) * NoiseHelpers.GradCoord(seed, i, j, x0, y0);

        float a1 = (float)(2 * (1 - 2 * G2) * (1 / G2 - 2)) * t + ((float)(-2 * (1 - 2 * G2) * (1 - 2 * G2)) + a0);
        float x1 = x0 - (float)(1 - 2 * G2);
        float y1 = y0 - (float)(1 - 2 * G2);
        value += (a1 * a1) * (a1 * a1) * NoiseHelpers.GradCoord(seed, i1, j1, x1, y1);

        // Nested conditionals were faster than compact bit logic/arithmetic.
        float xmyi = xi - yi;
        if (t > G2)
        {
            if (xi + xmyi > 1)
            {
                float x2 = x0 + (float)(3 * G2 - 2);
                float y2 = y0 + (float)(3 * G2 - 1);
                float a2 = (2.0f / 3.0f) - x2 * x2 - y2 * y2;
                if (a2 > 0)
                {
                    value += (a2 * a2) * (a2 * a2) * NoiseHelpers.GradCoord(seed, i + (NoiseHelpers.PrimeX << 1), j + NoiseHelpers.PrimeY, x2, y2);
                }
            }
            else
            {
                float x2 = x0 + (float)G2;
                float y2 = y0 + (float)(G2 - 1);
                float a2 = (2.0f / 3.0f) - x2 * x2 - y2 * y2;
                if (a2 > 0)
                {
                    value += (a2 * a2) * (a2 * a2) * NoiseHelpers.GradCoord(seed, i, j + NoiseHelpers.PrimeY, x2, y2);
                }
            }

            if (yi - xmyi > 1)
            {
                float x3 = x0 + (float)(3 * G2 - 1);
                float y3 = y0 + (float)(3 * G2 - 2);
                float a3 = (2.0f / 3.0f) - x3 * x3 - y3 * y3;
                if (a3 > 0)
                {
                    value += (a3 * a3) * (a3 * a3) * NoiseHelpers.GradCoord(seed, i + NoiseHelpers.PrimeX, j + (NoiseHelpers.PrimeY << 1), x3, y3);
                }
            }
            else
            {
                float x3 = x0 + (float)(G2 - 1);
                float y3 = y0 + (float)G2;
                float a3 = (2.0f / 3.0f) - x3 * x3 - y3 * y3;
                if (a3 > 0)
                {
                    value += (a3 * a3) * (a3 * a3) * NoiseHelpers.GradCoord(seed, i + NoiseHelpers.PrimeX, j, x3, y3);
                }
            }
        }
        else
        {
            if (xi + xmyi < 0)
            {
                float x2 = x0 + (float)(1 - G2);
                float y2 = y0 - (float)G2;
                float a2 = (2.0f / 3.0f) - x2 * x2 - y2 * y2;
                if (a2 > 0)
                {
                    value += (a2 * a2) * (a2 * a2) * NoiseHelpers.GradCoord(seed, i - NoiseHelpers.PrimeX, j, x2, y2);
                }
            }
            else
            {
                float x2 = x0 + (float)(G2 - 1);
                float y2 = y0 + (float)G2;
                float a2 = (2.0f / 3.0f) - x2 * x2 - y2 * y2;
                if (a2 > 0)
                {
                    value += (a2 * a2) * (a2 * a2) * NoiseHelpers.GradCoord(seed, i + NoiseHelpers.PrimeX, j, x2, y2);
                }
            }

            if (yi < xmyi)
            {
                float x2 = x0 - (float)G2;
                float y2 = y0 - (float)(G2 - 1);
                float a2 = (2.0f / 3.0f) - x2 * x2 - y2 * y2;
                if (a2 > 0)
                {
                    value += (a2 * a2) * (a2 * a2) * NoiseHelpers.GradCoord(seed, i, j - NoiseHelpers.PrimeY, x2, y2);
                }
            }
            else
            {
                float x2 = x0 + (float)G2;
                float y2 = y0 + (float)(G2 - 1);
                float a2 = (2.0f / 3.0f) - x2 * x2 - y2 * y2;
                if (a2 > 0)
                {
                    value += (a2 * a2) * (a2 * a2) * NoiseHelpers.GradCoord(seed, i, j + NoiseHelpers.PrimeY, x2, y2);
                }
            }
        }

        return value * 18.24196194486065f;
    }
    /// <summary>
    /// 3D noise at given position using current settings, without noise coord transformation
    /// </summary>
    /// <returns>
    /// Noise output bounded between -1...1
    /// </returns>
    public float GetNoise(int seed, float x, float y, float z)
    {
        // 3D OpenSimplex2S case uses two offset rotated cube grids.

        /*
         * --- Rotation moved to TransformNoiseCoordinate method ---
         * const FNfloat R3 = (FNfloat)(2.0 / 3.0);
         * FNfloat r = (x + y + z) * R3; // Rotation, not skew
         * x = r - x; y = r - y; z = r - z;
        */

        int i = Mathf.FloorToInt(x);
        int j = Mathf.FloorToInt(y);
        int k = Mathf.FloorToInt(z);
        float xi = (float)(x - i);
        float yi = (float)(y - j);
        float zi = (float)(z - k);

        i *= NoiseHelpers.PrimeX;
        j *= NoiseHelpers.PrimeY;
        k *= NoiseHelpers.PrimeZ;
        int seed2 = seed + 1293373;

        int xNMask = (int)(-0.5f - xi);
        int yNMask = (int)(-0.5f - yi);
        int zNMask = (int)(-0.5f - zi);

        float x0 = xi + xNMask;
        float y0 = yi + yNMask;
        float z0 = zi + zNMask;
        float a0 = 0.75f - x0 * x0 - y0 * y0 - z0 * z0;
        float value = (a0 * a0) * (a0 * a0) * NoiseHelpers.GradCoord(seed,
        i + (xNMask & NoiseHelpers.PrimeX), j + (yNMask & NoiseHelpers.PrimeY), k + (zNMask & NoiseHelpers.PrimeZ), x0, y0, z0);

        float x1 = xi - 0.5f;
        float y1 = yi - 0.5f;
        float z1 = zi - 0.5f;
        float a1 = 0.75f - x1 * x1 - y1 * y1 - z1 * z1;
        value += (a1 * a1) * (a1 * a1) * NoiseHelpers.GradCoord(seed2,
            i + NoiseHelpers.PrimeX, j + NoiseHelpers.PrimeY, k + NoiseHelpers.PrimeZ, x1, y1, z1);

        float xAFlipMask0 = ((xNMask | 1) << 1) * x1;
        float yAFlipMask0 = ((yNMask | 1) << 1) * y1;
        float zAFlipMask0 = ((zNMask | 1) << 1) * z1;
        float xAFlipMask1 = (-2 - (xNMask << 2)) * x1 - 1.0f;
        float yAFlipMask1 = (-2 - (yNMask << 2)) * y1 - 1.0f;
        float zAFlipMask1 = (-2 - (zNMask << 2)) * z1 - 1.0f;

        bool skip5 = false;
        float a2 = xAFlipMask0 + a0;
        if (a2 > 0)
        {
            float x2 = x0 - (xNMask | 1);
            float y2 = y0;
            float z2 = z0;
            value += (a2 * a2) * (a2 * a2) * NoiseHelpers.GradCoord(seed,
                i + (~xNMask & NoiseHelpers.PrimeX), j + (yNMask & NoiseHelpers.PrimeY), k + (zNMask & NoiseHelpers.PrimeZ), x2, y2, z2);
        }
        else
        {
            float a3 = yAFlipMask0 + zAFlipMask0 + a0;
            if (a3 > 0)
            {
                float x3 = x0;
                float y3 = y0 - (yNMask | 1);
                float z3 = z0 - (zNMask | 1);
                value += (a3 * a3) * (a3 * a3) * NoiseHelpers.GradCoord(seed,
                    i + (xNMask & NoiseHelpers.PrimeX), j + (~yNMask & NoiseHelpers.PrimeY), k + (~zNMask & NoiseHelpers.PrimeZ), x3, y3, z3);
            }

            float a4 = xAFlipMask1 + a1;
            if (a4 > 0)
            {
                float x4 = (xNMask | 1) + x1;
                float y4 = y1;
                float z4 = z1;
                value += (a4 * a4) * (a4 * a4) * NoiseHelpers.GradCoord(seed2,
                    i + (xNMask & (NoiseHelpers.PrimeX * 2)), j + NoiseHelpers.PrimeY, k + NoiseHelpers.PrimeZ, x4, y4, z4);
                skip5 = true;
            }
        }

        bool skip9 = false;
        float a6 = yAFlipMask0 + a0;
        if (a6 > 0)
        {
            float x6 = x0;
            float y6 = y0 - (yNMask | 1);
            float z6 = z0;
            value += (a6 * a6) * (a6 * a6) * NoiseHelpers.GradCoord(seed,
                i + (xNMask & NoiseHelpers.PrimeX), j + (~yNMask & NoiseHelpers.PrimeY), k + (zNMask & NoiseHelpers.PrimeZ), x6, y6, z6);
        }
        else
        {
            float a7 = xAFlipMask0 + zAFlipMask0 + a0;
            if (a7 > 0)
            {
                float x7 = x0 - (xNMask | 1);
                float y7 = y0;
                float z7 = z0 - (zNMask | 1);
                value += (a7 * a7) * (a7 * a7) * NoiseHelpers.GradCoord(seed,
                    i + (~xNMask & NoiseHelpers.PrimeX), j + (yNMask & NoiseHelpers.PrimeY), k + (~zNMask & NoiseHelpers.PrimeZ), x7, y7, z7);
            }

            float a8 = yAFlipMask1 + a1;
            if (a8 > 0)
            {
                float x8 = x1;
                float y8 = (yNMask | 1) + y1;
                float z8 = z1;
                value += (a8 * a8) * (a8 * a8) * NoiseHelpers.GradCoord(seed2,
                    i + NoiseHelpers.PrimeX, j + (yNMask & (NoiseHelpers.PrimeY << 1)), k + NoiseHelpers.PrimeZ, x8, y8, z8);
                skip9 = true;
            }
        }

        bool skipD = false;
        float aA = zAFlipMask0 + a0;
        if (aA > 0)
        {
            float xA = x0;
            float yA = y0;
            float zA = z0 - (zNMask | 1);
            value += (aA * aA) * (aA * aA) * NoiseHelpers.GradCoord(seed,
                i + (xNMask & NoiseHelpers.PrimeX), j + (yNMask & NoiseHelpers.PrimeY), k + (~zNMask & NoiseHelpers.PrimeZ), xA, yA, zA);
        }
        else
        {
            float aB = xAFlipMask0 + yAFlipMask0 + a0;
            if (aB > 0)
            {
                float xB = x0 - (xNMask | 1);
                float yB = y0 - (yNMask | 1);
                float zB = z0;
                value += (aB * aB) * (aB * aB) * NoiseHelpers.GradCoord(seed,
                    i + (~xNMask & NoiseHelpers.PrimeX), j + (~yNMask & NoiseHelpers.PrimeY), k + (zNMask & NoiseHelpers.PrimeZ), xB, yB, zB);
            }

            float aC = zAFlipMask1 + a1;
            if (aC > 0)
            {
                float xC = x1;
                float yC = y1;
                float zC = (zNMask | 1) + z1;
                value += (aC * aC) * (aC * aC) * NoiseHelpers.GradCoord(seed2,
                    i + NoiseHelpers.PrimeX, j + NoiseHelpers.PrimeY, k + (zNMask & (NoiseHelpers.PrimeZ << 1)), xC, yC, zC);
                skipD = true;
            }
        }

        if (!skip5)
        {
            float a5 = yAFlipMask1 + zAFlipMask1 + a1;
            if (a5 > 0)
            {
                float x5 = x1;
                float y5 = (yNMask | 1) + y1;
                float z5 = (zNMask | 1) + z1;
                value += (a5 * a5) * (a5 * a5) * NoiseHelpers.GradCoord(seed2,
                    i + NoiseHelpers.PrimeX, j + (yNMask & (NoiseHelpers.PrimeY << 1)), k + (zNMask & (NoiseHelpers.PrimeZ << 1)), x5, y5, z5);
            }
        }

        if (!skip9)
        {
            float a9 = xAFlipMask1 + zAFlipMask1 + a1;
            if (a9 > 0)
            {
                float x9 = (xNMask | 1) + x1;
                float y9 = y1;
                float z9 = (zNMask | 1) + z1;
                value += (a9 * a9) * (a9 * a9) * NoiseHelpers.GradCoord(seed2,
                    i + (xNMask & (NoiseHelpers.PrimeX * 2)), j + NoiseHelpers.PrimeY, k + (zNMask & (NoiseHelpers.PrimeZ << 1)), x9, y9, z9);
            }
        }

        if (!skipD)
        {
            float aD = xAFlipMask1 + yAFlipMask1 + a1;
            if (aD > 0)
            {
                float xD = (xNMask | 1) + x1;
                float yD = (yNMask | 1) + y1;
                float zD = z1;
                value += (aD * aD) * (aD * aD) * NoiseHelpers.GradCoord(seed2,
                    i + (xNMask & (NoiseHelpers.PrimeX << 1)), j + (yNMask & (NoiseHelpers.PrimeY << 1)), k + NoiseHelpers.PrimeZ, xD, yD, zD);
            }
        }

        return value * 9.046026385208288f;
    }
}


