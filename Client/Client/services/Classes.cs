using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.services
{
    class Classes
    {
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
}
