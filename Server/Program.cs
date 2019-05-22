using System;
using Server.modules;
using Server.services;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {

            TcpConnection con = new TcpConnection();

            WebServer ws = new WebServer();

            Console.WriteLine("this is debugging in 2019");
            //Jump j = JumpHelper.ParseDifficulty("5,3,5,5,B", 3);
            //Console.WriteLine(j.Difficulty);

        }
    }
}