using System;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Server.modules;
using Server.services;
using Newtonsoft.Json;

namespace Server.services
{
    static class competitions
    {
        public static bool createCompetition(string Judge, string Divers)
        {
            Competition c = new Competition();
            //exempel
            if(Judge == "hej")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static Competition getAllCompetitions()
        {
            Competition c = new Competition();
            return c;
            //if(competitions != null)
                //send it all back baby
            //else
                //send error back baby
        }

        public static Competition getActiveCompetitions()
        {
            return true;
        }
    }
}