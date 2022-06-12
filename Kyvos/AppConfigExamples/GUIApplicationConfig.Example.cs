using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Kyvos.AppConfigExamples;

public partial class Config
{
    public class Example
    {
        Dictionary<string, object> internalData;
        JsonSerializerOptions options;

        private Example()
        {
            internalData = new Dictionary<string, object>();
            options = CreateDefualtOptions();
        }

        public Example WithSerializeOptions(JsonSerializerOptions options)
        {
            this.options = options;
            return this;
        }

        public Example With(string key, object data)
        {
            if (!internalData.ContainsKey(key))
                this.internalData.Add(key, data);
            return this;
        }

        public void WriteTo(string filePath)
        {
            File.WriteAllText(filePath, JsonSerializer.Serialize(internalData, options));
        }

        public override string ToString()
            => JsonSerializer.Serialize(internalData, options);

        public void LogTo(Action<string> logTo)
            => logTo(JsonSerializer.Serialize(internalData, options));


        internal static Example Builder
            => new();

    }

}




