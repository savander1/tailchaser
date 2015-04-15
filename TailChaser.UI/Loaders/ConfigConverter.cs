using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TailChaser.Entity;
using TailChaser.Entity.Configuration;
using TailChaser.UI.Exceptions;
using TailChaser.UI.ViewModels;
using TailChaser.UI.ViewModels.CommandPane;

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
                CommandPaneItemViewModel item;
                if (typeof (Container) == child.GetType())
                {
                    item = new ContainerCommandPaneItemViewModel(child); 
                }
                else 
                {
                    item = new FileCommandPaneItemViewModel(child);
                }
                viewModel.CommandPane.AddItem(item);
            }

            return viewModel;
        }

        public static Container ConvertToEntity(MainWindowViewModel viewModel)
        {
            if (viewModel == null) throw new ArgumentNullException("viewModel");

            var container = new Container();

            foreach (var item in viewModel.CommandPane.CommandPaneItems)
            {
                container.AddChild(item.Item);
            }

            return container;
        }
    }
}
