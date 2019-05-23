using Client.services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    /// Interaction logic for Create.xaml
    /// </summary>
    /// 
    //public class JumperViewer
    //{
    //    public string JumperName { get; set; }
    //    public string JumperSSN { get; set; }
    //    public int Heigt1 { get; set; }
    //    public string Code1 { get; set; }
    //    public int Heigt2 { get; set; }
    //    public string Code2 { get; set; }
    //    public int Heigt3 { get; set; }
    //    public string Code3 { get; set; }
    //    public int Heigt4 { get; set; }
    //    public string Code4 { get; set; }
    //    public int Heigt5 { get; set; }
    //    public string Code5 { get; set; }
    //    public int Heigt6 { get; set; }
    //    public string Code6 { get; set; }
    //    public int Heigt7 { get; set; }
    //    public string Code7 { get; set; }
    //    public int Heigt8 { get; set; }
    //    public string Code8 { get; set; }
    //    public int Heigt9 { get; set; }
    //    public string Code9 { get; set; }
    //    public int Heigt10 { get; set; }
    //    public string Code10 { get; set; }
    //}

    public partial class CreateContest : Page
    {
        private string selectedHeigt;
        CompetitionWithUser newCompetition = new CompetitionWithUser();
        List<User> users = new List<User>();
        List<User> judges = new List<User>();
        List<Jump> alljumps = new List<Jump>();
        //List<JumperViewer> allJumpersWithJumps = new List<JumperViewer>();
        List<User> judgeDatabase = new List<User>();
        List<User> jumperDatabase = new List<User>();

        private ObservableCollection<string> items = new ObservableCollection<string>()
        {
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
        };
        public CreateContest()
        {
            InitializeComponent();
            this.DataContext = this;
            GetJudges();
            GetJumpers();
            newCompetition.Users = users;
            newCompetition.Judges = judges;
        }

        private void GetJudges()
        {
            Message getJudges = new Message();
            getJudges.Type = MessageType.Judges;
            getJudges.Data = "getAll";
            ClientControll.Send(getJudges);
        }

        private void GetJumpers()
        {
            Message getJumpers = new Message();
            getJumpers.Type = MessageType.Jumpers;
            getJumpers.Data = "getAll";
            ClientControll.Send(getJumpers);
        }

        public static void FillUserDatabase(List<User> users)
        {
            App.Current.Dispatcher.Invoke((Action)delegate
            {
                CreateContest currentPage = App.MainWindowRef.Main.Content as CreateContest;
                if (users[0].Group == 0)
                    currentPage.jumperDatabase = users;
                else
                    currentPage.judgeDatabase = users;

            });
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            App.MainWindowRef.Main.Navigate(new AdminMainPage());
        }

        public IEnumerable Items {
            get { return items; }
        }

        public string SelectedItem {
            get { return selectedHeigt; }
            set {
                selectedHeigt = value;
                OnPropertyChanged("SelectedHeight");
            }
        }

        public string NewItem {
            set {
                if (SelectedItem != null)
                {
                    return;
                }
                if (!string.IsNullOrEmpty(value))
                {
                    items.Add(value);
                    SelectedItem = value;
                }
            }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            var handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void JumpHeight_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (SelectedItem)
            {
                case "4":
                    jump1.Visibility = Visibility.Visible;
                    jump2.Visibility = Visibility.Visible;
                    jump3.Visibility = Visibility.Visible;
                    jump4.Visibility = Visibility.Visible;
                    jump5.Visibility = Visibility.Hidden;
                    jump6.Visibility = Visibility.Hidden;
                    jump7.Visibility = Visibility.Hidden;
                    jump8.Visibility = Visibility.Hidden;
                    jump9.Visibility = Visibility.Hidden;
                    jump10.Visibility = Visibility.Hidden;
                    jump1Height.Visibility = Visibility.Visible;
                    jump2Height.Visibility = Visibility.Visible;
                    jump3Height.Visibility = Visibility.Visible;
                    jump4Height.Visibility = Visibility.Visible;
                    jump5Height.Visibility = Visibility.Hidden;
                    jump6Height.Visibility = Visibility.Hidden;
                    jump7Height.Visibility = Visibility.Hidden;
                    jump8Height.Visibility = Visibility.Hidden;
                    jump9Height.Visibility = Visibility.Hidden;
                    jump10Height.Visibility = Visibility.Hidden;
                    break;
                case "5":
                    jump1.Visibility = Visibility.Visible;
                    jump2.Visibility = Visibility.Visible;
                    jump3.Visibility = Visibility.Visible;
                    jump4.Visibility = Visibility.Visible;
                    jump5.Visibility = Visibility.Visible;
                    jump6.Visibility = Visibility.Hidden;
                    jump7.Visibility = Visibility.Hidden;
                    jump8.Visibility = Visibility.Hidden;
                    jump9.Visibility = Visibility.Hidden;
                    jump10.Visibility = Visibility.Hidden;
                    jump1Height.Visibility = Visibility.Visible;
                    jump2Height.Visibility = Visibility.Visible;
                    jump3Height.Visibility = Visibility.Visible;
                    jump4Height.Visibility = Visibility.Visible;
                    jump5Height.Visibility = Visibility.Visible;
                    jump6Height.Visibility = Visibility.Hidden;
                    jump7Height.Visibility = Visibility.Hidden;
                    jump8Height.Visibility = Visibility.Hidden;
                    jump9Height.Visibility = Visibility.Hidden;
                    jump10Height.Visibility = Visibility.Hidden;
                    break;
                case "6":
                    jump1.Visibility = Visibility.Visible;
                    jump2.Visibility = Visibility.Visible;
                    jump3.Visibility = Visibility.Visible;
                    jump4.Visibility = Visibility.Visible;
                    jump5.Visibility = Visibility.Visible;
                    jump6.Visibility = Visibility.Visible;
                    jump7.Visibility = Visibility.Hidden;
                    jump8.Visibility = Visibility.Hidden;
                    jump9.Visibility = Visibility.Hidden;
                    jump10.Visibility = Visibility.Hidden;
                    jump1Height.Visibility = Visibility.Visible;
                    jump2Height.Visibility = Visibility.Visible;
                    jump3Height.Visibility = Visibility.Visible;
                    jump4Height.Visibility = Visibility.Visible;
                    jump5Height.Visibility = Visibility.Visible;
                    jump6Height.Visibility = Visibility.Visible;
                    jump7Height.Visibility = Visibility.Hidden;
                    jump8Height.Visibility = Visibility.Hidden;
                    jump9Height.Visibility = Visibility.Hidden;
                    jump10Height.Visibility = Visibility.Hidden;
                    break;
                case "7":
                    jump1.Visibility = Visibility.Visible;
                    jump2.Visibility = Visibility.Visible;
                    jump3.Visibility = Visibility.Visible;
                    jump4.Visibility = Visibility.Visible;
                    jump5.Visibility = Visibility.Visible;
                    jump6.Visibility = Visibility.Visible;
                    jump7.Visibility = Visibility.Visible;
                    jump8.Visibility = Visibility.Hidden;
                    jump9.Visibility = Visibility.Hidden;
                    jump10.Visibility = Visibility.Hidden;
                    jump1Height.Visibility = Visibility.Visible;
                    jump2Height.Visibility = Visibility.Visible;
                    jump3Height.Visibility = Visibility.Visible;
                    jump4Height.Visibility = Visibility.Visible;
                    jump5Height.Visibility = Visibility.Visible;
                    jump6Height.Visibility = Visibility.Visible;
                    jump7Height.Visibility = Visibility.Visible;
                    jump8Height.Visibility = Visibility.Hidden;
                    jump9Height.Visibility = Visibility.Hidden;
                    jump10Height.Visibility = Visibility.Hidden;
                    break;
                case "8":
                    jump1.Visibility = Visibility.Visible;
                    jump2.Visibility = Visibility.Visible;
                    jump3.Visibility = Visibility.Visible;
                    jump4.Visibility = Visibility.Visible;
                    jump5.Visibility = Visibility.Visible;
                    jump6.Visibility = Visibility.Visible;
                    jump7.Visibility = Visibility.Visible;
                    jump8.Visibility = Visibility.Visible;
                    jump9.Visibility = Visibility.Hidden;
                    jump10.Visibility = Visibility.Hidden;
                    jump1Height.Visibility = Visibility.Visible;
                    jump2Height.Visibility = Visibility.Visible;
                    jump3Height.Visibility = Visibility.Visible;
                    jump4Height.Visibility = Visibility.Visible;
                    jump5Height.Visibility = Visibility.Visible;
                    jump6Height.Visibility = Visibility.Visible;
                    jump7Height.Visibility = Visibility.Visible;
                    jump8Height.Visibility = Visibility.Visible;
                    jump9Height.Visibility = Visibility.Hidden;
                    jump10Height.Visibility = Visibility.Hidden;
                    break;
                case "9":
                    jump1.Visibility = Visibility.Visible;
                    jump2.Visibility = Visibility.Visible;
                    jump3.Visibility = Visibility.Visible;
                    jump4.Visibility = Visibility.Visible;
                    jump5.Visibility = Visibility.Visible;
                    jump6.Visibility = Visibility.Visible;
                    jump7.Visibility = Visibility.Visible;
                    jump8.Visibility = Visibility.Visible;
                    jump9.Visibility = Visibility.Visible;
                    jump10.Visibility = Visibility.Hidden;
                    jump1Height.Visibility = Visibility.Visible;
                    jump2Height.Visibility = Visibility.Visible;
                    jump3Height.Visibility = Visibility.Visible;
                    jump4Height.Visibility = Visibility.Visible;
                    jump5Height.Visibility = Visibility.Visible;
                    jump6Height.Visibility = Visibility.Visible;
                    jump7Height.Visibility = Visibility.Visible;
                    jump8Height.Visibility = Visibility.Visible;
                    jump9Height.Visibility = Visibility.Visible;
                    jump10Height.Visibility = Visibility.Hidden;
                    break;
                case "10":
                    jump1.Visibility = Visibility.Visible;
                    jump2.Visibility = Visibility.Visible;
                    jump3.Visibility = Visibility.Visible;
                    jump4.Visibility = Visibility.Visible;
                    jump5.Visibility = Visibility.Visible;
                    jump6.Visibility = Visibility.Visible;
                    jump7.Visibility = Visibility.Visible;
                    jump8.Visibility = Visibility.Visible;
                    jump9.Visibility = Visibility.Visible;
                    jump10.Visibility = Visibility.Visible;
                    jump1Height.Visibility = Visibility.Visible;
                    jump2Height.Visibility = Visibility.Visible;
                    jump3Height.Visibility = Visibility.Visible;
                    jump4Height.Visibility = Visibility.Visible;
                    jump5Height.Visibility = Visibility.Visible;
                    jump6Height.Visibility = Visibility.Visible;
                    jump7Height.Visibility = Visibility.Visible;
                    jump8Height.Visibility = Visibility.Visible;
                    jump9Height.Visibility = Visibility.Visible;
                    jump10Height.Visibility = Visibility.Visible;
                    break;
            }
        }

        private void AddJudgeBtn_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Dispatcher.Invoke((Action)delegate
            {
                bool found = false;
                CreateContest currentPage = App.MainWindowRef.Main.Content as CreateContest;
                List<User> judges = currentPage.judgeDatabase;
                User tempjudge = new User();
                foreach (User judge in judges)
                    if (judgeName.Text + judgeSSN.Text == judge.Name + judge.SSN)
                    {
                        foreach (User judge2 in newCompetition.Judges)
                        {
                            if (judgeName.Text + judgeSSN.Text == judge2.Name + judge2.SSN)
                                found = true;
                        }
                        tempjudge = judge;
                    }
                if (!found)
                {
                    newCompetition.Judges.Add(tempjudge);
                    FillJudgesListBox(newCompetition.Judges);
                    judgeName.Text = "Name";
                    judgeSSN.Text = "xxxx-xx-xx-xxxx";
                }


            });

        }

        private void AddJumperBtn_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Dispatcher.Invoke((Action)delegate
            {

                bool found = false;
                CreateContest currentPage = App.MainWindowRef.Main.Content as CreateContest;
                List<User> jumpers = currentPage.jumperDatabase;
                User tempjumper = new User();
                foreach (User jumper in jumpers)
                    if (jumperName.Text + jumperSSN.Text == jumper.Name + jumper.SSN)
                    {
                        foreach (User jumper2 in newCompetition.Users)
                        {
                            if (jumperName.Text + jumperSSN.Text == jumper2.Name + jumper2.SSN)
                                found = true;
                        }
                        tempjumper = jumper;
                    }
                if (!found)
                {
                    newCompetition.Users.Add(tempjumper);
                    FillUsersListBox(newCompetition.Users);
                    SaveJumps(tempjumper.ID);
                    jumperName.Text = "Name";
                    jumperSSN.Text = "xxxx-xx-xx-xxxx";
                }

            });
        }

        private void SaveJumps(int jumperid)
        {
            //    public class Jump{
            //    public int CUID { get; set; } (Ett user id)
            //    public string Code { get; set; }
            //    public int Number { get; set; }
            //    public int Height { get; set; }
            //}
            if (jump1.Visibility != Visibility.Hidden && jump1.Text != "" && jump1Height.Text != "")
            {
                Jump tempjump1 = new Jump();
                tempjump1.CUID = jumperid;
                tempjump1.Code = jump1.Text;
                tempjump1.Number = 1;
                tempjump1.Height = Convert.ToInt32(jump1Height.Text);
                alljumps.Add(tempjump1);

                if (jump2.Visibility != Visibility.Hidden && jump2.Text != "" && jump2Height.Text != "")
                {
                    Jump tempjump2 = new Jump();
                    tempjump2.CUID = jumperid;
                    tempjump2.Code = jump2.Text;
                    tempjump2.Number = 1;
                    tempjump2.Height = Convert.ToInt32(jump2Height.Text);
                    alljumps.Add(tempjump2);
                }
                if (jump3.Visibility != Visibility.Hidden && jump3.Text != "" && jump3Height.Text != "")
                {
                    Jump tempjump3 = new Jump();
                    tempjump3.CUID = jumperid;
                    tempjump3.Code = jump3.Text;
                    tempjump3.Number = 1;
                    tempjump3.Height = Convert.ToInt32(jump3Height.Text);
                    alljumps.Add(tempjump3);
                }
                if (jump4.Visibility != Visibility.Hidden && jump4.Text != "" && jump4Height.Text != "")
                {
                    Jump tempjump4 = new Jump();
                    tempjump4.CUID = jumperid;
                    tempjump4.Code = jump4.Text;
                    tempjump4.Number = 1;
                    tempjump4.Height = Convert.ToInt32(jump4Height.Text);
                    alljumps.Add(tempjump4);
                }
                if (jump5.Visibility != Visibility.Hidden && jump5.Text != "" && jump5Height.Text != "")
                {
                    Jump tempjump5 = new Jump();
                    tempjump5.CUID = jumperid;
                    tempjump5.Code = jump5.Text;
                    tempjump5.Number = 1;
                    tempjump5.Height = Convert.ToInt32(jump5Height.Text);
                    alljumps.Add(tempjump5);
                }
                if (jump6.Visibility != Visibility.Hidden && jump6.Text != "" && jump6Height.Text != "")
                {
                    Jump tempjump6 = new Jump();
                    tempjump6.CUID = jumperid;
                    tempjump6.Code = jump6.Text;
                    tempjump6.Number = 1;
                    tempjump6.Height = Convert.ToInt32(jump6Height.Text);
                    alljumps.Add(tempjump6);
                }
                if (jump7.Visibility != Visibility.Hidden && jump7.Text != "" && jump7Height.Text != "")
                {
                    Jump tempjump7 = new Jump();
                    tempjump7.CUID = jumperid;
                    tempjump7.Code = jump7.Text;
                    tempjump7.Number = 1;
                    tempjump7.Height = Convert.ToInt32(jump7Height.Text);
                    alljumps.Add(tempjump7);
                }
                if (jump8.Visibility != Visibility.Hidden && jump8.Text != "" && jump8Height.Text != "")
                {
                    Jump tempjump8 = new Jump();
                    tempjump8.CUID = jumperid;
                    tempjump8.Code = jump8.Text;
                    tempjump8.Number = 1;
                    tempjump8.Height = Convert.ToInt32(jump8Height.Text);
                    alljumps.Add(tempjump8);
                }
                if (jump9.Visibility != Visibility.Hidden && jump9.Text != "" && jump9Height.Text != "")
                {
                    Jump tempjump9 = new Jump();
                    tempjump9.CUID = jumperid;
                    tempjump9.Code = jump9.Text;
                    tempjump9.Number = 1;
                    tempjump9.Height = Convert.ToInt32(jump9Height.Text);
                    alljumps.Add(tempjump9);
                }
                if (jump10.Visibility != Visibility.Hidden && jump10.Text != "" && jump10Height.Text != "")
                {
                    Jump tempjump10 = new Jump();
                    tempjump10.CUID = jumperid;
                    tempjump10.Code = jump10.Text;
                    tempjump10.Number = 1;
                    tempjump10.Height = Convert.ToInt32(jump10Height.Text);
                    alljumps.Add(tempjump10);
                }
            }
        }

        public static void FillJudgesListBox(List<User> judges)
        {

            App.Current.Dispatcher.Invoke((Action)delegate
            {
                CreateContest currentPage = App.MainWindowRef.Main.Content as CreateContest;
                //if (judges != null)
                //currentPage.judgeListBox.ItemsSource = null;
                //currentPage.judgeListBox.ItemsSource = judges;

                currentPage.judgeListBox.Items.Add(judges);
            });
        }
        public static void FillUsersListBox(List<User> jumpers)
        {

            App.Current.Dispatcher.Invoke((Action)delegate
            {
                CreateContest currentPage = App.MainWindowRef.Main.Content as CreateContest;
                if (jumpers != null)
                    currentPage.usersListBox.ItemsSource = null;
                currentPage.usersListBox.ItemsSource = jumpers;
                    //currentPage.usersListBox.ItemsSource = jumps;
                });
        }

        private void JudgeSSN_GotFocus(object sender, RoutedEventArgs e)
        {
            if (judgeSSN.Text == "xxxx-xx-xx-xxxx")
                judgeSSN.Text = "";
        }

        private void JumperSSN_GotFocus(object sender, RoutedEventArgs e)
        {
            if (jumperSSN.Text == "xxxx-xx-xx-xxxx")
                jumperSSN.Text = "";
        }

        private void JudgeName_GotFocus(object sender, RoutedEventArgs e)
        {
            if (judgeName.Text == "Name")
                judgeName.Text = "";
        }

        private void CompetitionTitle_LostFocus(object sender, RoutedEventArgs e)
        {
            if (competitionTitle.Text != "")
                competitionHeadline.Content = competitionTitle.Text;
        }

        private void JumperName_GotFocus(object sender, RoutedEventArgs e)
        {
            if (jumperName.Text == "Name")
                jumperName.Text = "";
        }
    }
}


