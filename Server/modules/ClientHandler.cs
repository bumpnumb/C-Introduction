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
using Server.modules;

namespace Server.services
{
    class TcpConnection
    {
        static IPAddress ip = Dns.GetHostEntry("localhost").AddressList[0];
        static TcpListener server = new TcpListener(ip, 8787);
        static TcpClient client = default(TcpClient);
        CancellationTokenSource TokenSource;
        public TcpConnection()
        {
            try
            {
                server.Start();
                Console.WriteLine("Server started...");
                //Create a token for cancellation, not used but might be needed
                this.TokenSource = new CancellationTokenSource();
                //Start a new thread to accept clients
                Thread acceptThread = new Thread(new ParameterizedThreadStart(acceptClient));
                acceptThread.Start(TokenSource.Token);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.Read();
            }

        }

        public void acceptClient(object obj)
        {
            //Await clients
            //This needs to be threaded because we have other acceptors.
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


        //Holds all clients & token as static to remove individual clients
        static List<ClientHandler> allClients = new List<ClientHandler>();
        static List<CancellationTokenSource> allTokens = new List<CancellationTokenSource>();
        static int nextN = 0;

        //SyncLock so no two clients can conflict numbers
        private readonly object syncLock = new object();

        public ClientHandler(TcpClient clientInfo)
        {
            //Instantiate new client
            this.Client = clientInfo;
            this.ID = nextN;
            this.Stream = Client.GetStream();
            this.TokenSource = new CancellationTokenSource();

            lock (syncLock)
            {
                allClients.Add(this);
                nextN++;
            }

            //Fire of thread for client
            Thread clientThread = new Thread(new ParameterizedThreadStart(Listen));
            clientThread.Start(TokenSource.Token);
        }

        public void Listen(object obj)
        {
            //Can be cancelled with token
            CancellationToken ct = (CancellationToken)obj;
            while (!ct.IsCancellationRequested)
            {
                //Readbuffer, since reading from stream.
                byte[] recievedBuffer = new byte[1024];
                int bytesRead = 0;
                StringBuilder msg = new StringBuilder();

                //While DataAvailable read from stream and append to msg
                do
                {
                    try
                    {
                        bytesRead = Stream.Read(recievedBuffer, 0, recievedBuffer.Length);
                        msg.AppendFormat("{0}", Encoding.Unicode.GetString(recievedBuffer, 0, bytesRead));
                    }
                    catch (System.IO.IOException)
                    {
                        //IOException should only be when client disconnect during message transit
                        //Removes all trace of client
                        Console.WriteLine("Client: " + this.ID.ToString() + " has disconnected");

                        Stream.Close();
                        Client.Close();

                        allClients.Remove(this);
                        allTokens.Remove(this.TokenSource);
                        break;
                    }
                }
                while (Stream.DataAvailable);

                string readMsg = msg.ToString();
                if (readMsg != "")
                {
                    //Parse message
                    Message objMessage = JsonConvert.DeserializeObject<Message>(readMsg);
                    //Generate response
                    Response rsp = objMessage.CreateResponse();

                    //Send message back to client
                    Console.WriteLine("replying with: " + JsonConvert.SerializeObject(rsp));
                    Send(JsonConvert.SerializeObject(rsp));
                }
                else
                {
                    //breaks stream on empty message?
                    Console.WriteLine("Client: " + this.ID.ToString() + " has disconnected");

                    Stream.Close();
                    Client.Close();

                    allClients.Remove(this);
                    allTokens.Remove(this.TokenSource);
                    break;
                }
            }
        }
        void Send(string msg)
        {
            int byteCount = Encoding.Unicode.GetByteCount(msg);
            byte[] sendData = new byte[byteCount];
            sendData = Encoding.Unicode.GetBytes(msg);

            Stream.Write(sendData, 0, sendData.Length);
        }
    }
}
