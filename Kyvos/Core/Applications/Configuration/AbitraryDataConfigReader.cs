using System;
using System.Text.Json;

namespace Kyvos.Core.Applications.Configuration
{
    public class AbitraryDataConfigReader<T> : IConfig.Reader<T>
    {
        public ConfigKey Key { get; init; }
        public T Default { get; init; }

        public JsonSerializerOptions Options { get; init; }

        public T Parse( string jsontext )
        {
            try
            {
                return JsonSerializer.Deserialize<T>( jsontext, Options );
            } catch (Exception) 
            {
                return Default;
            }
        }
    }


}
