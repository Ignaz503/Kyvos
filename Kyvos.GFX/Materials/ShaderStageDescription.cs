using System;
using System.Text;
using Veldrid;

namespace Kyvos.Graphics.Materials;

public struct ShaderStageDescription 
{
    public ShaderStages ShaderStages { get; init; }    
    public string EntryPoint { get; init; }

    public ShaderCode Code { get; init; }

    public byte[] GetShaderCode() 
    {
        //TODO not this
        return Code.GetShaderCode();
    }

}

public struct ShaderCode 
{
    public enum StorageType
    {
        FileIdentifier,
        Code
    }
    public StorageType StorageIdentifier { get; init; }
    public string Data { get; init; }


    public byte[] GetShaderCode()
    {
        return StorageIdentifier switch
        {
            StorageType.FileIdentifier => LoadFromFile(),
            StorageType.Code => Encoding.UTF8.GetBytes(Data),
            _ => throw new UnknownShaderCodeType()
        };
    }

    byte[] LoadFromFile() 
    {
        throw new NotImplementedException();
    }

}