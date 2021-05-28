using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using ProseMirror.Model;
using System.Collections.Generic;

namespace ProseMirror.Serializer
{
    public class ProseMirrorSerializer
    {
        public static string Serialize(Node proseMirrorNode, bool indent = false) => JsonConvert.SerializeObject(proseMirrorNode, new JsonSerializerSettings
        {
            Formatting = indent ? Formatting.Indented : Formatting.None,
            NullValueHandling = NullValueHandling.Ignore,
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        });
        public static Node Deserialize(string jSon, params CustomNodeSelector[] customNodeSelectors)
        {
            var customNodesConverter = new CustomNodesConverter(); 
            foreach (var customNodeSelector in customNodeSelectors)
                customNodesConverter.CustomNodeSelectors.Add(customNodeSelector?.NodeType, customNodeSelector?.NodeActivator);
            var converters = new List<JsonConverter>()
            {
                customNodesConverter,
                new InterfaceConverter(typeof(NodeAttributes)),
                new InterfaceConverter(typeof(Marks)),
                new InterfaceConverter(typeof(MarkAttributes))
            };

            return JsonConvert.DeserializeObject<Node>(jSon, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Converters = converters
            });
        }
    }
}
