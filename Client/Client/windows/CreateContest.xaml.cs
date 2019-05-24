using Client.services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using Newtonsoft.Json;

namespace Client.windows
{
    /// <summary>
    /// Interaction logic for Create.xaml
    /// </summary>
    public partial class CreateContest : Page
    {
        private string selectedHeigt;
        static CompetitionWithUser newCompetition = new CompetitionWithUser();
        static List<Jump> newJumps = new List<Jump>();
        static List<User> judgeDatabase = new List<User>();
        static List<string> judgeDatabaseName = new List<string>();
        static List<User> jumperDatabase = new List<User>();
        static List<string> jumperDatabaseName = new List<string>();

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
            GetUsers();
            newCompetition.Judges = new List<User>();
            newCompetition.Users = new List<User>();
            
            App.MainWindowRef.currentpage = App.MainWindowRef.Main.Content.ToString();
        }

        private void GetUsers()
        {
            Message getUsers = new Message();
            getUsers.Type = MessageType.User;
            getUsers.Data = "Get All";
            ClientControll.Send(getUsers);
        }

        public static void FillUserDatabase(List<User> users)
        {
            jumperDatabase = new List<User>();
            judgeDatabase = new List<User>();
            jumperDatabaseName = new List<string>();
            judgeDatabaseName = new List<string>();
            App.Current.Dispatcher.Invoke((Action)delegate
            {
                foreach (User u in users)
                {
                    if (u.Group == GroupType.User)
                    {
                        jumperDatabase.Add(u);
                        jumperDatabaseName.Add(u.Name);

                    }
                    else if (u.Group == GroupType.Judge)
                    {
                        judgeDatabase.Add(u);
                        judgeDatabaseName.Add(u.Name);
                    }
                }
            });
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            App.MainWindowRef.Main.Navigate(new AdminMainPage());
        }

        public IEnumerable Items
        {
            get { return items; }
        }

        public string SelectedItem
        {
            get { return selectedHeigt; }
            set
            {
                selectedHeigt = value;
                OnPropertyChanged("SelectedHeight");
            }
        }

        public string NewItem
        {
            set
            {
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
            jumpHeightLable.Visibility = Visibility.Visible;
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

        private List<string> ReadJumps()
        {
            List<string> jumps = new List<string>();
            int j = Int32.Parse(jumpHeight.SelectedItem.ToString());
            if (j >= 4)
            {
                jumps.Add(jump1.Text.ToString() + "%" + jump1Height.Text.ToString());
                jumps.Add(jump2.Text.ToString() + "%" + jump2Height.Text.ToString());
                jumps.Add(jump3.Text.ToString() + "%" + jump3Height.Text.ToString());
                jumps.Add(jump4.Text.ToString() + "%" + jump4Height.Text.ToString());
            }
            if (j >= 5)
                jumps.Add(jump5.Text.ToString() + "%" + jump5Height.Text.ToString());
            if (j >= 6)
                jumps.Add(jump6.Text.ToString() + "%" + jump6Height.Text.ToString());
            if (j >= 7)
                jumps.Add(jump7.Text.ToString() + "%" + jump7Height.Text.ToString());
            if (j >= 8)
                jumps.Add(jump8.Text.ToString() + "%" + jump8Height.Text.ToString());
            if (j >= 9)
                jumps.Add(jump9.Text.ToString() + "%" + jump9Height.Text.ToString());
            if (j >= 10)
                jumps.Add(jump10.Text.ToString() + "%" + jump10Height.Text.ToString());

            foreach (string jt in jumps)
            {
                if (jt == "%")
                    jumps.Remove(jt);
            }

            return jumps;
        }

        private void JudgeName_TextChanged(object sender, TextChangedEventArgs e)
        {
            judgeNameDropdown.Items.Clear();
            if (judgeName.Text.Trim() != "")
            {
                string regexPattern = (judgeName.Text.ToString()) + "\\w*";
                regexPattern = regexPattern[0] + regexPattern.Substring(1); //prvo slovo veliko
                        int index = 0; //where in original this was found.
                foreach (User u in judgeDatabase)
                {
                    Match match = Regex.Match(u.Name, regexPattern, RegexOptions.IgnoreCase);
                    if (match.Success && match.Value != "")
                    {
                        judgeNameDropdown.Items.Add(judgeDatabase[index].Name.ToString() + "    " + judgeDatabase[index].SSN.ToString());

                        judgeNameDropdown.Visibility = Visibility.Visible;
                        int height = judgeNameDropdown.Items.Count * 21;
                        if (height > 200)
                            height = 200;
                        judgeNameDropdown.Height = height;
                        judgeNameDropdown.SelectedItem = judgeNameDropdown.Items.GetItemAt(0);
                    }

                    index++;
                }
            }

            if (judgeNameDropdown.Items.IsEmpty) //|| judgeNameDropdown.Items.Count == judgeDatabase.Count
            {
                judgeNameDropdown.Visibility = Visibility.Collapsed;
               // if (judgeNameDropdown.Items.Count == judgeDatabase.Count) judgeNameDropdown.Items.Clear();
            }
        }
        private void judgeNameDropdown_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                string text = judgeNameDropdown.SelectedItem as string;
                judgeNameDropdown.Visibility = Visibility.Collapsed;
                judgeNameDropdown.Items.Clear();

                string[] arr = text.Split(new string[] { "    " }, StringSplitOptions.None);

                judgeName.Text = "";
                User u = judgeDatabase.FirstOrDefault(x => x.SSN == arr[1].Trim());
                newCompetition.Judges.Add(u);
                judgeListBox.Items.Add("" + u.Name + "    " + u.SSN);

            }

        }
        private void JumperName_TextChanged(object sender, TextChangedEventArgs e)
        {
            jumperNameDropdown.Items.Clear();
            if (jumperName.Text.Trim() != "")
            {
                string regexPattern = (jumperName.Text.ToString()) + "\\w*";
                regexPattern = char.ToUpper(regexPattern[0]) + regexPattern.Substring(1); //prvo slovo veliko
                int index = 0;
                foreach (User u in jumperDatabase)
                {
                    Match match = Regex.Match(u.Name, regexPattern, RegexOptions.IgnoreCase);
                    if (match.Success && match.Value != "")
                    {
                         //where in original this was found.
                        jumperNameDropdown.Items.Add(jumperDatabase[index].Name.ToString() + "    " + jumperDatabase[index].SSN.ToString());
                        jumperNameDropdown.Visibility = Visibility.Visible;
                        int height = jumperNameDropdown.Items.Count * 21;
                        if (height > 200)
                            height = 200;
                        jumperNameDropdown.Height = height;
                        jumperNameDropdown.SelectedItem = jumperNameDropdown.Items.GetItemAt(0);
                    }

                    index++;
                }
            }

            if (jumperNameDropdown.Items.IsEmpty) //|| jumperNameDropdown.Items.Count == jumperDatabase.Count
            {
                jumperNameDropdown.Visibility = Visibility.Collapsed;
                //if (jumperNameDropdown.Items.Count == jumperDatabase.Count) jumperNameDropdown.Items.Clear();
            }
        }
        private void jumperNameDropdown_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                if (usersListBox.Items.Count > 0)
                {
                    string[] jumps = usersListBox.Items[usersListBox.Items.Count - 1].ToString()
                        .Split(new string[] { "    " }, StringSplitOptions.None);

                    if (jumpHeight.SelectedItem == null)
                    {
                        SetJumpNumberColor(210, 167, 167);
                        return;
                    }

                    if (jumps.Length < Int32.Parse(jumpHeight.SelectedItem.ToString()))
                    {
                        SetJumpBackgroundColor(210, 167, 167);
                        return;
                    }
                }

                string text = jumperNameDropdown.SelectedItem as string;
                jumperNameDropdown.Visibility = Visibility.Collapsed;
                jumperNameDropdown.Items.Clear();

                string[] arr = text.Split(new string[] { "    " }, StringSplitOptions.None);

                jumperName.Text = "";
                User u = jumperDatabase.FirstOrDefault(x => x.SSN == arr[1].Trim());
                newCompetition.Users.Add(u);
                usersListBox.Items.Add("" + u.Name + "    " + u.SSN);

            }

        }

        private void SetJumpNumberColor(int R, int G, int B)
        {
            jumpHeight.Background = new SolidColorBrush(Color.FromRgb((byte)R, (byte)G, (byte)B));
        }

        private void SetJumpBackgroundColor(int R, int G, int B)
        {
            jump1.Background = new SolidColorBrush(Color.FromRgb((byte)R, (byte)G, (byte)B));
            jump2.Background = new SolidColorBrush(Color.FromRgb((byte)R, (byte)G, (byte)B));
            jump3.Background = new SolidColorBrush(Color.FromRgb((byte)R, (byte)G, (byte)B));
            jump4.Background = new SolidColorBrush(Color.FromRgb((byte)R, (byte)G, (byte)B));
            jump5.Background = new SolidColorBrush(Color.FromRgb((byte)R, (byte)G, (byte)B));
            jump6.Background = new SolidColorBrush(Color.FromRgb((byte)R, (byte)G, (byte)B));
            jump7.Background = new SolidColorBrush(Color.FromRgb((byte)R, (byte)G, (byte)B));
            jump8.Background = new SolidColorBrush(Color.FromRgb((byte)R, (byte)G, (byte)B));
            jump9.Background = new SolidColorBrush(Color.FromRgb((byte)R, (byte)G, (byte)B));
            jump10.Background = new SolidColorBrush(Color.FromRgb((byte)R, (byte)G, (byte)B));
            jump1Height.Background = new SolidColorBrush(Color.FromRgb((byte)R, (byte)G, (byte)B));
            jump2Height.Background = new SolidColorBrush(Color.FromRgb((byte)R, (byte)G, (byte)B));
            jump3Height.Background = new SolidColorBrush(Color.FromRgb((byte)R, (byte)G, (byte)B));
            jump4Height.Background = new SolidColorBrush(Color.FromRgb((byte)R, (byte)G, (byte)B));
            jump5Height.Background = new SolidColorBrush(Color.FromRgb((byte)R, (byte)G, (byte)B));
            jump6Height.Background = new SolidColorBrush(Color.FromRgb((byte)R, (byte)G, (byte)B));
            jump7Height.Background = new SolidColorBrush(Color.FromRgb((byte)R, (byte)G, (byte)B));
            jump8Height.Background = new SolidColorBrush(Color.FromRgb((byte)R, (byte)G, (byte)B));
            jump9Height.Background = new SolidColorBrush(Color.FromRgb((byte)R, (byte)G, (byte)B));
            jump10Height.Background = new SolidColorBrush(Color.FromRgb((byte)R, (byte)G, (byte)B));
        }

        private void ClearJumps()
        {
            jump1.Text = "";
            jump2.Text = "";
            jump3.Text = "";
            jump4.Text = "";
            jump5.Text = "";
            jump6.Text = "";
            jump7.Text = "";
            jump8.Text = "";
            jump9.Text = "";
            jump10.Text = "";
            jump1Height.Text = "";
            jump2Height.Text = "";
            jump3Height.Text = "";
            jump4Height.Text = "";
            jump5Height.Text = "";
            jump6Height.Text = "";
            jump7Height.Text = "";
            jump8Height.Text = "";
            jump9Height.Text = "";
            jump10Height.Text = "";
        }
        private void RevertColor()
        {
            //this.Background = new SolidColorBrush(Color.FromRgb((byte)255, (byte)0, (byte)0));
            //this.Background = ClearValue(TextBox.BorderBrushProperty);
            jump1.ClearValue(TextBox.BorderBrushProperty);
            jump2.ClearValue(TextBox.BorderBrushProperty);
            jump3.ClearValue(TextBox.BorderBrushProperty);
            jump4.ClearValue(TextBox.BorderBrushProperty);
            jump5.ClearValue(TextBox.BorderBrushProperty);
            jump6.ClearValue(TextBox.BorderBrushProperty);
            jump7.ClearValue(TextBox.BorderBrushProperty);
            jump8.ClearValue(TextBox.BorderBrushProperty);
            jump9.ClearValue(TextBox.BorderBrushProperty);
            jump10.ClearValue(TextBox.BorderBrushProperty);
            jump1Height.ClearValue(TextBox.BorderBrushProperty);
            jump2Height.ClearValue(TextBox.BorderBrushProperty);
            jump3Height.ClearValue(TextBox.BorderBrushProperty);
            jump4Height.ClearValue(TextBox.BorderBrushProperty);
            jump5Height.ClearValue(TextBox.BorderBrushProperty);
            jump6Height.ClearValue(TextBox.BorderBrushProperty);
            jump7Height.ClearValue(TextBox.BorderBrushProperty);
            jump8Height.ClearValue(TextBox.BorderBrushProperty);
            jump9Height.ClearValue(TextBox.BorderBrushProperty);
            jump10Height.ClearValue(TextBox.BorderBrushProperty);
        }

        private void AddJumps(object sender, KeyEventArgs e)
        {
            DependencyObject dpobj = sender as DependencyObject;
            string name = dpobj.GetValue(FrameworkElement.NameProperty) as string;
            if (e.Key == Key.Tab && name == "jump" + jumpHeight.SelectedItem.ToString())
            {
                List<string> jumps = new List<string>();
                jumps.AddRange(ReadJumps());
                if (jumps.Count == Int32.Parse(jumpHeight.SelectedItem.ToString()))
                {

                    List<Jump> addJumps = new List<Jump>();
                    string visibleString = "";
                    int i = 0;
                    foreach (string j in jumps)
                    {
                        visibleString += j + "    ";
                        Jump temp = new Jump();
                        temp.Number = i;
                        i++;
                        temp.CUID = newCompetition.Users[newCompetition.Users.Count - 1].ID;
                        temp.Code = j.Split('%')[0];
                        temp.Height = Int32.Parse(j.Split('%')[1]);

                        newJumps.Add(temp);
                    }

                    usersListBox.Items.Add(visibleString);
                    //usersListBox.Items.Add(MainWindow.ID.ToString());

                    ClearJumps();
                    RevertColor();
                }
            }
            else if (e.Key == Key.Tab && name.EndsWith("t"))
            {
                TraversalRequest tRequest = new TraversalRequest(FocusNavigationDirection.Right);
                UIElement keyboardFocus = Keyboard.FocusedElement as UIElement;

                if (keyboardFocus != null)
                {
                    keyboardFocus.MoveFocus(tRequest);
                }

                e.Handled = true;
            }
            else if (e.Key == Key.Tab)
            {
                TraversalRequest tRequest = new TraversalRequest(FocusNavigationDirection.Left);
                UIElement keyboardFocus = Keyboard.FocusedElement as UIElement;

                if (keyboardFocus != null)
                {
                    keyboardFocus.MoveFocus(tRequest);
                }

                tRequest = new TraversalRequest(FocusNavigationDirection.Down);
                keyboardFocus = Keyboard.FocusedElement as UIElement;

                if (keyboardFocus != null)
                {
                    keyboardFocus.MoveFocus(tRequest);
                }

                e.Handled = true;
            }
        }

        private void SaveCompetition(object sender, RoutedEventArgs e)
        {
            Message msg = new Message();

            if (competitionTitle.Text == "")
            {
                competitionTitle.Background = new SolidColorBrush(Color.FromRgb((byte)210, (byte)167, (byte)167));
            }
            else if (datePicker.Text == "")
            {
            }
            else
            {

                newCompetition.Name = competitionTitle.Text;
                newCompetition.Start = DateTime.Parse(datePicker.Text);
                newCompetition.Finished = new DateTime(0001, 1, 1, 0, 0, 0);
                newCompetition.Jumps = Int32.Parse(SelectedItem);





                msg.Type = MessageType.Competition;
                msg.Data = "CreateCompetition\r\n" + JsonConvert.SerializeObject(newCompetition) + "\r\n" +
                           JsonConvert.SerializeObject(newJumps);

                ClientControll.Send(msg);
            }
        }
    }
}

