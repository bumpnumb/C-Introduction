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

namespace Client.windows
{
    /// <summary>
    /// Interaction logic for Edit.xaml
    /// </summary>
    public partial class AdminMainPage : Page
    {
        public AdminMainPage() {

            InitializeComponent();
            App.MainWindowRef.CenterWindowOnScreen();
        }

        private void Edit_Create_Btn(object sender, RoutedEventArgs e) {

            App.MainWindowRef.Main.Navigate(new CreateAndEditPage());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Message loginMsg = new Message();
            loginMsg.Type = MessageType.Competition;
            loginMsg.Data = "GetActive"; //DONT FORGET TO ADD RESTRICTIONS TO NAMING
            ClientControll.Send(loginMsg);
        }

        public void FillCompetitionDataBox(string rsp)
        {
            this.competitionDataBox.Text = rsp;
            
        }
    }
}
//            /*Fixa en snygg json grej här för ID och PW så det går att ha vilket namn som helst
//            "{
//                'ID': id,
//                'PW': pw
//        }
//    }
//}
