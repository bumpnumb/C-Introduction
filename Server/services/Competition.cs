using System;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Server.modules;
using Server.services;
using Newtonsoft.Json;

namespace Server.services
{
    public class JumpHelper
    {
        public static string GenerateJumpNameFromCode(string code)
        {
            char[] chars = code.ToCharArray();
            string CodeString = "";

            if (chars[0] == 1) {
                CodeString += "Front ";
                if (chars[1] == 1)
                {
                    CodeString += "Flying ";
                    if (chars[2] == 2) {
                        CodeString += "Somersaults";    //112
                    }
                    else if (chars[2] == 3) {
                        CodeString += "1 1/2 Somersaults";  //113
                    }
                    else if (chars[2] == 5) {
                        CodeString += "2 1/2 Somersaults";     //115
                    }
                }
                else if (chars[1] == 0){
                    if (chars[2] == 1){
                        CodeString += "Dive";   //101
                    }
                    else if (chars[2] == 2){
                        CodeString += "Somersaults";    //102
                    }
                    else if (chars[2] == 3){
                        CodeString += "1 1/2 Somersaults";  //103
                    }
                    else if (chars[2] == 4){
                        CodeString += "2 Somersaults";  //104
                    }
                    else if (chars[2] == 5){
                        CodeString += "2 1/2 Somersaults";  //105
                    }
                    else if (chars[2] == 6){
                        CodeString += "3 Somersaults";  //106
                    }
                    else if (chars[2] == 7){
                        CodeString += "3 1/2 Somersaults";  //107
                    }
                    else if (chars[2] == 8){
                        CodeString += "4 Somersaults";  //108
                    }
                    else if (chars[2] == 9){
                        CodeString += "4 1/2 Somersaults";  //109
                    }
                }
            }
            else if (chars[0] == 2){
                CodeString += "Back ";
                if (chars[1] == 1){
                    CodeString += "Flying ";
                }
            }
            else if (chars[0] == 3){
                CodeString += "Reverse ";
                if (chars[1] == 1){
                    CodeString += "Flying ";
                }
            }
            else if (chars[0] == 4){
                CodeString += "Inward ";
                if (chars[1] == 1){
                    CodeString += "Flying ";
                }
            }
            else if (chars[0] == 5){
                CodeString += "Twisting ";
            }
            else if (chars[0] == 6){
                CodeString += "Armstand ";
            }
                return "hello";
        }


        public static float GenerateJumpDifficultyFromCode(string code)
        {


            return 1.0f;
        }

    }

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
    //jag tycker, oavsett vad, att vi behåller denna kommentar
}