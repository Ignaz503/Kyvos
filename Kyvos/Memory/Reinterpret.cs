using System.Runtime.CompilerServices;

namespace Kyvos.Memory
{
    public static partial class Reinterpret
    {
        public static ToValue As<FromData, ToValue>( FromData b )
        {
            return Unsafe.As<FromData, ToValue>( ref b );
        }

        public static ToValue As<FromData, ToValue>( FromData[] b )
        {
            return Unsafe.As<FromData[], ToValue>( ref b );
        }
    }
}
