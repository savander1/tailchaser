using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using TailChaser.Entity;

namespace TailChaser.UI.UiHelpers
{
    public class ContextMenuHelper
    {
        public static ContextMenu GetContextMenu(object element, RoutedEventHandler clickHandler)
        {
            var contextMenu = new ContextMenu();
            var add = new MenuItem { Header = ContextMenuButtonType.Add.ToString(), CommandParameter = element };
            add.Click += clickHandler;
            var delete = new MenuItem { Header = ContextMenuButtonType.Delete.ToString(), CommandParameter = element };
            delete.Click += clickHandler;
            var settings = new MenuItem { Header = ContextMenuButtonType.Settings.ToString(), CommandParameter = element };
            settings.Click += clickHandler;

            if (element.GetType() == typeof(Machine) || element.GetType() == typeof(Group))
            {
                contextMenu.Items.Add(add);
                contextMenu.Items.Add(delete);
            }
            else if (element.GetType() == typeof(TailedFile))
            {
                contextMenu.Items.Add(settings);
                contextMenu.Items.Add(delete);
            }

            return contextMenu;
        }


    }
}
