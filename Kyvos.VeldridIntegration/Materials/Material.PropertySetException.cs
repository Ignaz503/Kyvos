using System;
using System.Runtime.Serialization;

namespace Kyvos.VeldridIntegration.Materials;

public partial class Material
{
    public abstract class PropertySetException : MaterialException
    {
        protected PropertySetException()
        {
        }

        protected PropertySetException(string message) : base(message)
        {
        }

        protected PropertySetException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        protected PropertySetException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }

    public class NonExistentPropertyException : PropertySetException 
    {
        public NonExistentPropertyException(string name): base($"Property of name {name} does not exist")
        {

        }
    }

    public class WrongUpdateMethodException<T> : PropertySetException 
        where T: Property
    {
        public WrongUpdateMethodException(string name, Type t) : base($"Property {name} is of type {t}, but needs to be of type {typeof(T)} to be updated by this method.") 
        {}
    }

}

