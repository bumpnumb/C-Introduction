using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.services
{
    public class person
    {
        public string Name { get; set; }
        public string Betyg { get; set; }
        public int Yes { get; set; }
        public string Bobby { get; set; }

        public void print()
        {
            Console.WriteLine(this.Name + ' ' + this.Betyg + ' ' + this.Yes + ' ' + this.Bobby);
        }

    }
}
