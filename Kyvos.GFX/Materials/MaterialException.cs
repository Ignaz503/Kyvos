using System;
using System.Runtime.Serialization;

namespace Kyvos.Graphics.Materials;

public abstract class MaterialException : Exception
{
    protected MaterialException()
    {
    }

    protected MaterialException(string message) : base(message)
    {
    }

    protected MaterialException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    protected MaterialException(string message, Exception innerException) : base(message, innerException)
    {
    }
}

public class NonExistentPropertySetIdx : MaterialException 
{
    public NonExistentPropertySetIdx(string materialName, uint setIdx): base($"Material {materialName} has no property set with index: {setIdx}")
    {

    }
}

public class UnkownGlobalSetIdx : MaterialException
{
    public UnkownGlobalSetIdx(uint setIdx) : base($"{setIdx} is not the set index of any global set")
    {

    }
}

public class UnknownShaderCodeType : MaterialException 
{
}