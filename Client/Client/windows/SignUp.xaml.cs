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
using Client.services;

namespace Client.windows
{
    /// <summary>
    /// Interaction logic for SignUp.xaml
    /// </summary>
    public partial class SignUp : Window
    {
        public SignUp()
        {
            InitializeComponent();
        }
        private void Pw1GotFocus(object sender, RoutedEventArgs e)
        {
            if (PwBox1.Password == "Password")
                PwBox1.Password = "";
        }
        private void Pw2GotFocus(object sender, RoutedEventArgs e)
        {
            if (PwBox2.Password == "Password")
                PwBox2.Password = "";
        }

        private void Pw1LostFocus(object sender, RoutedEventArgs e)
        {
            if (PwBox1.Password == "")
            {
                PwBox1.Password = "Password";
            }
        }
        private void Pw2LostFocus(object sender, RoutedEventArgs e)
        {
            if (PwBox2.Password == "")
            {
                PwBox2.Password = "Password";
            }
        }

        private void IDBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (IDBox.Text == "Username")
                IDBox.Text = "";
        }

        private void IDBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (IDBox.Text == "")
            {
                IDBox.Text = "Username";
            }
        }

        private void Window_KeyDownEvent(object sender, KeyEventArgs e)
        {
            if (IDBox.Text != "" && IDBox.Text != "Username")
            {
                if (PwBox1.Password != "" && PwBox1.Password != "Password" && PwBox2.Password != "" && PwBox2.Password != "Password")
                {
                    if (e.Key == Key.Enter)
                    {
                        AsynchronousClient.StartClient();
                    }
                }

            }
        }

        private void signup(object sender, RoutedEventArgs e)
        {
            AsynchronousClient.StartClient();

        }

        private void signinBtn(object sender, MouseButtonEventArgs e)
        {

            MainWindow mw = new MainWindow();

            mw.Top = this.Top;
            mw.Left = this.Left;
            mw.Height = this.Height;
            mw.Width = this.Width;

            App.Current.MainWindow = mw;
            this.Close();
            mw.Show();

        }
    }
}
