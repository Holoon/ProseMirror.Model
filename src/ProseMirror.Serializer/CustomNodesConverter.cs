using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ProseMirror.Model;
using System.Linq;

namespace ProseMirror.Serializer
{
    public class CustomNodesConverter : JsonConverter
    {
        public Dictionary<string, Func<CustomNode>> CustomNodeSelectors { get; }
        public CustomNodesConverter() => CustomNodeSelectors = new Dictionary<string, Func<CustomNode>>();
        public override bool CanConvert(Type objectType) => typeof(Node).IsAssignableFrom(objectType) && objectType != typeof(StandardNode);
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var json = JObject.Load(reader);
            var typeValue = json.Property("type").Value.ToString();
            if (Node.DefaultNodeType.Contains(typeValue))
                return serializer.Deserialize(json.CreateReader(), typeof(StandardNode));

            if (!CustomNodeSelectors.ContainsKey(typeValue))
                return serializer.Deserialize(json.CreateReader(), typeof(StandardNode));

            var instance = CustomNodeSelectors[typeValue]?.Invoke();
            serializer.Populate(json.CreateReader(), instance);
            return instance;
        }
        public override bool CanWrite => false;
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) 
            => throw new NotSupportedException($"It is not necessary to use {nameof(CustomNodesConverter)} when serializing.");
    }
}
