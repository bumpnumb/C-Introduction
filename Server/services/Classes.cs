using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Server.modules
{
    //These are a set of classes being used by different parts of the program.
    //Mostly used to parse messages and to retrieve data from db
    //Explains itself.
    public enum MessageType { NoType, Login, Register, Competition }
    public class Response
    {
        public MessageType Type { get; set; }
        public string Data { get; set; }
        public User user { get; set; }
    }
    public class User
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string SSN { get; set; }
        public int Group { get; set; }
        public string Salt { get; set; }
        public string Hash { get; set; }
    }
    public class UICJump
    {
        public int ID { get; set; }
        public int UICID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public float Difficulty { get; set; }
    }
    public class Result
    {
        public int UICJID { get; set; }
        public float Score { get; set; }
    }
    public class Competition
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string[] Judges { get; set; }
        public string[] Divers { get; set; }
        public DateTime Start { get; set; }
        public DateTime Finished { get; set; }
    }

    public class CompetitionWithUser
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime Start { get; set; }
        public DateTime Finished { get; set; }
        public List<User> Users { get; set; }
        public List<User> Judges { get; set; }

    }

    public class CompetitionUser
    {
        public int ID { get; set; }
        public int UID { get; set; }
        public int CID { get; set; }
    }
    public class CompetitionJudge
    {
        public int UID { get; set; }
        public int CID { get; set; }
    }
    public class config
    {
        public string Server { get; set; }
        public string Port { get; set; }
        public string Database { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public bool AllowZeroDatetime { get; set; }
        public bool ConvertZeroDatetime { get; set; }



        public string Read(string filepath)
        {
            string text = File.ReadAllText(filepath);
            config c = new config();
            JsonConvert.PopulateObject(text, c);
            return "Server=" + c.Server + "; port=" + c.Port + "; database=" + c.Database + "; user=" + c.User + "; password=" + c.Password + "; Allow Zero Datetime=" + c.AllowZeroDatetime + "; Convert Zero Datetime=" + c.ConvertZeroDatetime + ";";
        }
    }
}
