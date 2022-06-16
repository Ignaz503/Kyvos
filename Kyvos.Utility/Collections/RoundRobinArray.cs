using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Kyvos.Utility.Collections;

public interface IArrayInitializer<T> 
{
    T GetForIdx(int idx);
}

public class RoundRobinArray<T> : IEnumerable<T>, IList<T>
{
    T[] data;
    private int size;

    public int Size { get => size; init => size = value; }

    public int Count => throw new NotImplementedException();
    
    public bool IsReadOnly => false;

    public T this[int index] { get => data[index % size]; set => data[index % size] = value; }

    public ref T GetRef(int index) 
        => ref data[index % size];


    int head;
    public int Head => head;

    public RoundRobinArray(int capacity)
    {
        Size = capacity;
        data = new T[Size];
        head = 0;
    }

    public RoundRobinArray(int capacity, IArrayInitializer<T> initalizer)
    {
        Size = capacity;
        data = new T[Size];
        for (int i = 0; i < Size; i++)
        {
            Add(initalizer.GetForIdx(i));
        }
        head = 0;
    }

    public void AdvanceHead()
        => head = (head+1)%size;

    public int IndexOf(T item)
    {
        for (int i = 0; i < data.Length; i++)
        {
            var elem = data[i];

            if (EqualityComparer<T>.Default.Equals(elem, item))
            {
                return i;
            }
        }
        return -1;
    }

    public void Insert(int index, T item)
    {
        this[index] = item;
    }

    public IEnumerator<T> GetEnumerator()
    {
        foreach (var item in data)
            yield return item;
    }

    IEnumerator IEnumerable.GetEnumerator()
        => GetEnumerator();

    public void Add(T item) 
    {
        this[head++] = item;
    }

    public void RemoveAt(int index)
    {
#pragma warning disable CS8601
        this[index] = default;
#pragma warning restore CS8601
    }

    public void Clear()
    {
        for (int i = 0; i < data.Length; i++)
        {
#pragma warning disable CS8601
            data[i] = default;
#pragma warning restore CS8601
        }
        head = 0;
    }

    public bool Contains(T item)
    {
        foreach (var elem in data)
        {
            if (EqualityComparer<T>.Default.Equals(elem, item))
            {
                return true;
            }
        }
        return false;
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
        Array.Copy(data, arrayIndex, array, 0, array.Length);
    }

    public bool Remove(T item)
    {
        bool removed = false;
        for (int i = 0; i < data.Length; i++)
        {
            if (EqualityComparer<T>.Default.Equals(data[i], item)) 
            {
                removed = true;
#pragma warning disable CS8601
                data[i] = default;
#pragma warning restore CS8601
            }
        }
        return removed;
    }

    public override string ToString() 
    {
        StringBuilder builder = new("[ ");

        for (int i = 0; i < data.Length; i++)
        {
            if (i == data.Length - 1)
            {
                builder.Append(data[i]?.ToString() ?? "null");
            }
            else
            {
                builder.Append((data[i]?.ToString() ?? "null") + ", ");
            }
        }

        return builder.Append(" ]").ToString();
    }
        

}
