namespace Kyvos.Utility;

public readonly ref struct Option<T>
{
    enum OptionType
    {
        None,
        Some
    }

    readonly OptionType type;
    readonly T? value;

    public Option()
        => (type, value) = (OptionType.None, default);

    public Option(T val)
        => (type, value) = (OptionType.Some, val);


    public void Match(Action<T> some, Action none)
    {
        if (type == OptionType.Some)
            some(value!);
        else
            none();
    }

    public static Option<T> None => new();

}

//TODO
/*
 [AttributeUsage(AttributeTargets.Class|AttributeTargets.Struct,AllowMultiple = false)]
public class CustomEnum : Attribute
{
    public enum EnumerationType 
    {
        Byte,
        Uint,
        Int,
        Short,
        UShort,
        Long,
        ULong
    }

    public Type DataType { get; init; }
    public EnumerationType TypeEnumeration { get; init; }

    public string[] UsingStatements { get; init; }

    public CustomEnum(Type dataType, EnumerationType enumerationType)
    {
        
        DataType = dataType ?? throw new ArgumentNullException(nameof(dataType));
        this.TypeEnumeration = enumerationType;
        this.UsingStatements = Array.Empty<string>();
    }

    public CustomEnum(Type dataType, EnumerationType enumerationType, params string[] usings)
    {
        DataType = dataType ?? throw new ArgumentNullException(nameof(dataType));
        this.TypeEnumeration = enumerationType;
        this.UsingStatements = usings;
    }

    public CustomEnum(Type dataType) 
    {
        this.DataType = dataType ?? throw new ArgumentNullException(nameof(dataType));
        this.TypeEnumeration = EnumerationType.Int;

        this.UsingStatements = Array.Empty<string>();
    }
    public CustomEnum(Type dataType, params string[] usings)
    {
        this.DataType = dataType ?? throw new ArgumentNullException(nameof(dataType));
        this.TypeEnumeration = EnumerationType.Int;

        this.UsingStatements = usings;
    }


    public CustomEnum()
    {
        this.DataType = typeof(int);
        this.TypeEnumeration = EnumerationType.Int;

        this.UsingStatements = Array.Empty<string>();
    }
}

[AttributeUsage(AttributeTargets.Field , AllowMultiple = false)]
public class EnumValueAttribute : Attribute 
{}


[CustomEnum(DataType = typeof(Vector3), TypeEnumeration = CustomEnum.EnumerationType.Int, UsingStatements = new string[] { "using System.Numerics;" })]
public partial struct ArbitraryEnum 
{
    static readonly Vector3 first = new Vector3(0, 0, 1);
    static readonly Vector3 second = new Vector3(0, 1, 0);
    static readonly Vector3 third = new Vector3(1, 0, 0);
    static readonly Vector3 last = new Vector3(1, 1, 1);
}

/// <summary>
/// generated code
/// </summary>
public partial struct ArbitraryEnum : IEquatable<ArbitraryEnum>, IEquatable<int>, IComparable<ArbitraryEnum>, IComparable<int>
{
    public static readonly ArbitraryEnum First = new(0);
    public static readonly ArbitraryEnum Second = new(1);
    public static readonly ArbitraryEnum Third = new(2);
    public static readonly ArbitraryEnum Last = new(3);

    int val;
    
    public ArbitraryEnum() => val = 0;

    public ArbitraryEnum(int val) => this.val = val;

    public static explicit operator Vector3(ArbitraryEnum eVal) =>
        eVal.val switch {
        0 => first,
        1 => second,
        2 => third,
        3 => last,
        //int known from Attribute EnumerateType
        _ => throw new UndefinedEnumValue<int>(eVal.val) };

    public static explicit operator int(ArbitraryEnum eval)
        => eval.val;

    public static explicit operator ArbitraryEnum(int val)
        => new(val);

    public static bool operator ==(ArbitraryEnum lhs, ArbitraryEnum rhs)
        => lhs.val == rhs.val;

    public static bool operator !=(ArbitraryEnum lhs, ArbitraryEnum rhs)
        => lhs.val != rhs.val;

    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        if (obj is ArbitraryEnum val)
            return this == val; 
        if(obj is int integer)
            return this == (ArbitraryEnum)integer;
        //TODO maybe all the other convertiable to int options
        //like byte short etc
        return false;
    }

    public override int GetHashCode() => val.GetHashCode();

    public bool Equals(ArbitraryEnum other) => other.val.Equals(val);

    public bool Equals(int other) => val.Equals(other);

    public int CompareTo(ArbitraryEnum other) => val.CompareTo(other.val);

    public int CompareTo(int other) => val.CompareTo(other);
    public static IEnumerator<ArbitraryEnum> Enumerate() 
    {
        yield return First;
        yield return Second;
        yield return Third;
        yield return Last;
    }

    public static IEnumerator<Vector3> EnumerateData()
    {
        yield return first;
        yield return second;
        yield return third;
        yield return last;
    }
}
 
 */