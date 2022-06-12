//Adapted From: 
//
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

// Switch between using floats or doubles for input position


namespace Kyvos.Maths.NoiseFunctions;
public struct VornoiNoiseGenerator : INoiseGenerator
{
    public const float DefaultJitterModifier = 1.0f;
    public const VornoiDistanceFunction DefaultDistanceFunction = VornoiDistanceFunction.EuclideanSq;
    public const VornoiReturnType DefaultReturnType = VornoiReturnType.Distance;

    public float VornoiJitterModifier;
    public VornoiDistanceFunction VornoiDistanceFunction;
    public VornoiReturnType VornoiReturnType;

    public VornoiNoiseGenerator(float vornoiJitterModifier = DefaultJitterModifier, VornoiDistanceFunction distFunction = DefaultDistanceFunction, VornoiReturnType returnType = DefaultReturnType)
    {
        this.VornoiJitterModifier = vornoiJitterModifier;
        this.VornoiDistanceFunction = distFunction;
        this.VornoiReturnType = returnType;
    }

    public NoiseType Type => NoiseType.Vornoi;

    /// <summary>
    /// 2D noise at given position using current settings, with frequency coord transform 
    /// </summary>
    /// <returns>
    /// Noise output bounded between -1...1
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveOptimization)]
    public float GetNoise(float x, float y, int seed = NoiseHelpers.DefaultSeed, float frequency = NoiseHelpers.DefaultFrequency)
    {
        NoiseHelpers.TransformNoiseCoordinate(ref x, ref y, frequency);

        return GetNoise(seed, x, y);
    }

    /// <summary>
    /// 3D noise at given position using current settings, with frequency and rotation coord transform 
    /// </summary>
    /// <returns>
    /// Noise output bounded between -1...1
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveOptimization)]
    public float GetNoise(float x, float y, float z, int seed = NoiseHelpers.DefaultSeed, float frequency = NoiseHelpers.DefaultFrequency, RotationType3D rotationType = NoiseHelpers.DefaultRotationType)
    {
        NoiseHelpers.TransformNoiseCoordinate(ref x, ref y, ref z, frequency, NoiseHelpers.GetTransformType(rotationType, NoiseType.Vornoi));

        return GetNoise(seed, x, y, z);
    }

    /// <summary>
    /// 2D noise at given position using current settings, without frequency coord transform 
    /// </summary>
    /// <returns>
    /// Noise output bounded between -1...1
    /// </returns>
    public float GetNoise(int seed, float x, float y)
    {
        int xr = Mathf.RoundToInt(x);
        int yr = Mathf.RoundToInt(y);

        float distance0 = float.MaxValue;
        float distance1 = float.MaxValue;
        int closestHash = 0;

        float cellularJitter = 0.43701595f * VornoiJitterModifier;

        int xPrimed = (xr - 1) * NoiseHelpers.PrimeX;
        int yPrimedBase = (yr - 1) * NoiseHelpers.PrimeY;

        switch (VornoiDistanceFunction)
        {
            default:
            case VornoiDistanceFunction.Euclidean:
            case VornoiDistanceFunction.EuclideanSq:
                for (int xi = xr - 1; xi <= xr + 1; xi++)
                {
                    int yPrimed = yPrimedBase;

                    for (int yi = yr - 1; yi <= yr + 1; yi++)
                    {
                        int hash = NoiseHelpers.Hash(seed, xPrimed, yPrimed);
                        int idx = hash & (255 << 1);

                        float vecX = (float)(xi - x) + NoiseHelpers.RandVecs2D[idx] * cellularJitter;
                        float vecY = (float)(yi - y) + NoiseHelpers.RandVecs2D[idx | 1] * cellularJitter;

                        float newDistance = vecX * vecX + vecY * vecY;

                        distance1 = MathF.Max(MathF.Min(distance1, newDistance), distance0);
                        if (newDistance < distance0)
                        {
                            distance0 = newDistance;
                            closestHash = hash;
                        }
                        yPrimed += NoiseHelpers.PrimeY;
                    }
                    xPrimed += NoiseHelpers.PrimeX;
                }
                break;
            case VornoiDistanceFunction.Manhattan:
                for (int xi = xr - 1; xi <= xr + 1; xi++)
                {
                    int yPrimed = yPrimedBase;

                    for (int yi = yr - 1; yi <= yr + 1; yi++)
                    {
                        int hash = NoiseHelpers.Hash(seed, xPrimed, yPrimed);
                        int idx = hash & (255 << 1);

                        float vecX = (float)(xi - x) + NoiseHelpers.RandVecs2D[idx] * cellularJitter;
                        float vecY = (float)(yi - y) + NoiseHelpers.RandVecs2D[idx | 1] * cellularJitter;

                        float newDistance = MathF.Abs(vecX) + MathF.Abs(vecY);

                        distance1 = MathF.Max(MathF.Min(distance1, newDistance), distance0);
                        if (newDistance < distance0)
                        {
                            distance0 = newDistance;
                            closestHash = hash;
                        }
                        yPrimed += NoiseHelpers.PrimeY;
                    }
                    xPrimed += NoiseHelpers.PrimeX;
                }
                break;
            case VornoiDistanceFunction.Hybrid:
                for (int xi = xr - 1; xi <= xr + 1; xi++)
                {
                    int yPrimed = yPrimedBase;

                    for (int yi = yr - 1; yi <= yr + 1; yi++)
                    {
                        int hash = NoiseHelpers.Hash(seed, xPrimed, yPrimed);
                        int idx = hash & (255 << 1);

                        float vecX = (float)(xi - x) + NoiseHelpers.RandVecs2D[idx] * cellularJitter;
                        float vecY = (float)(yi - y) + NoiseHelpers.RandVecs2D[idx | 1] * cellularJitter;

                        float newDistance = (MathF.Abs(vecX) + MathF.Abs(vecY)) + (vecX * vecX + vecY * vecY);

                        distance1 = MathF.Max(MathF.Min(distance1, newDistance), distance0);
                        if (newDistance < distance0)
                        {
                            distance0 = newDistance;
                            closestHash = hash;
                        }
                        yPrimed += NoiseHelpers.PrimeY;
                    }
                    xPrimed += NoiseHelpers.PrimeX;
                }
                break;
        }
        if (VornoiDistanceFunction == VornoiDistanceFunction.Euclidean && VornoiReturnType >= VornoiReturnType.Distance)
        {
            distance0 = MathF.Sqrt(distance0);

            if (VornoiReturnType >= VornoiReturnType.Distance2)
            {
                distance1 = MathF.Sqrt(distance1);
            }
        }

        switch (VornoiReturnType)
        {
            case VornoiReturnType.CellValue:
                return closestHash * (1 / 2147483648.0f);
            case VornoiReturnType.Distance:
                return distance0 - 1;
            case VornoiReturnType.Distance2:
                return distance1 - 1;
            case VornoiReturnType.Distance2Add:
                return (distance1 + distance0) * 0.5f - 1;
            case VornoiReturnType.Distance2Sub:
                return distance1 - distance0 - 1;
            case VornoiReturnType.Distance2Mul:
                return distance1 * distance0 * 0.5f - 1;
            case VornoiReturnType.Distance2Div:
                return distance0 / distance1 - 1;
            default:
                return 0;
        }
    }

    /// <summary>
    /// 3D noise at given position using current settings, without frequency and rotation coord transform
    /// </summary>
    /// <returns>
    /// Noise output bounded between -1...1
    /// </returns>
    public float GetNoise(int seed, float x, float y, float z)
    {
        int xr = Mathf.RoundToInt(x);
        int yr = Mathf.RoundToInt(y);
        int zr = Mathf.RoundToInt(z);

        float distance0 = float.MaxValue;
        float distance1 = float.MaxValue;
        int closestHash = 0;

        float cellularJitter = 0.39614353f * VornoiJitterModifier;

        int xPrimed = (xr - 1) * NoiseHelpers.PrimeX;
        int yPrimedBase = (yr - 1) * NoiseHelpers.PrimeY;
        int zPrimedBase = (zr - 1) * NoiseHelpers.PrimeZ;

        switch (VornoiDistanceFunction)
        {
            case VornoiDistanceFunction.Euclidean:
            case VornoiDistanceFunction.EuclideanSq:
                for (int xi = xr - 1; xi <= xr + 1; xi++)
                {
                    int yPrimed = yPrimedBase;

                    for (int yi = yr - 1; yi <= yr + 1; yi++)
                    {
                        int zPrimed = zPrimedBase;

                        for (int zi = zr - 1; zi <= zr + 1; zi++)
                        {
                            int hash = NoiseHelpers.Hash(seed, xPrimed, yPrimed, zPrimed);
                            int idx = hash & (255 << 2);

                            float vecX = (float)(xi - x) + NoiseHelpers.RandVecs3D[idx] * cellularJitter;
                            float vecY = (float)(yi - y) + NoiseHelpers.RandVecs3D[idx | 1] * cellularJitter;
                            float vecZ = (float)(zi - z) + NoiseHelpers.RandVecs3D[idx | 2] * cellularJitter;

                            float newDistance = vecX * vecX + vecY * vecY + vecZ * vecZ;

                            distance1 = MathF.Max(MathF.Min(distance1, newDistance), distance0);
                            if (newDistance < distance0)
                            {
                                distance0 = newDistance;
                                closestHash = hash;
                            }
                            zPrimed += NoiseHelpers.PrimeZ;
                        }
                        yPrimed += NoiseHelpers.PrimeY;
                    }
                    xPrimed += NoiseHelpers.PrimeX;
                }
                break;
            case VornoiDistanceFunction.Manhattan:
                for (int xi = xr - 1; xi <= xr + 1; xi++)
                {
                    int yPrimed = yPrimedBase;

                    for (int yi = yr - 1; yi <= yr + 1; yi++)
                    {
                        int zPrimed = zPrimedBase;

                        for (int zi = zr - 1; zi <= zr + 1; zi++)
                        {
                            int hash = NoiseHelpers.Hash(seed, xPrimed, yPrimed, zPrimed);
                            int idx = hash & (255 << 2);

                            float vecX = (float)(xi - x) + NoiseHelpers.RandVecs3D[idx] * cellularJitter;
                            float vecY = (float)(yi - y) + NoiseHelpers.RandVecs3D[idx | 1] * cellularJitter;
                            float vecZ = (float)(zi - z) + NoiseHelpers.RandVecs3D[idx | 2] * cellularJitter;

                            float newDistance = MathF.Abs(vecX) + MathF.Abs(vecY) + MathF.Abs(vecZ);

                            distance1 = MathF.Max(MathF.Min(distance1, newDistance), distance0);
                            if (newDistance < distance0)
                            {
                                distance0 = newDistance;
                                closestHash = hash;
                            }
                            zPrimed += NoiseHelpers.PrimeZ;
                        }
                        yPrimed += NoiseHelpers.PrimeY;
                    }
                    xPrimed += NoiseHelpers.PrimeX;
                }
                break;
            case VornoiDistanceFunction.Hybrid:
                for (int xi = xr - 1; xi <= xr + 1; xi++)
                {
                    int yPrimed = yPrimedBase;

                    for (int yi = yr - 1; yi <= yr + 1; yi++)
                    {
                        int zPrimed = zPrimedBase;

                        for (int zi = zr - 1; zi <= zr + 1; zi++)
                        {
                            int hash = NoiseHelpers.Hash(seed, xPrimed, yPrimed, zPrimed);
                            int idx = hash & (255 << 2);

                            float vecX = (float)(xi - x) + NoiseHelpers.RandVecs3D[idx] * cellularJitter;
                            float vecY = (float)(yi - y) + NoiseHelpers.RandVecs3D[idx | 1] * cellularJitter;
                            float vecZ = (float)(zi - z) + NoiseHelpers.RandVecs3D[idx | 2] * cellularJitter;

                            float newDistance = (MathF.Abs(vecX) + MathF.Abs(vecY) + MathF.Abs(vecZ)) + (vecX * vecX + vecY * vecY + vecZ * vecZ);

                            distance1 = MathF.Max(MathF.Min(distance1, newDistance), distance0);
                            if (newDistance < distance0)
                            {
                                distance0 = newDistance;
                                closestHash = hash;
                            }
                            zPrimed += NoiseHelpers.PrimeZ;
                        }
                        yPrimed += NoiseHelpers.PrimeY;
                    }
                    xPrimed += NoiseHelpers.PrimeX;
                }
                break;
            default:
                break;
        }

        if (VornoiDistanceFunction == VornoiDistanceFunction.Euclidean && VornoiReturnType >= VornoiReturnType.Distance)
        {
            distance0 = MathF.Sqrt(distance0);

            if (VornoiReturnType >= VornoiReturnType.Distance2)
            {
                distance1 = MathF.Sqrt(distance1);
            }
        }

        switch (VornoiReturnType)
        {
            case VornoiReturnType.CellValue:
                return closestHash * (1 / 2147483648.0f);
            case VornoiReturnType.Distance:
                return distance0 - 1;
            case VornoiReturnType.Distance2:
                return distance1 - 1;
            case VornoiReturnType.Distance2Add:
                return (distance1 + distance0) * 0.5f - 1;
            case VornoiReturnType.Distance2Sub:
                return distance1 - distance0 - 1;
            case VornoiReturnType.Distance2Mul:
                return distance1 * distance0 * 0.5f - 1;
            case VornoiReturnType.Distance2Div:
                return distance0 / distance1 - 1;
            default:
                return 0;
        }
    }

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
        NoiseHelpers.TransformNoiseCoordinate(ref x, ref y, ref y, frequency, NoiseHelpers.GetTransformType(rotationType, NoiseType.Vornoi));
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


    public readonly static VornoiNoiseGenerator Default = new(DefaultJitterModifier, DefaultDistanceFunction, DefaultReturnType);

}


