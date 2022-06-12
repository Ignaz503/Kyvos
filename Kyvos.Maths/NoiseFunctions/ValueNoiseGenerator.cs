//Adapted From: 
//MIT License
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
public struct ValueNoiseGenerator : INoiseGenerator
{
    public NoiseType Type => NoiseType.Value;

    public Vector2 GetDerivative(float x, float y, int seed = NoiseHelpers.DefaultSeed, float frequency = NoiseHelpers.DefaultFrequency)
    {
        NoiseHelpers.TransformNoiseCoordinate(ref x, ref y, frequency);
        return GetDerivative(seed, x, y);
    }

    public Vector2 GetDerivative(int seed, float x, float y)
    {
        int x0 = Mathf.FloorToInt(x);
        int y0 = Mathf.FloorToInt(y);

        float xd0 = (float)(x - x0);
        float yd0 = (float)(y - y0);
        float u = NoiseHelpers.InterpHermite(xd0);
        float v = NoiseHelpers.InterpHermite(yd0);
        float uDeriv = NoiseHelpers.InterpHermiteDerivative(xd0);
        float vDeriv = NoiseHelpers.InterpHermiteDerivative(yd0);

        x0 *= NoiseHelpers.PrimeX;
        y0 *= NoiseHelpers.PrimeY;
        int x1 = x0 + NoiseHelpers.PrimeX;
        int y1 = y0 + NoiseHelpers.PrimeY;

        float k0 = NoiseHelpers.ValCoord(seed, x0, y0);
        float k1 = NoiseHelpers.ValCoord(seed, x1, y0);
        float k2 = NoiseHelpers.ValCoord(seed, x0, y1);
        float k3 = NoiseHelpers.ValCoord(seed, x1, y1);
        return NoiseHelpers.BilinearInerpolationDerivative(u, v, uDeriv, vDeriv, k0, k1, k2, k3);
    }

    public Vector3 GetDerivative(float x, float y, float z, int seed = NoiseHelpers.DefaultSeed, float frequency = NoiseHelpers.DefaultFrequency, RotationType3D rotationType = NoiseHelpers.DefaultRotationType)
    {
        NoiseHelpers.TransformNoiseCoordinate(ref x, ref y, ref z, frequency, NoiseHelpers.GetTransformType(rotationType, NoiseType.Value));
        return GetDerivative(seed, x, y, z);
    }

    public Vector3 GetDerivative(int seed, float x, float y, float z)
    {
        int x0 = Mathf.FloorToInt(x);
        int y0 = Mathf.FloorToInt(y);
        int z0 = Mathf.FloorToInt(z);

        float xd0 = (float)(x - x0);
        float yd0 = (float)(y - y0);
        float zd0 = (float)(z - z0);

        float u = NoiseHelpers.InterpHermite(xd0);
        float v = NoiseHelpers.InterpHermite(yd0);
        float w = NoiseHelpers.InterpHermite(zd0);
        float uDeriv = NoiseHelpers.InterpHermiteDerivative(xd0);
        float vDeriv = NoiseHelpers.InterpHermiteDerivative(yd0);
        float wDeriv = NoiseHelpers.InterpHermiteDerivative(zd0);


        x0 *= NoiseHelpers.PrimeX;
        y0 *= NoiseHelpers.PrimeY;
        z0 *= NoiseHelpers.PrimeZ;
        int x1 = x0 + NoiseHelpers.PrimeX;
        int y1 = y0 + NoiseHelpers.PrimeY;
        int z1 = z0 + NoiseHelpers.PrimeZ;


        float k0 = NoiseHelpers.ValCoord(seed, x0, y0, z0);
        float k1 = NoiseHelpers.ValCoord(seed, x1, y0, z0);
        float k2 = NoiseHelpers.ValCoord(seed, x0, y1, z0);
        float k3 = NoiseHelpers.ValCoord(seed, x1, y1, z0);
        float k4 = NoiseHelpers.ValCoord(seed, x0, y0, z1);
        float k5 = NoiseHelpers.ValCoord(seed, x1, y0, z1);
        float k6 = NoiseHelpers.ValCoord(seed, x0, y1, z1);
        float k7 = NoiseHelpers.ValCoord(seed, x1, y1, z1);

        return NoiseHelpers.TrilinearInerpolationDerivative(u, v, w, uDeriv, vDeriv, wDeriv, k0, k1, k2, k3, k4, k5, k6, k7);
    }

    /// <summary>
    /// 2D noise at given position using current settings,  with frequency coord transform 
    /// </summary>
    /// <returns>
    /// Noise output bounded between -1...1
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public float GetNoise(float x, float y, int seed = NoiseHelpers.DefaultSeed, float frequency = NoiseHelpers.DefaultFrequency)
    {
        NoiseHelpers.TransformNoiseCoordinate(ref x, ref y, frequency);
        return GetNoise(seed, x, y);

    }

    /// <summary>
    /// 3D noise at given position using current settings,  with frequency and rotation coord transform 
    /// </summary>
    /// <returns>
    /// Noise output bounded between -1...1
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public float GetNoise(float x, float y, float z, int seed = NoiseHelpers.DefaultSeed, float frequency = NoiseHelpers.DefaultFrequency, RotationType3D rotationType = NoiseHelpers.DefaultRotationType)
    {
        NoiseHelpers.TransformNoiseCoordinate(ref x, ref y, ref z, frequency, NoiseHelpers.GetTransformType(rotationType, NoiseType.Value));
        return GetNoise(seed, x, y, z);
    }

    /// <summary>
    /// 2D noise at given position using current settings,  without frequency coord transform 
    /// </summary>
    /// <returns>
    /// Noise output bounded between -1...1
    /// </returns>
    public float GetNoise(int seed, float x, float y)
    {
        int x0 = Mathf.FloorToInt(x);
        int y0 = Mathf.FloorToInt(y);

        float xs = NoiseHelpers.InterpHermite((float)(x - x0));
        float ys = NoiseHelpers.InterpHermite((float)(y - y0));

        x0 *= NoiseHelpers.PrimeX;
        y0 *= NoiseHelpers.PrimeY;
        int x1 = x0 + NoiseHelpers.PrimeX;
        int y1 = y0 + NoiseHelpers.PrimeY;

        float xf0 = NoiseHelpers.Lerp(NoiseHelpers.ValCoord(seed, x0, y0), NoiseHelpers.ValCoord(seed, x1, y0), xs);
        float xf1 = NoiseHelpers.Lerp(NoiseHelpers.ValCoord(seed, x0, y1), NoiseHelpers.ValCoord(seed, x1, y1), xs);

        return NoiseHelpers.Lerp(xf0, xf1, ys);
    }

    /// <summary>
    /// 2D noise at given position using current settings,  without frequency and rotation coord transform 
    /// </summary>
    /// <returns>
    /// Noise output bounded between -1...1
    /// </returns>
    public float GetNoise(int seed, float x, float y, float z)
    {
        int x0 = Mathf.FloorToInt(x);
        int y0 = Mathf.FloorToInt(y);
        int z0 = Mathf.FloorToInt(z);

        float xs = NoiseHelpers.InterpHermite((float)(x - x0));
        float ys = NoiseHelpers.InterpHermite((float)(y - y0));
        float zs = NoiseHelpers.InterpHermite((float)(z - z0));

        x0 *= NoiseHelpers.PrimeX;
        y0 *= NoiseHelpers.PrimeY;
        z0 *= NoiseHelpers.PrimeZ;
        int x1 = x0 + NoiseHelpers.PrimeX;
        int y1 = y0 + NoiseHelpers.PrimeY;
        int z1 = z0 + NoiseHelpers.PrimeZ;

        float xf00 = NoiseHelpers.Lerp(NoiseHelpers.ValCoord(seed, x0, y0, z0), NoiseHelpers.ValCoord(seed, x1, y0, z0), xs);
        float xf10 = NoiseHelpers.Lerp(NoiseHelpers.ValCoord(seed, x0, y1, z0), NoiseHelpers.ValCoord(seed, x1, y1, z0), xs);
        float xf01 = NoiseHelpers.Lerp(NoiseHelpers.ValCoord(seed, x0, y0, z1), NoiseHelpers.ValCoord(seed, x1, y0, z1), xs);
        float xf11 = NoiseHelpers.Lerp(NoiseHelpers.ValCoord(seed, x0, y1, z1), NoiseHelpers.ValCoord(seed, x1, y1, z1), xs);

        float yf0 = NoiseHelpers.Lerp(xf00, xf10, ys);
        float yf1 = NoiseHelpers.Lerp(xf01, xf11, ys);

        return NoiseHelpers.Lerp(yf0, yf1, zs);
    }
}



