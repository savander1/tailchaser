using System.Windows.Documents;

namespace TailChaser.UI
{
    public interface IFilePresenter
    {
        FlowDocument PresentFile();
    }
}