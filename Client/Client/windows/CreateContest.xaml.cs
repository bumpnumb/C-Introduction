using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace Client.windows
{
    /// <summary>
    /// Interaction logic for Create.xaml
    /// </summary>
    public partial class CreateContest : Page
    {
        private string selectedHeigt;

        private ObservableCollection<string> items = new ObservableCollection<string>()
        {
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
        };
        public CreateContest()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            App.MainWindowRef.Main.Navigate(new AdminMainPage());
        }

        public IEnumerable Items {
            get { return items; }
        }

        public string SelectedItem {
            get { return selectedHeigt; }
            set {
                selectedHeigt = value;
                OnPropertyChanged("SelectedHeight");
            }
        }

        public string NewItem {
            set {
                if (SelectedItem != null)
                {
                    return;
                }
                if (!string.IsNullOrEmpty(value))
                {
                    items.Add(value);
                    SelectedItem = value;
                }
            }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            var handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void JumpHeight_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (SelectedItem)
            {
                case "4":
                    jump1.Visibility = Visibility.Visible;
                    jump2.Visibility = Visibility.Visible;
                    jump3.Visibility = Visibility.Visible;
                    jump4.Visibility = Visibility.Visible;
                    jump5.Visibility = Visibility.Hidden;
                    jump6.Visibility = Visibility.Hidden;
                    jump7.Visibility = Visibility.Hidden;
                    jump8.Visibility = Visibility.Hidden;
                    jump9.Visibility = Visibility.Hidden;
                    jump10.Visibility = Visibility.Hidden;
                    jump1Height.Visibility = Visibility.Visible;
                    jump2Height.Visibility = Visibility.Visible;
                    jump3Height.Visibility = Visibility.Visible;
                    jump4Height.Visibility = Visibility.Visible;
                    jump5Height.Visibility = Visibility.Hidden;
                    jump6Height.Visibility = Visibility.Hidden;
                    jump7Height.Visibility = Visibility.Hidden;
                    jump8Height.Visibility = Visibility.Hidden;
                    jump9Height.Visibility = Visibility.Hidden;
                    jump10Height.Visibility = Visibility.Hidden;
                    break;
                case "5":
                    jump1.Visibility = Visibility.Visible;
                    jump2.Visibility = Visibility.Visible;
                    jump3.Visibility = Visibility.Visible;
                    jump4.Visibility = Visibility.Visible;
                    jump5.Visibility = Visibility.Visible;
                    jump6.Visibility = Visibility.Hidden;
                    jump7.Visibility = Visibility.Hidden;
                    jump8.Visibility = Visibility.Hidden;
                    jump9.Visibility = Visibility.Hidden;
                    jump10.Visibility = Visibility.Hidden;
                    jump1Height.Visibility = Visibility.Visible;
                    jump2Height.Visibility = Visibility.Visible;
                    jump3Height.Visibility = Visibility.Visible;
                    jump4Height.Visibility = Visibility.Visible;
                    jump5Height.Visibility = Visibility.Visible;
                    jump6Height.Visibility = Visibility.Hidden;
                    jump7Height.Visibility = Visibility.Hidden;
                    jump8Height.Visibility = Visibility.Hidden;
                    jump9Height.Visibility = Visibility.Hidden;
                    jump10Height.Visibility = Visibility.Hidden;
                    break;
                case "6":
                    jump1.Visibility = Visibility.Visible;
                    jump2.Visibility = Visibility.Visible;
                    jump3.Visibility = Visibility.Visible;
                    jump4.Visibility = Visibility.Visible;
                    jump5.Visibility = Visibility.Visible;
                    jump6.Visibility = Visibility.Visible;
                    jump7.Visibility = Visibility.Hidden;
                    jump8.Visibility = Visibility.Hidden;
                    jump9.Visibility = Visibility.Hidden;
                    jump10.Visibility = Visibility.Hidden;
                    jump1Height.Visibility = Visibility.Visible;
                    jump2Height.Visibility = Visibility.Visible;
                    jump3Height.Visibility = Visibility.Visible;
                    jump4Height.Visibility = Visibility.Visible;
                    jump5Height.Visibility = Visibility.Visible;
                    jump6Height.Visibility = Visibility.Visible;
                    jump7Height.Visibility = Visibility.Hidden;
                    jump8Height.Visibility = Visibility.Hidden;
                    jump9Height.Visibility = Visibility.Hidden;
                    jump10Height.Visibility = Visibility.Hidden;
                    break;
                case "7":
                    jump1.Visibility = Visibility.Visible;
                    jump2.Visibility = Visibility.Visible;
                    jump3.Visibility = Visibility.Visible;
                    jump4.Visibility = Visibility.Visible;
                    jump5.Visibility = Visibility.Visible;
                    jump6.Visibility = Visibility.Visible;
                    jump7.Visibility = Visibility.Visible;
                    jump8.Visibility = Visibility.Hidden;
                    jump9.Visibility = Visibility.Hidden;
                    jump10.Visibility = Visibility.Hidden;
                    jump1Height.Visibility = Visibility.Visible;
                    jump2Height.Visibility = Visibility.Visible;
                    jump3Height.Visibility = Visibility.Visible;
                    jump4Height.Visibility = Visibility.Visible;
                    jump5Height.Visibility = Visibility.Visible;
                    jump6Height.Visibility = Visibility.Visible;
                    jump7Height.Visibility = Visibility.Visible;
                    jump8Height.Visibility = Visibility.Hidden;
                    jump9Height.Visibility = Visibility.Hidden;
                    jump10Height.Visibility = Visibility.Hidden;
                    break;
                case "8":
                    jump1.Visibility = Visibility.Visible;
                    jump2.Visibility = Visibility.Visible;
                    jump3.Visibility = Visibility.Visible;
                    jump4.Visibility = Visibility.Visible;
                    jump5.Visibility = Visibility.Visible;
                    jump6.Visibility = Visibility.Visible;
                    jump7.Visibility = Visibility.Visible;
                    jump8.Visibility = Visibility.Visible;
                    jump9.Visibility = Visibility.Hidden;
                    jump10.Visibility = Visibility.Hidden;
                    jump1Height.Visibility = Visibility.Visible;
                    jump2Height.Visibility = Visibility.Visible;
                    jump3Height.Visibility = Visibility.Visible;
                    jump4Height.Visibility = Visibility.Visible;
                    jump5Height.Visibility = Visibility.Visible;
                    jump6Height.Visibility = Visibility.Visible;
                    jump7Height.Visibility = Visibility.Visible;
                    jump8Height.Visibility = Visibility.Visible;
                    jump9Height.Visibility = Visibility.Hidden;
                    jump10Height.Visibility = Visibility.Hidden;
                    break;
                case "9":
                    jump1.Visibility = Visibility.Visible;
                    jump2.Visibility = Visibility.Visible;
                    jump3.Visibility = Visibility.Visible;
                    jump4.Visibility = Visibility.Visible;
                    jump5.Visibility = Visibility.Visible;
                    jump6.Visibility = Visibility.Visible;
                    jump7.Visibility = Visibility.Visible;
                    jump8.Visibility = Visibility.Visible;
                    jump9.Visibility = Visibility.Visible;
                    jump10.Visibility = Visibility.Hidden;
                    jump1Height.Visibility = Visibility.Visible;
                    jump2Height.Visibility = Visibility.Visible;
                    jump3Height.Visibility = Visibility.Visible;
                    jump4Height.Visibility = Visibility.Visible;
                    jump5Height.Visibility = Visibility.Visible;
                    jump6Height.Visibility = Visibility.Visible;
                    jump7Height.Visibility = Visibility.Visible;
                    jump8Height.Visibility = Visibility.Visible;
                    jump9Height.Visibility = Visibility.Visible;
                    jump10Height.Visibility = Visibility.Hidden;
                    break;
                case "10":
                    jump1.Visibility = Visibility.Visible;
                    jump2.Visibility = Visibility.Visible;
                    jump3.Visibility = Visibility.Visible;
                    jump4.Visibility = Visibility.Visible;
                    jump5.Visibility = Visibility.Visible;
                    jump6.Visibility = Visibility.Visible;
                    jump7.Visibility = Visibility.Visible;
                    jump8.Visibility = Visibility.Visible;
                    jump9.Visibility = Visibility.Visible;
                    jump10.Visibility = Visibility.Visible;
                    jump1Height.Visibility = Visibility.Visible;
                    jump2Height.Visibility = Visibility.Visible;
                    jump3Height.Visibility = Visibility.Visible;
                    jump4Height.Visibility = Visibility.Visible;
                    jump5Height.Visibility = Visibility.Visible;
                    jump6Height.Visibility = Visibility.Visible;
                    jump7Height.Visibility = Visibility.Visible;
                    jump8Height.Visibility = Visibility.Visible;
                    jump9Height.Visibility = Visibility.Visible;
                    jump10Height.Visibility = Visibility.Visible;
                    break;
            }
        }
    }
}

