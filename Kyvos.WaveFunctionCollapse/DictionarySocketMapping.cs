namespace Kyvos.WaveFunctionCollapse;

public class DictionarySocketMapping<TDataType, TSocketData> : ISocketTypeMapping<TDataType, TSocketData>
        where TDataType : IEquatable<TDataType>
    where TSocketData : struct, IComparable<TSocketData>, IEquatable<TSocketData>
{
    ISocketDataDispenser<TSocketData> dataDispenser;
    object _lockObj;
    Dictionary<TDataType, SocketType<TSocketData>> mapping;

    public DictionarySocketMapping(ISocketDataDispenser<TSocketData> dataDispenser)
    {
        this.dataDispenser = dataDispenser;
        this.mapping = new();
        _lockObj = new();
    }

    public SocketType<TSocketData> GetOrCreateSocketForData(TDataType dataType)
    {
        lock (_lockObj) 
        {
            if (mapping.ContainsKey(dataType))
                return mapping[dataType];
            SocketType<TSocketData> newType = new(dataDispenser.GetFreeItem());
            mapping.Add(dataType, newType);
            return newType;
        }
    }

    public void RemoveSocketType(TDataType dataType)
    {
        lock (_lockObj) 
        {
            if (mapping.ContainsKey(dataType))
                dataDispenser.ReturnItem(mapping[dataType].Value);
            mapping.Remove(dataType);
        }
    }
}

