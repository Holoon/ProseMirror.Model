using ProseMirror.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using HtmlPart = HtmlBuilder.Html;
using HtmlBuilder;

namespace ProseMirror.Serializer.Html
{
    public class HtmlSerializer
    {
        public static string Serialize<TNode>(TNode value)
            where TNode : Node
        {
            return new HtmlSerializer().ToHtml(value);
        }
        public virtual string ToHtml<TNode>(TNode node)
            where TNode : Node
        {
            HtmlPart root = MapRecursively(node, node);
            return root?.ToString();
        }
        private HtmlPart MapRecursively<TNode>(TNode node, TNode rootNode) 
            where TNode : Node
        {
            var tag = node switch
            {
                StandardNode standard => MapStandard(standard, rootNode),
                CustomNode custom => MapCustom(custom, rootNode),
                _ => throw new ArgumentException($"Type \"{node?.GetType()?.Name}\" is not supported for \"{nameof(node)}\""),
            };

            foreach (var child in node?.Content ?? Array.Empty<Node>())
                tag.AddChild(MapRecursively(child, rootNode));

            return tag;
        }
        protected virtual HtmlPart MapStandard<TNode>(StandardNode node, TNode rootNode)
            where TNode : Node
        {
            var tag = node?.TypeEnum switch
            {
                NodeType.Doc => HtmlPart.Paragraph(),
                NodeType.Paragraph => HtmlPart.Paragraph(), 
                NodeType.Blockquote => HtmlPart.Blockquote(),
                NodeType.HorizontalRule => HtmlPart.HorizontalRule(),
                NodeType.Heading => HtmlPart.Heading(node?.Attrs?.Level),
                NodeType.CodeBlock => HtmlPart.CodeBlock(),
                NodeType.Text => HtmlPart.TextBlock(node?.Text, MarksToAttributes(node?.Marks)?.ToArray()),
                NodeType.Image => HtmlPart.Image(node?.Attrs?.Src, node?.Attrs?.Alt, node?.Attrs?.Title),
                NodeType.HardBreak => HtmlPart.HardBreak(),
                NodeType.OrderedList => HtmlPart.OrderedList(),
                NodeType.BulletList => HtmlPart.BulletList(),
                NodeType.ListItem => HtmlPart.ListItem(),
                _ => throw new ArgumentException($"Value \"{node?.TypeEnum}\" is not supported for \"{nameof(node.TypeEnum)}\""),
            };
            return tag;
        }
        protected virtual HtmlPart MapCustom<TNode>(CustomNode custom, TNode rootNode)
            where TNode : Node => HtmlPart.Paragraph();
        private static IEnumerable<InlineTag> MarksToAttributes(Marks[] marks)
        {
            foreach (var mark in marks ?? Array.Empty<Marks>())
                yield return mark?.TypeEnum switch
                {
                    MarkType.Bold => new InlineTag("strong"),
                    MarkType.Strong => new InlineTag("strong"),
                    MarkType.Strike => new InlineTag("s"),
                    MarkType.Em => new InlineTag("em"),
                    MarkType.Italic => new InlineTag("em"),
                    MarkType.Link => new InlineTag("a", KeyValuePair.Create("href", mark?.Attrs?.Href), KeyValuePair.Create("target", mark?.Attrs?.Target)),
                    MarkType.Code => new InlineTag("pre"),
                    _ => throw new ArgumentException($"Value \"{mark?.TypeEnum}\" is not supported for \"{nameof(mark.TypeEnum)}\""),
                };
            yield break;
        }
    }
}