using System;
using System.Threading;

namespace Kyvos.Maths.Topology;
public class LockedArray<T>
{
    SemaphoreSlim[] semaphores;
    T[] data;

    public int Length { get; private set; }
    public LockedArray(int size)
    {
        Length = size;
        data = new T[size];
        semaphores = new SemaphoreSlim[size];

        for (int i = 0; i < size; i++)
        {
            semaphores[i] = new SemaphoreSlim(1, 1);
        }
    }

    public T this[int idx]
    {
        get
        {
            T res = default;

            DoWorkProtected(idx, () => res = data[idx]);

            return res;
        }
        set
        {
            DoWorkProtected(idx, () => data[idx] = value);
        }
    }

    void DoWorkProtected(int idx, Action action)
    {
        if (idx < 0 || idx >= data.Length)
            throw new IndexOutOfRangeException();

        semaphores[idx].Wait();
        try
        {
            action();
        }
        finally
        {
            semaphores[idx].Release();
        }
    }

}


