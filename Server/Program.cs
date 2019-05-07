using System;
using Server.modules;
using Server.services;
using System.Net;
using System.Net.Sockets;
using WebSocketSharp.Server;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            //Database db = new Database();
            //db.StartConnection();
            //db.GetUserByID(1);


            //TcpConnection con = new TcpConnection();

            var wssv = new WebSocketServer(4649);
            wssv.AddWebSocketService<Echo>("/Echo");

            Console.WriteLine("this is debugging in 2019");

        }
    }
}
