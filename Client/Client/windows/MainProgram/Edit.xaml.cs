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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Client.windows;

namespace Client.windows.MainProgram
{
    /// <summary>
    /// Interaction logic for Edit.xaml
    /// </summary>
    public partial class Edit : Page
    {
        public Edit() {
            InitializeComponent();
        }

        private void Edit_Create_Btn(object sender, RoutedEventArgs e) {
            PageHolderWindow.Main.Content = new Create();
        }
    }
}
