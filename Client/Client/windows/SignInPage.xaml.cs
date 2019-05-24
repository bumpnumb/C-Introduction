using Client.services;
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

namespace Client.windows
{
    /// <summary>
    /// Interaction logic for SignInPage.xaml
    /// </summary>
    public partial class SignInPage : Page
    {
        public SignInPage()
        {
            InitializeComponent();
            App.MainWindowRef.CenterWindowOnScreen();
            ClientControll newClient = new ClientControll();
        }
        public void setTitle(string str)
        {
            this.Title = str;
        }

        private void PwGotFocus(object sender, RoutedEventArgs e)
        {
            if (PwBox.Password == "Password")
                PwBox.Password = "";
        }

        private void PwLostFocus(object sender, RoutedEventArgs e)
        {
            if (PwBox.Password == "")
            {
                PwBox.Password = "Password";
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            string username = IDBox.Text;

        }

        private void IDBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (IDBox.Text == "YYYY-MM-DD-XXXX")
                IDBox.Text = "";
        }

        private void IDBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (IDBox.Text == "")
            {
                IDBox.Text = "YYYY-MM-DD-XXXX";
            }
        }

        private void IDBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (IDBox.Text.Length == 4 || IDBox.Text.Length == 7 || IDBox.Text.Length == 10)
            {
                IDBox.Text += '-';
                IDBox.CaretIndex = IDBox.Text.Length;
            }
        }

        private void signin(object sender, RoutedEventArgs e)
        {
            if (IDBox.Text != "" && IDBox.Text != "Username")
            {
                if (PwBox.Password != "" && PwBox.Password != "Password")
                {
                    string ID = IDBox.Text;
                    string PW = PwBox.Password;
                    login(ID, PW);
                }

            }
        }

        private void signupBtn(object sender, MouseButtonEventArgs e)
        {
            App.MainWindowRef.Main.Navigate(new SignUpPage());
           
        }

        private void login(string id, string pw)
        {
            Message loginMsg = new Message();
            loginMsg.Type = MessageType.Login;
            loginMsg.Data = id + "\r\n" + pw; //DONT FORGET TO ADD RESTRICTIONS TO NAMING
            ClientControll.Send(loginMsg);
            /*Fixa en snygg json grej här för ID och PW så det går att ha vilket namn som helst
            "{
                'ID': id,
                'PW': pw
            }"*/

        }

        private void Key_Down_Event(object sender, KeyEventArgs e)
        {
            if (IDBox.Text != "" && IDBox.Text != "Username")
            {
                if (PwBox.Password != "" && PwBox.Password != "Password")
                {
                    if (e.Key == Key.Enter)
                    {
                        string ID = IDBox.Text;
                        string PW = PwBox.Password;
                        login(ID, PW);
                        
                    }
                }

            }
        }
    }
}
