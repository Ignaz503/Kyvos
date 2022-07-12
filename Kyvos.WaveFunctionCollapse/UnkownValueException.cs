using System.Runtime.Serialization;

namespace Kyvos.WaveFunctionCollapse
{
    [Serializable]
    internal class UnkownValueException<TValue> : Exception
    {
        public UnkownValueException()
        {
        }

        public UnkownValueException(TValue val) : base($"{val} is not a known value")
        {
        }

        public UnkownValueException(string? message) : base(message)
        {
        }

        public UnkownValueException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected UnkownValueException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}