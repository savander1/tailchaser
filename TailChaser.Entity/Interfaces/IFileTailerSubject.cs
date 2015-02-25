namespace TailChaser.Entity.Interfaces
{
    public interface IFileTailerSubject
    {
        void Subscribe(IFileContentObserver contentMaintainer);
        void Unsubscribe();
        void PublishFileChange();
    }
}