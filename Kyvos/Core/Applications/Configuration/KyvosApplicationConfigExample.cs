using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Kyvos.Core.Applications.Configuration
{
    public class KyvosApplicationConfigExample
    {
        Dictionary<string, object> internalData;
        JsonSerializerOptions options;

        private KyvosApplicationConfigExample() 
        {
            internalData = new Dictionary<string, object>();
            options = AppConfig.CreateDefualtOptions();
        }

        public KyvosApplicationConfigExample WithSerializeOptions( JsonSerializerOptions options ) 
        {
            this.options = options;
            return this;
        }

        public KyvosApplicationConfigExample With( string key, object data )
        {
            if(!internalData.ContainsKey(key))
                this.internalData.Add( key, data );
            return this;
        }

        public void WriteTo( string filePath ) 
        {
            File.WriteAllText( filePath, JsonSerializer.Serialize( internalData, options ));
        }

        public void LogTo( Action<string> logTo )
            => logTo( JsonSerializer.Serialize( internalData, options ) );


        public static KyvosApplicationConfigExample Builder
            => new();

    }
}
