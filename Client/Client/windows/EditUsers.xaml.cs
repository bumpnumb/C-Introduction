using System;
using System.Collections.Generic;
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
using Client.services;
using Newtonsoft.Json;

namespace Client.windows
{
    /// <summary>
    /// Interaction logic for EditUsers.xaml
    /// </summary>
    public partial class EditUsers : Page
    {
        static List<User> AllUsers = new List<User>();
        public List<String> AllUserNames;
        User CurrentUser = new User();

        public EditUsers()
        {
            InitializeComponent();
            GetUsers();
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
            AllUsers = new List<User>();
            AllUsers.AddRange(users);
        }

        private void userNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            userNameDropDown.Items.Clear();
            if (userNameTextBox.Text.Trim() != "")
            {
                string regexPattern = (userNameTextBox.Text.ToString()) + "\\w*";
                regexPattern = char.ToUpper(regexPattern[0]) + regexPattern.Substring(1); //prvo slovo veliko
                int index = 0;
                foreach (User u in AllUsers)
                {
                    Match match = Regex.Match(u.Name, regexPattern, RegexOptions.IgnoreCase);
                    if (match.Success && match.Value != "")
                    {
                        //where in original this was found.
                        userNameDropDown.Items.Add(AllUsers[index].Name.ToString() + "    " + AllUsers[index].SSN.ToString());
                        userNameDropDown.Visibility = Visibility.Visible;
                        int height = userNameDropDown.Items.Count * 22;
                        if (height > 200)
                            height = 200;
                        userNameDropDown.Height = height;
                        userNameDropDown.SelectedItem = userNameDropDown.Items.GetItemAt(0);
                    }

                    index++;
                }
            }

            if (userNameDropDown.Items.IsEmpty) //|| jumperNameDropdown.Items.Count == jumperDatabase.Count
            {
                userNameDropDown.Visibility = Visibility.Collapsed;
                //if (jumperNameDropdown.Items.Count == jumperDatabase.Count) jumperNameDropdown.Items.Clear();
            }
        }

        private void ClearBoxes()
        {
            CurrentUser = new User();
            group.Text = "";
            username.Text = "";
            ssn.Text = "";
            password.Text = "";
        }
        private void DisplayUser(User u)
        {
            CurrentUser = u;
            group.Text = "Please, select any value";
            username.Text = u.Name;
            ssn.Text = u.SSN;
            password.Text = "Enter a new Password";
            group.ItemsSource = new List<GroupType>() { GroupType.User, GroupType.Judge, GroupType.Admin };
            group.SelectedIndex = (int)u.Group;

        }
        private void userNameDropDown_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                string text = userNameDropDown.SelectedItem as string;
                userNameDropDown.Visibility = Visibility.Collapsed;
                userNameDropDown.Items.Clear();

                string[] arr = text.Split(new string[] { "    " }, StringSplitOptions.None);

                userNameTextBox.Text = "";
                User u = AllUsers.FirstOrDefault(x => x.SSN == arr[1].Trim());

                DisplayUser(u);

            }
        }

        private void ChangeButton_Click(object sender, RoutedEventArgs e)
        {
            
            User n = new User();
            n.SSN = ssn.Text + '%' + CurrentUser.SSN;
            n.Name = username.Text;
            n.Salt = null;
            if (password.Text != "Enter a new Password")
                n.Salt = password.Text;
            n.Group = (GroupType)group.SelectionBoxItem;

            if (CurrentUser.Group != n.Group || n.Salt != null || CurrentUser.SSN != n.SSN || CurrentUser.Name != n.Name)
            {
                Message msg = new Message();
                msg.Type = MessageType.ChangeUser;
                msg.Data = JsonConvert.SerializeObject(n);
                ClientControll.Send(msg);
            }


            ClearBoxes();
        }
    }
}
