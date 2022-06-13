using System.Collections;

namespace Kyvos.Utility.Collections;


public class FakeList<T> : IList<T>, IList, IReadOnlyList<T>
{
    public static readonly FakeList<T> Instance = new();

#pragma warning disable CS8603
    public T this[int index] { get => default; set { return; } }
#pragma warning restore CS8603

    object? IList.this[int index] { get => default; set { return; } }


    public int Count => 0;

    public bool IsReadOnly => false;

    public bool IsFixedSize => true;

    public bool IsSynchronized => false;

    public object SyncRoot => this;

    public void Add(T item) { }

    public int Add(object? value)
        => -1;

    public void Clear() { }

    public bool Contains(T item)
        => false;

    public bool Contains(object? value)
        => false;

    public void CopyTo(T[] array, int arrayIndex) { }
    public void CopyTo(Array array, int index) { }

    public IEnumerator<T> GetEnumerator()
    {
        foreach (var elem in Array.Empty<T>())
            yield return elem;
    }

    public int IndexOf(T item)
        => -1;

    public int IndexOf(object? value)
        => -1;

    public void Insert(int index, T item) { }

    public void Insert(int index, object? value) {}

    public bool Remove(T item)
        => false;

    public void Remove(object? value) {}
    public void RemoveAt(int index) {}

    IEnumerator IEnumerable.GetEnumerator()
        => GetEnumerator();

}
