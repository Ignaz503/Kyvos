using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Veldrid;

namespace Kyvos.Json
{
    public class ResourceBindingModelConverter : JsonConverter<ResourceBindingModel>
    {
        public override ResourceBindingModel Read( ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options )
        {
            return CommonEnumFunctionality.FromString<ResourceBindingModel>( reader.GetString() );
        }

        public override void Write( Utf8JsonWriter writer, ResourceBindingModel value, JsonSerializerOptions options )
        {
            writer.WriteStringValue( CommonEnumFunctionality.ToString( value ) );
            writer.WriteCommentValue( CommonEnumFunctionality.StringifyOptions<ResourceBindingModel>() );
        }
    }

    public class PixelFormatConverter : JsonConverter<PixelFormat?>
    {
        public override PixelFormat? Read( ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options )
        {
            var str = reader.GetString();
            if (str is null)
                return null;
            return CommonEnumFunctionality.FromString<PixelFormat>( str );
        }

        public override void Write( Utf8JsonWriter writer, PixelFormat? value, JsonSerializerOptions options )
        {
            if (value.HasValue)
                writer.WriteStringValue( CommonEnumFunctionality.ToString( value.Value ) );
            else
                writer.WriteStringValue((string)null );
            writer.WriteCommentValue(CommonEnumFunctionality.StringifyOptions<PixelFormat>().Replace("[","[null, ") );
        }
    }

    public class GraphicsBackendConverter: JsonConverter<GraphicsBackend>
    {
        public override GraphicsBackend Read( ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options )
        {
            return CommonEnumFunctionality.FromString<GraphicsBackend>( reader.GetString() );
        }

        public override void Write( Utf8JsonWriter writer, GraphicsBackend value, JsonSerializerOptions options )
        {
            writer.WriteStringValue( CommonEnumFunctionality.ToString( value ) );
            writer.WriteCommentValue( CommonEnumFunctionality.StringifyOptions<GraphicsBackend>());
        }
    }

    public class WindowStateConverter : JsonConverter<WindowState>
    {
        public override WindowState Read( ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options )
        {
            return CommonEnumFunctionality.FromString<WindowState>( reader.GetString() );
        }

        public override void Write( Utf8JsonWriter writer, WindowState value, JsonSerializerOptions options )
        {
            writer.WriteStringValue( CommonEnumFunctionality.ToString( value ) );
            writer.WriteCommentValue( CommonEnumFunctionality.StringifyOptions<WindowState>() );
        }
    }


    public static class CommonEnumFunctionality
    {
        public static T FromString<T>( string value )
        {
            return (T)Enum.Parse( typeof( T ), value );
        }

        public static string ToString<T>( T to )
        {
            return Enum.GetName( typeof( T ), to );
        }

        public static string StringifyOptions<T>() 
        {
            List<string> values = new();
            foreach (var val in Enum.GetValues( typeof( T ) ))
            {
                values.Add( val.ToString() );
            }
            return $"[{string.Join(", ", values)}]";
        }

    }
}
