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

namespace Server.services
{
    class WebServer
    {
        static TcpListener server = new TcpListener(IPAddress.Parse("127.0.0.1"), 80);
        static TcpClient client = default(TcpClient);
        public WebServer()
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
                WebClientHandler handle = new WebClientHandler(client);
            }
        }
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
                                string testeriioni = JsonConvert.SerializeObject(comp);

                                string holder = "{\"Competitions\":" + i + ",\"Data\":" + JsonConvert.SerializeObject(comp) + "}";
                                WebMessage wm = new WebMessage();
                                wm.Data = comp;
                                wm.Type = webType.Competitions;
                                wm.Num = i;




                                Send(JsonConvert.SerializeObject(wm));



                                break;
                            default:
                                break;
                        }



                    }
                }
            }
        }
        private byte[] EncodeOutgoingMessage(string message)
        {
            /* this is how and header should be made:
             *   - first byte  -> FIN + RSV1 + RSV2 + RSV3 + OPCODE
             *   - second byte -> MASK + payload length (only 7 bits)
             *   - third, fourth, fifth and sixth bytes -> (optional) XOR encoding key bytes
             *   - following bytes -> the encoded (if a key has been used) payload
             *
             *   FIN    [1 bit]      -> 1 if the whole message is contained in this frame, 0 otherwise
             *   RSVs   [1 bit each] -> MUST be 0 unless an extension is negotiated that defines meanings for non-zero values
             *   OPCODE [4 bits]     -> defines the interpretation of the carried payload
             *
             *   MASK           [1 bit]  -> 1 if the message is XOR masked with a key, 0 otherwise
             *   payload length [7 bits] -> can be max 1111111 (127 dec), so, the payload cannot be more than 127 bytes per frame
             *
             * valid OPCODES:
             *   - 0000 [0]             -> continuation frame
             *   - 0001 [1]             -> text frame
             *   - 0010 [2]             -> binary frame
             *   - 0011 [3] to 0111 [7] -> reserved for further non-control frames
             *   - 1000 [8]             -> connection close
             *   - 1001 [9]             -> ping
             *   - 1010 [A]             -> pong
             *   - 1011 [B] to 1111 [F] -> reserved for further control frames
             */
            // in our case the first byte will be 10000001 (129 dec = 81 hex).
            // the length is going to be (masked)1 << 7 (OR) 0 + payload length.
            byte[] payload = Encoding.UTF8.GetBytes(message);
            Console.WriteLine(payload);

            byte[] header = new byte[] { 1, (byte)(payload.Length) }; //0x81
            // by default the mask array is empty...
            byte[] maskKey = new byte[4];

            // let's get the bytes of the message to send.
            // this is going to be the whole frame to send.
            byte[] frame = new byte[header.Length + payload.Length];
            // add the header.
            Array.Copy(header, frame, header.Length);
            // add the mask if necessary.
            if (maskKey.Length > 0)
            {
                Array.Copy(maskKey, 0, frame, header.Length, maskKey.Length);
                // let's encode the payload using the mask.
                for (int i = 0; i < payload.Length; i++)
                {
                    payload[i] = (byte)(payload[i] ^ maskKey[i % maskKey.Length]);
                }
            }


            // add the payload.
            Array.Copy(payload, 0, frame, header.Length, payload.Length);
            foreach (char item in frame)
            {
                Console.Write(item);
            }
            Console.WriteLine();

            return frame;
        }
        void Send(string msg)
        {
            byte[] m = EncodeOutgoingMessage(msg);
            Stream.Write(m, 0, m.Length);
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

