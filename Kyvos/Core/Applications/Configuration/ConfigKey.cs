using System.Text.Json;

namespace Kyvos.Core.Applications.Configuration
{
    public struct ConfigKey 
    {
        public const char SeperatorChar = ':';

        public string[] Value { get; init; }


        public JsonElement? Traverse( JsonElement toTraverse ) 
        {
            JsonElement currentElem = toTraverse;
            for (int i = 0; i < Value.Length; i++)
            {
                var elem = Value[i];

                if (!currentElem.TryGetProperty( elem, out currentElem ))
                    return null;
            }
            return currentElem;
        } 

        public static implicit operator ConfigKey(string str)
            => new() { Value = str.Split(SeperatorChar)};

        public static implicit operator ConfigKey ( string[] values )
            => new() { Value = values };

        public static explicit operator string( ConfigKey key )
            => string.Join(SeperatorChar, key.Value);

        public static explicit operator string[]( ConfigKey key )
            => key.Value;

        public override string ToString()
            => $"[{string.Join( ",", Value )}]";
    }

}
