using System;

namespace Kyvos.Maths;
public static class Mathi 
{
    public static int Digits( int n )
             => n == 0 ? 1 : (int)Math.Floor( Math.Log10( Math.Abs( n ) ) + 1 );
}

