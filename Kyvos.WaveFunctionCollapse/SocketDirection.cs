namespace Kyvos.WaveFunctionCollapse;

public struct Direction : IEquatable<Direction>, IComparable<Direction>
{
    const int min = 0;
    const int max = 5;

    public static readonly Direction UP = (Direction)0;
    public static readonly Direction DOWN = (Direction)1;
    public static readonly Direction RIGHT = (Direction)2;
    public static readonly Direction LEFT = (Direction)3;
    public static readonly Direction AWAY = (Direction)4;
    public static readonly Direction TOWARDS = (Direction)5;

    public static readonly int MIN = UP.val;
    public static readonly int MAX3D = TOWARDS.val;
    public static readonly int MAX2D = LEFT.val;
   
    int val;
    public Direction()
        => val = 0;

    public Direction(uint val)
        => this.val = (int)val;
    public Direction(int val)
        => this.val = val;

    public static implicit operator int(Direction dir)
        => dir.val;

    public static explicit operator uint(Direction dir)
        => (uint)dir.val;

    public static explicit operator Direction(int dir)
        => new(dir);

    public static explicit operator Direction(uint dir)
        => new(dir);

    public override string ToString()
    => val switch
    {
        0 => nameof(UP),
        1 => nameof(DOWN),
        2 => nameof(RIGHT),
        3 => nameof(LEFT),
        4 => nameof(AWAY),
        5 => nameof(TOWARDS),
        _ => "unkown"
    };

    public bool Equals(Direction other) => val.Equals(other.val);

    public int CompareTo(Direction other) => val.CompareTo(other.val);

    public override bool Equals(object? obj)
    {
        if(obj is Direction dir)
            return Equals(dir);
        if(obj is int val)
            return val.Equals(this.val);
        return false;
    }

    public override int GetHashCode() => val.GetHashCode();

    public static bool operator ==(Direction left, Direction right) => left.Equals(right);

    public static bool operator !=(Direction left, Direction right) => !(left == right);

    public static bool operator <(Direction left, Direction right) => left.CompareTo(right) < 0;

    public static bool operator <=(Direction left, Direction right) => left.CompareTo(right) <= 0;

    public static bool operator >(Direction left, Direction right) => left.CompareTo(right) > 0;

    public static bool operator >=(Direction left, Direction right) => left.CompareTo(right) >= 0;

    public static Direction Opposite(Direction dir) 
    {
        /*
            UP(0) -> DOWN(1)
            DOWN(1) -> UP(0)
            RIGHT(2) -> LEFT(3)
            LEFT(3) -> RIGHT(2)
            AWAY(4) -> TOWRADS(5)
            TOWARDS(5) -> AWAY(4)
         */

        return (Direction)(dir.val % 2 == 0 ? dir.val + 1 : dir.val - 1);
    }
    public static Direction RotateClockwiseZAxis(Direction dir)
    {
        /*
            UP(0) -> RIGHT(2)
            DOWN(1) -> LEFT(3)

            RIGHT(2) -> DOWN(1)
            LEFT(3) -> UP(0)

            AWAY(4) -> AWAY(4)
            TOWARDS(5) -> TOWARDS(5)
         */

        if (dir >= AWAY) //front and back face don't change
            return dir;
        if (dir <= DOWN) 
            return (Direction)(dir.val + 2);

        return Opposite((Direction)((dir.val + 2 + 1) % MAX3D));
    }

    public static Direction RotateCounterClockwiseZAxis(Direction dir)
    {
        /*
            UP(0) -> LEFT(3)
            DOWN(1) -> RIGHT(2)

            RIGHT(2) -> UP(0)
            LEFT(3) -> DOWN(1)

            AWAY(4) -> AWAY(4)
            TOWRDS(5) -> TOWARDS(5)
         */

        if (dir >= AWAY) //front and back face don't change
            return dir;
        if (dir >= RIGHT) //only right left cause forward backwards caught by previous if
            return (Direction)(dir.val - 2);

        var localMax = LEFT.val;
        return (Direction)(localMax - ((dir.val + 2 + 1) % localMax));
    }

    public static Direction RotateClockwiseYAxis(Direction dir)
    {
        /*
            UP(0) -> UP(0)
            DOWN(1) -> DOWN(1)

            RIGHT(2) -> TOWARDS(5)
            LEFT(3) -> AWAY(4)

            AWAY(4) -> RIGHT(2)
            TOWRADS(5) -> LEFT(3)
         */
        if (dir <= DOWN)
            return dir;

        if (dir >= AWAY) 
        {
            return (Direction)((dir.val + 2 + 1) % MAX3D); //+2 to skip up and down, + 1 to rotate mod MAX to clamp
        }

        return (Direction)(MAX3D - ((dir.val + 2 + 1) % MAX3D));
    }

    public static Direction RotateCounterClockwiseYAxis(Direction dir)
    {
        /*
            UP(0) -> UP(0)
            DOWN(1) -> DOWN(1)

            RIGHT(2) -> AWAY(4)
            LEFT(3) -> TOWARDS(5)

            AWAY(4) -> LEFT(3)
            TOWRADS(5) -> RIGHT(2)
         */
        if (dir <= DOWN)
            return dir;

        if (dir >= AWAY)
        {
            return Opposite((Direction)((dir.val + 2 + 1)%MAX3D));
        }

        return (Direction)(dir.val + 2);

    }

    public static Direction RotateClockwiseXAxis(Direction dir)
    {
        /*
            UP(0) -> AWAY(4)
            DOWN(1) -> TOWARDS(5)

            RIGHT(2) -> RIGHT(2)
            LEFT(3) -> LEFT(3)

            AWAY(4) -> DOWN(1)
            TOWRADS(5) -> UP(0)
         */
        if (dir <= DOWN)
            return (Direction)(dir.val +2 +2);//skip and turn

        if(dir >= AWAY)
            return Opposite((Direction)((dir.val+1)%MAX3D));

        return dir;
    }

    public static Direction RotateCounterClockwiseXAxis(Direction dir)
    {
        /*
            UP(0) -> TOWARDS(5)
            DOWN(1) -> AWAY(4)

            RIGHT(2) -> RIGHT(2)
            LEFT(3) -> LEFT(3)

            AWAY(4) -> UP(0)
            TOWRADS(5) -> DOWN(1)
         */
        if (dir <= DOWN)
            return (Direction)(MAX3D - ((dir.val + MAX3D) % MAX3D));

        if (dir >= AWAY)
            return (Direction)((dir.val + 1) % MAX3D);

        return dir;
    }

    public static IEnumerable<Direction> Enumerate() 
    {
        yield return UP;
        yield return DOWN;
        yield return LEFT;
        yield return RIGHT;
        yield return AWAY;
        yield return TOWARDS;
    }

    public static IEnumerable<Direction> Enumerate2D() 
    {
        yield return UP;
        yield return DOWN;
        yield return LEFT;
        yield return RIGHT;
    }


    public static IEnumerable<Direction> Enumerate(Dimensions dim)
    {
        switch (dim) 
        {
            case Dimensions.ThreeD:
                return Enumerate();
            case Dimensions.TwoD:
                return Enumerate2D();

        }
        throw new InvalidDataException($"Unkown Dimension {dim}");
    }
}

