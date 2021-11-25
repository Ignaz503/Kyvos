using Kyvos.Core.Applications.Configuration;
using Kyvos.Json;
using System.IO;
using System.Text.Json;
using Veldrid;
using Veldrid.StartupUtilities;

namespace Kyvos.Core.Applications.Configuration
{
    public class AppConfig : IConfig
    {
        internal static readonly string  FilePath = "appconfig.json";

        JsonElement element;
        JsonSerializerOptions defaultOptions;

        private AppConfig(JsonElement element, JsonSerializerOptions defaultOptions) 
        {
            this.element = element;
            this.defaultOptions = defaultOptions;
        }

        public T ReadValue<T>( ConfigKey key )
        {
            try
            {
                var elem = key.Traverse(element);
                if (elem.HasValue)
                    return JsonSerializer.Deserialize<T>( elem.Value.GetRawText(), defaultOptions );
            } finally {
            }
            return default;
        }

        public T ReadValue<T>( IConfig.Reader<T> reader )
        {
            if (reader is null)
                return default;
            var elem = reader.Key.Traverse(element);
            if (!elem.HasValue)
                return reader.Default;
            return reader.Parse( elem.Value.GetRawText() );
        }

        public static AppConfig Load( string filePath, JsonSerializerOptions options )
            => new( JsonSerializer.Deserialize<JsonElement>( File.ReadAllText( filePath ), options ), options );

        public static AppConfig Load( string filePath )
            => Load( filePath, CreateDefualtOptions() );

        public static AppConfig Load()
            => Load( FileSystem.MakeAbsolute( FilePath, FileSystem.StorageLocation.InstallFolder ) );

        internal static JsonSerializerOptions CreateDefualtOptions()
        {
            var options = new JsonSerializerOptions()
            {
                IncludeFields = true,
                WriteIndented = true,
                ReadCommentHandling = JsonCommentHandling.Skip
            };
            options.Converters.Add( new ResourceBindingModelConverter() );
            options.Converters.Add( new WindowStateConverter() );
            options.Converters.Add( new GraphicsBackendConverter() );
            options.Converters.Add( new PixelFormatConverter() );
            return options;
        }

        public static void SeedConfigFile( Seeder seeder ) 
        {
            seeder.Seed( AppConfig.FilePath );
        }

        public class Seeder 
        {
            internal void Seed( string filePath ) 
            {
                var builder =  Build(KyvosApplicationConfigExample.Builder);

                builder.WriteTo( filePath );
            }

            virtual protected KyvosApplicationConfigExample Build( KyvosApplicationConfigExample builder )
            {
                builder.With( "window", new WindowCreateInfo() )
                    .With( "gfx", new GraphicsDeviceConfig { Options = new GraphicsDeviceOptions() { SwapchainDepthFormat = PixelFormat.R16_UNorm }, Backend = GraphicsBackend.Vulkan } )
                    .With( "time", new Time.Config() );
                return builder;
            }

        }

    }




}
