using Kyvos.Core.Configuration;
using Kyvos.Core;
using Kyvos.VeldridIntegration.Json;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using Kyvos.Core.Logging;

namespace Kyvos.AppConfigExamples;

public partial class Config : IConfig
{
    public const string  FileName = "appconfig.json";
    
    JsonElement root;
    JsonSerializerOptions defaultOptions;

    private Config(JsonElement element, JsonSerializerOptions defaultOptions) 
    {
        this.root = element;
        this.defaultOptions = defaultOptions;
    }

    public T? ReadValue<T>( ConfigKey key )
    {
        try
        {
            var elem = key.Traverse(root);
            if (elem.HasValue)
                return JsonSerializer.Deserialize<T>( elem.Value.GetRawText(), defaultOptions );
        } catch {
            Log<Config>.Debug("Failed to read value {Key}", key);
            Debug.WriteLine("Failed to read value {Key}", key);
            return default;
        }
        Log<Config>.Debug("Failed to find {Key} in configs", key);
        Debug.WriteLine("Failed to find {Key} in configs", key);
        return default;
    }

    public T? ReadValue<T>( IConfig.Reader<T> reader )
    {
        if (reader is null)
            return default;
        var elem = reader.Key.Traverse(root);
        if (!elem.HasValue)
            return reader.Default;
        return reader.Parse( elem.Value.GetRawText() );
    }

    public static Config Load( string filePath, JsonSerializerOptions options )
        => new( JsonSerializer.Deserialize<JsonElement>( File.ReadAllText( filePath ), options ), options );

    public static Config Load( string filePath )
        => Load( filePath, CreateDefualtOptions() );

    public static Config Load()
        => Load( FileSystem.MakeAbsolute( FileName, FileSystem.StorageLocation.InstallFolder ) );

    static Config LoadFromString(string json, JsonSerializerOptions options)
        => new(JsonSerializer.Deserialize<JsonElement>(json, options), options);
    static Config LoadFromString(string json)
        => LoadFromString(json,CreateDefualtOptions());

    public override string ToString()
    {
        return root.ToString();
    }

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

    public static void SeedGUIAppConfigFile() 
    {
        new GUIAppSeeder().Seed( Config.FileName );
    }
    
    public static string GetGUIAppConfigFileSeed() 
    {
        return new GUIAppSeeder().Build(Example.Builder).ToString();
    }

}
public static class GUIConfigApplicationExtensions
{
    public static IModifyableApplication WithConfig(this IModifyableApplication app, string configFileName)
    {
        app.AddComponent(LoadConfigFile(configFileName));
        return app;
    }
    static IConfig LoadConfigFile(string name)
    {
        return Config.Load(FileSystem.MakeAbsolute(name, FileSystem.StorageLocation.InstallFolder));
    }
}
