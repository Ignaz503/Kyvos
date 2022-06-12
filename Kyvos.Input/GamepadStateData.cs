using System;
using System.Diagnostics;
using System.Text;

namespace Kyvos.Input;


internal struct GamepadStateData : IEquatable<GamepadStateData>, IComparable, IComparable<GamepadStateData>
{
    short state;

    public GamepadStateData()
    {
        state = 0;
    }

    public bool this[GamepadButton button]
    {
        get
        {
            return ((state >> (byte)button) & 1) == 1;
        }
        internal set
        {
            if (value)
            {
                state |= (short)(1 << (byte)button);
            }
            else
            {
                state &= (short)~(1 << (byte)button);
            }
        }
    }

    public void Clear()
    {
        state = 0;
    }

    public void CopyTo(ref GamepadStateData other)
    {
        other.state = state;
    }


    public bool Equals(GamepadStateData other)
        => other.state == state;


    public override bool Equals(object? obj)
    {
        return obj is GamepadStateData && Equals((GamepadStateData)obj);
    }


    public override string ToString()
    {
        return ToBitStringShort(state);
        //what fucking abominations are those to bit functions
        string ToBitStringShort(short b)
        {
            StringBuilder builder = new();

            for (int i = 0; i < 16; i++)
            {
                builder.Insert(0, ((b % 2) == 0 ? "0" : "1"));
                b = (short)(b >> 1);
            }

            return builder.ToString();
        } 
    }

    public override int GetHashCode()
        => HashCode.Combine(state);

    public int CompareTo(object? obj)
    {
        if (obj is null)
            return 1;
        if (obj is GamepadStateData ohter)
            return CompareTo(ohter);
        throw new ArgumentException($"Must be {typeof(GamepadStateData)}");
    }

    public int CompareTo(GamepadStateData other)
    {
        return state.CompareTo(other.state);
    }
        
    public static GamepadStateData Default => new();

    public static bool operator <(GamepadStateData left, GamepadStateData right)
    {
        return left.CompareTo(right) < 0;
    }

    public static bool operator <=(GamepadStateData left, GamepadStateData right)
    {
        return left.CompareTo(right) <= 0;
    }

    public static bool operator >(GamepadStateData left, GamepadStateData right)
    {
        return left.CompareTo(right) > 0;
    }

    public static bool operator >=(GamepadStateData left, GamepadStateData right)
    {
        return left.CompareTo(right) >= 0;
    }

    public static bool operator ==(GamepadStateData left, GamepadStateData right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(GamepadStateData left, GamepadStateData right)
    {
        return !left.Equals(right);
    }

}


