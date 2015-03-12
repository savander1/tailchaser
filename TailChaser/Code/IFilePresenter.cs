using System.Windows.Documents;

namespace TailChaser.Code
{
    public interface IFilePresenter
    {
        FlowDocument PresentFile();
    }
}