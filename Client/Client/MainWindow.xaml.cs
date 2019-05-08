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
        string ID;
        string PW;
        public MainWindow()
        {
            
            InitializeComponent();
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

        private void Window_KeyDownEvent(object sender, KeyEventArgs e)
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
                        openMainProgramWindow();
                        //AsynchronousClient.StartClient();
                    }
                }

            }
        }

        private void signin(object sender, RoutedEventArgs e)
        {
            ID = IDBox.Text;
            PW = PwBox.Password;
            login(ID, PW);
            //listen for positive response
            openMainProgramWindow();
            // AsynchronousClient.StartClient();
        }

        private void signupBtn(object sender, MouseButtonEventArgs e)
        {
            SignUp sng = new SignUp();

            sng.Top = this.Top;
            sng.Left = this.Left;
            sng.Height = this.Height;
            sng.Width = this.Width;

            App.Current.MainWindow = sng;
            this.Close();
            sng.Show();
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

        public void openMainProgramWindow() {
            PageHolderWindow mnprg = new PageHolderWindow();
            App.Current.MainWindow = mnprg;
            this.Close();
            mnprg.Show();
        }
    }
}


//  kod för att starta client
//this.Title = "askdndhfjauishi";
