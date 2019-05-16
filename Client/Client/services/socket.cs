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
using System.Windows.Documents;
using Client.windows;

namespace Client.services
{
    public enum MessageType { NoType, Login, Register, Competition }
    public enum GroupType { User, Judge, Admin }

    public class Competition
    {
        public string Name { get; set; }
        public string Started { get; set; }
        public string Ended { get; set; }
    }

    public class User
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Salt { get; set; }
        public string Hash { get; set; }
        public GroupType Group { get; set; }
        public string SSN { get; set; }

    }
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
                    break;
                case MessageType.Competition:
                    List<CompetitionWithUser> competitions = JsonConvert.DeserializeObject<List<CompetitionWithUser>>(this.Data);

                    //foreach (CompetitionWithUser comp in competitions)
                    //{
                    //    int id = comp.ID;
                    //    string name = comp.Name;
                    //    DateTime stat = comp.Start;
                    //    List<User> users = comp.Users;
                    //    List<User> judges = comp.Judges;
                    //}


                    AdminMainPage.FillCompetitionListBox(competitions);



                    //AdminMainPage.FillCompetitionDataBox(this.Data); //Removed this textbox
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
                    bytesRead = Stream.Read(recievedBuffer, 0, recievedBuffer.Length);
                    msg.AppendFormat("{0}", Encoding.Unicode.GetString(recievedBuffer, 0, bytesRead));
                }
                while (Stream.DataAvailable);

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