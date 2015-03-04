namespace TailChaser.Entity.Interfaces
{
    public interface IFileTailerSubject
    {
        void Subscribe(TailedFile file);
        void Unsubscribe();
        void PublishFileChange();
    }
}