namespace ProseMirror.Model
{
    public class StandardNode : Node
    {
        public virtual NodeAttributes Attrs { get; set; }
        public virtual Marks[] Marks { get; set; }
        public string Text { get; set; }
    }
}
