using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TailChaser.Entity.Configuration;
using TailChaser.UI.Exceptions;
using TailChaser.UI.ViewModels;

namespace TailChaser.UI.Loaders
{
    public class ConfigConverter
    {
        public static MainWindowViewModel ConvertToViewModel(Container configuration)
        {
            if (configuration == null) throw new ArgumentNullException("configuration");

            if (!configuration.Name.Equals(Constants.RootContainer))
            {
                throw new InvalidConfigurationException(new Exception("Supplied Container is not the Root"));
            }

            var viewModel = new MainWindowViewModel();

            var iterator = configuration.Children.GetEnumerator();
            while (iterator.MoveNext())
            {
                var child = iterator.Current;
                if (typeof (Container) == child.GetType())
                {
                    var item = new CommandPaneItemViewModel(child.Id);
                    item.AddCommand(new DeleteCommandViewModel(command: ));
                    viewModel.CommandPane.AddItem(new CommandPaneItemViewModel(child.Id));
                }
            }
        }

      

        public static Container ConvertToEntity(MainWindowViewModel viewModel)
        {
            
        }
    }
}
