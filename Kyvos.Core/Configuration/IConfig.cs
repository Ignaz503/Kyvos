using System;
using System.Text;

namespace Kyvos.Core.Configuration;

public interface IConfig
{
    T? ReadValue<T>( ConfigKey key );

    T? ReadValue<T>( Reader<T> reader );


    public interface Reader<T>
    {
        public ConfigKey Key { get; }

        public T? Default { get; }

        public T? Parse( string jsontext );
    }

}

public static class IConfigExtension 
{
    public static T? ReadValue<T>( this IConfig config, string key )
        => config.ReadValue<T>( key );

    public static T? ReadValue<T>( this IConfig config, params string[] key )
        => config.ReadValue<T>( key );

    public static T? ReadValue<T>(this IConfig config)
        => throw new InvalidOperationException("No key specified");
}
