using System;
using ProseMirror.Model;

namespace ProseMirror.Serializer
{
    public abstract class CustomNodeSelector
    {
        protected CustomNodeSelector(string nodeType, Func<CustomNode> nodeActivator)
        {
            NodeType = nodeType;
            NodeActivator = nodeActivator;
        }
        public string NodeType { get; set; }
        public Func<CustomNode> NodeActivator { get; set; }
    }
    public class CustomNodeSelector<TNode> : CustomNodeSelector
        where TNode : CustomNode, new()
    {
        public CustomNodeSelector(string nodeType) : base(nodeType, () => new TNode()) { }
    }
}
