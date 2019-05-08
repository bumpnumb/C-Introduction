using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MySql.Data.EntityFrameworkCore;
using Server.modules;
using System.Globalization;

namespace Server.services
{
    class Database
    {
        public void StartConnection()
        {


            using (var context = new DivingCompDbContext())
            {
                context.Database.EnsureCreated();
                Console.WriteLine("Connection to database has been made");


                //User u = new User
                //{
                //    Name = "Daniel",
                //    Salt = "salty salt",
                //    Hash = "420",
                //    Cookie = "nomNom",
                //    CookieTime = DateTime.Now
                //};

                //context.Users.Add(u);

                //Competition c = new Competition
                //{
                //    Name = "Första Tävlingen",
                //    Start = DateTime.Now.AddHours(3),
                //    Finished = 0
                //};

                //context.Competitions.Add(c);


                context.SaveChanges();
            }
        }
        public void GetUserByID(int ID)
        {
            using (var context = new DivingCompDbContext())
            {
                var u = context.Users.Where(x => x.ID == ID);

                foreach (var user in u)
                {
                    var data = new StringBuilder();
                    data.AppendLine($"ID: {user.ID}");
                    data.AppendLine($"Name: {user.Name}");
                    data.AppendLine($"Name: {user.Group}");
                    data.AppendLine($"Salt: {user.Salt}");
                    data.AppendLine($"Hash: {user.Hash}");
                    Console.WriteLine(data.ToString());
                }
            }
        }




    }
}



