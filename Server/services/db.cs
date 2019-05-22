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
                Group = 0,
                //detta ska komma som ett argument!
                SSN = "temporary",
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
        public void CreateCompetition(CompetitionWithUser CompInfo, List<Jump> jumps)
        {
            var context = new DivingCompDbContext();
            context.Database.EnsureCreated();

            Competition c = new Competition();

            //c.ID = CompInfo.ID;
            c.Name = CompInfo.Name;
            c.Start = CompInfo.Start;
            //c.Finished = CompInfo.Finished
            c.Jumps = CompInfo.Jumps;

            context.Competitions.Add(c);

            List<int> CUIDs = new List<int>();

            foreach (User userJumper in CompInfo.Users)
            {
                CompetitionUser temp = new CompetitionUser();
                temp.CID = CompInfo.ID;
                temp.UID = userJumper.ID;
                context.CompetitionUsers.Add(temp);
                CUIDs.Add(temp.ID);
            }

            foreach (User userJudge in CompInfo.Judges)
            {
                CompetitionJudge temp = new CompetitionJudge();
                temp.CID = CompInfo.ID;
                temp.UID = userJudge.ID;
                context.CompetitionJudges.Add(temp);
            }

            //....................../´¯/)
            //....................,/¯../ 
            //.................../..../ 
            //............./´¯/'...'/´¯¯`·¸ 
            //........../'/.../..../......./¨¯\ 
            //........('(...´...´.... ¯~/'...') 
            //.........\.................'...../ 
            //..........''...\.......... _.·´ 
            //............\..............( 
            //..............\.............\...



            //public int CUID { get; set; } jump.cuid är just nu en id på en person.
            //public string Code { get; set; }
            //public string Name { get; set; }
            //public float Difficulty { get; set; }
            //public int Number { get; set; }


            foreach (Jump j in jumps)
            {
                Jump temp = new Jump();
                foreach (int ID in CUIDs) //cuid is a compuser ID
                {
                    if (ID == j.CUID)// this means our jump belongs to this compuser
                    {
                        temp.CUID = ID;
                        temp.Code = j.Code;
                        temp.Number = j.Number;
                        temp.Name = JumpHelper.GenerateJumpNameFromCode(j.Code, j.Height);
                        temp.Difficulty = JumpHelper.GenerateJumpDifficultyFromCode(j.Code, j.Height);
                        context.Jumps.Add(temp);
                    }
                }
            }
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
                    temp.Jumps = comp.Jumps;

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
                foreach (Competition comp in c)
                {
                    CompetitionWithUser temp = new CompetitionWithUser();
                    temp.ID = comp.ID;
                    temp.Name = comp.Name;
                    temp.Start = comp.Start;
                    temp.Finished = comp.Finished;
                    temp.Jumps = comp.Jumps;

                    List<User> users = context.Users.Where(u => context.CompetitionUsers.Any(cu => u.ID == cu.UID & cu.CID == comp.ID)).ToList();
                    List<User> judges = context.Users.Where(j => context.CompetitionJudges.Any(cj => j.ID == cj.UID & cj.CID == comp.ID)).ToList();

                    temp.Users = users;
                    temp.Judges = judges;
                    result.Add(temp);
                }

                return result;


            }
        }

        public CompetitionWithUser GetCompetitionWithUserFromID(int ID)
        {
            using (var context = new DivingCompDbContext())
            {
                Competition comp = context.Competitions.Where(x => x.ID == ID).FirstOrDefault();
                CompetitionWithUser cwu = new CompetitionWithUser();

                cwu.ID = comp.ID;
                cwu.Name = comp.Name;
                cwu.Start = comp.Start;
                cwu.Finished = comp.Finished;
                cwu.Jumps = comp.Jumps;
                List<User> users = context.Users.Where(u => context.CompetitionUsers.Any(cu => u.ID == cu.UID & cu.CID == comp.ID)).ToList();
                List<User> judges = context.Users.Where(j => context.CompetitionJudges.Any(cj => j.ID == cj.UID & cj.CID == comp.ID)).ToList();
                cwu.Users = users;
                cwu.Judges = users;
                return cwu;
            }
        }

        public CompetitionWithResult GetCompetitionWithResultFromID(int ID)
        {
            using (var context = new DivingCompDbContext())
            {
                CompetitionWithResult cwr = new CompetitionWithResult();
                cwr.Comp = GetCompetitionWithUserFromID(ID);

                //users = context.Users.Where(u => context.CompetitionUsers.Any(cu => u.ID == cu.UID & cu.CID == comp.ID)).ToList();
                //users where id = ( in competition users where cu.UID <-- and cu.cid == comp.id)
                cwr.Jumps = context.Jumps.Where(jump =>
                    context.CompetitionUsers.Any(cu => jump.CUID == cu.ID && cu.CID == cwr.Comp.ID)).ToList();

                cwr.Results = context.Results.Where(res => cwr.Jumps.Any(jump => res.JumpID == jump.ID)).ToList();


                return cwr;
            }
        }

        public void SetScoreToJump(Result ScoreInfo)
        {
            var context = new DivingCompDbContext();
            context.Database.EnsureCreated();

            Result r = new Result();
            r.JudgeID = ScoreInfo.JudgeID;
            r.JumpID = ScoreInfo.JumpID;
            r.Score = ScoreInfo.Score;

            context.Results.Add(r);
            context.SaveChanges();

        }

    }
}



