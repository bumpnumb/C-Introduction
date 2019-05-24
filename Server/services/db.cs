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

                context.SaveChanges();
            }
        }


        public List<User> GetAllUsers()
        {
            using (var context = new DivingCompDbContext())
            {
                //simply adds all users from the database to a list.
                return context.Users.ToList();
            }
        }

        public void GetUserByID(int ID)
        {
            using (var context = new DivingCompDbContext())
            {
                //find a user in the database with the specific ID.
                var u = context.Users.Where(x => x.ID == ID);

                //for each user found, print all info of the user.
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
                //returns the user with the specific name.
                var u = context.Users.Where(x => x.Name == name).FirstOrDefault();
                return u;
            }
        }
        public User GetUserBySSN(string ssn)
        {
            using (var context = new DivingCompDbContext())
            {
                //returns the user with the specific SSN.
                var u = context.Users.FirstOrDefault(x => x.SSN == ssn);
                return u;
            }
        }

        public void RegisterUser(string name,string ssn, string salt, string hash)
        {
            var context = new DivingCompDbContext();
            context.Database.EnsureCreated();

            //create a new user with provided information.
            User u = new User
            {
                Name = name,
                SSN = ssn,
                Salt = salt,
                Hash = hash,
                Group = 0,
            };

            //add the user in the database.
            context.Users.Add(u);
            context.SaveChanges();
        }
        public string GetSaltByID(int ID)
        {
            using (var context = new DivingCompDbContext())
            {
                //returns salt with the specific ID
                User u = context.Users.Where(x => x.ID == ID).FirstOrDefault();
                return u.Salt;
            }
        }
        public string GetHashByID(int ID)
        {
            using (var context = new DivingCompDbContext())
            {
                //returns salt from the specific ID
                User u = context.Users.Where(x => x.ID == ID).FirstOrDefault();
                return u.Hash;
            }
        }
        public void CreateCompetition(CompetitionWithUser CompInfo, List<Jump> jumps)
        {
            var context = new DivingCompDbContext();
            context.Database.EnsureCreated();

            //create a new competition object to later add tot the database.
            Competition c = new Competition();

            //add provided info to the competition object.
            c.Name = CompInfo.Name;
            c.Start = CompInfo.Start;
            c.Jumps = CompInfo.Jumps;

            //add it to the database.
            context.Competitions.Add(c);
            context.SaveChanges();

            List<CompetitionUser> CUIDs = new List<CompetitionUser>();

            //for each jumper in the provided information, add to the database and to the competition
            foreach (User userJumper in CompInfo.Users)
            {
                CompetitionUser temp = new CompetitionUser();
                temp.CID = c.ID;
                temp.UID = userJumper.ID;

                context.CompetitionUsers.Add(temp);
                context.SaveChanges();

                CompetitionUser tmp = new CompetitionUser();
                tmp.ID = temp.ID;
                tmp.UID = temp.UID;
                CUIDs.Add(tmp);
            }

            //for each judge in the provided information, add judge to the competition.
            foreach (User userJudge in CompInfo.Judges)
            {
                CompetitionJudge temp = new CompetitionJudge();
                temp.CID = c.ID;
                temp.UID = userJudge.ID;
                context.CompetitionJudges.Add(temp);
            }

            //aa...
            List<Jump> orderedJumps = jumps.OrderBy(o => o.CUID).ToList();
            int n = 0;
            for (int i = 0; i < orderedJumps.Count; i++)
            {
                if (i % c.Jumps == 0 && i != 0)
                    n++;

                orderedJumps[i].GlobalNumber = orderedJumps[i].Number * (orderedJumps.Count / c.Jumps) + n;
            }

            foreach (Jump j in orderedJumps)
            {
                Jump temp = new Jump();

                for (int i = 0; i < CUIDs.Count; i++) //for all CU stored
                // CUIDs.ID == compuser.ID, CUIDs.UID == user ID
                {
                    // currently, j.CUID == compuser.UID but this will be changed to compuser.ID
                    if (CUIDs[i].UID == j.CUID)// this means our jump belongs to this compuser
                    {
                        temp.CUID = CUIDs[i].ID; //here
                        temp.Code = j.Code;
                        temp.Number = j.Number;
                        temp.Height = j.Height;

                        temp.GlobalNumber = j.GlobalNumber;

                        Jump t = JumpHelper.ParseDifficulty(j.Code, j.Height);  
                        temp.Name = t.Name;
                        temp.Difficulty = t.Difficulty;
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
                //create two lists, one to get all active competitions, and one for all users in those competitions
                List<CompetitionWithUser> result = new List<CompetitionWithUser>();
                List<Competition> c = context.Competitions.Where(x => x.Start <= DateTime.Now && !helper.IsFinished(x.Finished)).ToList<Competition>();

                //for each active competition, add everything needed to the first list (result)
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
                //create two lists to, one for the result and one to acquire all info from the db.
                List<CompetitionWithUser> result = new List<CompetitionWithUser>();
                List<Competition> c = context.Competitions.ToList<Competition>();

                //add each comp to the result
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

        //return a competition with the user ID that is provided. 
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

        //return a competition with results from the user ID that is provided. 
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

                foreach (Jump j in cwr.Jumps)
                {
                    CompetitionUser temp = context.CompetitionUsers.FirstOrDefault(x => x.ID == j.CUID);
                    j.CUID = temp.UID;
                }

                cwr.Results = context.Results.Where(res => cwr.Jumps.Any(jump => res.JumpID == jump.ID)).ToList();
                return cwr;
            }
        }

        //Add a score by a judge to a specific jump.
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

        public List<User> GetAllJudges()
        {
            using (var context = new DivingCompDbContext())
            {
                //returns all judes in a list.
                return context.Users.Where(x => x.Group == 1).ToList();
            }
        }

        public List<User> GetAllJumpers()
        {
            using (var context = new DivingCompDbContext())
            {
                //returns all judes in a list.
                return context.Users.Where(x => x.Group == 0).ToList();
            }
        }
    }
}

