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

            //TcpConnection con = new TcpConnection();

            //WebServer ws = new WebServer();

            //Console.WriteLine("this is debugging in 2019");

            while (true)
            {
                string a = Console.ReadLine();
                if (a != null)
                {
                    if (a.Length == 5)
                    {
                        a.Split("");
                        Console.WriteLine(JumpHelper.ParseDifficulty(a[0].ToString() + ',' + a[1] + ',' + a[2] + ',' + a[3], Int32.Parse(a[4].ToString())).Difficulty);
                    }
                    else if (a.Length == 6)
                    {
                        a.Split("");
                        Console.WriteLine(JumpHelper.ParseDifficulty(a[0].ToString() + ',' + a[1] + ',' + a[2] + ',' + a[3] + ',' + a[4], Int32.Parse(a[5].ToString())).Difficulty);
                    }
                }
            }

            //Jump j = JumpHelper.ParseDifficulty("2,0,7,C", 3);
            //Console.WriteLine(j.Difficulty);

        }
    }
}