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
        private string _selectedItem;

        private ObservableCollection<string> _items = new ObservableCollection<string>()
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
            get { return _items; }
        }

        public string SelectedItem {
            get { return _selectedItem; }
            set {
                _selectedItem = value;
                OnPropertyChanged("SelectedItem");
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
                    _items.Add(value);
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
    }
}

