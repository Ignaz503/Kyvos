using Kyvos.Core;
using Kyvos.Core.Configuration;
using System.Collections.Generic;
using System;
using System.Reflection;
using System.Linq;
using System.Diagnostics;

namespace FullyAutomatedGayLuxurySpaceCommunism;

public static class EngineTestRegistry 
{
    static Dictionary<string, Type> registry;

    static EngineTestRegistry()
    {
        registry = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.IsDefined(typeof(EngineTestAttribute))).ToDictionary(t =>
        {
            var customAttrib = t.GetCustomAttribute<EngineTestAttribute>();
            if (customAttrib != null && customAttrib.HasName)
                return customAttrib.Name!;
            return t.Name;
        });
    }

    [Conditional("DEBUG")]
    public static void Dump() 
    {
        foreach (var kvp in registry)
        {
            Debug.WriteLine($"{kvp.Key} -> (Type){kvp.Value.Name}");
        }
    }


    public static IModifyableApplication BuildTest(IConfig config)
    {
        var test = config.ReadValue<EngineTest>(EngineTest.CONFIG_KEY);

        if (!registry.TryGetValue(test.Name, out var testType))
        {
            throw new Exception($"Test {test} not found");
           
        }
        return Activate(testType, config).BuildApp();  
    }

    private static void EnsureInherits<T>(Type testActivator)
    {
        var type = typeof(T);
        if (type.IsAssignableFrom(testActivator))
            throw new Exception($"Activator must be subclass of {type}");
    }

    static void EnsureConfigCtor(Type t)
    {
        var info = t.GetConstructor(System.Reflection.BindingFlags.Public, new Type[] { typeof(IConfig) });
        if (info is null)
            throw new Exception($"Test must have a public constructor taking {typeof(IConfig)}");
    }

    static IEngineTest Activate(Type test, IConfig config)
    {
        var activtor = System.Activator.CreateInstance(test, new object[] { config });
        return (activtor as IEngineTest) ?? throw new Exception("Failed to start engine test");
    }

}

