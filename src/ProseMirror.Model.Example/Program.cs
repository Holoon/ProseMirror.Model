using System.IO;
using ProseMirror.Serializer;

namespace ProseMirror.Model.Example
{
    class Program
    {
        static void Main(string[] args)
        {
            // NOTE: C# object <-> JSon example
            var json = File.ReadAllText("example.json");
            var proseMirrorNode = Serializer.JSon.JSonSerializer.Deserialize<Container>(json, new CustomNodeSelector<ReferenceNode>("reference"));
            var resultJSon = Serializer.JSon.JSonSerializer.Serialize(proseMirrorNode, true);

            // NOTE: C# object -> HTML example
            var resultHtml = new CustomHtmlSerializer().ToHtml(proseMirrorNode.Content, proseMirrorNode.References);
        }
    }
}
