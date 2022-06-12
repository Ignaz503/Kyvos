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
public struct OpenSimplexNoiseGenerator : INoiseGenerator
{
    public NoiseType Type => NoiseType.OpenSimplex2;

    public Vector2 GetDerivative(float x, float y, int seed = NoiseHelpers.DefaultSeed, float frequency = NoiseHelpers.DefaultFrequency)
    {
        NoiseHelpers.TransformNoiseCoordinateSimplex(ref x, ref y, frequency);
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
        NoiseHelpers.TransformNoiseCoordinate(ref x, ref y, ref z, frequency, NoiseHelpers.GetTransformType(rotationType, NoiseType.OpenSimplex2));
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
    /// 3D noise at given position using current settings, with frequency and rotation coord transformation
    /// </summary>
    /// <returns>
    /// Noise output bounded between -1...1
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public float GetNoise(float x, float y, float z, int seed = NoiseHelpers.DefaultSeed, float frequency = NoiseHelpers.DefaultFrequency, RotationType3D rotationType = NoiseHelpers.DefaultRotationType)
    {
        NoiseHelpers.TransformNoiseCoordinate(ref x, ref y, ref z, frequency, NoiseHelpers.GetTransformType(rotationType, NoiseType.OpenSimplex2));

        return GetNoise(seed, x, y, z);
    }

    /// <summary>
    /// 2D noise at given position using current settings, without frequency coord transformation
    /// </summary>
    /// <returns>
    /// Noise output bounded between -1...1
    /// </returns>
    public float GetNoise(int seed, float x, float y)
    {
        // 2D OpenSimplex2 case uses the same algorithm as ordinary Simplex.

        const float SQRT3 = 1.7320508075688772935274463415059f;
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

        float t = (xi + yi) * G2;
        float x0 = (float)(xi - t);
        float y0 = (float)(yi - t);

        i *= NoiseHelpers.PrimeX;
        j *= NoiseHelpers.PrimeY;

        float n0, n1, n2;

        float a = 0.5f - x0 * x0 - y0 * y0;
        if (a <= 0)
            n0 = 0;
        else
        {
            n0 = (a * a) * (a * a) * NoiseHelpers.GradCoord(seed, i, j, x0, y0);
        }

        float c = (float)(2 * (1 - 2 * G2) * (1 / G2 - 2)) * t + ((float)(-2 * (1 - 2 * G2) * (1 - 2 * G2)) + a);
        if (c <= 0)
            n2 = 0;
        else
        {
            float x2 = x0 + (2 * (float)G2 - 1);
            float y2 = y0 + (2 * (float)G2 - 1);
            n2 = (c * c) * (c * c) * NoiseHelpers.GradCoord(seed, i + NoiseHelpers.PrimeX, j + NoiseHelpers.PrimeY, x2, y2);
        }

        if (y0 > x0)
        {
            float x1 = x0 + (float)G2;
            float y1 = y0 + ((float)G2 - 1);
            float b = 0.5f - x1 * x1 - y1 * y1;
            if (b <= 0)
                n1 = 0;
            else
            {
                n1 = (b * b) * (b * b) * NoiseHelpers.GradCoord(seed, i, j + NoiseHelpers.PrimeY, x1, y1);
            }
        }
        else
        {
            float x1 = x0 + ((float)G2 - 1);
            float y1 = y0 + (float)G2;
            float b = 0.5f - x1 * x1 - y1 * y1;
            if (b <= 0)
                n1 = 0;
            else
            {
                n1 = (b * b) * (b * b) * NoiseHelpers.GradCoord(seed, i + NoiseHelpers.PrimeX, j, x1, y1);
            }
        }

        return (n0 + n1 + n2) * 99.83685446303647f;
    }

    /// <summary>
    /// 3D noise at given position using current settings, without frequency and rotation coord transformation
    /// </summary>
    /// <returns>
    /// Noise output bounded between -1...1
    /// </returns>
    public float GetNoise(int seed, float x, float y, float z)
    {
        // 3D OpenSimplex2 case uses two offset rotated cube grids.

        /*
         * --- Rotation moved to TransformNoiseCoordinate method ---
         * const FNfloat R3 = (FNfloat)(2.0 / 3.0);
         * FNfloat r = (x + y + z) * R3; // Rotation, not skew
         * x = r - x; y = r - y; z = r - z;
        */

        int i = Mathf.RoundToInt(x);
        int j = Mathf.RoundToInt(y);
        int k = Mathf.RoundToInt(z);
        float x0 = (float)(x - i);
        float y0 = (float)(y - j);
        float z0 = (float)(z - k);

        int xNSign = (int)(-1.0f - x0) | 1;
        int yNSign = (int)(-1.0f - y0) | 1;
        int zNSign = (int)(-1.0f - z0) | 1;

        float ax0 = xNSign * -x0;
        float ay0 = yNSign * -y0;
        float az0 = zNSign * -z0;

        i *= NoiseHelpers.PrimeX;
        j *= NoiseHelpers.PrimeY;
        k *= NoiseHelpers.PrimeZ;

        float value = 0;
        float a = (0.6f - x0 * x0) - (y0 * y0 + z0 * z0);

        for (int l = 0; ; l++)
        {
            if (a > 0)
            {
                value += (a * a) * (a * a) * NoiseHelpers.GradCoord(seed, i, j, k, x0, y0, z0);
            }

            if (ax0 >= ay0 && ax0 >= az0)
            {
                float b = a + ax0 + ax0;
                if (b > 1)
                {
                    b -= 1;
                    value += (b * b) * (b * b) * NoiseHelpers.GradCoord(seed, i - xNSign * NoiseHelpers.PrimeX, j, k, x0 + xNSign, y0, z0);
                }
            }
            else if (ay0 > ax0 && ay0 >= az0)
            {
                float b = a + ay0 + ay0;
                if (b > 1)
                {
                    b -= 1;
                    value += (b * b) * (b * b) * NoiseHelpers.GradCoord(seed, i, j - yNSign * NoiseHelpers.PrimeY, k, x0, y0 + yNSign, z0);
                }
            }
            else
            {
                float b = a + az0 + az0;
                if (b > 1)
                {
                    b -= 1;
                    value += (b * b) * (b * b) * NoiseHelpers.GradCoord(seed, i, j, k - zNSign * NoiseHelpers.PrimeZ, x0, y0, z0 + zNSign);
                }
            }

            if (l == 1)
                break;

            ax0 = 0.5f - ax0;
            ay0 = 0.5f - ay0;
            az0 = 0.5f - az0;

            x0 = xNSign * ax0;
            y0 = yNSign * ay0;
            z0 = zNSign * az0;

            a += (0.75f - ax0) - (ay0 + az0);

            i += (xNSign >> 1) & NoiseHelpers.PrimeX;
            j += (yNSign >> 1) & NoiseHelpers.PrimeY;
            k += (zNSign >> 1) & NoiseHelpers.PrimeZ;

            xNSign = -xNSign;
            yNSign = -yNSign;
            zNSign = -zNSign;

            seed = ~seed;
        }

        return value * 32.69428253173828125f;
    }
}


