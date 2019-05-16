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
            var context = new DivingCompDbContext();
            context.Database.EnsureCreated();

            int cID = 1;
            int uID = 2;

            string[] testJudge = {"J1", "J2", "J3"};
            foreach (string value in testJudge) {

                //CompetitionJudge yes = new CompetitionJudge
                //{
                //    UID = uID;
                //    CID = cID;
                //};
            }

            Competition c = new Competition();
            return true;
        }

        public static Competition getAllCompetitions()
        {
            Competition c = new Competition();
            return c;

            //Finns detta redan någonstans???
            //i dont tink så..
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
            //jag tycker, oavsett vad, att vi behåller denna kommentar
        }

        public static Competition getActiveCompetitions()
        {
            Competition c = new Competition();
            return c;
        }
    }
}