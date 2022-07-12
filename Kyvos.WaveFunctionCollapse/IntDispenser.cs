using System.Collections.Concurrent;

namespace Kyvos.WaveFunctionCollapse;

public class IntDispenser : ISocketDataDispenser<int>
{
    ConcurrentStack<int> freeIds;
    int current;

    public IntDispenser()
    {
        freeIds = new();
        current = -1;
    }


    public int GetFreeItem()
    {
        if (!freeIds.TryPop(out int id)) 
        {
            id = Interlocked.Increment(ref current);
        }
        return id;
    }

    public void ReturnItem(int item)
    {
        freeIds.Push(item);
    }
}

