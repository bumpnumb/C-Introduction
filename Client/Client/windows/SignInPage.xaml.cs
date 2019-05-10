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
        string ID;
        string PW;
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

        private void signin(object sender, RoutedEventArgs e)
        {
            if (IDBox.Text != "" && IDBox.Text != "Username")
            {
                if (PwBox.Password != "" && PwBox.Password != "Password")
                {
                    ID = IDBox.Text;
                    PW = PwBox.Password;
                    login(ID, PW);
                    //listen for positive response
                    //if ()
                        openMainProgramWindow();
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
            loginMsg.Data = id + "=;=" + pw; //DONT FORGET TO ADD RESTRICTIONS TO NAMING
            ClientControll.Send(loginMsg);
            /*Fixa en snygg json grej här för ID och PW så det går att ha vilket namn som helst
            "{
                'ID': id,
                'PW': pw
            }"*/

        }

        //private void listenForResponse() {
        //    Response responseMsg = new Response();
        //    responseMsg.Type = MessageType.Login;
        //    responseMsg.Data = "asdasd";
        //    responseMsg.user = 

        //    //ClientControll.Listen();
        //}

        public void openMainProgramWindow()
        {

            App.MainWindowRef.Height = 768;
            App.MainWindowRef.Width = 1028;
            App.MainWindowRef.Main.Navigate(new AdminMainPage());

        }

        private void Key_Down_Event(object sender, KeyEventArgs e)
        {
            if (IDBox.Text != "" && IDBox.Text != "Username")
            {
                if (PwBox.Password != "" && PwBox.Password != "Password")
                {
                    if (e.Key == Key.Enter)
                    {
                        ID = IDBox.Text;
                        PW = PwBox.Password;
                        login(ID, PW);
                        //listen for positive response
                        //if(listenForResponse())
                        //App.ResponseRef.HandleResponse(); fixa den här handlern;;;;; ATT GÖRA OVER HERE <-----------------------------------------------------
                        openMainProgramWindow();
                    }
                }

            }
        }
    }
}
