using System;
using System.IO;

namespace ProseMirror.Model.Example
{
    class Program
    {
        static void Main(string[] args)
        {
            var json = File.ReadAllText("example.json");
            var proseMirrorNode = Serializer.ProseMirrorSerializer
                .Deserialize(json);
            var result = Serializer.ProseMirrorSerializer
                .Serialize(proseMirrorNode);
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
}
