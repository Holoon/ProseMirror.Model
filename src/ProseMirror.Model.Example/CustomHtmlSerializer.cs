using ProseMirror.Serializer.Html;
using System.Collections.Generic;
using System.Linq;
using HtmlBuilder;

namespace ProseMirror.Model.Example
{
    public class CustomHtmlSerializer : HtmlSerializer
    {
        private readonly Dictionary<object, Reference[]> _ReferencesByNode = new();
        public string ToHtml<TNode>(TNode node, Reference[] references)
            where TNode : Node
        {
            _ReferencesByNode.Add(node, references);
            return base.ToHtml(node);
        }
        protected override Html MapCustom<TNode>(CustomNode custom, TNode rootNode) 
        {
            if (custom is ReferenceNode referenceNode)
                return MapReferenceNode(referenceNode, rootNode);

            return base.MapCustom(custom, rootNode);
        }
        private Html MapReferenceNode<TNode>(ReferenceNode referenceNode, TNode rootNode)
            where TNode : Node
        {
            var reference = _ReferencesByNode[rootNode].FirstOrDefault(r => r.ReferenceDocUid == referenceNode.Attrs.ReferenceDocUid);
            var tag = Html.TextBlock(reference?.Text, new InlineTag("strong", KeyValuePair.Create("class", "reference")));
            return tag;
        }
    }
}
