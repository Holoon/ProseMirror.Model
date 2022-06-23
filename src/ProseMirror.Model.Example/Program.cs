using System;
using System.IO;
using System.Text.Json;
using ProseMirror.Serializer;
using ProseMirror.Serializer.SystemTextJson.Json;

namespace ProseMirror.Model.Example
{
    class Program
    {
        static void Main(string[] args)
        {
            // NOTE: C# object <-> JSon example
            // var json = File.ReadAllText("example.json");
            // var proseMirrorNode = Serializer.JSon.JSonSerializer.Deserialize<Container>(json, new CustomNodeSelector<ReferenceNode>("reference"));
            // var resultJSon = Serializer.JSon.JSonSerializer.Serialize(proseMirrorNode, true);
            // Console.WriteLine(resultJSon);
            
            var json = File.ReadAllText("example.json");
            var proseMirrorNode = ProseMirrorJsonSerializer.Deserialize<Container>(json, new ProseMirror.Serializer.SystemTextJson.CustomNodeSelector<ReferenceNode>("reference"));
            //var proseMirrorNode = System.Text.Json.JsonSerializer.Deserialize<Container>(json, new JsonSerializerOptions().UseProseMirror(false, false));
            var resultJSon = ProseMirrorJsonSerializer.Serialize(proseMirrorNode, true);
            Console.WriteLine(resultJSon);

            // NOTE: C# object -> HTML example
            var resultHtml = new CustomHtmlSerializer().ToHtml(proseMirrorNode.Content, proseMirrorNode.References);
            Console.WriteLine(resultHtml);
        }
    }
}
