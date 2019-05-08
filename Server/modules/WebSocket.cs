using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Security.Cryptography;

public class WebServer
{
    private static void ThreadProc(object obj)
    {
        var client = (TcpClient)obj;

        var address = client.Client.RemoteEndPoint.ToString().Split(':');

        Console.WriteLine(String.Format("A client is connected from {0}", address[0]));

        NetworkStream stream = client.GetStream();

        //enter to an infinite cycle to be able to handle every change in stream
        while (true)
        {
            while (!stream.DataAvailable) ;

            Byte[] bytes = new Byte[client.Available];

            stream.Read(bytes, 0, bytes.Length);

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

                stream.Write(response, 0, response.Length);
            }
            else
            {
                Console.WriteLine(data);
            }
        }
    }

    static void Start()
    {
        TcpListener server = new TcpListener(IPAddress.Parse("127.0.0.1"), 80);

        server.Start();
        Console.WriteLine("Server has started on 127.0.0.1:80.{0}Waiting for a connection...", Environment.NewLine);

        while (true)
        {
            var clientConnection = server.AcceptTcpClient();
            ThreadPool.QueueUserWorkItem(ThreadProc, clientConnection);
        }

    }
}

