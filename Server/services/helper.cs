using System;
using System.Collections.Generic;
using System.Text;

namespace Server.services
{
    class helper
    {
        ///<summary>
        ///Returns a Boolean indicating if a DateTime is not '0001-01-01:000'
        ///</summary>
        public static bool IsFinished(DateTime time) 
        {
        //to avoid bad casting, time in db is defaulted to 0000000000,
        // if a time is set, comp IS finished.
        DateTime def = new DateTime(0001, 1, 1, 0, 0, 0);
            if (DateTime.Equals(def, time))
                return false;
            return true;
        }

}
}
