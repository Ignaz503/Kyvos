using System;

namespace Kyvos.ECS.Resources;

public class NoMangmentOptOuts : IResourceManagmentOptOuts
{
    public static readonly NoMangmentOptOuts Instance = new();

    private NoMangmentOptOuts() 
    { }


    public int Count => 0;

    public void Add(Type t)
        => throw new InvalidOperationException("Can't add opt out to no resource managment optout handler");
    

    public bool HasOptedOut(Type t)
        => false;
    
}

