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