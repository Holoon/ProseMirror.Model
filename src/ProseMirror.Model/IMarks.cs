namespace ProseMirror.Model
{
    public interface IMarks
    {
        IMarkAttributes Attrs { get; set; }
        string Type { get; set; }
        MarkType TypeEnum { get; set; }
    }
}
