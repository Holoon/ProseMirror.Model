using System.IO;
using Newtonsoft.Json;
using ProseMirror.Serializer;

namespace ProseMirror.Model.Example
{
    class Program
    {
        static void Main(string[] args)
        {
            var json = File.ReadAllText("example.json");
            var proseMirrorNode = ProseMirrorSerializer.Deserialize(json, new CustomNodeSelector<ReferenceNode>("reference"));
            var result = ProseMirrorSerializer.Serialize(proseMirrorNode, true);
        }
    }
    public class Container
    {
        
        public Container()
        {
            
        }
    }
    public class ReferenceAttributes : NodeAttributes
    {
        public string DecorationChar { get; set; }
        public string ReferenceDocUid { get; set; }
        public string Text { get; set; }
        public bool? Unknown { get; set; }
        public int? AssignmentId { get; set; }
    }
    public class ReferenceNode : CustomNode
    {
        [JsonConverter(typeof(InterfaceConverter), typeof(ReferenceAttributes))]
        public override INodeAttributes Attrs { get; set; }
    }
}
