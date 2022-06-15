namespace Kyvos.Utility.Collections;

public class Stack<T>
{
    const int GrowthMultiplyer = 2;
    const int InitialGrowth = 8;

    T?[] data;
    int top;

    int capacity;
    public int Capacity => capacity;
    public int Count => top;

    public Stack()
    {
        data = Array.Empty<T>();
        top = 0;
        capacity = 0;
    }

    public Stack(int cap)
    {
        this.capacity = cap;
        this.top = 0;
        this.data = new T[capacity];
    }

    public void Push(T item)
    {
        if (top >= capacity)
            GrowToFit();
        data![top++] = item;
    }


    public T? Peek(int distance = 0)
        => data[top - 1 - distance];

    public T? Pop()
    {
        var elem = data[top - 1];
        data[top - 1] = default;
        top--;
        return elem;
    }

    void GrowToFit()
    {
        var newCap = capacity == 0 ? InitialGrowth : capacity * GrowthMultiplyer;
        var newData = new T[newCap];

        data.CopyTo(newData, 0);

        data = newData;
        capacity = newCap;
    }

    public void Trim()
    {
        if (top >= capacity)
            return;

        var newData = new T[top];

        //copy from start of old data into start of new data with length of top
        Array.Copy(data, 0, newData, 0, top);

        data = newData;
        capacity = top;
    }
}
