namespace ProseMirror.Serializer.SystemTextJson;

public class CustomNodeSelector
{
    public CustomNodeSelector(string nodeType, Type nodeClassType)
    {
        NodeType = nodeType;
        NodeClassType = nodeClassType;
    }
    public string NodeType { get; set; }
    public Type NodeClassType { get; }
}

public class CustomNodeSelector<TNode>:CustomNodeSelector where TNode : CustomNode, new()
{
    public CustomNodeSelector(string nodeType): base(nodeType, typeof(TNode))
    {
    }
}