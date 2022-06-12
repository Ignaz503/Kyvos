using Kyvos.Core.Configuration;
using System;

namespace FullyAutomatedGayLuxurySpaceCommunism;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false, Inherited = false)]
public class EngineTestAttribute : Attribute
{

    public string? Name { get; }

    public bool HasName => !string.IsNullOrEmpty(Name);

    public EngineTestAttribute()
    {
        Name = null;
    }

    public EngineTestAttribute(string name)
    {
        this.Name = name;
    }

}
