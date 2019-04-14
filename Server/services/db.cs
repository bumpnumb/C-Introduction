using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MySql.Data.EntityFrameworkCore;
using Server.modules;

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
                //    Hash = "420"
                //};

                //context.Users.Add(u);
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
                    data.AppendLine($"Salt: {user.Salt}");
                    data.AppendLine($"Hash: {user.Hash}");
                    Console.WriteLine(data.ToString());
                }
            }
        }
    }
}



