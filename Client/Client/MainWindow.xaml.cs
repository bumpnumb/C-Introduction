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

        public void setTitle(string str)
        {
            this.Title = str;
        }

        private void loginPwGotFocus(object sender, RoutedEventArgs e)
        {
            if (loginPwBox.Password == "Password")
                loginPwBox.Password = "";
        }

        private void loginPwLostFocus(object sender, RoutedEventArgs e)
        {
            if (loginPwBox.Password == "")
            {
                loginPwBox.Password = "Password";
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            string username = loginIDBox.Text;

        }

        private void loginIDBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (loginIDBox.Text == "Username")
                loginIDBox.Text = "";
        }

        private void loginIDBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (loginIDBox.Text == "")
            {
                loginIDBox.Text = "Username";
            }
        }

        private void KeyDownEvent(object sender, KeyEventArgs e)
        {
            if (loginIDBox.Text != "" && loginIDBox.Text != "Username")
            {
                if (loginPwBox.Password != "" && loginPwBox.Password != "Password")
                {
                    if (e.Key == Key.Enter)
                    {
                        AsynchronousClient.StartClient();
                    }
                }

            }
        }

        private void login(object sender, RoutedEventArgs e)
        {
            AsynchronousClient.StartClient();

        }
    }
}


//  kod för att starta client
//this.Title = "askdndhfjauishi";
