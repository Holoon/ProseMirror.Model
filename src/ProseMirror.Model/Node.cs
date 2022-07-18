using System.Runtime.Serialization;

namespace ProseMirror.Model;

public abstract class Node
{
    public readonly static string[] DefaultNodeType = new string[]
    {
        "doc",
        "paragraph",
        "blockquote",
        "horizontal_rule",
        "horizontalRule",
        "heading",
        "code_block",
        "codeBlock",
        "text",
        "image",
        "hard_break",
        "hardBreak",
        "ordered_list",
        "orderedList",
        "bullet_list",
        "bulletList",
        "listItem"
    };
    public string Type { get; set; }
    [IgnoreDataMember] public NodeType TypeEnum { get => GetEnum(Type); set => SetEnum(value); }
    public Node[] Content { get; set; }
    private static NodeType GetEnum(string type)
    {
        return type switch
        {
            "doc" => NodeType.Doc,
            "paragraph" => NodeType.Paragraph,
            "blockquote" => NodeType.Blockquote,
            "horizontal_rule" => NodeType.HorizontalRule,
            "horizontalRule" => NodeType.HorizontalRule,
            "heading" => NodeType.Heading,
            "code_block" => NodeType.CodeBlock,
            "codeBlock" => NodeType.CodeBlock,
            "text" => NodeType.Text,
            "image" => NodeType.Image,
            "hard_break" => NodeType.HardBreak,
            "hardBreak" => NodeType.HardBreak,
            "ordered_list" => NodeType.OrderedList,
            "orderedList" => NodeType.OrderedList,
            "bullet_list" => NodeType.BulletList,
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
