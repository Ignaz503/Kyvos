namespace Kyvos.Assets;

public interface IAssetMapping 
{
    public const string CONFIG_KEY = "asset_database";

    public const string EntrySeperator = " | ";

    AssetLocation GetAssetLocation(AssetIdentifier id);

}