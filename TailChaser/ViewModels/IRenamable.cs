namespace TailChaser.ViewModels
{
    public interface IRenamable
    {
        bool IsRenaming { get; set; }
        bool IsRenameable { get;}
    }
}