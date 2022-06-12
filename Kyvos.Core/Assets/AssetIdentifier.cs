namespace Kyvos.Core.Assets;

public readonly ref struct AssetIdentifier
{
    public string Name { get; init; }

    public AssetIdentifier()
        => Name = string.Empty;
    
    public AssetIdentifier(string name)
        => Name = name;

    public static explicit operator AssetIdentifier(string name)
        => new(name);

    public static implicit operator string(AssetIdentifier assetID)
        => assetID.Name;

}
