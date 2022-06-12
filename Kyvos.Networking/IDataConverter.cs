namespace Kyvos.Networking;

public interface IDataConverter<T>
{
    T Read(Span<byte> data);

    int SizeInBytes(T data);

    void WriteInto(T data, Span<byte> buffer);

}
