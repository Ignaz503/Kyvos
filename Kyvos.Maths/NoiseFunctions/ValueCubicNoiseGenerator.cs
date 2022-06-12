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
public struct ValueCubicNoiseGenerator : INoiseGenerator
{
    public NoiseType Type => NoiseType.ValueCubic;

    public Vector2 GetDerivative(float x, float y, int seed = NoiseHelpers.DefaultSeed, float frequency = NoiseHelpers.DefaultFrequency)
    {
        NoiseHelpers.TransformNoiseCoordinate(ref x, ref y, frequency);
        return GetDerivative(seed, x, y);
    }

    public Vector2 GetDerivative(int seed, float x, float y)
    {
        int x1 = Mathf.FloorToInt(x);
        int y1 = Mathf.FloorToInt(y);

        float xs = (float)(x - x1);
        float ys = (float)(y - y1);

        x1 *= NoiseHelpers.PrimeX;
        y1 *= NoiseHelpers.PrimeY;
        int x0 = x1 - NoiseHelpers.PrimeX;
        int y0 = y1 - NoiseHelpers.PrimeY;
        int x2 = x1 + NoiseHelpers.PrimeX;
        int y2 = y1 + NoiseHelpers.PrimeY;
        int x3 = x1 + unchecked(NoiseHelpers.PrimeX * 2);
        int y3 = y1 + unchecked(NoiseHelpers.PrimeY * 2);

        float k0 = NoiseHelpers.ValCoord(seed, x0, y0);
        float k1 = NoiseHelpers.ValCoord(seed, x1, y0);
        float k2 = NoiseHelpers.ValCoord(seed, x2, y0);
        float k3 = NoiseHelpers.ValCoord(seed, x3, y0);
        float k4 = NoiseHelpers.ValCoord(seed, x0, y1);
        float k5 = NoiseHelpers.ValCoord(seed, x1, y1);
        float k6 = NoiseHelpers.ValCoord(seed, x2, y1);
        float k7 = NoiseHelpers.ValCoord(seed, x3, y1);
        float k8 = NoiseHelpers.ValCoord(seed, x0, y2);
        float k9 = NoiseHelpers.ValCoord(seed, x1, y2);
        float k10 = NoiseHelpers.ValCoord(seed, x2, y2);
        float k11 = NoiseHelpers.ValCoord(seed, x3, y2);
        float k12 = NoiseHelpers.ValCoord(seed, x0, y3);
        float k13 = NoiseHelpers.ValCoord(seed, x1, y3);
        float k14 = NoiseHelpers.ValCoord(seed, x2, y3);
        float k15 = NoiseHelpers.ValCoord(seed, x3, y3);


        var val0 = NoiseHelpers.CubicLerp(k0, k1, k2, k3, xs);
        var val1 = NoiseHelpers.CubicLerp(k4, k5, k6, k7, xs);
        var val2 = NoiseHelpers.CubicLerp(k8, k9, k10, k11, xs);
        var val3 = NoiseHelpers.CubicLerp(k12, k13, k14, k15, xs);

        float xSquared = xs * xs;
        //val0 = k3*xs^3 - k2*xs^3 - k0*xs^3 + k1*xs^3 + 2*k0*xs^2 - 2*k1*xs^2 - k3*xs^2 + k2*xs^2 + k2*xs - k0*xs + k1
        float dval0 = 3f * k3 * xSquared - 3f * k2 * xSquared - 3f * k0 * xSquared + 3f * k1 * xSquared + 4f * k0 * xs - 4f * k1 * xs - 2f * k3 * xs + 2f * k2 * xs + k2 - k0;

        //val1 = k7*xs^3 - k6*xs^3 - k4*xs^3 + k5*xs^3 + 2*k4*xs^2 - 2*k5*xs^2 - k7*xs^2 + k6*xs^2 + k6*xs - k4*xs + k5
        float dval1 = 3f * k7 * xSquared - 3f * k6 * xSquared - 3f * k4 * xSquared + 3f * k5 * xSquared + 4f * k4 * xs - 4f * k5 * xs - 2f * k7 * xs + 2f * k6 * xs + k6 - k4;

        //val2 = k11*xs^3 - k10*xs^3 - k8*xs^3 + k9*xs^3 + 2*k8*xs^2 - 2*k9*xs^2 - k11*xs^2 + k10*xs^2 + k10*xs - k8*xs + k9
        float dval2 = 3f * k11 * xSquared - 3f * k10 * xSquared - 3f * k8 * xSquared + 3f * k9 * xSquared + 4f * k8 * xs - 4f * k9 * xs - 2f * k11 * xs + 2f * k10 * xs + k10 - k8;

        //val3 = k15*xs^3 - k14*xs^3 - k12*xs^3 + k13*xs^3 + 2*k12*xs^2 - 2*k13*xs^2 - k15*xs^2 + k14*xs^2 + k14*xs - k12*xs + k13
        float dval3 = 3f * k15 * xSquared - 3f * k14 * xSquared - 3f * k12 * xSquared + 3f * k13 * xSquared + 4f * k12 * xs - 4f * k13 * xs - 2f * k15 * xs + 2f * k14 * xs + k14 - k12;

        float ySquared = ys * ys;
        float yCubed = ySquared * ys;
        //val final = val3*ys^3 - val2*ys^3 - val0*ys^3 + val1*ys^3 + 2*val0*ys^2 - 2*val1*ys^2 - val3*ys^2 + val2*ys^2 + val2*ys - val0*ys + val1

        float derivXs = (dval3) * yCubed - (dval2) * yCubed - (dval0) * yCubed + (dval1) * yCubed + 2f * (dval0) * ySquared - 2f * (dval1) * ySquared - (dval3) * ySquared + (dval2) * ySquared + (dval2) * ys - (dval0) * ys + (dval1);
        float derivYs = 3f * val3 * ySquared - 3f * val2 * ySquared - 3f * val0 * ySquared + 3f * val1 * ySquared + 4f * val0 * ys - 4f * val1 * ys - 2f * val3 * ys + 2f * val2 * ys + val2 - val0;

        return new Vector2(derivXs, derivYs) * (1 / (1.5f * 1.5f));
    }

    public Vector3 GetDerivative(float x, float y, float z, int seed = NoiseHelpers.DefaultSeed, float frequency = NoiseHelpers.DefaultFrequency, RotationType3D rotationType = NoiseHelpers.DefaultRotationType)
    {
        NoiseHelpers.TransformNoiseCoordinate(ref x, ref y, ref z, frequency, NoiseHelpers.GetTransformType(rotationType, NoiseType.ValueCubic));
        return GetDerivative(seed, x, y, z);
    }

    public Vector3 GetDerivative(int seed, float x, float y, float z)
    {
        int x1 = Mathf.FloorToInt(x);
        int y1 = Mathf.FloorToInt(y);
        int z1 = Mathf.FloorToInt(z);

        float xs = (float)(x - x1);
        float ys = (float)(y - y1);
        float zs = (float)(z - z1);

        x1 *= NoiseHelpers.PrimeX;
        y1 *= NoiseHelpers.PrimeY;
        z1 *= NoiseHelpers.PrimeZ;

        int x0 = x1 - NoiseHelpers.PrimeX;
        int y0 = y1 - NoiseHelpers.PrimeY;
        int z0 = z1 - NoiseHelpers.PrimeZ;
        int x2 = x1 + NoiseHelpers.PrimeX;
        int y2 = y1 + NoiseHelpers.PrimeY;
        int z2 = z1 + NoiseHelpers.PrimeZ;
        int x3 = x1 + unchecked(NoiseHelpers.PrimeX * 2);
        int y3 = y1 + unchecked(NoiseHelpers.PrimeY * 2);
        int z3 = z1 + unchecked(NoiseHelpers.PrimeZ * 2);

        float xSquared = xs * xs;
        float ySquared = ys * ys;
        float yCubed = ySquared * ys;
        float zSquared = zs * zs;
        float zCubed = zSquared * zs;

        var k00 = NoiseHelpers.ValCoord(seed, x0, y0, z0);
        var k01 = NoiseHelpers.ValCoord(seed, x1, y0, z0);
        var k02 = NoiseHelpers.ValCoord(seed, x2, y0, z0);
        var k03 = NoiseHelpers.ValCoord(seed, x3, y0, z0);
        var k04 = NoiseHelpers.ValCoord(seed, x0, y1, z0);
        var k05 = NoiseHelpers.ValCoord(seed, x1, y1, z0);
        var k06 = NoiseHelpers.ValCoord(seed, x2, y1, z0);
        var k07 = NoiseHelpers.ValCoord(seed, x3, y1, z0);
        var k08 = NoiseHelpers.ValCoord(seed, x0, y2, z0);
        var k09 = NoiseHelpers.ValCoord(seed, x1, y2, z0);
        var k010 = NoiseHelpers.ValCoord(seed, x2, y2, z0);
        var k011 = NoiseHelpers.ValCoord(seed, x3, y2, z0);
        var k012 = NoiseHelpers.ValCoord(seed, x0, y3, z0);
        var k013 = NoiseHelpers.ValCoord(seed, x1, y3, z0);
        var k014 = NoiseHelpers.ValCoord(seed, x2, y3, z0);
        var k015 = NoiseHelpers.ValCoord(seed, x3, y3, z0);

        //val00 = k03*xs^3 - k02*xs^3 - k00*xs^3 + k01*xs^3 + 2*k00*xs^2 - 2*k01*xs^2 - k03*xs^2 + k02*xs^2 + k02*xs - k00*xs + k01
        //dval00 = 3*k03*xs^2 - 3*k02*xs^2 - 3*k00*xs^2 + 3*k01*xs^2 + 4*k00*xs - 4*k01*xs - 2*k03*xs + 2*k02*xs + k02 - k00
        var val00 = NoiseHelpers.CubicLerp(k00, k01, k02, k03, xs);
        var dval00 = 3 * k03 * xSquared - 3 * k02 * xSquared - 3 * k00 * xSquared + 3 * k01 * xSquared + 4 * k00 * xs - 4 * k01 * xs - 2 * k03 * xs + 2 * k02 * xs + k02 - k00;

        //val01 = k07*xs^3 - k06*xs^3 - k04*xs^3 + k05*xs^3 + 2*k04*xs^2 - 2*k05*xs^2 - k07*xs^2 + k06*xs^2 + k06*xs - k04*xs + k05
        //dval01 = 3*k07*xs^2 - 3*k06*xs^2 - 3*k04*xs^2 + k05*xs^2 + 4*k04*xs - 4*k05*xs - 2*k07*xs + 2*k06*xs + k06 - k04
        var val01 = NoiseHelpers.CubicLerp(k04, k05, k06, k07, xs);
        var dval01 = 3 * k07 * xSquared - 3 * k06 * xSquared - 3 * k04 * xSquared + k05 * xSquared + 4 * k04 * xs - 4 * k05 * xs - 2 * k07 * xs + 2 * k06 * xs + k06 - k04;

        //val02 = k011*xs^3 - k010*xs^3 - k08*xs^3 + k09*xs^3 + 2*k08*xs^2 - 2*k09*xs^2 - k011*xs^2 + k010*xs^2 + k010*xs - k08*xs + k09
        //dval02 = 3*k011*xs^2 - 3*k010*xs^2 - 3*k08*xs^2 + 3*k09*xs^2 + 4*k08*xs - 4*k09*xs - 2*k011*xs + 2*k010*xs + k010 - k08
        var val02 = NoiseHelpers.CubicLerp(k08, k09, k010, k011, xs);
        var dval02 = 3 * k011 * xSquared - 3 * k010 * xSquared - 3 * k08 * xSquared + 3 * k09 * xSquared + 4 * k08 * xs - 4 * k09 * xs - 2 * k011 * xs + 2 * k010 * xs + k010 - k08;

        //val03 = k015*xs^3 - k014*xs^3 - k012*xs^3 + k013*xs^3 + 2*k012*xs^2 - 2*k013*xs^2 - k015*xs^2 + k014*xs^2 + k014*xs - k012*xs + k013
        //dval03 = 3*k015*xs^2 - 3*k014*xs^2 - 3*k012*xs^2 + 3*k013*xs^2 + 4*k012*xs - 4*k013*xs - 2*k015*xs + 2*k014*xs + k014 - k012;
        var val03 = NoiseHelpers.CubicLerp(k012, k013, k014, k015, xs);
        var dval03 = 3 * k015 * xSquared - 3 * k014 * xSquared - 3 * k012 * xSquared + 3 * k013 * xSquared + 4 * k012 * xs - 4 * k013 * xs - 2 * k015 * xs + 2 * k014 * xs + k014 - k012;

        //val0 = val03*ys^3 - val02*ys^3 - val00*ys^3 + val01*ys^3 + 2*val00*ys^2 - 2*val01*ys^2 - val03*ys^2 + val02*ys^2 + val02*ys - val00*ys + val01
        //dval0 = 3*val03*ys^2 - 3*val02*ys^2 - 3*val00*ys^2 + 3*val01*ys^2 + 4*val00*ys - 4*val01*ys - 2*val03*ys + 2*val02*ys + val02 - val00
        var val0 = NoiseHelpers.CubicLerp(val00, val01, val02, val03, ys);
        var dval0 = 3 * val03 * ySquared - 3 * val02 * ySquared - 3 * val00 * ySquared + 3 * val01 * ySquared + 4 * val00 * ys - 4 * val01 * ys - 2 * val03 * ys + 2 * val02 * ys + val02 - val00;

        var k10 = NoiseHelpers.ValCoord(seed, x0, y0, z1);
        var k11 = NoiseHelpers.ValCoord(seed, x1, y0, z1);
        var k12 = NoiseHelpers.ValCoord(seed, x2, y0, z1);
        var k13 = NoiseHelpers.ValCoord(seed, x3, y0, z1);
        var k14 = NoiseHelpers.ValCoord(seed, x0, y1, z1);
        var k15 = NoiseHelpers.ValCoord(seed, x1, y1, z1);
        var k16 = NoiseHelpers.ValCoord(seed, x2, y1, z1);
        var k17 = NoiseHelpers.ValCoord(seed, x3, y1, z1);
        var k18 = NoiseHelpers.ValCoord(seed, x0, y2, z1);
        var k19 = NoiseHelpers.ValCoord(seed, x1, y2, z1);
        var k110 = NoiseHelpers.ValCoord(seed, x2, y2, z1);
        var k111 = NoiseHelpers.ValCoord(seed, x3, y2, z1);
        var k112 = NoiseHelpers.ValCoord(seed, x0, y3, z1);
        var k113 = NoiseHelpers.ValCoord(seed, x1, y3, z1);
        var k114 = NoiseHelpers.ValCoord(seed, x2, y3, z1);
        var k115 = NoiseHelpers.ValCoord(seed, x3, y3, z1);

        //val10 = k13*xs^3 - k12*xs^3 - k10*xs^3 + k11*xs^3 + 2*k10*xs^2 - 2*k11*xs^2 - k13*xs^2 + k12*xs^2 + k12*xs - k10*xs + k11
        //dval10 = 3*k13*xs^2 - 3*k12*xs^2 - 3*k10*xs^2 + 3*k11*xs^2 + 4*k10*xs - 4*k11*xs - 2*k13*xs + 2*k12*xs + k12 - k10
        var val10 = NoiseHelpers.CubicLerp(k10, k11, k12, k13, xs);
        var dval10 = 3 * k13 * xSquared - 3 * k12 * xSquared - 3 * k10 * xSquared + 3 * k11 * xSquared + 4 * k10 * xs - 4 * k11 * xs - 2 * k13 * xs + 2 * k12 * xs + k12 - k10;

        //val11 = k17*xs^3 - k16*xs^3 - k14*xs^3 + k15*xs^3 + 2*k14*xs^2 - 2*k15*xs^2 - k17*xs^2 + k16*xs^2 + k16*xs - k14*xs + k15
        //dval11 = 3*k17*xs^2 - 3*k16*xs^2 - 3*k14*xs^2 + k15*xs^2 + 4*k14*xs - 4*k15*xs - 2*k17*xs + 2*k16*xs + k16 - k14
        var val11 = NoiseHelpers.CubicLerp(k14, k15, k16, k17, xs);
        var dval11 = 3 * k17 * xSquared - 3 * k16 * xSquared - 3 * k14 * xSquared + k15 * xSquared + 4 * k14 * xs - 4 * k15 * xs - 2 * k17 * xs + 2 * k16 * xs + k16 - k14;

        //val12 = k111*xs^3 - k110*xs^3 - k18*xs^3 + k19*xs^3 + 2*k18*xs^2 - 2*k19*xs^2 - k111*xs^2 + k110*xs^2 + k110*xs - k18*xs + k19
        //dval12 = 3*k111*xs^2 - 3*k110*xs^2 - 3*k18*xs^2 + 3*k19*xs^2 + 4*k18*xs - 4*k19*xs - 2*k111*xs + 2*k110*xs + k110 - k18
        var val12 = NoiseHelpers.CubicLerp(k18, k19, k110, k111, xs);
        var dval12 = 3 * k111 * xSquared - 3 * k110 * xSquared - 3 * k18 * xSquared + 3 * k19 * xSquared + 4 * k18 * xs - 4 * k19 * xs - 2 * k111 * xs + 2 * k110 * xs + k110 - k18;

        //val13 = k115*xs^3 - k114*xs^3 - k112*xs^3 + k113*xs^3 + 2*k112*xs^2 - 2*k113*xs^2 - k115*xs^2 + k114*xs^2 + k114*xs - k112*xs + k113
        //dval13 = 3*k115*xs^2 - 3*k114*xs^2 - 3*k112*xs^2 + 3*k113*xs^2 + 4*k112*xs - 4*k113*xs - 2*k115*xs + 2*k114*xs + k114 - k112
        var val13 = NoiseHelpers.CubicLerp(k112, k113, k114, k115, xs);
        var dval13 = 3 * k115 * xSquared - 3 * k114 * xSquared - 3 * k112 * xSquared + 3 * k113 * xSquared + 4 * k112 * xs - 4 * k113 * xs - 2 * k115 * xs + 2 * k114 * xs + k114 - k112;

        //val1 = val13*ys^3 - val12*ys^3 - val10*ys^3 + val11*ys^3 + 2*val10*ys^2 - 2*val11*ys^2 - val13*ys^2 + val12*ys^2 + val12*ys - val10*ys + val11
        //dval1 = 3*val13*ys^2 - 3*val12*ys^2 - 3*val10*ys^2 + 3*val11*ys^2 + 4*val10*ys - 4*val11*ys - 2*val13*ys + 2*val12*ys + val12 - val10
        var val1 = NoiseHelpers.CubicLerp(val10, val11, val12, val13, ys);
        var dval1 = 3 * val13 * ySquared - 3 * val12 * ySquared - 3 * val10 * ySquared + 3 * val11 * ySquared + 4 * val10 * ys - 4 * val11 * ys - 2 * val13 * ys + 2 * val12 * ys + val12 - val10;

        var k20 = NoiseHelpers.ValCoord(seed, x0, y0, z2);
        var k21 = NoiseHelpers.ValCoord(seed, x1, y0, z2);
        var k22 = NoiseHelpers.ValCoord(seed, x2, y0, z2);
        var k23 = NoiseHelpers.ValCoord(seed, x3, y0, z2);
        var k24 = NoiseHelpers.ValCoord(seed, x0, y1, z2);
        var k25 = NoiseHelpers.ValCoord(seed, x1, y1, z2);
        var k26 = NoiseHelpers.ValCoord(seed, x2, y1, z2);
        var k27 = NoiseHelpers.ValCoord(seed, x3, y1, z2);
        var k28 = NoiseHelpers.ValCoord(seed, x0, y2, z2);
        var k29 = NoiseHelpers.ValCoord(seed, x1, y2, z2);
        var k210 = NoiseHelpers.ValCoord(seed, x2, y2, z2);
        var k211 = NoiseHelpers.ValCoord(seed, x3, y2, z2);
        var k212 = NoiseHelpers.ValCoord(seed, x0, y3, z2);
        var k213 = NoiseHelpers.ValCoord(seed, x1, y3, z2);
        var k214 = NoiseHelpers.ValCoord(seed, x2, y3, z2);
        var k215 = NoiseHelpers.ValCoord(seed, x3, y3, z2);

        //val20 = k23*xs^3 - k22*xs^3 - k20*xs^3 + k21*xs^3 + 2*k20*xs^2 - 2*k21*xs^2 - k23*xs^2 + k22*xs^2 + k22*xs - k20*xs + k21
        //dval20 = 3*k23*xs^2 - 3*k22*xs^2 - 3*k20*xs^2 + 3*k21*xs^2 + 4*k20*xs - 4*k21*xs - 2*k23*xs + 2*k22*xs + k22 - k20
        var val20 = NoiseHelpers.CubicLerp(k20, k21, k22, k23, xs);
        var dval20 = 3 * k23 * xSquared - 3 * k22 * xSquared - 3 * k20 * xSquared + 3 * k21 * xSquared + 4 * k20 * xs - 4 * k21 * xs - 2 * k23 * xs + 2 * k22 * xs + k22 - k20;

        //val21 = k27*xs^3 - k26*xs^3 - k24*xs^3 + k25*xs^3 + 2*k24*xs^2 - 2*k25*xs^2 - k27*xs^2 + k26*xs^2 + k26*xs - k24*xs + k25
        //dval21 = 3*k27*xs^2 - 3*k26*xs^2 - 3*k24*xs^2 + k25*xs^2 + 4*k24*xs - 4*k25*xs - 2*k27*xs + 2*k26*xs + k26 - k24
        var val21 = NoiseHelpers.CubicLerp(k24, k25, k26, k27, xs);
        var dval21 = 3 * k27 * xSquared - 3 * k26 * xSquared - 3 * k24 * xSquared + k25 * xSquared + 4 * k24 * xs - 4 * k25 * xs - 2 * k27 * xs + 2 * k26 * xs + k26 - k24;

        //val22 = k211*xs^3 - k210*xs^3 - k28*xs^3 + k29*xs^3 + 2*k28*xs^2 - 2*k29*xs^2 - k211*xs^2 + k210*xs^2 + k210*xs - k28*xs + k29
        //dval22 = 3*k211*xs^2 - 3*k210*xs^2 - 3*k28*xs^2 + 3*k29*xs^2 + 4*k28*xs - 4*k29*xs - 2*k211*xs + 2*k210*xs + k210 - k28
        var val22 = NoiseHelpers.CubicLerp(k28, k29, k210, k211, xs);
        var dval22 = 3 * k211 * xSquared - 3 * k210 * xSquared - 3 * k28 * xSquared + 3 * k29 * xSquared + 4 * k28 * xs - 4 * k29 * xs - 2 * k211 * xs + 2 * k210 * xs + k210 - k28;

        //val23 = k215*xs^3 - k214*xs^3 - k212*xs^3 + k213*xs^3 + 2*k212*xs^2 - 2*k213*xs^2 - k215*xs^2 + k214*xs^2 + k214*xs - k212*xs + k213
        //dval23 = 3*k215*xs^2 - 3*k214*xs^2 - 3*k212*xs^2 + 3*k213*xs^2 + 4*k212*xs - 4*k213*xs - 2*k215*xs + 2*k214*xs + k214 - k212
        var val23 = NoiseHelpers.CubicLerp(k212, k213, k214, k215, xs);
        var dval23 = 3 * k215 * xSquared - 3 * k214 * xSquared - 3 * k212 * xSquared + 3 * k213 * xSquared + 4 * k212 * xs - 4 * k213 * xs - 2 * k215 * xs + 2 * k214 * xs + k214 - k212;

        //val2 = val23*ys^3 - val22*ys^3 - val20*ys^3 + val21*ys^3 + 2*val20*ys^2 - 2*val21*ys^2 - val23*ys^2 + val22*ys^2 + val22*ys - val20*ys + val21
        //dval2 = 3*val23*ys^2 - 3*val22*ys^2 - 3*val20*ys^2 + 3*val21*ys^2 + 4*val20*ys - 4*val21*ys - 2*val23*ys + 2*val22*ys + val22 - val20
        var val2 = NoiseHelpers.CubicLerp(val20, val21, val22, val23, ys);
        var dval2 = 3 * val23 * ySquared - 3 * val22 * ySquared - 3 * val20 * ySquared + 3 * val21 * ySquared + 4 * val20 * ys - 4 * val21 * ys - 2 * val23 * ys + 2 * val22 * ys + val22 - val20;

        var k30 = NoiseHelpers.ValCoord(seed, x0, y0, z3);
        var k31 = NoiseHelpers.ValCoord(seed, x1, y0, z3);
        var k32 = NoiseHelpers.ValCoord(seed, x2, y0, z3);
        var k33 = NoiseHelpers.ValCoord(seed, x3, y0, z3);
        var k34 = NoiseHelpers.ValCoord(seed, x0, y1, z3);
        var k35 = NoiseHelpers.ValCoord(seed, x1, y1, z3);
        var k36 = NoiseHelpers.ValCoord(seed, x2, y1, z3);
        var k37 = NoiseHelpers.ValCoord(seed, x3, y1, z3);
        var k38 = NoiseHelpers.ValCoord(seed, x0, y2, z3);
        var k39 = NoiseHelpers.ValCoord(seed, x1, y2, z3);
        var k310 = NoiseHelpers.ValCoord(seed, x2, y2, z3);
        var k311 = NoiseHelpers.ValCoord(seed, x3, y2, z3);
        var k312 = NoiseHelpers.ValCoord(seed, x0, y3, z3);
        var k313 = NoiseHelpers.ValCoord(seed, x1, y3, z3);
        var k314 = NoiseHelpers.ValCoord(seed, x2, y3, z3);
        var k315 = NoiseHelpers.ValCoord(seed, x3, y3, z3);

        //val30 = k33*xs^3 - k32*xs^3 - k30*xs^3 + k31*xs^3 + 2*k30*xs^2 - 2*k31*xs^2 - k33*xs^2 + k32*xs^2 + k32*xs - k30*xs + k31
        //dval30 = 3*k33*xs^2 - 3*k32*xs^2 - 3*k30*xs^2 + 3*k31*xs^2 + 4*k30*xs - 4*k31*xs - 2*k33*xs + 2*k32*xs + k32 - k30
        var val30 = NoiseHelpers.CubicLerp(k30, k31, k32, k33, xs);
        var dval30 = 3 * k33 * xSquared - 3 * k32 * xSquared - 3 * k30 * xSquared + 3 * k31 * xSquared + 4 * k30 * xs - 4 * k31 * xs - 2 * k33 * xs + 2 * k32 * xs + k32 - k30;

        //val31 = k37*xs^3 - k36*xs^3 - k34*xs^3 + k35*xs^3 + 2*k34*xs^2 - 2*k35*xs^2 - k37*xs^2 + k36*xs^2 + k36*xs - k34*xs + k35
        //dval31 = 3*k37*xs^2 - 3*k36*xs^2 - 3*k34*xs^2 + k35*xs^2 + 4*k34*xs - 4*k35*xs - 2*k37*xs + 2*k36*xs + k36 - k34
        var val31 = NoiseHelpers.CubicLerp(k34, k35, k36, k37, xs);
        var dval31 = 3 * k37 * xSquared - 3 * k36 * xSquared - 3 * k34 * xSquared + k35 * xSquared + 4 * k34 * xs - 4 * k35 * xs - 2 * k37 * xs + 2 * k36 * xs + k36 - k34;

        //val32 = k311*xs^3 - k310*xs^3 - k38*xs^3 + k39*xs^3 + 2*k38*xs^2 - 2*k39*xs^2 - k311*xs^2 + k310*xs^2 + k310*xs - k38*xs + k39
        //dval32 = 3*k311*xs^2 - 3*k310*xs^2 - 3*k38*xs^2 + 3*k39*xs^2 + 4*k38*xs - 4*k39*xs - 2*k311*xs + 2*k310*xs + k310 - k38
        var val32 = NoiseHelpers.CubicLerp(k38, k39, k310, k311, xs);
        var dval32 = 3 * k311 * xSquared - 3 * k310 * xSquared - 3 * k38 * xSquared + 3 * k39 * xSquared + 4 * k38 * xs - 4 * k39 * xs - 2 * k311 * xs + 2 * k310 * xs + k310 - k38;

        //val33 = k315*xs^3 - k314*xs^3 - k312*xs^3 + k313*xs^3 + 2*k312*xs^2 - 2*k313*xs^2 - k315*xs^2 + k314*xs^2 + k314*xs - k312*xs + k313
        //dval33 = 3*k315*xs^2 - 3*k314*xs^2 - 3*k312*xs^2 + 3*k313*xs^2 + 4*k312*xs - 4*k313*xs - 2*k315*xs + 2*k314*xs + k314 - k312
        var val33 = NoiseHelpers.CubicLerp(k312, k313, k314, k315, xs);
        var dval33 = 3 * k315 * xSquared - 3 * k314 * xSquared - 3 * k312 * xSquared + 3 * k313 * xSquared + 4 * k312 * xs - 4 * k313 * xs - 2 * k315 * xs + 2 * k314 * xs + k314 - k312;

        //val3 = val33*ys^3 - val32*ys^3 - val30*ys^3 + val31*ys^3 + 2*val30*ys^2 - 2*val31*ys^2 - val33*ys^2 + val32*ys^2 + val32*ys - val30*ys + val31
        //dval3 = 3*val33*ys^2 - 3*val32*ys^2 - 3*val30*ys^2 + 3*val31*ys^2 + 4*val30*ys - 4*val31*ys - 2*val33*ys + 2*val32*ys + val32 - val30
        var val3 = NoiseHelpers.CubicLerp(val30, val31, val32, val33, ys);
        var dval3 = 3 * val33 * ySquared - 3 * val32 * ySquared - 3 * val30 * ySquared + 3 * val31 * ySquared + 4 * val30 * ys - 4 * val31 * ys - 2 * val33 * ys + 2 * val32 * ys + val32 - val30;

        //valFinal = val3*zs^3 - val2*zs^3 - val0*zs^3 + val1*zs^3 + 2*val0*zs^2 - 2*val1*zs^2 - val3*zs^2 + val2*zs^2 + val2*zs - val0*zs + val1
        //valFinalY= (val3)*zs^3 - (val2)*zs^3 - (val0)*zs^3 + (val1)*zs^3 + 2*(val0)*zs^2 - 2*(val1)*zs^2 - (val3)*zs^2 + (val2)*zs^2 + (val2)*zs - (val0)*zs + (val1)

        //valFinalX= (val33*ys^3 - val32*ys^3 - val30*ys^3 + val31*ys^3 + 2*val30*ys^2 - 2*val31*ys^2 - val33*ys^2 + val32*ys^2 + val32*ys - val30*ys + val31)*zs^3 - (val23*ys^3 - val22*ys^3 - val20*ys^3 + val21*ys^3 + 2*val20*ys^2 - 2*val21*ys^2 - val23*ys^2 + val22*ys^2 + val22*ys - val20*ys + val21)*zs^3 - (val03*ys^3 - val02*ys^3 - val00*ys^3 + val01*ys^3 + 2*val00*ys^2 - 2*val01*ys^2 - val03*ys^2 + val02*ys^2 + val02*ys - val00*ys + val01)*zs^3 + (val13*ys^3 - val12*ys^3 - val10*ys^3 + val11*ys^3 + 2*val10*ys^2 - 2*val11*ys^2 - val13*ys^2 + val12*ys^2 + val12*ys - val10*ys + val11)*zs^3 + 2*(val03*ys^3 - val02*ys^3 - val00*ys^3 + val01*ys^3 + 2*val00*ys^2 - 2*val01*ys^2 - val03*ys^2 + val02*ys^2 + val02*ys - val00*ys + val01)*zs^2 - 2*(val13*ys^3 - val12*ys^3 - val10*ys^3 + val11*ys^3 + 2*val10*ys^2 - 2*val11*ys^2 - val13*ys^2 + val12*ys^2 + val12*ys - val10*ys + val11)*zs^2 - (val33*ys^3 - val32*ys^3 - val30*ys^3 + val31*ys^3 + 2*val30*ys^2 - 2*val31*ys^2 - val33*ys^2 + val32*ys^2 + val32*ys - val30*ys + val31)*zs^2 + (val23*ys^3 - val22*ys^3 - val20*ys^3 + val21*ys^3 + 2*val20*ys^2 - 2*val21*ys^2 - val23*ys^2 + val22*ys^2 + val22*ys - val20*ys + val21)*zs^2 + (val23*ys^3 - val22*ys^3 - val20*ys^3 + val21*ys^3 + 2*val20*ys^2 - 2*val21*ys^2 - val23*ys^2 + val22*ys^2 + val22*ys - val20*ys + val21)*zs - (val03*ys^3 - val02*ys^3 - val00*ys^3 + val01*ys^3 + 2*val00*ys^2 - 2*val01*ys^2 - val03*ys^2 + val02*ys^2 + val02*ys - val00*ys + val01)*zs + (val13*ys^3 - val12*ys^3 - val10*ys^3 + val11*ys^3 + 2*val10*ys^2 - 2*val11*ys^2 - val13*ys^2 + val12*ys^2 + val12*ys - val10*ys + val11)

        //derivZ = 3*val3*zs^2 - 3*val2*zs^2 - 3*val0*zs^2 + 3*val1*zs^2 + 4*val0*zs - 4*val1*zs - 2*val3*zs + 2*val2*zs + val2 - val0
        var derivZ = 3 * val3 * zSquared - 3 * val2 * zSquared - 3 * val0 * zSquared + 3 * val1 * zSquared + 4 * val0 * zs - 4 * val1 * zs - 2 * val3 * zs + 2 * val2 * zs + val2 - val0;

        //derivY= (dval3)*zs^3 - (dval2)*zs^3 - (dval0)*zs^3 + (dval1)*zs^3 + 2*(dval0)*zs^2 - 2*(dval1)*zs^2 - (dval3)*zs^2 + (dval2)*zs^2 + (dval2)*zs - (dval0)*zs + (dval1)
        var derivY = (dval3) * zCubed - (dval2) * zCubed - (dval0) * zCubed + (dval1) * zCubed + 2 * (dval0) * zSquared - 2 * (dval1) * zSquared - (dval3) * zSquared + (dval2) * zSquared + (dval2) * zs - (dval0) * zs + (dval1);


        //derivX= (dval33*ys^3 - dval32*ys^3 - dval30*ys^3 + dval31*ys^3 + 2*dval30*ys^2 - 2*dval31*ys^2 - dval33*ys^2 + dval32*ys^2 + dval32*ys - dval30*ys + dval31)*zs^3 - (dval23*ys^3 - dval22*ys^3 - dval20*ys^3 + dval21*ys^3 + 2*dval20*ys^2 - 2*dval21*ys^2 - dval23*ys^2 + dval22*ys^2 + dval22*ys - dval20*ys + dval21)*zs^3 - (dval03*ys^3 - dval02*ys^3 - dval00*ys^3 + dval01*ys^3 + 2*dval00*ys^2 - 2*dval01*ys^2 - dval03*ys^2 + dval02*ys^2 + dval02*ys - dval00*ys + dval01)*zs^3 + (dval13*ys^3 - dval12*ys^3 - dval10*ys^3 + dval11*ys^3 + 2*dval10*ys^2 - 2*dval11*ys^2 - dval13*ys^2 + dval12*ys^2 + dval12*ys - dval10*ys + dval11)*zs^3 + 2*(dval03*ys^3 - dval02*ys^3 - dval00*ys^3 + dval01*ys^3 + 2*dval00*ys^2 - 2*dval01*ys^2 - dval03*ys^2 + dval02*ys^2 + dval02*ys - dval00*ys + dval01)*zs^2 - 2*(dval13*ys^3 - dval12*ys^3 - dval10*ys^3 + dval11*ys^3 + 2*dval10*ys^2 - 2*dval11*ys^2 - dval13*ys^2 + dval12*ys^2 + dval12*ys - dval10*ys + dval11)*zs^2 - (dval33*ys^3 - dval32*ys^3 - dval30*ys^3 + dval31*ys^3 + 2*dval30*ys^2 - 2*dval31*ys^2 - dval33*ys^2 + dval32*ys^2 + dval32*ys - dval30*ys + dval31)*zs^2 + (dval23*ys^3 - dval22*ys^3 - dval20*ys^3 + dval21*ys^3 + 2*dval20*ys^2 - 2*dval21*ys^2 - dval23*ys^2 + dval22*ys^2 + dval22*ys - dval20*ys + dval21)*zs^2 + (dval23*ys^3 - dval22*ys^3 - dval20*ys^3 + dval21*ys^3 + 2*dval20*ys^2 - 2*dval21*ys^2 - dval23*ys^2 + dval22*ys^2 + dval22*ys - dval20*ys + dval21)*zs - (dval03*ys^3 - dval02*ys^3 - dval00*ys^3 + dval01*ys^3 + 2*dval00*ys^2 - 2*dval01*ys^2 - dval03*ys^2 + dval02*ys^2 + dval02*ys - dval00*ys + dval01)*zs + (dval13*ys^3 - dval12*ys^3 - dval10*ys^3 + dval11*ys^3 + 2*dval10*ys^2 - 2*dval11*ys^2 - dval13*ys^2 + dval12*ys^2 + dval12*ys - dval10*ys + dval11)
        var derivX = (dval33 * yCubed - dval32 * yCubed - dval30 * yCubed + dval31 * yCubed + 2 * dval30 * ySquared - 2 * dval31 * ySquared - dval33 * ySquared + dval32 * ySquared + dval32 * ys - dval30 * ys + dval31) * zCubed - (dval23 * yCubed - dval22 * yCubed - dval20 * yCubed + dval21 * yCubed + 2 * dval20 * ySquared - 2 * dval21 * ySquared - dval23 * ySquared + dval22 * ySquared + dval22 * ys - dval20 * ys + dval21) * zCubed - (dval03 * yCubed - dval02 * yCubed - dval00 * yCubed + dval01 * yCubed + 2 * dval00 * ySquared - 2 * dval01 * ySquared - dval03 * ySquared + dval02 * ySquared + dval02 * ys - dval00 * ys + dval01) * zCubed + (dval13 * yCubed - dval12 * yCubed - dval10 * yCubed + dval11 * yCubed + 2 * dval10 * ySquared - 2 * dval11 * ySquared - dval13 * ySquared + dval12 * ySquared + dval12 * ys - dval10 * ys + dval11) * zCubed + 2 * (dval03 * yCubed - dval02 * yCubed - dval00 * yCubed + dval01 * yCubed + 2 * dval00 * ySquared - 2 * dval01 * ySquared - dval03 * ySquared + dval02 * ySquared + dval02 * ys - dval00 * ys + dval01) * zSquared - 2 * (dval13 * yCubed - dval12 * yCubed - dval10 * yCubed + dval11 * yCubed + 2 * dval10 * ySquared - 2 * dval11 * ySquared - dval13 * ySquared + dval12 * ySquared + dval12 * ys - dval10 * ys + dval11) * zSquared - (dval33 * yCubed - dval32 * yCubed - dval30 * yCubed + dval31 * yCubed + 2 * dval30 * ySquared - 2 * dval31 * ySquared - dval33 * ySquared + dval32 * ySquared + dval32 * ys - dval30 * ys + dval31) * zSquared + (dval23 * yCubed - dval22 * yCubed - dval20 * yCubed + dval21 * yCubed + 2 * dval20 * ySquared - 2 * dval21 * ySquared - dval23 * ySquared + dval22 * ySquared + dval22 * ys - dval20 * ys + dval21) * zSquared + (dval23 * yCubed - dval22 * yCubed - dval20 * yCubed + dval21 * yCubed + 2 * dval20 * ySquared - 2 * dval21 * ySquared - dval23 * ySquared + dval22 * ySquared + dval22 * ys - dval20 * ys + dval21) * zs - (dval03 * yCubed - dval02 * yCubed - dval00 * yCubed + dval01 * yCubed + 2 * dval00 * ySquared - 2 * dval01 * ySquared - dval03 * ySquared + dval02 * ySquared + dval02 * ys - dval00 * ys + dval01) * zs + (dval13 * yCubed - dval12 * yCubed - dval10 * yCubed + dval11 * yCubed + 2 * dval10 * ySquared - 2 * dval11 * ySquared - dval13 * ySquared + dval12 * ySquared + dval12 * ys - dval10 * ys + dval11);


        return new Vector3(derivX, derivY, derivZ) * (1 / (1.5f * 1.5f * 1.5f));
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
        NoiseHelpers.TransformNoiseCoordinate(ref x, ref y, frequency);
        return GetNoise(seed, x, y);
    }

    /// <summary>
    /// 3D noise at given position using current settings, with frequency and rotation coord transformation
    /// </summary>
    /// <returns>
    /// Noise output bounded between -1...1
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public float GetNoise(float x, float y, float z, int seed = NoiseHelpers.DefaultSeed, float frequency = NoiseHelpers.DefaultFrequency, RotationType3D rotationType3D = NoiseHelpers.DefaultRotationType)
    {
        NoiseHelpers.TransformNoiseCoordinate(ref x, ref y, ref z, frequency, NoiseHelpers.GetTransformType(rotationType3D, NoiseType.ValueCubic));

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
        int x1 = Mathf.FloorToInt(x);
        int y1 = Mathf.FloorToInt(y);

        float xs = (float)(x - x1);
        float ys = (float)(y - y1);

        x1 *= NoiseHelpers.PrimeX;
        y1 *= NoiseHelpers.PrimeY;
        int x0 = x1 - NoiseHelpers.PrimeX;
        int y0 = y1 - NoiseHelpers.PrimeY;
        int x2 = x1 + NoiseHelpers.PrimeX;
        int y2 = y1 + NoiseHelpers.PrimeY;
        int x3 = x1 + unchecked(NoiseHelpers.PrimeX * 2);
        int y3 = y1 + unchecked(NoiseHelpers.PrimeY * 2);

        return NoiseHelpers.CubicLerp(
            NoiseHelpers.CubicLerp(NoiseHelpers.ValCoord(seed, x0, y0), NoiseHelpers.ValCoord(seed, x1, y0), NoiseHelpers.ValCoord(seed, x2, y0), NoiseHelpers.ValCoord(seed, x3, y0),
            xs),
            NoiseHelpers.CubicLerp(NoiseHelpers.ValCoord(seed, x0, y1), NoiseHelpers.ValCoord(seed, x1, y1), NoiseHelpers.ValCoord(seed, x2, y1), NoiseHelpers.ValCoord(seed, x3, y1),
            xs),
            NoiseHelpers.CubicLerp(NoiseHelpers.ValCoord(seed, x0, y2), NoiseHelpers.ValCoord(seed, x1, y2), NoiseHelpers.ValCoord(seed, x2, y2), NoiseHelpers.ValCoord(seed, x3, y2),
            xs),
            NoiseHelpers.CubicLerp(NoiseHelpers.ValCoord(seed, x0, y3), NoiseHelpers.ValCoord(seed, x1, y3), NoiseHelpers.ValCoord(seed, x2, y3), NoiseHelpers.ValCoord(seed, x3, y3),
            xs),
            ys) * (1 / (1.5f * 1.5f));
    }

    /// <summary>
    /// 2D noise at given position using current settings, without frequency and rotation coord transformation
    /// </summary>
    /// <returns>
    /// Noise output bounded between -1...1
    /// </returns>
    public float GetNoise(int seed, float x, float y, float z)
    {
        int x1 = Mathf.FloorToInt(x);
        int y1 = Mathf.FloorToInt(y);
        int z1 = Mathf.FloorToInt(z);

        float xs = (float)(x - x1);
        float ys = (float)(y - y1);
        float zs = (float)(z - z1);

        x1 *= NoiseHelpers.PrimeX;
        y1 *= NoiseHelpers.PrimeY;
        z1 *= NoiseHelpers.PrimeZ;

        int x0 = x1 - NoiseHelpers.PrimeX;
        int y0 = y1 - NoiseHelpers.PrimeY;
        int z0 = z1 - NoiseHelpers.PrimeZ;
        int x2 = x1 + NoiseHelpers.PrimeX;
        int y2 = y1 + NoiseHelpers.PrimeY;
        int z2 = z1 + NoiseHelpers.PrimeZ;
        int x3 = x1 + unchecked(NoiseHelpers.PrimeX * 2);
        int y3 = y1 + unchecked(NoiseHelpers.PrimeY * 2);
        int z3 = z1 + unchecked(NoiseHelpers.PrimeZ * 2);


        return NoiseHelpers.CubicLerp(
            NoiseHelpers.CubicLerp(
            NoiseHelpers.CubicLerp(NoiseHelpers.ValCoord(seed, x0, y0, z0), NoiseHelpers.ValCoord(seed, x1, y0, z0), NoiseHelpers.ValCoord(seed, x2, y0, z0), NoiseHelpers.ValCoord(seed, x3, y0, z0), xs),
            NoiseHelpers.CubicLerp(NoiseHelpers.ValCoord(seed, x0, y1, z0), NoiseHelpers.ValCoord(seed, x1, y1, z0), NoiseHelpers.ValCoord(seed, x2, y1, z0), NoiseHelpers.ValCoord(seed, x3, y1, z0), xs),
            NoiseHelpers.CubicLerp(NoiseHelpers.ValCoord(seed, x0, y2, z0), NoiseHelpers.ValCoord(seed, x1, y2, z0), NoiseHelpers.ValCoord(seed, x2, y2, z0), NoiseHelpers.ValCoord(seed, x3, y2, z0), xs),
            NoiseHelpers.CubicLerp(NoiseHelpers.ValCoord(seed, x0, y3, z0), NoiseHelpers.ValCoord(seed, x1, y3, z0), NoiseHelpers.ValCoord(seed, x2, y3, z0), NoiseHelpers.ValCoord(seed, x3, y3, z0), xs),
            ys),
            NoiseHelpers.CubicLerp(
            NoiseHelpers.CubicLerp(NoiseHelpers.ValCoord(seed, x0, y0, z1), NoiseHelpers.ValCoord(seed, x1, y0, z1), NoiseHelpers.ValCoord(seed, x2, y0, z1), NoiseHelpers.ValCoord(seed, x3, y0, z1), xs),
            NoiseHelpers.CubicLerp(NoiseHelpers.ValCoord(seed, x0, y1, z1), NoiseHelpers.ValCoord(seed, x1, y1, z1), NoiseHelpers.ValCoord(seed, x2, y1, z1), NoiseHelpers.ValCoord(seed, x3, y1, z1), xs),
            NoiseHelpers.CubicLerp(NoiseHelpers.ValCoord(seed, x0, y2, z1), NoiseHelpers.ValCoord(seed, x1, y2, z1), NoiseHelpers.ValCoord(seed, x2, y2, z1), NoiseHelpers.ValCoord(seed, x3, y2, z1), xs),
            NoiseHelpers.CubicLerp(NoiseHelpers.ValCoord(seed, x0, y3, z1), NoiseHelpers.ValCoord(seed, x1, y3, z1), NoiseHelpers.ValCoord(seed, x2, y3, z1), NoiseHelpers.ValCoord(seed, x3, y3, z1), xs),
            ys),
            NoiseHelpers.CubicLerp(
            NoiseHelpers.CubicLerp(NoiseHelpers.ValCoord(seed, x0, y0, z2), NoiseHelpers.ValCoord(seed, x1, y0, z2), NoiseHelpers.ValCoord(seed, x2, y0, z2), NoiseHelpers.ValCoord(seed, x3, y0, z2), xs),
            NoiseHelpers.CubicLerp(NoiseHelpers.ValCoord(seed, x0, y1, z2), NoiseHelpers.ValCoord(seed, x1, y1, z2), NoiseHelpers.ValCoord(seed, x2, y1, z2), NoiseHelpers.ValCoord(seed, x3, y1, z2), xs),
            NoiseHelpers.CubicLerp(NoiseHelpers.ValCoord(seed, x0, y2, z2), NoiseHelpers.ValCoord(seed, x1, y2, z2), NoiseHelpers.ValCoord(seed, x2, y2, z2), NoiseHelpers.ValCoord(seed, x3, y2, z2), xs),
            NoiseHelpers.CubicLerp(NoiseHelpers.ValCoord(seed, x0, y3, z2), NoiseHelpers.ValCoord(seed, x1, y3, z2), NoiseHelpers.ValCoord(seed, x2, y3, z2), NoiseHelpers.ValCoord(seed, x3, y3, z2), xs),
            ys),
            NoiseHelpers.CubicLerp(
            NoiseHelpers.CubicLerp(NoiseHelpers.ValCoord(seed, x0, y0, z3), NoiseHelpers.ValCoord(seed, x1, y0, z3), NoiseHelpers.ValCoord(seed, x2, y0, z3), NoiseHelpers.ValCoord(seed, x3, y0, z3), xs),
            NoiseHelpers.CubicLerp(NoiseHelpers.ValCoord(seed, x0, y1, z3), NoiseHelpers.ValCoord(seed, x1, y1, z3), NoiseHelpers.ValCoord(seed, x2, y1, z3), NoiseHelpers.ValCoord(seed, x3, y1, z3), xs),
            NoiseHelpers.CubicLerp(NoiseHelpers.ValCoord(seed, x0, y2, z3), NoiseHelpers.ValCoord(seed, x1, y2, z3), NoiseHelpers.ValCoord(seed, x2, y2, z3), NoiseHelpers.ValCoord(seed, x3, y2, z3), xs),
            NoiseHelpers.CubicLerp(NoiseHelpers.ValCoord(seed, x0, y3, z3), NoiseHelpers.ValCoord(seed, x1, y3, z3), NoiseHelpers.ValCoord(seed, x2, y3, z3), NoiseHelpers.ValCoord(seed, x3, y3, z3), xs),
            ys),
            zs) * (1 / (1.5f * 1.5f * 1.5f));
    }

}

