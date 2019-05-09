using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using System.Net.Sockets;
using System.IO;
using System.Net;

using System.Security.Cryptography;
using Newtonsoft.Json;

namespace Server.services
{
    class TcpConnection
    {
        static IPAddress ip = Dns.GetHostEntry("localhost").AddressList[0];
        static TcpListener server = new TcpListener(ip, 8787);
        static TcpClient client = default(TcpClient);

        public TcpConnection()
        {
            try
            {
                server.Start();
                Console.WriteLine("Server started...");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.Read();

            }
            while (true)
            {
                client = server.AcceptTcpClient();
                ClientHandler handle = new ClientHandler(client);
            }
        }
    }
    class ClientHandler
    {
        TcpClient Client;
        int ID;
        NetworkStream Stream;
        CancellationTokenSource TokenSource;

        static List<ClientHandler> allClients = new List<ClientHandler>();
        static List<CancellationTokenSource> allTokens = new List<CancellationTokenSource>();
        static int nextN = 0;

        private readonly object syncLock = new object();

        public ClientHandler(TcpClient clientInfo)
        {
            this.Client = clientInfo;
            this.ID = nextN;
            this.Stream = Client.GetStream();
            this.TokenSource = new CancellationTokenSource();

            lock (syncLock)
            {
                allClients.Add(this);
                nextN++;
            }


            Thread clientThread = new Thread(new ParameterizedThreadStart(Listen));
            clientThread.Start(TokenSource.Token);
        }

        public void Listen(object obj)
        {
            CancellationToken ct = (CancellationToken)obj;
            while (!ct.IsCancellationRequested)
            {
                byte[] recievedBuffer = new byte[100]; // Dynamic buffer size here (or other fix maybe)
                try
                {
                    Stream.Read(recievedBuffer, 0, recievedBuffer.Length);
                }
                catch (System.IO.IOException)
                {
                    Console.WriteLine("Client: " + this.ID.ToString() + " has disconnected");

                    Stream.Close();
                    Client.Close();

                    allClients.Remove(this);
                    allTokens.Remove(this.TokenSource);


                    break;
                }

                string msg = Encoding.ASCII.GetString(recievedBuffer, 0, recievedBuffer.Length);

                Message message = JsonConvert.DeserializeObject<Message>(msg);
                Response re = message.CreateResponse();

                Console.WriteLine(ID.ToString() + " sent: " + msg);
                //Broadcast(Name + " sent: " + msg);

                Console.WriteLine("replying with: " + JsonConvert.SerializeObject(re));
                Send(JsonConvert.SerializeObject(re));

                //foreach (var c in allClients)
                //{
                //    //if (c != this) //uncomment for multiple client usage
                //    c.Send(msg);
                //}

            }
        }
        void Send(string msg)
        {
            int byteCount = Encoding.ASCII.GetByteCount(msg);
            byte[] sendData = new byte[byteCount];
            sendData = Encoding.ASCII.GetBytes(msg);

            Stream.Write(sendData, 0, sendData.Length);
        }
    }
}
