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
                    if(chars[2] == 2){
                        CodeString += "Somersaults";    //212
                    }
                    else if(chars[2] == 3){
                        CodeString += "1 1/2 Somersaults";  //213
                    }
                    else if (chars[2] == 5){
                        CodeString += "2 1/2 Somersaults";  //215
                    }
                }
                else if(chars[1] == 0){
                    if(chars[2] == 1){
                        CodeString += "Dive";   //201
                    }
                    else if (chars[2] == 2){
                        CodeString += "Somersaults";    //202
                    }
                    else if (chars[2] == 3){
                        CodeString += "1 1/2 Somersaults";  //203
                    }
                    else if (chars[2] == 4){
                        CodeString += "2 Somersaults";  //204
                    }
                    else if (chars[2] == 5){
                        CodeString += "2 1/2 Somersaults"; //205
                    }
                    else if (chars[2] == 6){
                        CodeString += "3 Somersaults";  //206
                    }
                    else if (chars[2] == 7){
                        CodeString += "3 1/2 Somersaults";  //207
                    }
                    else if (chars[2] == 8){
                        CodeString += "4 Somersaults";  //208
                    }
                    else if (chars[2] == 9){
                        CodeString += "4 1/2 Somersaults";  //209
                    }
                }
            }
            else if (chars[0] == 3){
                CodeString += "Reverse ";
                if (chars[1] == 1){
                    CodeString += "Flying ";
                    if (chars[2] == 2){
                        CodeString += "Somersaults";    //312
                    }
                    else if (chars[2] == 3){
                        CodeString += "1 1/2 Somersaults";  //313
                    }
                }
                else if (chars[1] == 0){
                    if (chars[2] == 1){
                        CodeString += "Dive";   //301
                    }
                    else if (chars[2] == 2){
                        CodeString += "Somersaults";    //302
                    }
                    else if (chars[2] == 3){
                        CodeString += "1 1/2 Somersaults";  //303
                    }
                    else if (chars[2] == 4){
                        CodeString += "2 Somersaults";  //304
                    }
                    else if (chars[2] == 5){
                        CodeString += "2 1/2 Somersaults";  //305
                    }
                    else if (chars[2] == 6){
                        CodeString += "3 Somersaults";  //306
                    }
                    else if (chars[2] == 7){
                        CodeString += "3 1/2 Somersaults";  //307
                    }
                    else if (chars[2] == 8){
                        CodeString += "4 Somersaults";  //308
                    }
                    else if (chars[2] == 9){
                        CodeString += "4 1/2 Somersaults";  //309
                    }
                }
            }
            else if (chars[0] == 4){
                CodeString += "Inward ";
                if (chars[1] == 1){
                    CodeString += "Flying ";
                    if(chars[2] == 2){
                        CodeString += "Somersaults";    //412
                    }
                    else if (chars[2] == 3){
                        CodeString += "1 1/2 Somersaults";  //413
                    }
                }
                else if(chars[1] == 0){
                    if(chars[2] == 1){
                        CodeString += "Dive";   //401
                    }
                    else if (chars[2] == 1){
                        CodeString += "Somersaults";    //402
                    }
                    else if (chars[2] == 1){
                        CodeString += "1 1/2 Somersaults";  //403
                    }
                    else if (chars[2] == 1){
                        CodeString += "2 Somersaults";  //404
                    }
                    else if (chars[2] == 1){
                        CodeString += "2 1/2 Somersaults";  //405
                    }
                    else if (chars[2] == 1){
                        CodeString += "3 1/2 Somersaults";  //407
                    }
                    else if (chars[2] == 1){
                        CodeString += "4 1/2 Somersaults";  //409
                    }
                }
            }
            else if (chars[0] == 5){
                if(chars[1] == 1){
                    CodeString += "Forward ";
                    if(chars[2] == 1){
                        CodeString += "Dive ";
                        if(chars[3] == 1){
                            CodeString += "1/2 Twist";  //5111
                        }
                        else if(chars[3] == 2){
                            CodeString += "1 Twist";    //5112
                        }
                    }
                    if (chars[2] == 2){
                        CodeString += "Somersault ";
                        if(chars[3] == 1){
                            CodeString += "1/2 Twist";  //5121
                        }
                        else if (chars[3] == 2){
                            CodeString += "1 Twist";    //5122
                        }
                        else if (chars[3] == 4){
                            CodeString += "2 Twists";   //5124
                        }
                        else if (chars[3] == 6){
                            CodeString += "3 Twists";   //5126
                        }
                    }
                    if (chars[2] == 3){
                        CodeString += "1 1/2 Somersaults ";
                        if(chars[3] == 1){
                            CodeString += "1/2 Twist";  //5131
                        }
                        else if (chars[3] == 2){
                            CodeString += "1 Twist";    //5132
                        }
                        else if (chars[3] == 4){
                            CodeString += "2 Twists";    //5134
                        }
                        else if (chars[3] == 6){
                            CodeString += "3 Twists";    //5136
                        }
                        else if (chars[3] == 8){
                            CodeString += "4 Twists";    //5138
                        }
                    }
                    if (chars[2] == 5){
                        CodeString += "2 1/2 Somersaults ";
                        if(chars[3] == 1){
                            CodeString += "1/2 Twist";  //5151
                        }
                        else if (chars[3] == 2){
                            CodeString += "1 Twist";    //5152
                        }
                        else if (chars[3] == 4){
                            CodeString += "2 Twists";   //5154
                        }
                        else if (chars[3] == 6){
                            CodeString += "3 Twist";    //5156
                        }
                    }
                    if (chars[2] == 7){
                        CodeString += "3 1/2 Somersaults ";
                        if(chars[3] == 2){
                            CodeString += "1 Twist";    //5172
                        }
                    }
                }
                if (chars[1] == 2){
                    CodeString += "Back ";
                    if(chars[2] == 1){
                        CodeString += "Dive ";
                        if(chars[3] == 1){
                            CodeString += "1/2 Twist";  //5211
                        }
                        else if (chars[3] == 1){
                            CodeString += "1 Twist";    //5212
                        }
                    }
                    else if (chars[2] == 2){
                        CodeString += "Somersault ";
                        if(chars[3] == 1){
                            CodeString += "1/2 Twist";  //5221
                        }
                        else if (chars[3] == 2){
                            CodeString += "1 Twist";    //5222
                        }
                        else if (chars[3] == 3){
                            CodeString += " 1 1/2 Twists";  //5223
                        }
                        else if (chars[3] == 5){
                            CodeString += " 2 1/2 Twists";  //5225
                        }
                        else if (chars[3] == 7){
                            CodeString += " 3 1/2 Twists";  //5227
                        }
                    }
                    else if (chars[2] == 3){
                        CodeString += "1 1/2 Somersault ";
                        if(chars[3] == 1){
                            CodeString += "1/2 Twist";  //5231
                        }
                        else if (chars[3] == 1){
                            CodeString += "1 Twist";    //5232
                        }
                        else if (chars[3] == 1){
                            CodeString += "2 1/2 Twists";    //5235
                        }
                        else if (chars[3] == 1){
                            CodeString += "3 1/2 Twists";    //5237
                        }
                        else if (chars[3] == 1){
                            CodeString += "4 1/2 Twists";    //5239
                        }
                    }
                    else if (chars[2] == 5){
                        CodeString += "2 1/2 Somersaults ";
                        if(chars[3] == 1){
                            CodeString += "1/2 Twist";  //5251
                        }
                        else if (chars[3] == 3){
                            CodeString += "1 1/2 Twists";   //5253
                        }
                        else if (chars[3] == 5){
                            CodeString += "2 1/2 Twists";   //5255
                        }
                    }
                }
                else if (chars[1] == 3){

                }
                else if (chars[1] == 4){

                }
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