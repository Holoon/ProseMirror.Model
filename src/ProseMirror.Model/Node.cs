using System.Runtime.Serialization;

namespace ProseMirror.Model
{
    public class Node
    {
        public NodeAttributes Attrs { get; set; }
        public string Type { get; set; }
        [IgnoreDataMember] public NodeType TypeEnum { get => GetEnum(Type); set => SetEnum(value); }
        public Node[] Content { get; set; }
        public Marks[] Marks { get; set; }
        public string Text { get; set; }
        private static NodeType GetEnum(string type)
        {
            return type switch
            {
                "doc" => NodeType.Doc,
                "paragraph" => NodeType.Paragraph,
                "blockquote" => NodeType.Blockquote,
                "horizontal_rule" => NodeType.HorizontalRule,
                "heading" => NodeType.Heading,
                "code_block" => NodeType.CodeBlock,
                "text" => NodeType.Text,
                "image" => NodeType.Image,
                "hard_break" => NodeType.HardBreak,
                "orderedList" => NodeType.OrderedList,
                "bulletList" => NodeType.BulletList,
                "listItem" => NodeType.ListItem,
                _ => NodeType.Custom
            };
        }
        private NodeType SetEnum(NodeType type)
        {
#pragma warning disable CS8509 // NOTE: We do not want to set the string Type property if the NodeType is custom. 
            Type = type switch
#pragma warning restore CS8509 
            {
                NodeType.Doc => "doc",
                NodeType.Paragraph => "paragraph",
                NodeType.Blockquote => "blockquote",
                NodeType.HorizontalRule => "horizontal_rule",
                NodeType.Heading => "heading",
                NodeType.CodeBlock => "code_block",
                NodeType.Text => "text",
                NodeType.Image => "image",
                NodeType.HardBreak => "hard_break",
                NodeType.OrderedList => "orderedList",
                NodeType.BulletList => "bulletList",
                NodeType.ListItem => "listItem"
            };
            return type;
        }
    }
}
