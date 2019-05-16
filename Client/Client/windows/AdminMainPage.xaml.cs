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
        public AdminMainPage()
        {

            InitializeComponent();
            App.MainWindowRef.CenterWindowOnScreen();
        }

        private void Edit_Create_Btn(object sender, RoutedEventArgs e)
        {

            App.MainWindowRef.Main.Navigate(new CreateAndEditPage());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Message getCompetitions = new Message();
            getCompetitions.Type = MessageType.Competition;
            getCompetitions.Data = "GetAll"; //DONT FORGET TO ADD RESTRICTIONS TO NAMING
            ClientControll.Send(getCompetitions);
        }

        //public static void FillCompetitionDataBox(string data)
        //{
        //    App.Current.Dispatcher.Invoke((Action)delegate
        //    {
        //        AdminMainPage currentPage = App.MainWindowRef.Main.Content as AdminMainPage;

        //        currentPage.competitionDataBox.Text = data;

        //    });
        //}

        public static void FillCompetitionListBox(List<CompetitionWithUser> competitions)
        {

            App.Current.Dispatcher.Invoke((Action)delegate
            {
                AdminMainPage currentPage = App.MainWindowRef.Main.Content as AdminMainPage;
                currentPage.competitionListBox.ItemsSource = competitions;
            });
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
