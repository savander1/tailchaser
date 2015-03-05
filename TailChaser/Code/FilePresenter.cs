using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using TailChaser.Entity;

namespace TailChaser.Code
{
    public class FilePresenter : IFilePresenter
    {
        private readonly FilePresentationSettings _settings;

        public FilePresenter(FilePresentationSettings settings)
        {
            _settings = settings;
        }

        public FlowDocument PresentFile(string contents)
        {
            return new FlowDocument();
        }
    }
}
