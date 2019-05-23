using System;
using System.Collections.Generic;
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

            CompetitionWithUser c = new CompetitionWithUser();
            c.Name = "Genererad tävling";
            c.Start = DateTime.Now;
            c.Jumps = 3;

            List<User> judges = new List<User>();
            User j1 = new User();
            j1.ID = 21;
            judges.Add(j1);
            User j2 = new User();
            j2.ID = 23;
            judges.Add(j2);
            User j3 = new User();
            j3.ID = 24;
            judges.Add(j3);
            
            c.Judges = judges;

            List<User> jumpers = new List<User>();
            User u1 = new User();
            u1.ID = 22;
            jumpers.Add(u1);
            User u2 = new User();
            u2.ID = 27;
            jumpers.Add(u2);
            User u3 = new User();
            u3.ID = 28;
            jumpers.Add(u3);

            c.Users = jumpers;

            List<Jump> jumps = new List<Jump>();

            Jump jump1 = new Jump();
            jump1.Code = "2,0,7,B";
            jump1.Height = 3;
            jump1.CUID = 22;
            jump1.Number = 0;
            jumps.Add(jump1);

            jump1 = new Jump();
            jump1.Code = "3,0,5,B";
            jump1.Height = 3;
            jump1.CUID = 22;
            jump1.Number = 1;
            jumps.Add(jump1);

            jump1 = new Jump();
            jump1.Code = "4,0,5,C";
            jump1.Height = 3;
            jump1.CUID = 22;
            jump1.Number = 2;
            jumps.Add(jump1);

            jump1 = new Jump();
            jump1.Code = "4,0,9,B";
            jump1.Height = 3;
            jump1.CUID = 27;
            jump1.Number = 0;
            jumps.Add(jump1);

            jump1 = new Jump();
            jump1.Code = "4,1,3,C";
            jump1.Height = 1;
            jump1.CUID = 27;
            jump1.Number = 1;
            jumps.Add(jump1);

            jump1 = new Jump();
            jump1.Code = "2,0,7,B";
            jump1.Height = 3;
            jump1.CUID = 27;
            jump1.Number = 2;
            jumps.Add(jump1);

            jump1 = new Jump();
            jump1.Code = "2,0,7,B";
            jump1.Height = 3;
            jump1.CUID = 28;
            jump1.Number = 0;
            jumps.Add(jump1);

            jump1 = new Jump();
            jump1.Code = "2,0,7,B";
            jump1.Height = 3;
            jump1.CUID = 28;
            jump1.Number = 1;
            jumps.Add(jump1);

            jump1 = new Jump();
            jump1.Code = "2,0,7,B";
            jump1.Height = 3;
            jump1.CUID = 28;
            jump1.Number = 2;
            jumps.Add(jump1);

            Database db = new Database();
            db.CreateCompetition(c, jumps);

            //Jump j = JumpHelper.ParseDifficulty("5,3,5,5,B", 3);
            //Console.WriteLine(j.Difficulty);

        }
    }
}