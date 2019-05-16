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

            int cID = 1;
            int uID = 2;

            string[] testJudge = { "J1", "J2", "J3" };
            foreach (string value in testJudge)
            {

                //CompetitionJudge yes = new CompetitionJudge
                //{
                //    UID = uID;
                //    CID = cID;
                //};
            }

            Competition c = new Competition();      //detta skall obv. bort sen
            context.Competitions.Add(c);
            context.SaveChanges();
        }


        public List<CompetitionWithUser> GetActiveCompetitions()
        {
            using (var context = new DivingCompDbContext())
            {
                List<CompetitionWithUser> result = new List<CompetitionWithUser>();
                List<Competition> c = context.Competitions.Where(x => x.Start <= DateTime.Now && !helper.IsFinished(x.Finished)).ToList<Competition>();
                foreach (Competition comp in c)
                {
                    CompetitionWithUser temp = new CompetitionWithUser();
                    temp.ID = comp.ID;
                    temp.Name = comp.Name;
                    temp.Start = comp.Start;
                    temp.Finished = comp.Finished;

                    List<User> users = context.Users.Where(u => context.CompetitionUsers.Any(cu => u.ID == cu.UID & cu.CID == comp.ID)).ToList();
                    List<User> judges = context.Users.Where(j => context.CompetitionJudges.Any(cj => j.ID == cj.UID & cj.CID == comp.ID)).ToList();

                    temp.Users = users;
                    temp.Judges = judges;
                    result.Add(temp);
                }

                return result;
            }
        }

        public List<CompetitionWithUser> GetAllCompetitions()
        {
            using (var context = new DivingCompDbContext())
            {
                List<CompetitionWithUser> result = new List<CompetitionWithUser>();
                List<Competition> c = context.Competitions.ToList<Competition>();
                int i = 0;
                foreach (Competition comp in c)
                {
                    CompetitionWithUser temp = new CompetitionWithUser();
                    temp.ID = comp.ID;
                    temp.Name = comp.Name;
                    temp.Start = comp.Start;
                    temp.Finished = comp.Finished;

                    List<User> users = context.Users.Where(u => context.CompetitionUsers.Any(cu => u.ID == cu.UID & cu.CID == comp.ID)).ToList();
                    List<User> judges = context.Users.Where(j => context.CompetitionJudges.Any(cj => j.ID == cj.UID & cj.CID == comp.ID)).ToList();

                    temp.Users = users;
                    temp.Judges = judges;
                    result.Add(temp);
                }

                return result;


            }
        }
    }
}



