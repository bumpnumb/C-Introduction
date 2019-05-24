using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.services
{

    public class Competition
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime Start { get; set; }
        public DateTime Finished { get; set; }
        public int Jumps { get; set; }
    }

    public class CompetitionWithUser
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime Start { get; set; }
        public DateTime Finished { get; set; }
        public int Jumps { get; set; }
        public List<User> Users { get; set; }
        public List<User> Judges { get; set; }

    }

    public class CompetitionWithResult
    {
        public CompetitionWithUser Comp { get; set; }
        public List<Jump> Jumps { get; set; }
        public List<Result> Results { get; set; }
    }

    public class Jump
    {
        public int ID { get; set; }
        public int CUID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public float Difficulty { get; set; }
        public int Number { get; set; }
        public int GlobalNumber { get; set; }
        public int Height { get; set; }
    }

    public class User
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Salt { get; set; }
        public string Hash { get; set; }
        public GroupType Group { get; set; }
        public string SSN { get; set; }

    }

    public class Result
    {
        public int JumpID { get; set; }
        public int JudgeID { get; set; }
        public float Score { get; set; }
    }
}
