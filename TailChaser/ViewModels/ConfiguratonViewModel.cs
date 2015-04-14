using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TailChaser.Entity;

namespace TailChaser.ViewModels
{
    public class ConfiguratonViewModel : WorkspaceViewModel
    {
        private readonly Configuration _configuration;

        public ConfiguratonViewModel(Configuration configuration) :base()
        {
            _configuration = configuration;
        }
    }
}
