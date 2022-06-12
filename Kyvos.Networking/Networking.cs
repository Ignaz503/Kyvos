using Kyvos.Core.Logging;
using Kyvos.Utility;
using System.Runtime.InteropServices;

namespace Kyvos.Networking;

internal static class EnetNetworking
{
   
    static ReferenceCounter refCounter;

    static EnetNetworking()
    {
        refCounter = new ReferenceCounter(0);
    }

    public static void Initialize()
    {
        var c = refCounter.Increment();
        if (c == 1)//only initialize once or crash           
        {
            //var callbacks = new Callbacks(Alloc,Free,NoMemory);
            //if(ENet.Library.Initialize(callbacks))
            if(ENet.Library.Initialize())
                Log.Information("Initialized ENet");
        }
    }

    public static void Shutdown()
    {
        var c = refCounter.Decrement();
        if (c == 0)
            ENet.Library.Deinitialize();
    }

    static IntPtr Alloc(IntPtr size) 
    {
        Log.Information("Allocating {size} bytes", size);
        return Marshal.AllocHGlobal(size);
    }

    static void Free(IntPtr ptr) 
    {
        Log.Information($"Freeing some memory");    
        Marshal.FreeHGlobal(ptr);
    }

    static void NoMemory() 
    {
        Log.Information("out of memory");
        throw new OutOfMemoryException();
    }

}
