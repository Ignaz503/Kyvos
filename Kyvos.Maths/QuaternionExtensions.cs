using System;
using System.Numerics;

namespace Kyvos.Maths;

public static class QuaternionExtensions 
{
    public static Vector3 ToEuler(this Quaternion q) 
    {
        throw new NotImplementedException();
        //Vector3 res = new();
        ////https://en.wikipedia.org/wiki/Conversion_between_quaternions_and_Euler_angles
        //float sinr_cosp = 2f * (q.W * q.X + q.Y * q.Z);
        //float cosr_cosp = 1f - 2f * (q.X * q.X + q.Y * q.Y);

        //res.Z = MathF.Atan2(sinr_cosp, cosr_cosp);

        //float sinp = 2 * (q.W * q.Y - q.Z * q.X);

        //if (MathF.Abs(sinp) >= 1)
        //{
        //    //values out of range set to 90deg default
        //    res.X = MathF.CopySign(MathF.PI/2f,sinp);
        //}
        //else 
        //{
        //    res.X = MathF.Asin(sinp);
        //}

        //float siny_cosp = 2f * (q.W * q.Z + q.X * q.Y);
        //float cosy_cosp = 1f - 2f * (q.Y * q.Y + q.Z * q.Z);
        //res.Y = MathF.Atan2(siny_cosp, cosy_cosp);

        //return res;
    }
}