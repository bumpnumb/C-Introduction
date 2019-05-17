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
using Client.services;
using Client.windows;
using Newtonsoft.Json;

namespace Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            App.MainWindowRef = this;
            this.Main.Navigate(new SignInPage());
            CenterWindowOnScreen();
        }

        public void CenterWindowOnScreen() {
            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            double windowWidth = this.Width;
            double windowHeight = this.Height;
            this.Left = (screenWidth / 2) - (windowWidth / 2);
            this.Top = (screenHeight / 2) - (windowHeight / 2);
        }

        public void pageSwitcher(Page pg)
        {

            //App.MainWindowRef.Height = 850;
            //App.MainWindowRef.Width = 1050;
            App.MainWindowRef.Main.Navigate(pg);
            App.MainWindowRef.MinHeight = 850;
            App.MainWindowRef.MinWidth = 1050;
            App.MainWindowRef.CenterWindowOnScreen();
         
        }

        public void setActiveUser(Response activeUser)
        {
            this.activeAs.Content = "Active as: ";
            this.loggedInLabel.Content = "Logout";
            this.usernameLabel.Content = activeUser.user.Name;
        }

        private void LoggedInLabel_MouseEnter(object sender, MouseEventArgs e)
        {
            //highlight text funktion here
            loggedInLabel.Foreground = new SolidColorBrush(Colors.Gray);
        }

        private void LoggedInLabel_MouseLeave(object sender, MouseEventArgs e)
        {
            //stop highlighting text funktion here
            loggedInLabel.Foreground = new SolidColorBrush(Colors.Black);
        }

        private void LoggedInLabel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //Add logout function here
            Message quit = new Message();
            quit.Data = "quit";
            ClientControll.Send(quit);
        }
    }
}


