using System.IO;
using ProseMirror.Serializer;

namespace ProseMirror.Model.Example;

static class Program
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0059:Useless assignation", Justification = "Exemple class")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Minor Code Smell", "S1481:Unused local variables should be removed", Justification = "Exemple class")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Unused parameter", Justification = "Exemple class")]
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
