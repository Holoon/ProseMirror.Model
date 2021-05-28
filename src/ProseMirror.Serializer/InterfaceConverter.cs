using Newtonsoft.Json;
using System;

namespace ProseMirror.Serializer
{
    public class InterfaceConverter : JsonConverter
    {
        private readonly Type _ImplementationType;
        public InterfaceConverter(Type implementationType) => _ImplementationType = implementationType;
        public override bool CanConvert(Type objectType) => _ImplementationType != null && objectType != null && objectType.IsAssignableFrom(_ImplementationType) && objectType.IsInterface;
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) => serializer.Deserialize(reader, _ImplementationType);
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) => serializer.Serialize(writer, value);
    }
}
