using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MySql.Data.EntityFrameworkCore;
using Server.modules;
using System.Globalization;
using Server.services;

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
                    data.AppendLine($"Salt: {user.Salt}");
                    data.AppendLine($"Hash: {user.Hash}");
                    Console.WriteLine(data.ToString());
                }
            }
        }
        public User GetUserByName(string name)
        {
            using (var context = new DivingCompDbContext())
            {
                var u = context.Users.Where(x => x.Name == name).FirstOrDefault();
                return u;
            }
        }

        public void RegisterUser(string name, string salt, string hash)
        {
            var context = new DivingCompDbContext(); //är detta en ny "tabell" i databasen? i detta fall en ny user?
            context.Database.EnsureCreated();

            User u = new User
            {
                Name = name,
                Salt = salt,
                Hash = hash,
                //detta ska komma som ett argument!
                SSN = "temporary",
                Group = 0
            };

            context.Users.Add(u);
            context.SaveChanges();
        }
        public string GetSaltByID(int ID)
        {
            using (var context = new DivingCompDbContext())
            {
                User u = context.Users.Where(x => x.ID == ID).FirstOrDefault();
                return u.Salt;
            }
        }
        public string GetHashByID(int ID)
        {
            using (var context = new DivingCompDbContext())
            {
                User u = context.Users.Where(x => x.ID == ID).FirstOrDefault();
                return u.Hash;
            }
        }
        public void CreateCompetitions(/*argument*/)
        {
            var context = new DivingCompDbContext();
            context.Database.EnsureCreated();

            Competition c = new Competition
            {
                //ID = ID,              //
                //Name = name,          //kanske ska vara listor... eller hur funkar det här?
                //Start = time,         //var ska vi lagra Divers, och Judges som är knutna till denna competition? 
                //Finished = time       //Ska judges och Divers ha på sin class, vilka competitions de hör till?
            };
            context.Competitions.Add(c);
            context.SaveChanges();
        }

        public List<Competition> GetAllCompetitions()
        {
            using (var context = new DivingCompDbContext())
            {
                return context.Competitions.ToList<Competition>();
            }
        }
    }
}



