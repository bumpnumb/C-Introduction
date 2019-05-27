using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
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
        private static Timer refreshTimer;
        public AdminMainPage()
        {

            InitializeComponent();

            App.Current.Dispatcher.Invoke((Action)delegate
            {
                App.MainWindowRef.currentpage = "Client.windows.AdminMainPage";
            });

            refreshTimer = new Timer(Refresh,null, 1000, 5000);

            
        }

        private void Refresh(Object source)
        {
            Message getCompetitions = new Message();
            getCompetitions.Type = MessageType.Competition;
            getCompetitions.Data = "GetAll";
            ClientControll.Send(getCompetitions);
        }

        private void Edit_Create_Btn(object sender, RoutedEventArgs e)
        {
            refreshTimer.Dispose();
            App.MainWindowRef.Main.Navigate(new CreateContest());
        }

        public static void FillCompetitionListBox(List<CompetitionWithUser> competitions)
        {

            App.Current.Dispatcher.Invoke((Action)delegate
            {

                AdminMainPage currentPage = App.MainWindowRef.Main.Content as AdminMainPage;
                currentPage.competitionListBox.ItemsSource = competitions;
                currentPage.competitionListBox.UnselectAll();
            });
        }

        private void DisplaySelectedContest(object sender, MouseButtonEventArgs e)
        {
            App.Current.Dispatcher.Invoke((Action)delegate
            {
                AdminMainPage currentPage = App.MainWindowRef.Main.Content as AdminMainPage;
                dynamic data = currentPage.competitionListBox.SelectedItem as dynamic;
                int id = data.ID;
                List<User> jumpers = data.Users;
                List<User> judges = data.Judges;
                Console.WriteLine(id);
                FillUsersListBox(jumpers);
                FillJudgesListBox(judges);
            });
        }

        public static void FillJudgesListBox(List<User> judges)
        {
            App.Current.Dispatcher.Invoke((Action)delegate
            {
                AdminMainPage currentPage = App.MainWindowRef.Main.Content as AdminMainPage;
                currentPage.judgeListBox.ItemsSource = judges;
            });
        }
        public static void FillUsersListBox(List<User> jumpers)
        {
            App.Current.Dispatcher.Invoke((Action)delegate
            {
                AdminMainPage currentPage = App.MainWindowRef.Main.Content as AdminMainPage;
                currentPage.usersListBox.ItemsSource = jumpers;
            });
        }

        private void EditUsersBtn_Click(object sender, RoutedEventArgs e)
        {
            refreshTimer.Dispose();
            App.Current.Dispatcher.Invoke((Action)delegate
            {
                App.MainWindowRef.pageSwitcher(new EditUsers(), 380, 250);
            });


        }

        private void EditContestBtn_Click(object sender, RoutedEventArgs e)
        {
            refreshTimer.Dispose();
            App.Current.Dispatcher.Invoke((Action)delegate
            {
                App.MainWindowRef.pageSwitcher(new EditContest(), 850, 1050);
            });
        }


    }
}
