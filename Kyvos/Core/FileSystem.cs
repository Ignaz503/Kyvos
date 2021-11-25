using System.IO;
using System;
using System.Runtime.Serialization;

namespace Kyvos.Core
{
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

        public static string Assets = Path.Combine(AppData,Constants.AssetsFolderName);

        public static string MakeAbsolute( string localPath, StorageLocation location = StorageLocation.Assets )
            => Path.Combine( GetPathFromLocation( location ), localPath );

        static string GetPathFromLocation( StorageLocation location ) 
        {
            switch (location)
            {
                case StorageLocation.Assets:
                    return Assets;
                case StorageLocation.InstallFolder:
                    return InstallLocation;
                case StorageLocation.LocalAppData:
                    return AppData;
                default:
                    throw new UnknownStorageLocation(location);
            }
        }

    }

    public abstract class FileSystemException : Exception
    {
        protected FileSystemException()
        {
        }

        protected FileSystemException( string message ) : base( message )
        {
        }

        protected FileSystemException( SerializationInfo info, StreamingContext context ) : base( info, context )
        {
        }

        protected FileSystemException( string message, Exception innerException ) : base( message, innerException )
        {
        }
    }

    public class UnknownStorageLocation : FileSystemException 
    {
        public UnknownStorageLocation(FileSystem.StorageLocation location):base($"{location} is not a known storage location")
        {}
    }

}
