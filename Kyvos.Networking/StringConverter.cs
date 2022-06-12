using System.Text;

namespace Kyvos.Networking;

public class StringConverter : IDataConverter<string>
{
    public readonly static StringConverter Instance = new();

    private StringConverter()
    { }

    public string Read(Span<byte> data)
        => Encoding.UTF8.GetString(data);

    public int SizeInBytes(string data)
        => Encoding.UTF8.GetByteCount(data);

    public void WriteInto(string data, Span<byte> buffer)
        => Encoding.UTF8.GetBytes(data, buffer);
}
