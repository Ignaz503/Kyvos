namespace Kyvos.Assets;

public struct AssetLocation
{
    string basePath;
    string[] entries;
    public int NumberOfLocations => entries.Length;
    public IReadOnlyList<string> Locations => entries;
    public string BasePath => basePath;

    public string First => Path.Combine(basePath, entries[0]);

    public AssetLocation()
    {
        entries = Array.Empty<string>();
        basePath = string.Empty;
    }

    public AssetLocation(string localPath, string @base)
    {
        this.basePath = @base;
        this.entries = new string[1] { localPath };
    }

    public AssetLocation(string @base, params string[] localPaths) 
    {
        this.basePath = @base;
        this.entries = localPaths;
    }

    public string this[int idx]
        => entries[idx];

    public IEnumerator<string> Enumerate() 
    {
        for (int i = 0; i < NumberOfLocations; i++)
        {
            yield return Path.Combine(basePath, entries[i]);
        }
    }
}

