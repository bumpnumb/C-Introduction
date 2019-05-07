using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.windows.MainProgram
{
    class Switcher
    {
        public static PageHolderWindow pageSwitcher;

        public static void Switch(UserControl newPage) {
            pageSwitcher.Navigate(newPage);
        }

        public static void Switch(UserControl newPage, object state) {
            pageSwitcher.Navigate(newPage, state);
        }
    }
}
