﻿using Client.services;
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
    /// Interaction logic for SignUpPage.xaml
    /// </summary>
    public partial class SignUpPage : Page
    {
        public SignUpPage()
        {
            InitializeComponent();
            App.MainWindowRef.CenterWindowOnScreen();
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

        private void SSNBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (SSNBox.Text == "YYYY-MM-DD-XXXX")
                SSNBox.Text = "";
        }

        private void SSNBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (SSNBox.Text == "")
                SSNBox.Text = "YYYY-MM-DD-XXXX";

        }

        private void signup(object sender, RoutedEventArgs e)
        {
            if (PwBox1.Password == PwBox2.Password && PwBox1.Password != "" && PwBox1.Password != "Password" && PwBox2.Password != "" && PwBox2.Password != "Password")
            {
                string ID = IDBox.Text;
                string SSN = SSNBox.Text;
                string PW = PwBox1.Password;
                register(ID, SSN, PW);
                App.MainWindowRef.Main.Navigate(new SignInPage());
            }
        }

        private void signinBtn(object sender, MouseButtonEventArgs e)
        {

            App.MainWindowRef.Main.Navigate(new SignInPage());



        }

        private void register(string id, string SSN, string pw)
        {
            Message registerMsg = new Message();
            registerMsg.Type = MessageType.Register;
            registerMsg.Data = id + "\r\n" + SSN + "\r\n" + pw; //DONT FORGET TO ADD RESTRICTIONS TO NAMING
            ClientControll.Send(registerMsg);
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
                if (PwBox1.Password != "" && PwBox1.Password != "Password" && PwBox2.Password != "" && PwBox2.Password != "Password" && PwBox1.Password == PwBox2.Password)
                {
                    if (e.Key == Key.Enter)
                    {
                        string ID = IDBox.Text;
                        string PW = PwBox1.Password;
                        string SSN = SSNBox.Text;
                        register(ID, SSN, PW);
                        App.MainWindowRef.Main.Navigate(new SignInPage());

                    }
                }

            }
        }

        private void SSNBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SSNBox.Text.Length == 4 || SSNBox.Text.Length == 7 || SSNBox.Text.Length == 10 )
            {
                SSNBox.Text += '-';
                SSNBox.CaretIndex = SSNBox.Text.Length;
                //1997-09-19-4899
            }
        }
    }
}
