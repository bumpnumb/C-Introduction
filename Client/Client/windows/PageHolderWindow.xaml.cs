using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Client.windows
{
    /// <summary>
    /// Interaction logic for MainProgram.xaml
    /// </summary>
    public partial class PageHolderWindow : Window
    {
        public PageHolderWindow() {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;

            InitializeComponent();
            Main.Content = new AdminMainPage();
        }

        private static void SwitchWindow(string newPage) {
            if(newPage == "Create")
                Main.Content = new Create();
        }


    }
}
