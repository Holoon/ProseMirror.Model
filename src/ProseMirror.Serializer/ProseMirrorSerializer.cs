using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using ProseMirror.Model;
using System.Collections.Generic;

namespace ProseMirror.Serializer
{
    public class ProseMirrorSerializer
    {
        public static string Serialize(Node proseMirrorNode) => JsonConvert.SerializeObject(proseMirrorNode, new JsonSerializerSettings
        {
            Formatting = Formatting.Indented,
            NullValueHandling = NullValueHandling.Ignore,
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            Converters = new List<JsonConverter>() { new StringEnumConverter() }
        });
        public static Node Deserialize(string jSon) => JsonConvert.DeserializeObject<Node>(jSon, new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            Converters = new List<JsonConverter>() { new StringEnumConverter() }
        });
    }
}
