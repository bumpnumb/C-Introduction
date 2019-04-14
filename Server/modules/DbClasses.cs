using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Server.modules
{
    public class User
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Salt { get; set; }
        public string Hash { get; set; }
        public string Cookie { get; set; }
        public DateTime CookieTime { get; set; }
    }

    public class config
    {
        public string Server { get; set; }
        public string Port { get; set; }
        public string Database { get; set; }
        public string User { get; set; }
        public string Password { get; set; }

        public string Read(string filepath)
        {
            string text = File.ReadAllText(filepath);
            config c = new config();
            JsonConvert.PopulateObject(text, c);
            return "Server=" + c.Server + "; port=" + c.Port + "; database=" + c.Database + "; user=" + c.User + "; password=" + c.Password + ";";
        }
    }
}
