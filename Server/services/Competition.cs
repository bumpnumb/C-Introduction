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
            if(Judge == "hello" && Divers == "world")
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

            //Finns detta redan någonstans???
            //( ͡° ͜ʖ ͡°)
            //( ͡° ͜ʖ ͡°)
            //( ͡° ͜ʖ ͡°)
            //( ͡° ͜ʖ ͡°)
            //( ͡° ͜ʖ ͡°)

            //░░░░░░░░░░░░░░░░░░░░░░░░░░░░░
            //░░░░░░░░░░░░░▄▄▄▄▄▄▄░░░░░░░░░
            //░░░░░░░░░▄▀▀▀░░░░░░░▀▄░░░░░░░
            //░░░░░░░▄▀░░░░░░░░░░░░▀▄░░░░░░
            //░░░░░░▄▀░░░░░░░░░░▄▀▀▄▀▄░░░░░
            //░░░░▄▀░░░░░░░░░░▄▀░░██▄▀▄░░░░
            //░░░▄▀░░▄▀▀▀▄░░░░█░░░▀▀░█▀▄░░░
            //░░░█░░█▄▄░░░█░░░▀▄░░░░░▐░█░░░
            //░░▐▌░░█▀▀░░▄▀░░░░░▀▄▄▄▄▀░░█░░
            //░░▐▌░░█░░░▄▀░░░░░░░░░░░░░░█░░
            //░░▐▌░░░▀▀▀░░░░░░░░░░░░░░░░▐▌░
            //░░▐▌░░░░░░░░░░░░░░░▄░░░░░░▐▌░
            //░░▐▌░░░░░░░░░▄░░░░░█░░░░░░▐▌░
            //░░░█░░░░░░░░░▀█▄░░▄█░░░░░░▐▌░
            //░░░▐▌░░░░░░░░░░▀▀▀▀░░░░░░░▐▌░
            //░░░░█░░░░░░░░░░░░░░░░░░░░░█░░
            //░░░░▐▌▀▄░░░░░░░░░░░░░░░░░▐▌░░
            //░░░░░█░░▀░░░░░░░░░░░░░░░░▀░░░
            //░░░░░░░░░░░░░░░░░░░░░░░░░░░░░
        }

        public static Competition getActiveCompetitions()
        {
            Competition c = new Competition();
            return c;
        }
    }
}