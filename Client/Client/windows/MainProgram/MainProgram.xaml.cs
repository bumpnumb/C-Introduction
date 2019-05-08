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

namespace Client.windows.MainProgram
{
    /// <summary>
    /// Interaction logic for MainProgram.xaml
    /// </summary>
    public partial class PageHolderWindow : Window {
        public PageHolderWindow() {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;

            InitializeComponent();
            Switcher.pageSwitcher = this;
            Switcher.Switch(new Edit());
        }

        private void InitializeComponent() {
            throw new NotImplementedException();
        }

        public void Navigate(UserControl nextPage) {
            this.Content = nextPage;
        }

        public void Navigate(UserControl nextPage, object state) {
            this.Content = nextPage;
            SwitchInterface s = nextPage as SwitchInterface;

            if (s != null)
                s.UtilizeState(state);
            else
                throw new ArgumentException("NextPage is not ISwitchable! "
                  + nextPage.Name.ToString());
        }
    }
}
