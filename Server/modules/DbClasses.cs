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
    }

    public class config
    {
        public string server { get; set; }
        public string port { get; set; }
        public string database { get; set; }
        public string user { get; set; }
        public string password { get; set; }

        public string Read(string filepath)
        {
            string text = File.ReadAllText(filepath);
            config c = new config();
            JsonConvert.PopulateObject(text, c);
            return "Server=" + c.server + "; port=" + c.port + "; database=" + c.database + "; user=" + c.user + "; password=" + c.password + ";";
        }
    }
}
