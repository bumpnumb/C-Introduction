using System;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using System.Security.Cryptography;
using System.Windows.Documents;
using Client.windows;
using System.Windows.Controls;

namespace Client.services
{
    public enum MessageType { NoType, Login, Register, Competition, ScoreToJump, Result, Judges, Jumpers }
    public enum GroupType { User, Judge, Admin }




    public class Response
    {
        public MessageType Type { get; set; }
        public string Data { get; set; }
        public User user { get; set; }

        public void HandleResponse()
        {
            switch (this.Type)
            {
                case MessageType.NoType:
                    break;
                case MessageType.Login:
                    if (this.Data == "success") // thomas och nedim bestämmer // Successfull login!
                    {
                        switch (this.user.Group)
                        {
                            case GroupType.Admin:
                                App.Current.Dispatcher.Invoke((Action)delegate
                                {
                                    App.MainWindowRef.pageSwitcher(new AdminMainPage());
                                    App.MainWindowRef.setActiveUser(this);

                                });
                                break;
                            case GroupType.Judge:
                                App.Current.Dispatcher.Invoke((Action)delegate
                                {
                                    App.MainWindowRef.pageSwitcher(new JudgePage());
                                    App.MainWindowRef.setActiveUser(this);
                                });
                                break;
                            case GroupType.User:
                                App.Current.Dispatcher.Invoke((Action)delegate
                                {
                                    UserLoginPopUpWindow tempWin = new UserLoginPopUpWindow();
                                    tempWin.Show();
                                });

                                break;
                            default:
                                Console.WriteLine("Error");
                                break;
                        }
                        //LoginSucessFunction();
                    }
                    else
                    {
                        App.Current.Dispatcher.Invoke((Action)delegate
                        {
                            WrongUnameAndPword tempErrorWin = new WrongUnameAndPword();
                            tempErrorWin.Show();
                            App.MainWindowRef.CenterWindowOnScreen();
                        });
                    }
                    break;
                case MessageType.Register:
                    //Här skulle man kunna copy-pasta nästan allt under login för att logga in direkt efter register
                    //men det blir problem eftersom att man sätts i en group efter att man har registrerats.

                    //här kanske man kan slänga upp en window i clienten som säger att man har lyckats skapa en user.
                    break;
                case MessageType.Competition:
                    var stringMessage = this.Data;
                    if (stringMessage == "Competition created")
                    {
                        App.Current.Dispatcher.Invoke((Action) delegate
                        {
                            App.MainWindowRef.Main.Navigate(new CreateContest());
                        });
                    }
                    else if (stringMessage == "GetAll")
                    {
                        List<CompetitionWithUser> competitions =
                            JsonConvert.DeserializeObject<List<CompetitionWithUser>>(this.Data);

                        //foreach (CompetitionWithUser comp in competitions)
                        //{
                        //    int id = comp.ID;
                        //    string name = comp.Name;
                        //    DateTime stat = comp.Start;
                        //    List<User> users = comp.Users;
                        //    List<User> judges = comp.Judges;
                        //}

                        App.Current.Dispatcher.Invoke((Action)delegate
                       {
                           string currentpage = App.MainWindowRef.Main.Content.ToString();
                           AdminMainPage.FillCompetitionListBox(competitions);
                       });
                    }
                    else
                    {
                        CompetitionWithResult data = JsonConvert.DeserializeObject<CompetitionWithResult>(this.Data);
                        App.Current.Dispatcher.Invoke((Action)delegate
                        {
                            string currentpage = App.MainWindowRef.Main.Content.ToString();
                            JudgePage.CompetitionData(data);
                        });
                    }

                    //AdminMainPage.FillCompetitionDataBox(this.Data); //Removed this textbox
                    break;
                case MessageType.Judges:
                    List<User> judges = JsonConvert.DeserializeObject<List<User>>(this.Data);
                    App.Current.Dispatcher.Invoke((Action)delegate
                    {
                        string currentpage = App.MainWindowRef.Main.Content.ToString();
                        CreateContest.FillUserDatabase(judges);
                    });
                    break;
                case MessageType.Jumpers:
                    List<User> jumpers = JsonConvert.DeserializeObject<List<User>>(this.Data);
                    App.Current.Dispatcher.Invoke((Action)delegate
                    {
                        string currentpage = App.MainWindowRef.Main.Content.ToString();
                        CreateContest.FillUserDatabase(jumpers);
                    });
                    break;
                
                default:
                    break;
            }
        }
    }

    public class Message
    {
        public MessageType Type { get; set; }
        public string Data { get; set; }
        public string Cookie { get; set; }
    }


    class ClientControll
    {
        static TcpClient Client;
        static NetworkStream Stream;
        static CancellationTokenSource ct;

        public ClientControll()
        {
            string IP = "localhost";
            int port = 8787;
            Client = new TcpClient(IP, port);

            Stream = Client.GetStream();

            ct = new CancellationTokenSource();
            Thread listenerThread = new Thread(new ParameterizedThreadStart(ClientControll.Listen));
            listenerThread.Start(ct.Token);
        }

        public static void Listen(object obj)
        {
            CancellationToken ct = (CancellationToken)obj;
            byte[] recievedBuffer = new byte[1024]; // Fixa en bättre buffersize än en specifik siffra (dynamisk vore najs)
            int bytesRead = 0;
            StringBuilder msg = new StringBuilder();
            while (!ct.IsCancellationRequested)
            {

                do
                {
                    try
                    {
                        bytesRead = Stream.Read(recievedBuffer, 0, recievedBuffer.Length);
                        msg.AppendFormat("{0}", Encoding.Unicode.GetString(recievedBuffer, 0, bytesRead));
                    }
                    catch (Exception e)
                    {
                        return;
                    }

                }
                while (Stream.DataAvailable && !ct.IsCancellationRequested);

                string readMsg = msg.ToString();
                Response resp = JsonConvert.DeserializeObject<Response>(readMsg);

                resp.HandleResponse();
                msg.Clear();
            }
        }

        public static void Send(Message msg)
        {
            if (msg.Data == "quit")
            {
                ct.Cancel();
                Stream.Close();
                Client.Close();
                System.Windows.Application.Current.Shutdown();
                return;
            }
            Console.WriteLine("Sending: " + msg);

            //Fixa det här json.seroalisedickus

            int byteCount = Encoding.Unicode.GetByteCount(JsonConvert.SerializeObject(msg)); // Ta ut längden i bytes på meddelandet
            byte[] sendData = new byte[byteCount];
            sendData = Encoding.Unicode.GetBytes(JsonConvert.SerializeObject(msg));

            Stream.Write(sendData, 0, sendData.Length);
        }
    }
}