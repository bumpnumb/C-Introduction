using Client.services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace Client.windows {
    /// <summary>
    /// Interaction logic for Judge.xaml
    /// </summary>
    public partial class JudgePage : Page {

        CompetitionWithResult compdata = new CompetitionWithResult();
        public static int jumptracker = 0;

        public JudgePage() {
            InitializeComponent();
            App.MainWindowRef.CenterWindowOnScreen();
            GetActiveCompetition();
            //PagePainter(jumptracker);
        }

        public void GetActiveCompetition()
        {
            Message getCompetitions = new Message();
            getCompetitions.Type = MessageType.Competition;
            getCompetitions.Data = "GetActive";
            ClientControll.Send(getCompetitions);
        }

        public static void CompetitionData(CompetitionWithResult compdata)
        {
            App.Current.Dispatcher.Invoke((Action)delegate
            {
                JudgePage currentPage = App.MainWindowRef.Main.Content as JudgePage;
                currentPage.compdata = compdata;
            });
        }

        public static void PagePainter(int jumpnumber)
        {
            App.Current.Dispatcher.Invoke((Action)delegate
            {
                JudgePage currentPage = App.MainWindowRef.Main.Content as JudgePage;
                Jump j = currentPage.compdata.Jumps.FirstOrDefault(x => x.GlobalNumber == jumpnumber);
            User u = currentPage.compdata.Comp.Users.FirstOrDefault(x => x.ID == j.CUID);
                currentPage.jumperNameHeader.Text = u.Name;
                currentPage.jumpSpecificsHeader.Text = "Jump " + j.Number + " - " + j.Name + " " + j.Code + "Difficulty " + j.Difficulty;
                currentPage.jumpIDSecretBox.Text = j.ID.ToString();
            });
        }

        private void SubmitBtn_Click(object sender, RoutedEventArgs e)
        {
            SubmitScore();
            jumptracker++;

            PagePainter(jumptracker);
        }

        private void SubmitScore()
        {
            Result result = new Result();
            result.JumpID = Convert.ToInt32(jumpIDSecretBox.Text);
            result.JudgeID = Convert.ToInt32(MainWindow.ID);
            result.Score = float.Parse(scoreBox.Text, CultureInfo.InvariantCulture.NumberFormat);
            Message msg = new Message();
            msg.Data = JsonConvert.SerializeObject(result);
            msg.Type = MessageType.ScoreToJump;
            ClientControll.Send(msg);
            
        }
    }
}
