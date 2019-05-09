using System;
using Server.modules;
using Server.services;
using System.Net;
using System.Net.Sockets;


namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            //Database db = new Database();
            //db.StartConnection();
            //db.GetUserByID(1);

            //AsynchronousSocketListener.StartListening();

            TcpConnection con = new TcpConnection();

            WebServer ws = new WebServer();

            Console.WriteLine("this is debugging in 2019");

        }
    }
}