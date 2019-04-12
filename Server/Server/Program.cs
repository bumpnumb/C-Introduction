using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.services;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {

            AsynchronousSocketListener.StartListening();

            
            //database.ExecuteServer();
            //database.TestConnection();

            Console.ReadLine();
        }
    }
}
