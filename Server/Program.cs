using System;
using Server.modules;
using Server.services;


namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Database db = new Database();
            db.StartConnection();
            db.GetUserByID(1);

            AsynchronousSocketListener.StartListening();
        }
    }
}
