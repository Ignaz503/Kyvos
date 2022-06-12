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


using System.Runtime.CompilerServices;

namespace Kyvos.Maths.NoiseFunctions;
public static class DomainWarp
{
    public const float DefaultAmplification = 1.0f;
    public const Type DefaultWarpType = Type.OpenSimplex2;

    public enum Type
    {
        OpenSimplex2,
        OpenSimplex2Reduced,
        BasicGrid
    };

    // Domain Warp Single
    public static void Single(ref float x, ref float y, int seed = NoiseHelpers.DefaultSeed, float warpAmp = DefaultAmplification, float fractalBounding = NoiseHelpers.DefaultFractalBounding, float freq = NoiseHelpers.DefaultFrequency, Type warpType = DefaultWarpType)
    {
        float amp = warpAmp * fractalBounding;

        float xs = x;
        float ys = y;
        TransformDomainWarpCoordinate(ref xs, ref ys, warpType);

        DoSingleDomainWarp(seed, amp, freq, xs, ys, ref x, ref y, warpType);
    }
    public static void Single(ref float x, ref float y, ref float z, int seed = NoiseHelpers.DefaultSeed, float warpAmp = DefaultAmplification, float fractalBounding = NoiseHelpers.DefaultFractalBounding, float freq = NoiseHelpers.DefaultFrequency, RotationType3D rotationType = NoiseHelpers.DefaultRotationType, Type warpType = DefaultWarpType)
    {
        float amp = warpAmp * fractalBounding;

        float xs = x;
        float ys = y;
        float zs = z;
        TransformDomainWarpCoordinate(ref xs, ref ys, ref zs, GetWarpTransformType3D(warpType, rotationType));

        DoSingleDomainWarp(seed, amp, freq, xs, ys, zs, ref x, ref y, ref z, warpType);
    }
    // Domain Warp Fractal Progressive
    public static void FractalProgressive(ref float x, ref float y, int seed = NoiseHelpers.DefaultSeed, float warpAmp = DefaultAmplification, float fractalBounding = NoiseHelpers.DefaultFractalBounding, float freq = NoiseHelpers.DefaultFrequency, int octaves = NoiseHelpers.DefaultOctaves, float gain = NoiseHelpers.DefaultGain, float lacunarity = NoiseHelpers.DefaultLacunarity, Type warpType = DefaultWarpType)
    {
        float amp = warpAmp * fractalBounding;

        for (int i = 0; i < octaves; i++)
        {
            float xs = x;
            float ys = y;
            TransformDomainWarpCoordinate(ref xs, ref ys, warpType);

            DoSingleDomainWarp(seed, amp, freq, xs, ys, ref x, ref y, warpType);

            seed++;
            amp *= gain;
            freq *= lacunarity;
        }
    }
    public static void FractalProgressive(ref float x, ref float y, ref float z, int seed = NoiseHelpers.DefaultSeed, float warpAmp = DefaultAmplification, float fractalBounding = NoiseHelpers.DefaultFractalBounding, float freq = NoiseHelpers.DefaultFrequency, int octaves = NoiseHelpers.DefaultOctaves, float gain = NoiseHelpers.DefaultGain, float lacunarity = NoiseHelpers.DefaultLacunarity, RotationType3D rotationType = NoiseHelpers.DefaultRotationType, Type warpType = DefaultWarpType)
    {
        float amp = warpAmp * fractalBounding;

        for (int i = 0; i < octaves; i++)
        {
            float xs = x;
            float ys = y;
            float zs = z;
            TransformDomainWarpCoordinate(ref xs, ref ys, ref zs, GetWarpTransformType3D(warpType, rotationType));

            DoSingleDomainWarp(seed, amp, freq, xs, ys, zs, ref x, ref y, ref z, warpType);

            seed++;
            amp *= gain;
            freq *= lacunarity;
        }
    }
    // Domain Warp Fractal Independant
    public static void FractalIndependent(ref float x, ref float y, int seed = NoiseHelpers.DefaultSeed, float warpAmp = DefaultAmplification, float fractalBounding = NoiseHelpers.DefaultFractalBounding, float freq = NoiseHelpers.DefaultFrequency, int octaves = NoiseHelpers.DefaultOctaves, float gain = NoiseHelpers.DefaultGain, float lacunarity = NoiseHelpers.DefaultLacunarity, Type warpType = DefaultWarpType)
    {
        float xs = x;
        float ys = y;
        TransformDomainWarpCoordinate(ref xs, ref ys, warpType);

        float amp = warpAmp * fractalBounding;

        for (int i = 0; i < octaves; i++)
        {
            DoSingleDomainWarp(seed, amp, freq, xs, ys, ref x, ref y, warpType);

            seed++;
            amp *= gain;
            freq *= lacunarity;
        }
    }
    public static void FractalIndependent(ref float x, ref float y, ref float z, int seed = NoiseHelpers.DefaultSeed, float warpAmp = DefaultAmplification, float fractalBounding = NoiseHelpers.DefaultFractalBounding, float freq = NoiseHelpers.DefaultFrequency, int octaves = NoiseHelpers.DefaultOctaves, float gain = NoiseHelpers.DefaultGain, float lacunarity = NoiseHelpers.DefaultLacunarity, RotationType3D rotationType = NoiseHelpers.DefaultRotationType, Type warpType = DefaultWarpType)
    {
        float xs = x;
        float ys = y;
        float zs = z;
        TransformDomainWarpCoordinate(ref xs, ref ys, ref zs, GetWarpTransformType3D(warpType, rotationType));

        float amp = warpAmp * fractalBounding;

        for (int i = 0; i < octaves; i++)
        {
            DoSingleDomainWarp(seed, amp, freq, xs, ys, zs, ref x, ref y, ref z, warpType);

            seed++;
            amp *= gain;
            freq *= lacunarity;
        }
    }

    //helper function
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void TransformDomainWarpCoordinate(ref float x, ref float y, Type warpType)
    {
        switch (warpType)
        {
            case Type.OpenSimplex2:
            case Type.OpenSimplex2Reduced:
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
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void TransformDomainWarpCoordinate(ref float x, ref float y, ref float z, TransformType3D transformType3D)
    {
        switch (transformType3D)
        {
            case TransformType3D.ImproveXYPlanes:
                {
                    float xy = x + y;
                    float s2 = xy * -(float)0.211324865405187;
                    z *= (float)0.577350269189626;
                    x += s2 - z;
                    y = y + s2 - z;
                    z += xy * (float)0.577350269189626;
                }
                break;
            case TransformType3D.ImproveXZPlanes:
                {
                    float xz = x + z;
                    float s2 = xz * -(float)0.211324865405187;
                    y *= (float)0.577350269189626;
                    x += s2 - y;
                    z += s2 - y;
                    y += xz * (float)0.577350269189626;
                }
                break;
            case TransformType3D.DefaultOpenSimplex2:
                {
                    const float R3 = (float)(2.0 / 3.0);
                    float r = (x + y + z) * R3; // Rotation, not skew
                    x = r - x;
                    y = r - y;
                    z = r - z;
                }
                break;
            default:
                break;
        }
    }
    //do warp
    private static void DoSingleDomainWarp(int seed, float amp, float freq, float x, float y, ref float xr, ref float yr, Type warpType)
    {
        switch (warpType)
        {
            case Type.OpenSimplex2:
                SingleDomainWarpSimplexGradient(seed, amp * 38.283687591552734375f, freq, x, y, ref xr, ref yr, false);
                break;
            case Type.OpenSimplex2Reduced:
                SingleDomainWarpSimplexGradient(seed, amp * 16.0f, freq, x, y, ref xr, ref yr, true);
                break;
            case Type.BasicGrid:
                SingleDomainWarpBasicGrid(seed, amp, freq, x, y, ref xr, ref yr);
                break;
        }
    }
    private static void DoSingleDomainWarp(int seed, float amp, float freq, float x, float y, float z, ref float xr, ref float yr, ref float zr, Type warpType)
    {
        switch (warpType)
        {
            case Type.OpenSimplex2:
                SingleDomainWarpOpenSimplex2Gradient(seed, amp * 32.69428253173828125f, freq, x, y, z, ref xr, ref yr, ref zr, false);
                break;
            case Type.OpenSimplex2Reduced:
                SingleDomainWarpOpenSimplex2Gradient(seed, amp * 7.71604938271605f, freq, x, y, z, ref xr, ref yr, ref zr, true);
                break;
            case Type.BasicGrid:
                SingleDomainWarpBasicGrid(seed, amp, freq, x, y, z, ref xr, ref yr, ref zr);
                break;
        }
    }
    // Domain Warp Basic Grid
    private static void SingleDomainWarpBasicGrid(int seed, float warpAmp, float frequency, float x, float y, ref float xr, ref float yr)
    {
        float xf = x * frequency;
        float yf = y * frequency;

        int x0 = Mathf.FloorToInt(xf);
        int y0 = Mathf.FloorToInt(yf);

        float xs = NoiseHelpers.InterpHermite((float)(xf - x0));
        float ys = NoiseHelpers.InterpHermite((float)(yf - y0));

        x0 *= NoiseHelpers.PrimeX;
        y0 *= NoiseHelpers.PrimeY;
        int x1 = x0 + NoiseHelpers.PrimeX;
        int y1 = y0 + NoiseHelpers.PrimeY;

        int hash0 = NoiseHelpers.Hash(seed, x0, y0) & (255 << 1);
        int hash1 = NoiseHelpers.Hash(seed, x1, y0) & (255 << 1);

        float lx0x = NoiseHelpers.Lerp(NoiseHelpers.RandVecs2D[hash0], NoiseHelpers.RandVecs2D[hash1], xs);
        float ly0x = NoiseHelpers.Lerp(NoiseHelpers.RandVecs2D[hash0 | 1], NoiseHelpers.RandVecs2D[hash1 | 1], xs);

        hash0 = NoiseHelpers.Hash(seed, x0, y1) & (255 << 1);
        hash1 = NoiseHelpers.Hash(seed, x1, y1) & (255 << 1);

        float lx1x = NoiseHelpers.Lerp(NoiseHelpers.RandVecs2D[hash0], NoiseHelpers.RandVecs2D[hash1], xs);
        float ly1x = NoiseHelpers.Lerp(NoiseHelpers.RandVecs2D[hash0 | 1], NoiseHelpers.RandVecs2D[hash1 | 1], xs);

        xr += NoiseHelpers.Lerp(lx0x, lx1x, ys) * warpAmp;
        yr += NoiseHelpers.Lerp(ly0x, ly1x, ys) * warpAmp;
    }
    private static void SingleDomainWarpBasicGrid(int seed, float warpAmp, float frequency, float x, float y, float z, ref float xr, ref float yr, ref float zr)
    {
        float xf = x * frequency;
        float yf = y * frequency;
        float zf = z * frequency;

        int x0 = Mathf.FloorToInt(xf);
        int y0 = Mathf.FloorToInt(yf);
        int z0 = Mathf.FloorToInt(zf);

        float xs = NoiseHelpers.InterpHermite((float)(xf - x0));
        float ys = NoiseHelpers.InterpHermite((float)(yf - y0));
        float zs = NoiseHelpers.InterpHermite((float)(zf - z0));

        x0 *= NoiseHelpers.PrimeX;
        y0 *= NoiseHelpers.PrimeY;
        z0 *= NoiseHelpers.PrimeZ;
        int x1 = x0 + NoiseHelpers.PrimeX;
        int y1 = y0 + NoiseHelpers.PrimeY;
        int z1 = z0 + NoiseHelpers.PrimeZ;

        int hash0 = NoiseHelpers.Hash(seed, x0, y0, z0) & (255 << 2);
        int hash1 = NoiseHelpers.Hash(seed, x1, y0, z0) & (255 << 2);

        float lx0x = NoiseHelpers.Lerp(NoiseHelpers.RandVecs3D[hash0], NoiseHelpers.RandVecs3D[hash1], xs);
        float ly0x = NoiseHelpers.Lerp(NoiseHelpers.RandVecs3D[hash0 | 1], NoiseHelpers.RandVecs3D[hash1 | 1], xs);
        float lz0x = NoiseHelpers.Lerp(NoiseHelpers.RandVecs3D[hash0 | 2], NoiseHelpers.RandVecs3D[hash1 | 2], xs);

        hash0 = NoiseHelpers.Hash(seed, x0, y1, z0) & (255 << 2);
        hash1 = NoiseHelpers.Hash(seed, x1, y1, z0) & (255 << 2);

        float lx1x = NoiseHelpers.Lerp(NoiseHelpers.RandVecs3D[hash0], NoiseHelpers.RandVecs3D[hash1], xs);
        float ly1x = NoiseHelpers.Lerp(NoiseHelpers.RandVecs3D[hash0 | 1], NoiseHelpers.RandVecs3D[hash1 | 1], xs);
        float lz1x = NoiseHelpers.Lerp(NoiseHelpers.RandVecs3D[hash0 | 2], NoiseHelpers.RandVecs3D[hash1 | 2], xs);

        float lx0y = NoiseHelpers.Lerp(lx0x, lx1x, ys);
        float ly0y = NoiseHelpers.Lerp(ly0x, ly1x, ys);
        float lz0y = NoiseHelpers.Lerp(lz0x, lz1x, ys);

        hash0 = NoiseHelpers.Hash(seed, x0, y0, z1) & (255 << 2);
        hash1 = NoiseHelpers.Hash(seed, x1, y0, z1) & (255 << 2);

        lx0x = NoiseHelpers.Lerp(NoiseHelpers.RandVecs3D[hash0], NoiseHelpers.RandVecs3D[hash1], xs);
        ly0x = NoiseHelpers.Lerp(NoiseHelpers.RandVecs3D[hash0 | 1], NoiseHelpers.RandVecs3D[hash1 | 1], xs);
        lz0x = NoiseHelpers.Lerp(NoiseHelpers.RandVecs3D[hash0 | 2], NoiseHelpers.RandVecs3D[hash1 | 2], xs);

        hash0 = NoiseHelpers.Hash(seed, x0, y1, z1) & (255 << 2);
        hash1 = NoiseHelpers.Hash(seed, x1, y1, z1) & (255 << 2);

        lx1x = NoiseHelpers.Lerp(NoiseHelpers.RandVecs3D[hash0], NoiseHelpers.RandVecs3D[hash1], xs);
        ly1x = NoiseHelpers.Lerp(NoiseHelpers.RandVecs3D[hash0 | 1], NoiseHelpers.RandVecs3D[hash1 | 1], xs);
        lz1x = NoiseHelpers.Lerp(NoiseHelpers.RandVecs3D[hash0 | 2], NoiseHelpers.RandVecs3D[hash1 | 2], xs);

        xr += NoiseHelpers.Lerp(lx0y, NoiseHelpers.Lerp(lx0x, lx1x, ys), zs) * warpAmp;
        yr += NoiseHelpers.Lerp(ly0y, NoiseHelpers.Lerp(ly0x, ly1x, ys), zs) * warpAmp;
        zr += NoiseHelpers.Lerp(lz0y, NoiseHelpers.Lerp(lz0x, lz1x, ys), zs) * warpAmp;
    }
    // Domain Warp Simplex/OpenSimplex2
    private static void SingleDomainWarpSimplexGradient(int seed, float warpAmp, float frequency, float x, float y, ref float xr, ref float yr, bool outGradOnly)
    {
        const float SQRT3 = 1.7320508075688772935274463415059f;
        const float G2 = (3 - SQRT3) / 6;

        x *= frequency;
        y *= frequency;

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

        float vx, vy;
        vx = vy = 0;

        float a = 0.5f - x0 * x0 - y0 * y0;
        if (a > 0)
        {
            float aaaa = (a * a) * (a * a);
            float xo, yo;
            if (outGradOnly)
                NoiseHelpers.GradCoordOut(seed, i, j, out xo, out yo);
            else
                NoiseHelpers.GradCoordDual(seed, i, j, x0, y0, out xo, out yo);
            vx += aaaa * xo;
            vy += aaaa * yo;
        }

        float c = (float)(2 * (1 - 2 * G2) * (1 / G2 - 2)) * t + ((float)(-2 * (1 - 2 * G2) * (1 - 2 * G2)) + a);
        if (c > 0)
        {
            float x2 = x0 + (2 * (float)G2 - 1);
            float y2 = y0 + (2 * (float)G2 - 1);
            float cccc = (c * c) * (c * c);
            float xo, yo;
            if (outGradOnly)
                NoiseHelpers.GradCoordOut(seed, i + NoiseHelpers.PrimeX, j + NoiseHelpers.PrimeY, out xo, out yo);
            else
                NoiseHelpers.GradCoordDual(seed, i + NoiseHelpers.PrimeX, j + NoiseHelpers.PrimeY, x2, y2, out xo, out yo);
            vx += cccc * xo;
            vy += cccc * yo;
        }

        if (y0 > x0)
        {
            float x1 = x0 + (float)G2;
            float y1 = y0 + ((float)G2 - 1);
            float b = 0.5f - x1 * x1 - y1 * y1;
            if (b > 0)
            {
                float bbbb = (b * b) * (b * b);
                float xo, yo;
                if (outGradOnly)
                    NoiseHelpers.GradCoordOut(seed, i, j + NoiseHelpers.PrimeY, out xo, out yo);
                else
                    NoiseHelpers.GradCoordDual(seed, i, j + NoiseHelpers.PrimeY, x1, y1, out xo, out yo);
                vx += bbbb * xo;
                vy += bbbb * yo;
            }
        }
        else
        {
            float x1 = x0 + ((float)G2 - 1);
            float y1 = y0 + (float)G2;
            float b = 0.5f - x1 * x1 - y1 * y1;
            if (b > 0)
            {
                float bbbb = (b * b) * (b * b);
                float xo, yo;
                if (outGradOnly)
                    NoiseHelpers.GradCoordOut(seed, i + NoiseHelpers.PrimeX, j, out xo, out yo);
                else
                    NoiseHelpers.GradCoordDual(seed, i + NoiseHelpers.PrimeX, j, x1, y1, out xo, out yo);
                vx += bbbb * xo;
                vy += bbbb * yo;
            }
        }

        xr += vx * warpAmp;
        yr += vy * warpAmp;
    }
    private static void SingleDomainWarpOpenSimplex2Gradient(int seed, float warpAmp, float frequency, float x, float y, float z, ref float xr, ref float yr, ref float zr, bool outGradOnly)
    {
        x *= frequency;
        y *= frequency;
        z *= frequency;

        /*
         * --- Rotation moved to TransformDomainWarpCoordinate method ---
         * const FNfloat R3 = (FNfloat)(2.0 / 3.0);
         * FNfloat r = (x + y + z) * R3; // Rotation, not skew
         * x = r - x; y = r - y; z = r - z;
        */

        int i = Mathf.RoundToInt(x);
        int j = Mathf.RoundToInt(y);
        int k = Mathf.RoundToInt(z);
        float x0 = (float)x - i;
        float y0 = (float)y - j;
        float z0 = (float)z - k;

        int xNSign = (int)(-x0 - 1.0f) | 1;
        int yNSign = (int)(-y0 - 1.0f) | 1;
        int zNSign = (int)(-z0 - 1.0f) | 1;

        float ax0 = xNSign * -x0;
        float ay0 = yNSign * -y0;
        float az0 = zNSign * -z0;

        i *= NoiseHelpers.PrimeX;
        j *= NoiseHelpers.PrimeY;
        k *= NoiseHelpers.PrimeZ;

        float vx, vy, vz;
        vx = vy = vz = 0;

        float a = (0.6f - x0 * x0) - (y0 * y0 + z0 * z0);
        for (int l = 0; ; l++)
        {
            if (a > 0)
            {
                float aaaa = (a * a) * (a * a);
                float xo, yo, zo;
                if (outGradOnly)
                    NoiseHelpers.GradCoordOut(seed, i, j, k, out xo, out yo, out zo);
                else
                    NoiseHelpers.GradCoordDual(seed, i, j, k, x0, y0, z0, out xo, out yo, out zo);
                vx += aaaa * xo;
                vy += aaaa * yo;
                vz += aaaa * zo;
            }

            float b = a;
            int i1 = i;
            int j1 = j;
            int k1 = k;
            float x1 = x0;
            float y1 = y0;
            float z1 = z0;

            if (ax0 >= ay0 && ax0 >= az0)
            {
                x1 += xNSign;
                b = b + ax0 + ax0;
                i1 -= xNSign * NoiseHelpers.PrimeX;
            }
            else if (ay0 > ax0 && ay0 >= az0)
            {
                y1 += yNSign;
                b = b + ay0 + ay0;
                j1 -= yNSign * NoiseHelpers.PrimeY;
            }
            else
            {
                z1 += zNSign;
                b = b + az0 + az0;
                k1 -= zNSign * NoiseHelpers.PrimeZ;
            }

            if (b > 1)
            {
                b -= 1;
                float bbbb = (b * b) * (b * b);
                float xo, yo, zo;
                if (outGradOnly)
                    NoiseHelpers.GradCoordOut(seed, i1, j1, k1, out xo, out yo, out zo);
                else
                    NoiseHelpers.GradCoordDual(seed, i1, j1, k1, x1, y1, z1, out xo, out yo, out zo);
                vx += bbbb * xo;
                vy += bbbb * yo;
                vz += bbbb * zo;
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

            seed += 1293373;
        }

        xr += vx * warpAmp;
        yr += vy * warpAmp;
        zr += vz * warpAmp;
    }
    private static TransformType3D GetWarpTransformType3D(Type warpType, RotationType3D rotationType)
    {
        switch (rotationType)
        {
            case RotationType3D.ImproveXYPlanes:
                return TransformType3D.ImproveXYPlanes;

            case RotationType3D.ImproveXZPlanes:
                return TransformType3D.ImproveXZPlanes;

            default:
                switch (warpType)
                {
                    case Type.OpenSimplex2:
                    case Type.OpenSimplex2Reduced:
                        return TransformType3D.DefaultOpenSimplex2;
                    default:
                        return TransformType3D.None;
                }
        }
    }

}
