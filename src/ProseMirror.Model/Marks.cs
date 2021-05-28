using System.Runtime.Serialization;

namespace ProseMirror.Model
{
    public class Marks
    {
        public MarkAttributes Attrs { get; set; }
        public string Type { get; set; }
        [IgnoreDataMember] public MarkType TypeEnum { get => GetEnum(Type); set => SetEnum(value); }
        private static MarkType GetEnum(string type)
        {
            return type switch
            {
                "bold" => MarkType.Bold,
                "italic" => MarkType.Italic,
                "link" => MarkType.Link,
                "strike" => MarkType.Strike,
                "em" => MarkType.Em,
                "strong" => MarkType.Strong,
                "code" => MarkType.Code,
                _ => MarkType.Custom
            };
        }
        private MarkType SetEnum(MarkType type)
        {
#pragma warning disable CS8509 // NOTE: We do not want to set the string Type property if the NodeType is custom. 
            Type = type switch
#pragma warning restore CS8509 
            {
                MarkType.Bold => "bold",
                MarkType.Italic => "italic",
                MarkType.Link => "link",
                MarkType.Strike => "strike",
                MarkType.Em => "em",
                MarkType.Strong => "strong",
                MarkType.Code => "code"
            };
            return type;
        }
    }
}
