using System.IO;
using ProseMirror.Serializer;

namespace ProseMirror.Model.Example
{
    class Program
    {
        static void Main(string[] args)
        {
            var json = File.ReadAllText("example.json");
            var proseMirrorNode = ProseMirrorSerializer.Deserialize<Container>(json, new CustomNodeSelector<ReferenceNode>("reference"));
            var result = ProseMirrorSerializer.Serialize(proseMirrorNode, true);
        }
    }
    public class Container
    {
        public Node Content { get; set; }
        public object[] References { get; set; }
    }
    public class ReferenceAttributes 
    {
        public string DecorationChar { get; set; }
        public string ReferenceDocUid { get; set; }
        public string Text { get; set; }
        public bool? Unknown { get; set; }
        public int? AssignmentId { get; set; }
    }
    public class ReferenceNode : CustomNode
    {
        public ReferenceAttributes Attrs { get; set; }
    }
}
