namespace TailChaser.Tail.Interfaces
{
    public interface ITail
    {
        void TailFile(string filePath, OnFileUpdated onFileUpdated);
    }

    public delegate void OnFileUpdated();
}
