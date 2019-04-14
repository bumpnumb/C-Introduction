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


                User u = new User
                {
                    Name = "Daniel",
                    Salt = "salty salt",
                    Hash = "420",
                    Cookie = "nomNom",
                    CookieTime = DateTime.Now
                };

                context.Users.Add(u);
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
                    data.AppendLine($"Cookie: {user.Cookie}");
                    data.AppendLine($"CookieTime: {user.CookieTime.ToString("u", CultureInfo.CreateSpecificCulture("en-US"))}");
                    Console.WriteLine(data.ToString());
                }
            }
        }
        public User GetUserByCookie(string Cookie)
        {
            using (var context = new DivingCompDbContext())
            {
                var u = context.Users.Where(x => x.Cookie == Cookie);

                if (u != null)
                {
                    User temp = new User();
                    foreach (var user in u) //should just return one but just in case ;)
                    {
                        //add error handling for multiple entries
                        temp.ID = user.ID;
                        temp.Name = user.Name;
                        temp.Salt = user.Salt;
                        temp.Hash = user.Hash;
                        temp.Cookie = user.Cookie;
                        temp.CookieTime = user.CookieTime;
                    }
                    return temp; //I guess this is some bootleg error handling
                }
            }
            return null;
        }


        public void UpdateCookieTimeByID(int ID, DateTime t)
        {
            using (var context = new DivingCompDbContext())
            {
                User u = context.Users.Where(x => x.ID == ID).FirstOrDefault();

                if (u != null)
                {
                    context.Users.Update(u);
                    u.CookieTime = t;
                    context.SaveChanges();
                }
            }
        }
    }
}



