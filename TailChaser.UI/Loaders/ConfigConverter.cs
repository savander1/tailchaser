using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TailChaser.Entity.Configuration;
using TailChaser.UI.ViewModels;

namespace TailChaser.UI.Loaders
{
    public class ConfigConverter
    {
        public static MainWindowViewModel ConvertToViewModel(Container configuration)
        {
            if (configuration.Name.Equals(Constants.RootContainer))
            {
                
            }
            var viewModel = new MainWindowViewModel();

            var iterator = configuration.Children.GetEnumerator();
            while (iterator.MoveNext())
            {
                var child = iterator.Current;
                if (typeof (Container) == child.GetType())
                {
                    
                }
            }
        }

        private static void BuildViewModel(Container container, MainWindowViewModel viewModel)
        {
            var iterator = container.Children.GetEnumerator();
            while (iterator.MoveNext())
            {
                var child = iterator.Current;
                if (typeof (Container) == child.GetType())
                {
                    BuildViewModel(container, viewModel);
                }
                else
                {
                    var fileCommandViewModel = n
                }
            }
        }

        public static Container ConvertToEntity(MainWindowViewModel viewModel)
        {
            
        }
    }
}
