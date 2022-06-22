using System;
using System.IO;
using System.Runtime.Serialization;

namespace Kyvos.Core;
public static class FileSystem
{
    public enum StorageLocation
    {
        Assets,
        LocalAppData,
        InstallFolder
    }

    public static class Constants
    {
        public const string AssetsFolderName = "Assets";
    }

    public static string AppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
    public static string InstallLocation => AppDomain.CurrentDomain.BaseDirectory;

    public static string Assets = System.IO.Path.Combine(InstallLocation, Constants.AssetsFolderName);

    public static string MakeAbsolute(string localPath, StorageLocation location = StorageLocation.Assets)
        => System.IO.Path.Combine(GetPathFromLocation(location), localPath);


    static string GetPathFromLocation(StorageLocation location)
    {
        return location switch
        {
            StorageLocation.Assets => Assets,
            StorageLocation.InstallFolder => InstallLocation,
            StorageLocation.LocalAppData => AppData,
            _ => throw new UnknownStorageLocation(location),
        };
    }

}

public struct Path 
{
    public enum Anchoring 
    {
        Local,
        Absolute,
        None
    }

    public enum Endpoint 
    {
        Directory,
        File,
        Error
    }

    string value;
    public string Value => value;

    Anchoring anchoring;
    public bool IsLocal => anchoring == Anchoring.Local;
    public bool IsAbsolute => anchoring == Anchoring.Absolute;

    Endpoint endpoint;
    public bool IsFile => endpoint == Endpoint.File;
    public bool IsDirectory => endpoint == Endpoint.Directory;

    public Path()
    {
        value = string.Empty;
        anchoring = Anchoring.None;
        endpoint = Endpoint.Error;
    }

    public Path(string p) 
    {
        value = p;
        endpoint = System.IO.Path.HasExtension(value.AsSpan()) ? Endpoint.File : Endpoint.Directory;
        anchoring = System.IO.Path.IsPathRooted(value.AsSpan()) ? Anchoring.Absolute : Anchoring.Local;
    }

    public static explicit operator Path(string s)
        => new(s);

    public static explicit operator string(Path p)
        => p.value;

    public static explicit operator ReadOnlySpan<char>(Path p)
        => p.value.AsSpan();
    
    public static explicit operator Path(ReadOnlySpan<char> p)
        => new(new string(p));
}

public abstract class FileSystemException : Exception
{
    protected FileSystemException()
    {
    }

    protected FileSystemException(string message) : base(message)
    {
    }

    protected FileSystemException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    protected FileSystemException(string message, Exception innerException) : base(message, innerException)
    {
    }
}

public class UnknownStorageLocation : FileSystemException
{
    public UnknownStorageLocation(FileSystem.StorageLocation location) : base($"{location} is not a known storage location")
    { }
}


