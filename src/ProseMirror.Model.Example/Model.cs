namespace ProseMirror.Model.Example;

public class Container
{
    public Node Content { get; set; }
    public Reference[] References { get; set; }
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
public class Reference
{
    public string Text { get; set; }
    public string ReferenceDocUid { get; set; }
    public object[] Entities { get; set; }
}
