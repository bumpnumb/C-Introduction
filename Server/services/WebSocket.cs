using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Security.Cryptography;
using System.Collections.Generic;
using Server.modules;
using Newtonsoft.Json;
using System.Linq;

namespace Server.services
{
    class WebServer
    {
        static TcpListener server = new TcpListener(IPAddress.Parse("127.0.0.1"), 80);
        static TcpClient client = default(TcpClient);
        CancellationTokenSource TokenSource;
        public WebServer()
        {
            try
            {
                server.Start();
                Console.WriteLine("WebServer started...");
                this.TokenSource = new CancellationTokenSource();
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
            while (true)
            {
                client = server.AcceptTcpClient();
                WebClientHandler handle = new WebClientHandler(client);
            }
        }

        //cancel()
        //token set cancel


    }
    class WebClientHandler
    {
        TcpClient Client;
        int ID;
        NetworkStream Stream;
        CancellationTokenSource TokenSource;

        static List<WebClientHandler> allClients = new List<WebClientHandler>();
        static List<CancellationTokenSource> allTokens = new List<CancellationTokenSource>();
        static int nextN = 0;

        private readonly object syncLock = new object();

        public WebClientHandler(TcpClient clientInfo)
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

        private void Disconnect()
        {
            Console.WriteLine("WebClient: " + this.ID.ToString() + " has disconnected");
            TokenSource.Cancel();
            Stream.Close();
            Client.Close();
            allClients.Remove(this);
        }
        private void Listen(object obj)
        {
            var address = Client.Client.RemoteEndPoint.ToString().Split(':');
            Console.WriteLine(String.Format("A client is connected from {0}", address[0]));

            CancellationToken ct = (CancellationToken)obj;
            while (!ct.IsCancellationRequested)
            {
                while (Stream.DataAvailable)
                {
                    Byte[] bytes = new Byte[Client.Available];
                    try
                    {
                        Stream.Read(bytes, 0, bytes.Length);
                    }
                    catch (System.IO.IOException)
                    {
                        Disconnect();
                        return;
                    }
                    //translate bytes of request to string
                    String data = Encoding.UTF8.GetString(bytes);

                    if (new Regex("^GET").IsMatch(data))
                    {
                        Byte[] response = Encoding.UTF8.GetBytes("HTTP/1.1 101 Switching Protocols" + Environment.NewLine
                            + "Connection: Upgrade" + Environment.NewLine
                            + "Upgrade: websocket" + Environment.NewLine
                            + "Sec-WebSocket-Accept: " + Convert.ToBase64String(
                                SHA1.Create().ComputeHash(
                                    Encoding.UTF8.GetBytes(
                                        new Regex("Sec-WebSocket-Key: (.*)").Match(data).Groups[1].Value.Trim() + "258EAFA5-E914-47DA-95CA-C5AB0DC85B11"
                                    )
                                )
                            ) + Environment.NewLine
                            + Environment.NewLine);
                        Stream.Write(response, 0, response.Length);
                        Console.WriteLine("Do the thing");
                    }
                    else
                    {
                        string msg = Encoding.UTF8.GetString(javaScriptUser(bytes));
                        if (msg == "Exit<00>")
                        {
                            Disconnect();
                            return;
                        }


                        switch (msg)
                        {
                            case "GET ALL COMPETITIONS":
                                Database db = new Database();
                                List<Competition> comp = db.GetAllCompetitions();
                                int i = 0;
                                foreach (Competition c in comp)
                                {
                                    i++;
                                }

                                string json = JsonConvert.SerializeObject(comp);
                                Send("{\"Type\":Competition,\"Num\":" + i + ",\"Data\":" + json + '}');

                                break;
                            case "GET COMPETITION:":
                                break;
                            default:
                                break;
                        }



                    }
                }
            }
        }
        public static byte[] Decode(string s)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(s);
            var hexString = BitConverter.ToString(bytes);
            hexString = hexString.Replace("-", "");
            return Encoding.Unicode.GetBytes(hexString);
        }
        void Send(string msg)
        {
            try
            {
                byte[] byteMsg = Decode(msg);
                byte[] sendMsg = new byte[0];
                int _maxLengthMessage = 125;
                bool flagStart = false;
                int sourceIndex = 0;
                while (byteMsg.Length - sourceIndex > _maxLengthMessage)
                {
                    sendMsg = new byte[127];
                    // I cut the mesasge in smaller pieces to send
                    if (!flagStart)
                    {
                        // In doc of Websockets i sign this piece: not the end, text
                        //*  % x1 denotes a text frame
                        sendMsg[0] = (byte)1;
                        flagStart = !flagStart;
                    }
                    else
                    {
                        // In doc of Websockets i sign this piece: not the end, continuation
                        //*  % x0 denotes a continuation frame
                        sendMsg[0] = (byte)0;
                    }
                    sendMsg[1] = (byte)_maxLengthMessage;
                    //Copy (Array sourceArray, long sourceIndex, Array destinationArray, long destinationIndex, long length);
                    Array.Copy(byteMsg, sourceIndex, sendMsg, 2, _maxLengthMessage);
                    sourceIndex += _maxLengthMessage;

                    Stream.Write(sendMsg, 0, _maxLengthMessage + 2);
                }
                sendMsg = new byte[(byteMsg.Length - sourceIndex) + 2];
                if (!flagStart)
                {
                    // If is this the only message we mark with: end of message, text
                    sendMsg[0] = (byte)(0x81);
                    flagStart = !flagStart;
                }
                else
                {
                    //else Is the end of the message but is the continuation frame
                    sendMsg[0] = (byte)(0x80);
                }

                sendMsg[1] = (byte)(byteMsg.Length - sourceIndex);
                Array.Copy(byteMsg, sourceIndex, sendMsg, 2, (byteMsg.Length - sourceIndex));
                Stream.Write(sendMsg, 0, (byteMsg.Length - sourceIndex) + 2);
                sourceIndex += _maxLengthMessage;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public byte[] javaScriptUser(Byte[] data)
        {
            //encoded[1] => size of msg
            Byte[] encoded = new Byte[((int)data[1] - 128)];
            Array.Copy(data, 6, encoded, 0, encoded.Length);
            Byte[] decoded = new Byte[((int)data[1] - 128)];

            //KeyCode positions
            Byte[] key = new Byte[4] { data[2], data[3], data[4], data[5] };

            for (int i = 0; i < encoded.Length; i++)
            {
                decoded[i] = (Byte)(encoded[i] ^ key[i % 4]);
            }
            Array.Copy(decoded, data, decoded.Length);
            return decoded;
        }
    }
}

