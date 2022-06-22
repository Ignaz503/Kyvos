using System;
using System.Collections;

namespace Kyvos.Utility;

public static class StringExtensions 
{

    public static SplitEnumerator Split(this ReadOnlySpan<char> s, char separator)
    {
        if (s.IsEmpty)
        {
            return SplitEnumerator.Empty;
        }
        return new SplitEnumerator(s, separator);
    }

    public static SplitEnumeratorMultiSeperator Split(this ReadOnlySpan<char> s, ReadOnlySpan<char> separator)
    {
        if (s.IsEmpty)
        {
            return SplitEnumeratorMultiSeperator.Empty;
        }
        return new SplitEnumeratorMultiSeperator(s, separator);
    }
}

public ref struct SplitEnumeratorMultiSeperator
{
    private ReadOnlySpan<char> _str;
    private readonly ReadOnlySpan<char> seperators;

    public SplitEnumeratorMultiSeperator(ReadOnlySpan<char> str, ReadOnlySpan<char> seperators)
    {
        _str = str;
        this.seperators = seperators;
        Current = default;
    }

    // Needed to be compatible with the foreach operator
    public SplitEnumeratorMultiSeperator GetEnumerator() => this;

    public bool MoveNext()
    {
        var span = _str;
        if (span.Length == 0) // Reach the end of the string
            return false;

        var index = span.IndexOfAny(seperators);
        if (index == -1) // The string is composed of only one line
        {
            _str = ReadOnlySpan<char>.Empty; // The remaining string is an empty string
            Current = span;
            return true;
        }

        if (index < span.Length - 1 && span[index] == seperators[0])
        {
            var nextIndexOffset = 1;    
            var next = span[index + nextIndexOffset];
            //consume all seperators
            while (InIdxRange(nextIndexOffset,seperators.Length) && seperators[nextIndexOffset] == next) 
            {
                nextIndexOffset++;
                next = span[index + nextIndexOffset];
            }
            if (!InIdxRange(nextIndexOffset, seperators.Length))//
            {
                Current = span[..index];

                _str = span[(index + nextIndexOffset)..];
                return true;
            }
        }

        Current = span[..index];
        _str = span[(index + 1)..];
        return true;

        bool InIdxRange(int idx, int length)
            => idx < length;

    }

    public ReadOnlySpan<char> Current { get; private set; }

    public static SplitEnumeratorMultiSeperator Empty => new(ReadOnlySpan<char>.Empty, ReadOnlySpan<char>.Empty);

}


public ref struct SplitEnumerator
{
    private ReadOnlySpan<char> _str;
    private readonly char seperator;

    public SplitEnumerator(ReadOnlySpan<char> str, char seperator)
    {
        _str = str;
        this.seperator = seperator;
        Current = default;
    }

    // Needed to be compatible with the foreach operator
    public SplitEnumerator GetEnumerator() => this;

    public bool MoveNext()
    {
        var span = _str;
        if (span.Length == 0) // Reach the end of the string
            return false;

        var index = span.IndexOf(seperator);
        if (index == -1)//no occurance
        {
            _str = ReadOnlySpan<char>.Empty;
            Current = span;
            return true;
        }

        if (index < span.Length - 1 && span[index] == seperator)
        {

            Current = span[..index];

            _str = span[(index + 1)..];
            return true;
            
        }

        Current = span[..index];
        _str = span[(index + 1)..];
        return true;

    }

    public ReadOnlySpan<char> Current { get; private set; }

    public static SplitEnumerator Empty => new(ReadOnlySpan<char>.Empty, char.MinValue);

}