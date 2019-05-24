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

namespace Client.windows
{
    /// <summary>
    /// Interaction logic for EditUsers.xaml
    /// </summary>
    public partial class EditUsers : Page
    {
        public static List<User> AllUsers;
        public static List<String> AllUserNames; 


        public EditUsers()
        {
            InitializeComponent();
        }

        private void GetUsers()
        {
            Message getUsers = new Message();
            getUsers.Type = MessageType.User;
            getUsers.Data = "Get All";
            ClientControll.Send(getUsers);
        }


        private void userNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            userNameDropDown.Items.Clear();
            if (userNameTextBox.Text.Trim() != "")
            {
                string regexPattern = (userNameTextBox.Text.ToString()) + "\\w*";
                regexPattern = char.ToUpper(regexPattern[0]) + regexPattern.Substring(1); //prvo slovo veliko
                foreach (User u in AllUsers)
                {
                    Match match = Regex.Match(u.Name, regexPattern, RegexOptions.IgnoreCase);
                    if (match.Success && match.Value != "")
                    {
                        int index = match.Index; //where in original this was found.
                        userNameDropDown.Items.Add(match.Value.ToString() + "    " + AllUsers[index].SSN.ToString());
                        userNameDropDown.Visibility = Visibility.Visible;
                        int height = userNameDropDown.Items.Count * 21;
                        if (height > 200)
                            height = 200;
                        userNameDropDown.Height = height;
                        userNameDropDown.SelectedItem = userNameDropDown.Items.GetItemAt(0);
                    }
                }
            }

            if (userNameDropDown.Items.IsEmpty || userNameDropDown.Items.Count == AllUsers.Count)
            {
                userNameDropDown.Visibility = Visibility.Collapsed;
                if (userNameDropDown.Items.Count == AllUsers.Count) userNameDropDown.Items.Clear();
            }
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
                //newCompetition.Judges.Add(u);
                //judgeListBox.Items.Add("" + u.Name + "    " + u.SSN);

            }
        }

        //private void JudgeName_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    judgeNameDropdown.Items.Clear();
        //    if (judgeName.Text.Trim() != "")
        //    {
        //        string regexPattern = (judgeName.Text.ToString()) + "\\w*";
        //        regexPattern = char.ToUpper(regexPattern[0]) + regexPattern.Substring(1); //prvo slovo veliko
        //        foreach (User u in judgeDatabase)
        //        {
        //            Match match = Regex.Match(u.Name, regexPattern, RegexOptions.IgnoreCase);
        //            if (match.Success && match.Value != "")
        //            {
        //                int index = match.Index; //where in original this was found.
        //                judgeNameDropdown.Items.Add(match.Value.ToString() + "    " + judgeDatabase[index].SSN.ToString());
        //                judgeNameDropdown.Visibility = Visibility.Visible;
        //                int height = judgeNameDropdown.Items.Count * 21;
        //                if (height > 200)
        //                    height = 200;
        //                judgeNameDropdown.Height = height;
        //                judgeNameDropdown.SelectedItem = judgeNameDropdown.Items.GetItemAt(0);
        //            }
        //        }
        //    }

        //    if (judgeNameDropdown.Items.IsEmpty || judgeNameDropdown.Items.Count == judgeDatabase.Count)
        //    {
        //        judgeNameDropdown.Visibility = Visibility.Collapsed;
        //        if (judgeNameDropdown.Items.Count == judgeDatabase.Count) judgeNameDropdown.Items.Clear();
        //    }
        //}
        //private void judgeNameDropdown_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.Key == Key.Tab)
        //    {
        //        string text = judgeNameDropdown.SelectedItem as string;
        //        judgeNameDropdown.Visibility = Visibility.Collapsed;
        //        judgeNameDropdown.Items.Clear();

        //        string[] arr = text.Split(new string[] { "    " }, StringSplitOptions.None);

        //        judgeName.Text = "";
        //        User u = judgeDatabase.FirstOrDefault(x => x.SSN == arr[1].Trim());
        //        newCompetition.Judges.Add(u);
        //        judgeListBox.Items.Add("" + u.Name + "    " + u.SSN);

        //    }

        //}



    }
}
