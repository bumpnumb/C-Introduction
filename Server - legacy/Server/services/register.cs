using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.services
{
    class register
    {
        public static bool TryRegister(string msg)
        {
            // Register data will look like following:
            // {type: 'judge'; ID: 'Daniel'; PW: 'losen';}
            string[] data = msg.Split(';');

            if (!database.UserExists(data[1]))
            {

            }

            return true;
        }
    }
}
