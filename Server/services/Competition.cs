using System;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.ComponentModel.Design;
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

        /// <summary>
        /// Uses String Code and Height To fetch jump stats
        /// </summary>
        public static Jump ParseDifficulty(string code, int height)
        {
            string[] letters = code.Split(',');
            float A = 0, B = 0, C = 0, D = 0, E = 0;

            char[] chars = code.ToCharArray();
            string CodeString = "";
            if (chars[0] == 1)
            {
                CodeString += "Front ";
                if (chars[1] == 1)
                {
                    CodeString += "Flying ";
                    if (chars[2] == 2)
                    {
                        CodeString += "Somersaults";    //112
                    }
                    else if (chars[2] == 3)
                    {
                        CodeString += "1 1/2 Somersaults";  //113
                    }
                    else if (chars[2] == 4)
                    {
                        CodeString += "2 Somersaults";     //114
                    }
                    else if (chars[2] == 5)
                    {
                        CodeString += "2 1/2 Somersaults";     //115
                    }
                }
                else if (chars[1] == 0)
                {
                    if (chars[2] == 1)
                    {
                        CodeString += "Dive";   //101
                        if (chars[3] == 1){
                            CodeString = "";
                            CodeString = "Forward 5 1/2 Somerasaults";  //1011
                        }
                    }
                    else if (chars[2] == 2)
                    {
                        CodeString += "Somersaults";    //102
                    }
                    else if (chars[2] == 3)
                    {
                        CodeString += "1 1/2 Somersaults";  //103
                    }
                    else if (chars[2] == 4)
                    {
                        CodeString += "2 Somersaults";  //104
                    }
                    else if (chars[2] == 5)
                    {
                        CodeString += "2 1/2 Somersaults";  //105
                    }
                    else if (chars[2] == 6)
                    {
                        CodeString += "3 Somersaults";  //106
                    }
                    else if (chars[2] == 7)
                    {
                        CodeString += "3 1/2 Somersaults";  //107
                    }
                    else if (chars[2] == 8)
                    {
                        CodeString += "4 Somersaults";  //108
                    }
                    else if (chars[2] == 9)
                    {
                        CodeString += "4 1/2 Somersaults";  //109
                    }
                }
            }
            else if (chars[0] == 2)
            {
                CodeString += "Back ";
                if (chars[1] == 1)
                {
                    CodeString += "Flying ";
                    if (chars[2] == 2)
                    {
                        CodeString += "Somersaults";    //212
                    }
                    else if (chars[2] == 3)
                    {
                        CodeString += "1 1/2 Somersaults";  //213
                    }
                    else if (chars[2] == 5)
                    {
                        CodeString += "2 1/2 Somersaults";  //215
                    }
                }
                else if (chars[1] == 0)
                {
                    if (chars[2] == 1)
                    {
                        CodeString += "Dive";   //201
                    }
                    else if (chars[2] == 2)
                    {
                        CodeString += "Somersaults";    //202
                    }
                    else if (chars[2] == 3)
                    {
                        CodeString += "1 1/2 Somersaults";  //203
                    }
                    else if (chars[2] == 4)
                    {
                        CodeString += "2 Somersaults";  //204
                    }
                    else if (chars[2] == 5)
                    {
                        CodeString += "2 1/2 Somersaults"; //205
                    }
                    else if (chars[2] == 6)
                    {
                        CodeString += "3 Somersaults";  //206
                    }
                    else if (chars[2] == 7)
                    {
                        CodeString += "3 1/2 Somersaults";  //207
                    }
                    else if (chars[2] == 8)
                    {
                        CodeString += "4 Somersaults";  //208
                    }
                    else if (chars[2] == 9)
                    {
                        CodeString += "4 1/2 Somersaults";  //209
                    }
                }
            }
            else if (chars[0] == 3)
            {
                CodeString += "Reverse ";
                if (chars[1] == 1)
                {
                    CodeString += "Flying ";
                    if (chars[2] == 2)
                    {
                        CodeString += "Somersaults";    //312
                    }
                    else if (chars[2] == 3)
                    {
                        CodeString += "1 1/2 Somersaults";  //313
                    }
                }
                else if (chars[1] == 0)
                {
                    if (chars[2] == 1)
                    {
                        CodeString += "Dive";   //301
                    }
                    else if (chars[2] == 2)
                    {
                        CodeString += "Somersaults";    //302
                    }
                    else if (chars[2] == 3)
                    {
                        CodeString += "1 1/2 Somersaults";  //303
                    }
                    else if (chars[2] == 4)
                    {
                        CodeString += "2 Somersaults";  //304
                    }
                    else if (chars[2] == 5)
                    {
                        CodeString += "2 1/2 Somersaults";  //305
                    }
                    else if (chars[2] == 6)
                    {
                        CodeString += "3 Somersaults";  //306
                    }
                    else if (chars[2] == 7)
                    {
                        CodeString += "3 1/2 Somersaults";  //307
                    }
                    else if (chars[2] == 8)
                    {
                        CodeString += "4 Somersaults";  //308
                    }
                    else if (chars[2] == 9)
                    {
                        CodeString += "4 1/2 Somersaults";  //309
                    }
                }
            }
            else if (chars[0] == 4)
            {
                CodeString += "Inward ";
                if (chars[1] == 1)
                {
                    CodeString += "Flying ";
                    if (chars[2] == 2)
                    {
                        CodeString += "Somersaults";    //412
                    }
                    else if (chars[2] == 3)
                    {
                        CodeString += "1 1/2 Somersaults";  //413
                    }
                }
                else if (chars[1] == 0)
                {
                    if (chars[2] == 1)
                    {
                        CodeString += "Dive";   //401
                    }
                    else if (chars[2] == 2)
                    {
                        CodeString += "Somersaults";    //402
                    }
                    else if (chars[2] == 3)
                    {
                        CodeString += "1 1/2 Somersaults";  //403
                    }
                    else if (chars[2] == 4)
                    {
                        CodeString += "2 Somersaults";  //404
                    }
                    else if (chars[2] == 5)
                    {
                        CodeString += "2 1/2 Somersaults";  //405
                    }
                    else if (chars[2] == 6)
                    {
                        CodeString += "3 Somersaults";  //406
                    }
                    else if (chars[2] == 7)
                    {
                        CodeString += "3 1/2 Somersaults";  //407
                    }
                    else if (chars[2] == 8)
                    {
                        CodeString += "4 Somersaults";  //408
                    }
                    else if (chars[2] == 9)
                    {
                        CodeString += "4 1/2 Somersaults";  //409
                    }
                }
            }
            else if (chars[0] == 5)
            {
                if (chars[1] == 1)
                {
                    CodeString += "Forward ";
                    if (chars[2] == 1)
                    {
                        CodeString += "Dive ";
                        if (chars[3] == 1)
                        {
                            CodeString += "1/2 Twist";  //5111
                        }
                        else if (chars[3] == 2)
                        {
                            CodeString += "1 Twist";    //5112
                        }
                    }
                    if (chars[2] == 2)
                    {
                        CodeString += "Somersault ";
                        if (chars[3] == 1)
                        {
                            CodeString += "1/2 Twist";  //5121
                        }
                        else if (chars[3] == 2)
                        {
                            CodeString += "1 Twist";    //5122
                        }
                        else if (chars[3] == 4)
                        {
                            CodeString += "2 Twists";   //5124
                        }
                        else if (chars[3] == 6)
                        {
                            CodeString += "3 Twists";   //5126
                        }
                    }
                    if (chars[2] == 3)
                    {
                        CodeString += "1 1/2 Somersaults ";
                        if (chars[3] == 1)
                        {
                            CodeString += "1/2 Twist";  //5131
                        }
                        else if (chars[3] == 2)
                        {
                            CodeString += "1 Twist";    //5132
                        }
                        else if (chars[3] == 4)
                        {
                            CodeString += "2 Twists";    //5134
                        }
                        else if (chars[3] == 6)
                        {
                            CodeString += "3 Twists";    //5136
                        }
                        else if (chars[3] == 8)
                        {
                            CodeString += "4 Twists";    //5138
                        }
                    }
                    if (chars[2] == 5)
                    {
                        CodeString += "2 1/2 Somersaults";
                        CodeString += "2 1/2 Somersaults ";
                        if (chars[3] == 1)
                        {
                            CodeString += "1/2 Twist";
                            CodeString += "1/2 Twist";  //5151
                        }
                        else if (chars[3] == 2)
                        {
                            CodeString += "1 Twist";
                            CodeString += "1 Twist";    //5152
                        }
                        else if (chars[3] == 4)
                        {
                            CodeString += "2 Twists";
                            CodeString += "2 Twists";   //5154
                        }
                        else if (chars[3] == 6)
                        {
                            CodeString += "3 Twist";
                            CodeString += "3 Twist";    //5156
                        }
                    }
                    if (chars[2] == 7)
                    {

                        CodeString += "3 1/2 Somersaults ";
                        if (chars[3] == 2)
                        {
                            CodeString += "1 Twist";    //5172
                        }
                    }
                }
                if (chars[1] == 2)
                {
            int position = Int32.Parse(letters[0]);
            int letter2 = Int32.Parse(letters[1]);
            int somersaults = Int32.Parse(letters[2]);
            string group = letters[3];
            int twists = 0;
            if (position == 5)
            {
                twists = Int32.Parse(letters[3]);
                group = letters[4];
            }

                    CodeString += "Back ";
                    if (chars[2] == 1)
                    {
                        CodeString += "Dive ";
                        if (chars[3] == 1)
                        {
                            CodeString += "1/2 Twist";  //5211
                        }
                        else if (chars[3] == 1)
                        {
                            CodeString += "1 Twist";    //5212
                        }
                    }
                    else if (chars[2] == 2)
                    {
                        CodeString += "Somersault ";
                        if (chars[3] == 1)
                        {
                            CodeString += "1/2 Twist";  //5221
                        }
                        else if (chars[3] == 2)
                        {
                            CodeString += "1 Twist";    //5222
                        }
                        else if (chars[3] == 3)
                        {
                            CodeString += " 1 1/2 Twists";  //5223
                        }
                        else if (chars[3] == 5)
                        {
                            CodeString += " 2 1/2 Twists";  //5225
                        }
                        else if (chars[3] == 7)
                        {
                            CodeString += " 3 1/2 Twists";  //5227
                        }
                    }
                    else if (chars[2] == 3)
                    {
                        CodeString += "1 1/2 Somersault ";
                        if (chars[3] == 1)
                        {
                            CodeString += "1/2 Twist";  //5231
                        }
                        else if (chars[3] == 1)
                        {
                            CodeString += "1 Twist";    //5232
                        }
                        else if (chars[3] == 1)
                        {
                            CodeString += "2 1/2 Twists";    //5235
                        }
                        else if (chars[3] == 1)
                        {
                            CodeString += "3 1/2 Twists";    //5237
                        }
                        else if (chars[3] == 1)
                        {
                            CodeString += "4 1/2 Twists";    //5239
                        }
                    }
                    else if (chars[2] == 5)
                    {
                        CodeString += "2 1/2 Somersaults ";
                        if (chars[3] == 1)
                        {
                            CodeString += "1/2 Twist";  //5251
                        }
                        else if (chars[3] == 3)
                        {
                            CodeString += "1 1/2 Twists";   //5253
                        }
                        else if (chars[3] == 5)
                        {
                            CodeString += "2 1/2 Twists";   //5255
                        }
                        else if (chars[3] == 7)
                        {
                            CodeString += "3 1/2 Twists";   //5257
                        }
                    }
                    else if (chars[2] == 7){
                        CodeString += "3 1/2 Somersaults ";
                        if(chars[3] == 1){
                            CodeString += "1/2 Twist";
                        }
                        else if (chars[3] == 3){
                            CodeString += "1 1/2 Twists";
                        }
                        else if (chars[3] == 5){
                            CodeString += "2 1/2 Twists";
                        }
                    }
                }
                else if (chars[1] == 3)
                {
                    CodeString += "Reverse ";
                    if (chars[2] == 1)
                    {
                        CodeString += "Dive ";
                        if (chars[3] == 1)
                        {
                            CodeString += "1/2 Twist";  //5311
                        }
                        else if (chars[3] == 2)
                        {
                            CodeString += "1 Twist";    //5312
                        }
                    }
                    else if (chars[2] == 2)
                    {
                        CodeString += "Somersault ";
                        if (chars[3] == 1)
                        {
                            CodeString += "1/2 Twist";  //5321
                        }
                        else if (chars[3] == 1)
                        {
                            CodeString += "1 Twist";    //5322
                        }
                        else if (chars[3] == 1)
                        {
                            CodeString += "1 1/2 Twists";   //5323
                        }
                        else if (chars[3] == 1)
                        {
                            CodeString += "2 1/2 Twists";   //5325
                        }
                    }
                    else if (chars[2] == 3)
                    {
                        CodeString += "1 1/2 Somersaults ";
                        if (chars[3] == 1)
                        {
                            CodeString += "1/2 Twist";  //5331
                        }
                        else if (chars[3] == 3)
                        {
                            CodeString += "1 1/2 Twists";   //5333
                        }
                        else if (chars[3] == 5)
                        {
                            CodeString += "2 1/2 Twists";   //5335
                        }
                        else if (chars[3] == 7)
                        {
                            CodeString += "3 1/2 Twists";   //5337
                        }
                        else if (chars[3] == 9)
                        {
                            CodeString += "4 1/2 Twists";   //5339
                        }
                    }
                    else if (chars[2] == 5)
                    {
                        CodeString += "2 1/2 Somersaults ";
                        if (chars[3] == 1)
                        {
                            CodeString += "1/2 Twist";  //5351
                        }
                        else if (chars[3] == 3)
                        {
                            CodeString += "1 1/2 Twists";   //5353
                        }
                        else if (chars[3] == 5)
                        {
                            CodeString += "2 1/2 Twists";   //5355
                        }
                    }
                    else if (chars[2] == 7)
                    {
                        CodeString += "3 1/2 Somersaults ";
                        if (chars[3] == 1)
                        {
                            CodeString += "1/2 Twist";  //5371
                        }
                        if (chars[3] == 3)
                        {
                            CodeString += "1 1/2 Twists";   //5373
                        }
                        if (chars[3] == 5)
                        {
                            CodeString += "2 1/2 Twists";   //5375
                        }
                    }
                }
                else if (chars[1] == 4){
                    CodeString += "Inward ";
                    if(chars[2] == 1){
                        CodeString += "Dive ";
                        if(chars[3] == 1) {
                            CodeString += "1/2 Twist";  //5411
                        }
                        else if (chars[3] == 1){
                            CodeString += "1 Twist";    //5412
                        }
                    }
                    if (chars[2] == 2){
                        CodeString += "Somersault ";
                        if(chars[3] == 1){
                            CodeString += "1/2 Twist";  //5421
                        }
                        else if (chars[3] == 2){
                            CodeString += "1 Twist";    //5422
                        }
                    }
                    if (chars[2] == 3){
                        CodeString += "1 1/2 Somersaults ";
                        if(chars[3] == 2){
                            CodeString += "1 Twist";    //5432
                        }
                        if (chars[3] == 4){
                            CodeString += "2 Twist";    //5434
                        }
                        if (chars[3] == 6){
                            CodeString += "3 Twist";    //5436
                        }
                    }
                }
            }
            else if(chars[0] == 6)
            {
                CodeString += "Armstand ";
                if(chars[1] == 0){
                    CodeString += "Dive";   //600
                }
                else if (chars[1] == 1){
                    CodeString += "Forward ";
                    if(chars[2] == 1){
                        CodeString += "1/2 Somersault"; //611
                    }
                    else if(chars[2] == 2){
                        CodeString += "1 Somersault ";  //612
                        if(chars[3] == 2){
                            CodeString += "1 Twist";    //6122
                        }
                        else if (chars[3] == 4){
                            CodeString += "2 Twists";   //6124
                        }
                    }
                    else if (chars[2] == 4){
                        CodeString += "2 Somersaults "; //614
                        if(chars[3] == 2){
                            CodeString += "1 Twist";    //6142
                        }
                        else if (chars[3] == 2){
                            CodeString += "2 Twists";   //6144
                        }
                    }
                    else if (chars[2] == 6){
                        CodeString += "3 Somersaults "; //616
                        if(chars[3] == 2){
                            CodeString += "1 Twist";    //6162
                        }
                    }
                }
                else if (chars[1] == 2){
                    CodeString += "Back ";
                    if(chars[2] == 1){
                        CodeString += "1/2 Somersault"; //621
                    }
                    else if (chars[2] == 2){
                        CodeString += "1 Somersault ";  //622
                        if(chars[3] == 1){
                            CodeString += "1/2 Twist";    //6221
                        }
                    }
                    else if (chars[2] == 3){
                        CodeString += "1 1/2 Somersaults";  //623
                    }
                    else if (chars[2] == 4){
                        CodeString += "2 Somersaults "; //624
                        if(chars[3] == 1){
                            CodeString += "1/2 Twist";  //6241
                        }
                        else if (chars[3] == 3){
                            CodeString += "1 1/2 Twists";   //6243
                        }
                        else if (chars[3] == 5){
                            CodeString += "2 1/2 Twists";   //6245
                        }
                        else if (chars[3] == 7){
                            CodeString += "3 1/2 Twists";//6247
                        }
                    }
                    else if (chars[2] == 6){
                        CodeString += "3 Somersaults "; //626
                        if(chars[3] == 1){
                            CodeString += "1/2 Twist";  //6261
                        }
                        else if (chars[3] == 3){
                            CodeString += "1 1/2 Twists";   //6263
                        }
                        else if (chars[3] == 5){
                            CodeString += "2 1/2 Twists";   //6265
                        }
                    }
                    else if (chars[2] == 8){
                        CodeString += "4 Somersaults";  //628
                    }
                }
                else if (chars[1] == 3){
                    CodeString += "Reverse ";
                    if(chars[2] == 1){
                        CodeString += "1/2 Somersault";
                    }
                    if (chars[2] == 2){
                        CodeString += "1 Somersault";
                    }
                    if (chars[2] == 3){
                        CodeString += "1 1/2 Somersaults";
                    }
                    if (chars[2] == 4){
                        CodeString += "2 Somersaults";
                    }
                    if (chars[2] == 6){
                        CodeString += "3 Somersaults";
                    }
                    if (chars[2] == 8){
                        CodeString += "4 Somersaults";
                    }
                }
            }
            return CodeString;
        }
            A = CalculateA(somersaults, height);
            B = CalculateB(position, letter2, somersaults, group, height);
            C = CalculateC(position, letter2, somersaults, twists, height);
            D = CalculateD(position, letter2, somersaults, height, twists);
            E = CalculateE(position, letter2, somersaults, height, twists);

            Jump j = new Jump();
            j.Difficulty = A + B + C + D + E;
            return j;
        }

        private static float CalculateA(int somersaults, int height)
        {
            float A = 0f;
            switch (somersaults)
            {
                case 0:
                    if (height == 1)
                        A = 0.9f;
                    else if (height == 3)
                        A = 1.0f;
                    else if (height == 5)
                        A = 0.9f;
                    else if (height == 7)
                        A = 1.0f;
                    else if (height == 10)
                        A = 1.0f;
                    break;
                case 1:
                    if (height == 1)
                        A = 1.1f;
                    else if (height == 3)
                        A = 1.3f;
                    else if (height == 5)
                        A = 1.1f;
                    else if (height == 7)
                        A = 1.3f;
                    else if (height == 10)
                        A = 1.3f;
                    break;
                case 2:
                    if (height == 1)
                        A = 1.2f;
                    else if (height == 3)
                        A = 1.3f;
                    else if (height == 5)
                        A = 1.2f;
                    else if (height == 7)
                        A = 1.3f;
                    else if (height == 10)
                        A = 1.4f;
                    break;
                case 3:
                    if (height == 1)
                        A = 1.6f;
                    else if (height == 3)
                        A = 1.5f;
                    else if (height == 5)
                        A = 1.6f;
                    else if (height == 7)
                        A = 1.5f;
                    else if (height == 10)
                        A = 1.5f;
                    break;
                case 4:
                    if (height == 1)
                        A = 2.0f;
                    else if (height == 3)
                        A = 1.8f;
                    else if (height == 5)
                        A = 2.0f;
                    else if (height == 7)
                        A = 1.8f;
                    else if (height == 10)
                        A = 1.9f;
                    break;
                case 5:
                    if (height == 1)
                        A = 2.4f;
                    else if (height == 3)
                        A = 2.2f;
                    else if (height == 5)
                        A = 2.4f;
                    else if (height == 7)
                        A = 2.2f;
                    else if (height == 10)
                        A = 2.1f;
                    break;
                case 6:
                    if (height == 1)
                        A = 2.7f;
                    else if (height == 3)
                        A = 2.3f;
                    else if (height == 5)
                        A = 2.7f;
                    else if (height == 7)
                        A = 2.3f;
                    else if (height == 10)
                        A = 2.5f;
                    break;
                case 7:
                    if (height == 1)
                        A = 3.0f;
                    else if (height == 3)
                        A = 2.8f;
                    else if (height == 5)
                        A = 3.0f;
                    else if (height == 7)
                        A = 2.8f;
                    else if (height == 10)
                        A = 2.7f;
                    break;
                case 8:
                    if (height == 1)
                        A = 3.3f;
                    else if (height == 3)
                        A = 2.9f;
                    else if (height == 5)
                        A = 0f;
                    else if (height == 7)
                        A = 3.5f;
                    else if (height == 10)
                        A = 3.5f;
                    break;
                case 9:
                    if (height == 1)
                        A = 3.8f;
                    else if (height == 3)
                        A = 3.5f;
                    else if (height == 5)
                        A = 0f;
                    else if (height == 7)
                        A = 3.5f;
                    else if (height == 10)
                        A = 3.5f;
                    break;
                case 11:
                    A = height == 10 ? 4.5f : 0f;
                    break;
                default:
                    Console.WriteLine("wrong input");
                    break;

            }

            return A;
        }
        private static float CalculateB(int position, int letter2, int somersaults, string group, int height)
        {
            float B = 0f;
            if (height == 1 || height == 3)
            {
                if (letter2 == 1 && position <= 4) //if flying action
                {
                    switch (position) //what group
                    {
                        case 1: //front
                            switch (somersaults) //number of somersaults
                            {
                                case 0:
                                case 1:
                                case 2:
                                    B = 0.2f;
                                    break;
                                case 3:
                                case 4:
                                    B = 0.2f;
                                    break;
                                case 5:
                                    B = 0.3f;
                                    break;
                                case 6:
                                case 7:
                                    B = 0.4f;
                                    break;
                                case 8:
                                case 9:
                                    B = -1f;
                                    break;
                            }

                            break;
                        case 2: //back
                            switch (somersaults) //number of somersaults
                            {
                                case 0:
                                case 1:
                                case 2:
                                    B = 0.1f;
                                    break;
                                case 3:
                                case 4:
                                    B = 0.2f;
                                    break;
                                case 5:
                                    B = 0.3f;
                                    break;
                                case 6:
                                case 7:
                                    B = -1f;
                                    break;
                                case 8:
                                case 9:
                                    B = -1f;
                                    break;
                            }

                            break;
                        case 3: //reverse
                            switch (somersaults) //number of somersaults
                            {
                                case 0:
                                case 1:
                                case 2:
                                    B = 0.1f;
                                    break;
                                case 3:
                                case 4:
                                    B = 0.2f;
                                    break;
                                case 5:
                                    B = 0.3f;
                                    break;
                                case 6:
                                case 7:
                                    B = -1f;
                                    break;
                                case 8:
                                case 9:
                                    B = -1f;
                                    break;
                            }

                            break;
                        case 4: //inward
                            switch (somersaults) //number of somersaults
                            {
                                case 0:
                                case 1:
                                case 2:
                                    B = 0.4f;
                                    break;
                                case 3:
                                case 4:
                                    B = 0.5f;
                                    break;
                                case 5:
                                    B = 0.7f;
                                    break;
                                case 6:
                                case 7:
                                    B = -1f;
                                    break;
                                case 8:
                                case 9:
                                    B = -1f;
                                    break;
                            }

                            break;
                        default:
                            break;
                    }

                }

                switch (position)
                {
                    case 1: //front
                        switch (somersaults) //number of somersaults
                        {
                            case 0:
                            case 1:
                            case 2:
                                switch (group) //flight position
                                {
                                    case "C":
                                        B += 0.1f;
                                        break;
                                    case "B":
                                        B += 0.2f;
                                        break;
                                    case "A":
                                        B = 0.3f;
                                        break;
                                    case "D":
                                        B = 0.1f;
                                        break;
                                    default:
                                        break;
                                }

                                break;
                            case 3:
                            case 4:
                                switch (group) //flight position
                                {
                                    case "C":
                                        B += 0f;
                                        break;
                                    case "B":
                                        B += 0.1f;
                                        break;
                                    case "A":
                                        B = 0.4f;
                                        break;
                                    case "D":
                                        B = 0f;
                                        break;
                                    default:
                                        break;
                                }

                                break;
                            case 5:
                                switch (group) //flight position
                                {
                                    case "C":
                                        B += 0f;
                                        break;
                                    case "B":
                                        B += 0.2f;
                                        break;
                                    case "A":
                                        B = 0.6f;
                                        break;
                                    case "D":
                                        B = 0f;
                                        break;
                                    default:
                                        break;
                                }

                                break;
                            case 6:
                            case 7:
                                switch (group) //flight position
                                {
                                    case "C":
                                        B += 0f;
                                        break;
                                    case "B":
                                        B += 0.3f;
                                        break;
                                    case "A":
                                        B = -1f;
                                        break;
                                    case "D":
                                        B = 0f;
                                        break;
                                    default:
                                        break;
                                }

                                break;
                            case 8:
                            case 9:
                                switch (group) //flight position
                                {
                                    case "C":
                                        B += 0f;
                                        break;
                                    case "B":
                                        B += 0.4f;
                                        break;
                                    case "A":
                                        B = -1f;
                                        break;
                                    case "D":
                                        B = -1f;
                                        break;
                                    default:
                                        break;
                                }

                                break;
                        }

                        break;
                    case 2: //back
                        switch (somersaults) //number of somersaults
                        {
                            case 0:
                            case 1:
                            case 2:
                                switch (group) //flight position
                                {
                                    case "C":
                                        B += 0.1f;
                                        break;
                                    case "B":
                                        B += 0.2f;
                                        break;
                                    case "A":
                                        B = 0.3f;
                                        break;
                                    case "D":
                                        B = 0.1f;
                                        break;
                                    default:
                                        break;
                                }

                                break;
                            case 3:
                            case 4:
                                switch (group) //flight position
                                {
                                    case "C":
                                        B += 0f;
                                        break;
                                    case "B":
                                        B += 0.3f;
                                        break;
                                    case "A":
                                        B = 0.5f;
                                        break;
                                    case "D":
                                        B = -0.1f;
                                        break;
                                    default:
                                        break;
                                }

                                break;
                            case 5:
                                switch (group) //flight position
                                {
                                    case "C":
                                        B += 0.1f;
                                        break;
                                    case "B":
                                        B += 0.3f;
                                        break;
                                    case "A":
                                        B = 0.7f;
                                        break;
                                    case "D":
                                        B = -0.1f;
                                        break;
                                    default:
                                        break;
                                }

                                break;
                            case 6:
                            case 7:
                                switch (group) //flight position
                                {
                                    case "C":
                                        B += 0f;
                                        break;
                                    case "B":
                                        B += 0.3f;
                                        break;
                                    case "A":
                                        B = -1f;
                                        break;
                                    case "D":
                                        B = 0f;
                                        break;
                                    default:
                                        break;
                                }

                                break;
                            case 8:
                            case 9:
                                switch (group) //flight position
                                {
                                    case "C":
                                        B += 0.1f;
                                        break;
                                    case "B":
                                        B += 0.4f;
                                        break;
                                    case "A":
                                        B = -1f;
                                        break;
                                    case "D":
                                        B = -1f;
                                        break;
                                    default:
                                        break;
                                }

                                break;
                        }

                        break;
                    case 3: //reversed
                        switch (somersaults) //number of somersaults
                        {
                            case 0:
                            case 1:
                            case 2:
                                switch (group) //flight position
                                {
                                    case "C":
                                        B += 0.1f;
                                        break;
                                    case "B":
                                        B += 0.2f;
                                        break;
                                    case "A":
                                        B = 0.3f;
                                        break;
                                    case "D":
                                        B = 0.1f;
                                        break;
                                    default:
                                        break;
                                }

                                break;
                            case 3:
                            case 4:
                                switch (group) //flight position
                                {
                                    case "C":
                                        B += 0f;
                                        break;
                                    case "B":
                                        B += 0.3f;
                                        break;
                                    case "A":
                                        B = 0.6f;
                                        break;
                                    case "D":
                                        B = -0.1f;
                                        break;
                                    default:
                                        break;
                                }

                                break;
                            case 5:
                                switch (group) //flight position
                                {
                                    case "C":
                                        B += 0f;
                                        break;
                                    case "B":
                                        B += 0.2f;
                                        break;
                                    case "A":
                                        B = 0.6f;
                                        break;
                                    case "D":
                                        B = -0.2f;
                                        break;
                                    default:
                                        break;
                                }

                                break;
                            case 6:
                            case 7:
                                switch (group) //flight position
                                {
                                    case "C":
                                        B += 0f;
                                        break;
                                    case "B":
                                        B += 0.3f;
                                        break;
                                    case "A":
                                        B = -1f;
                                        break;
                                    case "D":
                                        B = 0f;
                                        break;
                                    default:
                                        break;
                                }

                                break;
                            case 8:
                            case 9:
                                switch (group) //flight position
                                {
                                    case "C":
                                        B += 0.2f;
                                        break;
                                    case "B":
                                        B += 0.5f;
                                        break;
                                    case "A":
                                        B = -1f;
                                        break;
                                    case "D":
                                        B = -1f;
                                        break;
                                    default:
                                        break;
                                }

                                break;
                        }

                        break;
                    case 4: //inward
                        switch (somersaults) //number of somersaults
                        {
                            case 0:
                            case 1:
                            case 2:
                                switch (group) //flight position
                                {
                                    case "C":
                                        B += -0.3f;
                                        break;
                                    case "B":
                                        B += -0.2f;
                                        break;
                                    case "A":
                                        B = 0.1f;
                                        break;
                                    case "D":
                                        B = -0.1f;
                                        break;
                                    default:
                                        break;
                                }

                                break;
                            case 3:
                            case 4:
                                switch (group) //flight position
                                {
                                    case "C":
                                        B += 0.1f;
                                        break;
                                    case "B":
                                        B += 0.3f;
                                        break;
                                    case "A":
                                        B = 0.8f;
                                        break;
                                    case "D":
                                        B = 0.2f;
                                        break;
                                    default:
                                        break;
                                }

                                break;
                            case 5:
                                switch (group) //flight position
                                {
                                    case "C":
                                        B += 0.2f;
                                        break;
                                    case "B":
                                        B += 0.5f;
                                        break;
                                    case "A":
                                        B = -1f;
                                        break;
                                    case "D":
                                        B = 0.4f;
                                        break;
                                    default:
                                        break;
                                }

                                break;
                            case 6:
                            case 7:
                                switch (group) //flight position
                                {
                                    case "C":
                                        B += 0.3f;
                                        break;
                                    case "B":
                                        B += 0.6f;
                                        break;
                                    case "A":
                                        B = -1f;
                                        break;
                                    case "D":
                                        B = -1f;
                                        break;
                                    default:
                                        break;
                                }

                                break;
                            case 8:
                            case 9:
                                switch (group) //flight position
                                {
                                    case "C":
                                        B += 0.4f;
                                        break;
                                    case "B":
                                        B += 0.8f;
                                        break;
                                    case "A":
                                        B = -1f;
                                        break;
                                    case "D":
                                        B = -1f;
                                        break;
                                    default:
                                        break;
                                }

                                break;
                        }

                        break;
                    case 5: //twisting
                    case 6:
                        switch (letter2)
                        {
                            case 1: //front
                                switch (somersaults) //number of somersaults
                                {
                                    case 0:
                                    case 1:
                                    case 2:
                                        switch (group) //flight position
                                        {
                                            case "C":
                                                B = 0.1f;
                                                break;
                                            case "B":
                                                B = 0.2f;
                                                break;
                                            case "A":
                                                B = 0.3f;
                                                break;
                                            case "D":
                                                B = 0.1f;
                                                break;
                                            default:
                                                break;
                                        }

                                        break;
                                    case 3:
                                    case 4:
                                        switch (group) //flight position
                                        {
                                            case "C":
                                                B = 0f;
                                                break;
                                            case "B":
                                                B = 0.1f;
                                                break;
                                            case "A":
                                                B = 0.4f;
                                                break;
                                            case "D":
                                                B = 0f;
                                                break;
                                            default:
                                                break;
                                        }

                                        break;
                                    case 5:
                                        switch (group) //flight position
                                        {
                                            case "C":
                                                B = 0f;
                                                break;
                                            case "B":
                                                B = 0.2f;
                                                break;
                                            case "A":
                                                B = 0.6f;
                                                break;
                                            case "D":
                                                B = 0f;
                                                break;
                                            default:
                                                break;
                                        }

                                        break;
                                    case 6:
                                    case 7:
                                        switch (group) //flight position
                                        {
                                            case "C":
                                                B = 0f;
                                                break;
                                            case "B":
                                                B = 0.3f;
                                                break;
                                            case "A":
                                                B = -1f;
                                                break;
                                            case "D":
                                                B = 0f;
                                                break;
                                            default:
                                                break;
                                        }

                                        break;
                                    case 8:
                                    case 9:
                                        switch (group) //flight position
                                        {
                                            case "C":
                                                B = 0f;
                                                break;
                                            case "B":
                                                B = 0.4f;
                                                break;
                                            case "A":
                                                B = -1f;
                                                break;
                                            case "D":
                                                B = -1f;
                                                break;
                                            default:
                                                break;
                                        }

                                        break;
                                }

                                break;
                            case 2: //back
                                switch (somersaults) //number of somersaults
                                {
                                    case 0:
                                    case 1:
                                    case 2:
                                        switch (group) //flight position
                                        {
                                            case "C":
                                                B = 0.1f;
                                                break;
                                            case "B":
                                                B = 0.2f;
                                                break;
                                            case "A":
                                                B = 0.3f;
                                                break;
                                            case "D":
                                                B = 0.1f;
                                                break;
                                            default:
                                                break;
                                        }

                                        break;
                                    case 3:
                                    case 4:
                                        switch (group) //flight position
                                        {
                                            case "C":
                                                B = 0f;
                                                break;
                                            case "B":
                                                B = 0.3f;
                                                break;
                                            case "A":
                                                B = 0.5f;
                                                break;
                                            case "D":
                                                B = -0.1f;
                                                break;
                                            default:
                                                break;
                                        }

                                        break;
                                    case 5:
                                        switch (group) //flight position
                                        {
                                            case "C":
                                                B = 0.1f;
                                                break;
                                            case "B":
                                                B = 0.3f;
                                                break;
                                            case "A":
                                                B = 0.7f;
                                                break;
                                            case "D":
                                                B = -0.1f;
                                                break;
                                            default:
                                                break;
                                        }

                                        break;
                                    case 6:
                                    case 7:
                                        switch (group) //flight position
                                        {
                                            case "C":
                                                B = 0f;
                                                break;
                                            case "B":
                                                B = 0.3f;
                                                break;
                                            case "A":
                                                B = -1f;
                                                break;
                                            case "D":
                                                B = 0f;
                                                break;
                                            default:
                                                break;
                                        }

                                        break;
                                    case 8:
                                    case 9:
                                        switch (group) //flight position
                                        {
                                            case "C":
                                                B = 0.1f;
                                                break;
                                            case "B":
                                                B = 0.4f;
                                                break;
                                            case "A":
                                                B = -1f;
                                                break;
                                            case "D":
                                                B = -1f;
                                                break;
                                            default:
                                                break;
                                        }

                                        break;
                                }

                                break;
                            case 3: //reversed
                                switch (somersaults) //number of somersaults
                                {
                                    case 0:
                                    case 1:
                                    case 2:
                                        switch (group) //flight position
                                        {
                                            case "C":
                                                B = 0.1f;
                                                break;
                                            case "B":
                                                B = 0.2f;
                                                break;
                                            case "A":
                                                B = 0.3f;
                                                break;
                                            case "D":
                                                B = 0.1f;
                                                break;
                                            default:
                                                break;
                                        }

                                        break;
                                    case 3:
                                    case 4:
                                        switch (group) //flight position
                                        {
                                            case "C":
                                                B = 0f;
                                                break;
                                            case "B":
                                                B = 0.3f;
                                                break;
                                            case "A":
                                                B = 0.6f;
                                                break;
                                            case "D":
                                                B = -0.1f;
                                                break;
                                            default:
                                                break;
                                        }

                                        break;
                                    case 5:
                                        switch (group) //flight position
                                        {
                                            case "C":
                                                B = 0f;
                                                break;
                                            case "B":
                                                B = 0.2f;
                                                break;
                                            case "A":
                                                B = 0.6f;
                                                break;
                                            case "D":
                                                B = -0.2f;
                                                break;
                                            default:
                                                break;
                                        }

                                        break;
                                    case 6:
                                    case 7:
                                        switch (group) //flight position
                                        {
                                            case "C":
                                                B = 0f;
                                                break;
                                            case "B":
                                                B = 0.3f;
                                                break;
                                            case "A":
                                                B = -1f;
                                                break;
                                            case "D":
                                                B = 0f;
                                                break;
                                            default:
                                                break;
                                        }

                                        break;
                                    case 8:
                                    case 9:
                                        switch (group) //flight position
                                        {
                                            case "C":
                                                B = 0.2f;
                                                break;
                                            case "B":
                                                B = 0.5f;
                                                break;
                                            case "A":
                                                B = -1f;
                                                break;
                                            case "D":
                                                B = -1f;
                                                break;
                                            default:
                                                break;
                                        }

                                        break;
                                }

                                break;
                            case 4: //inward
                                switch (somersaults) //number of somersaults
                                {
                                    case 0:
                                    case 1:
                                    case 2:
                                        switch (group) //flight position
                                        {
                                            case "C":
                                                B = -0.3f;
                                                break;
                                            case "B":
                                                B = -0.2f;
                                                break;
                                            case "A":
                                                B = 0.1f;
                                                break;
                                            case "D":
                                                B = -0.1f;
                                                break;
                                            default:
                                                break;
                                        }

                                        break;
                                    case 3:
                                    case 4:
                                        switch (group) //flight position
                                        {
                                            case "C":
                                                B = 0.1f;
                                                break;
                                            case "B":
                                                B = 0.3f;
                                                break;
                                            case "A":
                                                B = 0.8f;
                                                break;
                                            case "D":
                                                B = 0.2f;
                                                break;
                                            default:
                                                break;
                                        }

                                        break;
                                    case 5:
                                        switch (group) //flight position
                                        {
                                            case "C":
                                                B = 0.2f;
                                                break;
                                            case "B":
                                                B = 0.5f;
                                                break;
                                            case "A":
                                                B = -1f;
                                                break;
                                            case "D":
                                                B = 0.4f;
                                                break;
                                            default:
                                                break;
                                        }

                                        break;
                                    case 6:
                                    case 7:
                                        switch (group) //flight position
                                        {
                                            case "C":
                                                B = 0.3f;
                                                break;
                                            case "B":
                                                B = 0.6f;
                                                break;
                                            case "A":
                                                B = -1f;
                                                break;
                                            case "D":
                                                B = -1f;
                                                break;
                                            default:
                                                break;
                                        }

                                        break;
                                    case 8:
                                    case 9:
                                        switch (group) //flight position
                                        {
                                            case "C":
                                                B = 0.4f;
                                                break;
                                            case "B":
                                                B = 0.8f;
                                                break;
                                            case "A":
                                                B = -1f;
                                                break;
                                            case "D":
                                                B = -1f;
                                                break;
                                            default:
                                                break;
                                        }

                                        break;
                                }

                                break;
                        }

                        break;
                }
            }
            else
            {
                if (letter2 == 1 && position <= 4) //if flying action
                {
                    switch (position) //what group
                    {
                        case 1: //front
                            switch (somersaults) //number of somersaults
                            {
                                case 0:
                                case 1:
                                case 2:
                                    B = 0.2f;
                                    break;
                                case 3:
                                case 4:
                                    B = 0.2f;
                                    break;
                                case 5:
                                    B = 0.3f;
                                    break;
                                case 6:
                                case 7:
                                    B = 0.4f;
                                    break;
                                case 8:
                                case 9:
                                    B = -1f;
                                    break;
                            }

                            break;
                        case 2: //back
                            switch (somersaults) //number of somersaults
                            {
                                case 0:
                                case 1:
                                case 2:
                                    B = 0.1f;
                                    break;
                                case 3:
                                case 4:
                                    B = 0.2f;
                                    break;
                                case 5:
                                    B = 0.3f;
                                    break;
                                case 6:
                                case 7:
                                    B = -1f;
                                    break;
                                case 8:
                                case 9:
                                    B = -1f;
                                    break;
                            }

                            break;
                        case 3: //reverse
                            switch (somersaults) //number of somersaults
                            {
                                case 0:
                                case 1:
                                case 2:
                                    B = 0.1f;
                                    break;
                                case 3:
                                case 4:
                                    B = 0.2f;
                                    break;
                                case 5:
                                    B = 0.3f;
                                    break;
                                case 6:
                                case 7:
                                    B = -1f;
                                    break;
                                case 8:
                                case 9:
                                    B = -1f;
                                    break;
                            }

                            break;
                        case 4: //inward
                            switch (somersaults) //number of somersaults
                            {
                                case 0:
                                case 1:
                                case 2:
                                    B = 0.4f;
                                    break;
                                case 3:
                                case 4:
                                    B = 0.5f;
                                    break;
                                case 5:
                                    B = 0.7f;
                                    break;
                                case 6:
                                case 7:
                                    B = -1f;
                                    break;
                                case 8:
                                case 9:
                                    B = -1f;
                                    break;
                            }

                            break;
                        default:
                            break;
                    }

                }

                switch (position)
                {
                    case 1: //front
                        switch (somersaults) //number of somersaults
                        {
                            case 0:
                            case 1:
                            case 2:
                                switch (group) //flight position
                                {
                                    case "C":
                                        B += 0.1f;
                                        break;
                                    case "B":
                                        B += 0.2f;
                                        break;
                                    case "A":
                                        B = 0.3f;
                                        break;
                                    case "D":
                                        B = 0.1f;
                                        break;
                                    default:
                                        break;
                                }

                                break;
                            case 3:
                            case 4:
                                switch (group) //flight position
                                {
                                    case "C":
                                        B += 0f;
                                        break;
                                    case "B":
                                        B += 0.1f;
                                        break;
                                    case "A":
                                        B = 0.4f;
                                        break;
                                    case "D":
                                        B = 0f;
                                        break;
                                    default:
                                        break;
                                }

                                break;
                            case 5:
                                switch (group) //flight position
                                {
                                    case "C":
                                        B += 0f;
                                        break;
                                    case "B":
                                        B += 0.2f;
                                        break;
                                    case "A":
                                        B = 0.6f;
                                        break;
                                    case "D":
                                        B = 0f;
                                        break;
                                    default:
                                        break;
                                }

                                break;
                            case 6:
                            case 7:
                                switch (group) //flight position
                                {
                                    case "C":
                                        B += 0f;
                                        break;
                                    case "B":
                                        B += 0.3f;
                                        break;
                                    case "A":
                                        B = -1f;
                                        break;
                                    case "D":
                                        B = 0f;
                                        break;
                                    default:
                                        break;
                                }

                                break;
                            case 8:
                            case 9:
                                switch (group) //flight position
                                {
                                    case "C":
                                        B += 0f;
                                        break;
                                    case "B":
                                        B += 0.4f;
                                        break;
                                    case "A":
                                        B = -1f;
                                        break;
                                    case "D":
                                        B = -1f;
                                        break;
                                    default:
                                        break;
                                }

                                break;
                            case 11:
                                switch (group) //flight position
                                {
                                    case "C":
                                        B += 0f;
                                        break;
                                    case "B":
                                        B += -1f;
                                        break;
                                    case "A":
                                        B = -1f;
                                        break;
                                    case "D":
                                        B = -1f;
                                        break;
                                    default:
                                        break;
                                }

                                break;
                        }

                        break;
                    case 2: //back
                        switch (somersaults) //number of somersaults
                        {
                            case 0:
                            case 1:
                            case 2:
                                switch (group) //flight position
                                {
                                    case "C":
                                        B += 0.1f;
                                        break;
                                    case "B":
                                        B += 0.2f;
                                        break;
                                    case "A":
                                        B = 0.3f;
                                        break;
                                    case "D":
                                        B = 0.1f;
                                        break;
                                    default:
                                        break;
                                }

                                break;
                            case 3:
                            case 4:
                                switch (group) //flight position
                                {
                                    case "C":
                                        B += 0f;
                                        break;
                                    case "B":
                                        B += 0.3f;
                                        break;
                                    case "A":
                                        B = 0.5f;
                                        break;
                                    case "D":
                                        B = -0.1f;
                                        break;
                                    default:
                                        break;
                                }

                                break;
                            case 5:
                                switch (group) //flight position
                                {
                                    case "C":
                                        B += 0.1f;
                                        break;
                                    case "B":
                                        B += 0.3f;
                                        break;
                                    case "A":
                                        B = 0.7f;
                                        break;
                                    case "D":
                                        B = -0.1f;
                                        break;
                                    default:
                                        break;
                                }

                                break;
                            case 6:
                            case 7:
                                switch (group) //flight position
                                {
                                    case "C":
                                        B += 0f;
                                        break;
                                    case "B":
                                        B += 0.3f;
                                        break;
                                    case "A":
                                        B = -1f;
                                        break;
                                    case "D":
                                        B = 0f;
                                        break;
                                    default:
                                        break;
                                }

                                break;
                            case 8:
                            case 9:
                                switch (group) //flight position
                                {
                                    case "C":
                                        B += 0.1f;
                                        break;
                                    case "B":
                                        B += 0.4f;
                                        break;
                                    case "A":
                                        B = -1f;
                                        break;
                                    case "D":
                                        B = -1f;
                                        break;
                                    default:
                                        break;
                                }

                                break;
                        }

                        break;
                    case 3: //reversed
                        switch (somersaults) //number of somersaults
                        {
                            case 0:
                            case 1:
                            case 2:
                                switch (group) //flight position
                                {
                                    case "C":
                                        B += 0.1f;
                                        break;
                                    case "B":
                                        B += 0.2f;
                                        break;
                                    case "A":
                                        B = 0.3f;
                                        break;
                                    case "D":
                                        B = 0.1f;
                                        break;
                                    default:
                                        break;
                                }

                                break;
                            case 3:
                            case 4:
                                switch (group) //flight position
                                {
                                    case "C":
                                        B += 0f;
                                        break;
                                    case "B":
                                        B += 0.3f;
                                        break;
                                    case "A":
                                        B = 0.6f;
                                        break;
                                    case "D":
                                        B = -0.1f;
                                        break;
                                    default:
                                        break;
                                }

                                break;
                            case 5:
                                switch (group) //flight position
                                {
                                    case "C":
                                        B += 0f;
                                        break;
                                    case "B":
                                        B += 0.2f;
                                        break;
                                    case "A":
                                        B = 0.6f;
                                        break;
                                    case "D":
                                        B = -0.2f;
                                        break;
                                    default:
                                        break;
                                }

                                break;
                            case 6:
                            case 7:
                                switch (group) //flight position
                                {
                                    case "C":
                                        B += 0f;
                                        break;
                                    case "B":
                                        B += 0.3f;
                                        break;
                                    case "A":
                                        B = -1f;
                                        break;
                                    case "D":
                                        B = 0f;
                                        break;
                                    default:
                                        break;
                                }

                                break;
                            case 8:
                            case 9:
                                switch (group) //flight position
                                {
                                    case "C":
                                        B += 0.3f;
                                        break;
                                    case "B":
                                        B += 0.6f;
                                        break;
                                    case "A":
                                        B = -1f;
                                        break;
                                    case "D":
                                        B = -1f;
                                        break;
                                    default:
                                        break;
                                }

                                break;
                        }

                        break;
                    case 4: //inward
                        switch (somersaults) //number of somersaults
                        {
                            case 0:
                            case 1:
                            case 2:
                                switch (group) //flight position
                                {
                                    case "C":
                                        B += -0.3f;
                                        break;
                                    case "B":
                                        B += -0.2f;
                                        break;
                                    case "A":
                                        B = 0.1f;
                                        break;
                                    case "D":
                                        B = -0.1f;
                                        break;
                                    default:
                                        break;
                                }

                                break;
                            case 3:
                            case 4:
                                switch (group) //flight position
                                {
                                    case "C":
                                        B += 0.1f;
                                        break;
                                    case "B":
                                        B += 0.3f;
                                        break;
                                    case "A":
                                        B = 0.8f;
                                        break;
                                    case "D":
                                        B = 0.2f;
                                        break;
                                    default:
                                        break;
                                }

                                break;
                            case 5:
                                switch (group) //flight position
                                {
                                    case "C":
                                        B += 0.2f;
                                        break;
                                    case "B":
                                        B += 0.5f;
                                        break;
                                    case "A":
                                        B = -1f;
                                        break;
                                    case "D":
                                        B = 0.4f;
                                        break;
                                    default:
                                        break;
                                }

                                break;
                            case 6:
                            case 7:
                                switch (group) //flight position
                                {
                                    case "C":
                                        B += 0.3f;
                                        break;
                                    case "B":
                                        B += 0.6f;
                                        break;
                                    case "A":
                                        B = -1f;
                                        break;
                                    case "D":
                                        B = -1f;
                                        break;
                                    default:
                                        break;
                                }

                                break;
                            case 8:
                            case 9:
                                switch (group) //flight position
                                {
                                    case "C":
                                        B += 0.4f;
                                        break;
                                    case "B":
                                        B += 0.7f;
                                        break;
                                    case "A":
                                        B = -1f;
                                        break;
                                    case "D":
                                        B = -1f;
                                        break;
                                    default:
                                        break;
                                }

                                break;
                        }

                        break;
                    case 5: //twisting
                        switch (letter2)
                        {
                            case 1: //front
                                switch (somersaults) //number of somersaults
                                {
                                    case 0:
                                    case 1:
                                    case 2:
                                        switch (group) //flight position
                                        {
                                            case "C":
                                                B = 0.1f;
                                                break;
                                            case "B":
                                                B = 0.2f;
                                                break;
                                            case "A":
                                                B = 0.3f;
                                                break;
                                            case "D":
                                                B = 0.1f;
                                                break;
                                            default:
                                                break;
                                        }

                                        break;
                                    case 3:
                                    case 4:
                                        switch (group) //flight position
                                        {
                                            case "C":
                                                B = 0f;
                                                break;
                                            case "B":
                                                B = 0.1f;
                                                break;
                                            case "A":
                                                B = 0.4f;
                                                break;
                                            case "D":
                                                B = 0f;
                                                break;
                                            default:
                                                break;
                                        }

                                        break;
                                    case 5:
                                        switch (group) //flight position
                                        {
                                            case "C":
                                                B = 0f;
                                                break;
                                            case "B":
                                                B = 0.2f;
                                                break;
                                            case "A":
                                                B = 0.6f;
                                                break;
                                            case "D":
                                                B = 0f;
                                                break;
                                            default:
                                                break;
                                        }

                                        break;
                                    case 6:
                                    case 7:
                                        switch (group) //flight position
                                        {
                                            case "C":
                                                B = 0f;
                                                break;
                                            case "B":
                                                B = 0.3f;
                                                break;
                                            case "A":
                                                B = -1f;
                                                break;
                                            case "D":
                                                B = 0f;
                                                break;
                                            default:
                                                break;
                                        }

                                        break;
                                    case 8:
                                    case 9:
                                        switch (group) //flight position
                                        {
                                            case "C":
                                                B = 0f;
                                                break;
                                            case "B":
                                                B = 0.4f;
                                                break;
                                            case "A":
                                                B = -1f;
                                                break;
                                            case "D":
                                                B = -1f;
                                                break;
                                            default:
                                                break;
                                        }

                                        break;
                                }

                                break;
                            case 2: //back
                                switch (somersaults) //number of somersaults
                                {
                                    case 0:
                                    case 1:
                                    case 2:
                                        switch (group) //flight position
                                        {
                                            case "C":
                                                B = 0.1f;
                                                break;
                                            case "B":
                                                B = 0.2f;
                                                break;
                                            case "A":
                                                B = 0.3f;
                                                break;
                                            case "D":
                                                B = 0.1f;
                                                break;
                                            default:
                                                break;
                                        }

                                        break;
                                    case 3:
                                    case 4:
                                        switch (group) //flight position
                                        {
                                            case "C":
                                                B = 0f;
                                                break;
                                            case "B":
                                                B = 0.3f;
                                                break;
                                            case "A":
                                                B = 0.5f;
                                                break;
                                            case "D":
                                                B = -0.1f;
                                                break;
                                            default:
                                                break;
                                        }

                                        break;
                                    case 5:
                                        switch (group) //flight position
                                        {
                                            case "C":
                                                B = 0.1f;
                                                break;
                                            case "B":
                                                B = 0.3f;
                                                break;
                                            case "A":
                                                B = 0.7f;
                                                break;
                                            case "D":
                                                B = -0.1f;
                                                break;
                                            default:
                                                break;
                                        }

                                        break;
                                    case 6:
                                    case 7:
                                        switch (group) //flight position
                                        {
                                            case "C":
                                                B = 0f;
                                                break;
                                            case "B":
                                                B = 0.3f;
                                                break;
                                            case "A":
                                                B = -1f;
                                                break;
                                            case "D":
                                                B = 0f;
                                                break;
                                            default:
                                                break;
                                        }

                                        break;
                                    case 8:
                                    case 9:
                                        switch (group) //flight position
                                        {
                                            case "C":
                                                B = 0.1f;
                                                break;
                                            case "B":
                                                B = 0.4f;
                                                break;
                                            case "A":
                                                B = -1f;
                                                break;
                                            case "D":
                                                B = -1f;
                                                break;
                                            default:
                                                break;
                                        }

                                        break;
                                }

                                break;
                            case 3: //reversed
                                switch (somersaults) //number of somersaults
                                {
                                    case 0:
                                    case 1:
                                    case 2:
                                        switch (group) //flight position
                                        {
                                            case "C":
                                                B = 0.1f;
                                                break;
                                            case "B":
                                                B = 0.2f;
                                                break;
                                            case "A":
                                                B = 0.3f;
                                                break;
                                            case "D":
                                                B = 0.1f;
                                                break;
                                            default:
                                                break;
                                        }

                                        break;
                                    case 3:
                                    case 4:
                                        switch (group) //flight position
                                        {
                                            case "C":
                                                B = 0f;
                                                break;
                                            case "B":
                                                B = 0.3f;
                                                break;
                                            case "A":
                                                B = 0.6f;
                                                break;
                                            case "D":
                                                B = -0.1f;
                                                break;
                                            default:
                                                break;
                                        }

                                        break;
                                    case 5:
                                        switch (group) //flight position
                                        {
                                            case "C":
                                                B = 0f;
                                                break;
                                            case "B":
                                                B = 0.2f;
                                                break;
                                            case "A":
                                                B = 0.6f;
                                                break;
                                            case "D":
                                                B = -0.2f;
                                                break;
                                            default:
                                                break;
                                        }

                                        break;
                                    case 6:
                                    case 7:
                                        switch (group) //flight position
                                        {
                                            case "C":
                                                B = 0f;
                                                break;
                                            case "B":
                                                B = 0.3f;
                                                break;
                                            case "A":
                                                B = -1f;
                                                break;
                                            case "D":
                                                B = 0f;
                                                break;
                                            default:
                                                break;
                                        }

                                        break;
                                    case 8:
                                    case 9:
                                        switch (group) //flight position
                                        {
                                            case "C":
                                                B = 0.2f;
                                                break;
                                            case "B":
                                                B = 0.5f;
                                                break;
                                            case "A":
                                                B = -1f;
                                                break;
                                            case "D":
                                                B = -1f;
                                                break;
                                            default:
                                                break;
                                        }

                                        break;
                                }

                                break;
                            case 4: //inward
                                switch (somersaults) //number of somersaults
                                {
                                    case 0:
                                    case 1:
                                    case 2:
                                        switch (group) //flight position
                                        {
                                            case "C":
                                                B = -0.3f;
                                                break;
                                            case "B":
                                                B = -0.2f;
                                                break;
                                            case "A":
                                                B = 0.1f;
                                                break;
                                            case "D":
                                                B = -0.1f;
                                                break;
                                            default:
                                                break;
                                        }

                                        break;
                                    case 3:
                                    case 4:
                                        switch (group) //flight position
                                        {
                                            case "C":
                                                B = 0.1f;
                                                break;
                                            case "B":
                                                B = 0.3f;
                                                break;
                                            case "A":
                                                B = 0.8f;
                                                break;
                                            case "D":
                                                B = 0.2f;
                                                break;
                                            default:
                                                break;
                                        }

                                        break;
                                    case 5:
                                        switch (group) //flight position
                                        {
                                            case "C":
                                                B = 0.2f;
                                                break;
                                            case "B":
                                                B = 0.5f;
                                                break;
                                            case "A":
                                                B = -1f;
                                                break;
                                            case "D":
                                                B = 0.4f;
                                                break;
                                            default:
                                                break;
                                        }

                                        break;
                                    case 6:
                                    case 7:
                                        switch (group) //flight position
                                        {
                                            case "C":
                                                B = 0.3f;
                                                break;
                                            case "B":
                                                B = 0.6f;
                                                break;
                                            case "A":
                                                B = -1f;
                                                break;
                                            case "D":
                                                B = -1f;
                                                break;
                                            default:
                                                break;
                                        }

                                        break;
                                    case 8:
                                    case 9:
                                        switch (group) //flight position
                                        {
                                            case "C":
                                                B = 0.4f;
                                                break;
                                            case "B":
                                                B = 0.8f;
                                                break;
                                            case "A":
                                                B = -1f;
                                                break;
                                            case "D":
                                                B = -1f;
                                                break;
                                            default:
                                                break;
                                        }

                                        break;
                                }

                                break;
                        }

                        break;
                    case 6:
                        switch (somersaults) //number of somersaults
                        {
                            case 0:
                            case 1:
                            case 2:
                                switch (group) //flight position
                                {
                                    case "C":
                                        B += 0.1f;
                                        break;
                                    case "B":
                                        B += 0.3f;
                                        break;
                                    case "A":
                                        B = 0.4f;
                                        break;
                                    case "D":
                                        B = 0f;
                                        break;
                                    default:
                                        break;
                                }

                                break;
                            case 3:
                            case 4:
                                switch (group) //flight position
                                {
                                    case "C":
                                        B += 0f;
                                        break;
                                    case "B":
                                        B += 0.3f;
                                        break;
                                    case "A":
                                        B = 0.5f;
                                        break;
                                    case "D":
                                        B = 0f;
                                        break;
                                    default:
                                        break;
                                }

                                break;
                            case 5:
                                switch (group) //flight position
                                {
                                    case "C":
                                        B += 0.1f;
                                        break;
                                    case "B":
                                        B += 0f;
                                        break;
                                    case "A":
                                        B = -1f;
                                        break;
                                    case "D":
                                        B = 0f;
                                        break;
                                    default:
                                        break;
                                }

                                break;
                            case 6:
                            case 7:
                                switch (group) //flight position
                                {
                                    case "C":
                                        B += 0.2f;
                                        break;
                                    case "B":
                                        B += 0.4f;
                                        break;
                                    case "A":
                                        B = -1f;
                                        break;
                                    case "D":
                                        B = -1f;
                                        break;
                                    default:
                                        break;
                                }

                                break;
                            case 8:
                            case 9:
                                switch (group) //flight position
                                {
                                    case "C":
                                        B += 0.3f;
                                        break;
                                    case "B":
                                        B += 0.5f;
                                        break;
                                    case "A":
                                        B = -1f;
                                        break;
                                    case "D":
                                        B = -1f;
                                        break;
                                    default:
                                        break;
                                }

                                break;
                        }
                        break;
                }
            }
            return B;
        }

        private static float CalculateC(int position, int letter2, int somersaults, int twists, int height)
        {
            float C = 0f;
            if (height == 1 || height == 3)
            {
                if (position == 5 || position == 6)
                {
                    switch (twists)
                    {
                        case 1: //number of half twists
                            switch (somersaults)
                            {
                                case 1:
                                case 2:
                                    switch (letter2) //position
                                    {
                                        case 1:
                                            C = 0.4f;
                                            break;
                                        case 2:
                                            C = 0.2f;
                                            break;
                                        case 3:
                                            C = 0.2f;
                                            break;
                                        case 4:
                                            C = 0.2f;
                                            break;
                                    }

                                    break;
                                case 3:
                                case 4:
                                    switch (letter2) //position
                                    {
                                        case 1:
                                            C = 0.4f;
                                            break;
                                        case 2:
                                            C = 0.4f;
                                            break;
                                        case 3:
                                            C = 0.4f;
                                            break;
                                        case 4:
                                            C = 0.4f;
                                            break;
                                    }

                                    break;
                                case 5:
                                    switch (letter2) //position
                                    {
                                        case 1:
                                            C = 0.4f;
                                            break;
                                        case 2:
                                            C = 0f;
                                            break;
                                        case 3:
                                            C = 0f;
                                            break;
                                        case 4:
                                            C = 0.2f;
                                            break;
                                    }

                                    break;
                                case 6:
                                case 7:
                                    switch (letter2) //position
                                    {
                                        case 1:
                                            C = 0.4f;
                                            break;
                                        case 2:
                                            C = 0f;
                                            break;
                                        case 3:
                                            C = 0f;
                                            break;
                                        case 4:
                                            C = 0.4f;
                                            break;
                                    }

                                    break;
                            }

                            break;
                        case 2:
                            switch (letter2) //position
                            {
                                case 1:
                                    C = 0.6f;
                                    break;
                                case 2:
                                    C = 0.4f;
                                    break;
                                case 3:
                                    C = 0.4f;
                                    break;
                                case 4:
                                    C = 0.4f;
                                    break;
                            }
                            break;
                        case 3:
                            switch (somersaults)
                            {
                                case 1:
                                case 2:
                                case 3:
                                case 4:
                                    switch (letter2) //position
                                    {
                                        case 1:
                                            C = 0.8f;
                                            break;
                                        case 2:
                                            C = 0.8f;
                                            break;
                                        case 3:
                                            C = 0.8f;
                                            break;
                                        case 4:
                                            C = 0.8f;
                                            break;
                                    }

                                    break;
                                case 5:
                                case 6:
                                case 7:
                                    switch (letter2) //position
                                    {
                                        case 1:
                                            C = 0.8f;
                                            break;
                                        case 2:
                                            C = 0.7f;
                                            break;
                                        case 3:
                                            C = 0.6f;
                                            break;
                                        case 4:
                                            C = 0.8f;
                                            break;
                                    }

                                    break;

                            }

                            break;
                        case 4:
                            switch (letter2) //position
                            {
                                case 1:
                                    C = 1.0f;
                                    break;
                                case 2:
                                    C = 0.8f;
                                    break;
                                case 3:
                                    C = 0.8f;
                                    break;
                                case 4:
                                    C = 0.8f;
                                    break;
                            }

                            break;
                        case 5:
                            switch (somersaults)
                            {
                                case 1:
                                case 2:
                                case 3:
                                case 4:
                                    switch (letter2) //position
                                    {
                                        case 1:
                                            C = 1.2f;
                                            break;
                                        case 2:
                                            C = 1.2f;
                                            break;
                                        case 3:
                                            C = 1.2f;
                                            break;
                                        case 4:
                                            C = 1.2f;
                                            break;
                                    }

                                    break;
                                case 5:
                                case 6:
                                case 7:
                                    switch (letter2) //position
                                    {
                                        case 1:
                                            C = 1.2f;
                                            break;
                                        case 2:
                                            C = 1.1f;
                                            break;
                                        case 3:
                                            C = 1.0f;
                                            break;
                                        case 4:
                                            C = 1.2f;
                                            break;
                                    }

                                    break;
                            }

                            break;
                        case 6:
                            switch (letter2) //position
                            {
                                case 1:
                                    C = 1.5f;
                                    break;
                                case 2:
                                    C = 1.4f;
                                    break;
                                case 3:
                                    C = 1.4f;
                                    break;
                                case 4:
                                    C = 1.5f;
                                    break;
                            }

                            break;
                        case 7:
                            switch (letter2) //position
                            {
                                case 1:
                                    C = 1.6f;
                                    break;
                                case 2:
                                    C = 1.7f;
                                    break;
                                case 3:
                                    C = 1.8f;
                                    break;
                                case 4:
                                    C = 1.6f;
                                    break;
                            }

                            break;
                        case 8:
                            switch (letter2) //position
                            {
                                case 1:
                                    C = 1.9f;
                                    break;
                                case 2:
                                    C = 1.8f;
                                    break;
                                case 3:
                                    C = 1.8f;
                                    break;
                                case 4:
                                    C = 1.9f;
                                    break;
                            }

                            break;
                        case 9:
                            switch (letter2) //position
                            {
                                case 1:
                                    C = 2.0f;
                                    break;
                                case 2:
                                    C = 2.1f;
                                    break;
                                case 3:
                                    C = 2.1f;
                                    break;
                                case 4:
                                    C = 2.0f;
                                    break;
                            }

                            break;
                    }
                }
            }
            else
            {
                if (position == 5)
                {
                    switch (twists)
                    {
                        case 1: //number of half twists
                            switch (somersaults)
                            {
                                case 1:
                                case 2:
                                    switch (letter2) //position
                                    {
                                        case 1:
                                            C = 0.4f;
                                            break;
                                        case 2:
                                            C = 0.2f;
                                            break;
                                        case 3:
                                            C = 0.2f;
                                            break;
                                        case 4:
                                            C = 0.2f;
                                            break;
                                    }

                                    break;
                                case 3:
                                case 4:
                                    switch (letter2) //position
                                    {
                                        case 1:
                                            C = 0.4f;
                                            break;
                                        case 2:
                                            C = 0.4f;
                                            break;
                                        case 3:
                                            C = 0.4f;
                                            break;
                                        case 4:
                                            C = 0.4f;
                                            break;
                                    }

                                    break;
                                case 5:
                                    switch (letter2) //position
                                    {
                                        case 1:
                                            C = 0.4f;
                                            break;
                                        case 2:
                                            C = 0f;
                                            break;
                                        case 3:
                                            C = 0f;
                                            break;
                                        case 4:
                                            C = 0.2f;
                                            break;
                                    }

                                    break;
                                case 6:
                                case 7:
                                    switch (letter2) //position
                                    {
                                        case 1:
                                            C = 0.4f;
                                            break;
                                        case 2:
                                            C = 0f;
                                            break;
                                        case 3:
                                            C = 0f;
                                            break;
                                        case 4:
                                            C = 0.4f;
                                            break;
                                    }

                                    break;
                            }

                            break;
                        case 2:
                            switch (somersaults)
                            {
                                case 0:
                                    switch (letter2) //position
                                    {
                                        case 1:
                                            C = 0.6f;
                                            break;
                                        case 2:
                                            C = 0.4f;
                                            break;
                                        case 3:
                                            C = 0.4f;
                                            break;
                                        case 4:
                                            C = 0.4f;
                                            break;
                                    }

                                    break;
                            }

                            break;
                        case 3:
                            switch (somersaults)
                            {
                                case 1:
                                case 2:
                                case 3:
                                case 4:
                                    switch (letter2) //position
                                    {
                                        case 1:
                                            C = 0.8f;
                                            break;
                                        case 2:
                                            C = 0.8f;
                                            break;
                                        case 3:
                                            C = 0.8f;
                                            break;
                                        case 4:
                                            C = 0.8f;
                                            break;
                                    }

                                    break;
                                case 5:
                                case 6:
                                case 7:
                                    switch (letter2) //position
                                    {
                                        case 1:
                                            C = 0.8f;
                                            break;
                                        case 2:
                                            C = 0.6f;
                                            break;
                                        case 3:
                                            C = 0.6f;
                                            break;
                                        case 4:
                                            C = 0.8f;
                                            break;
                                    }

                                    break;

                            }

                            break;
                        case 4:
                            switch (somersaults) //position
                            {
                                case 1:
                                    C = 1.0f;
                                    break;
                                case 2:
                                    C = 0.8f;
                                    break;
                                case 3:
                                    C = 0.8f;
                                    break;
                                case 4:
                                    C = 0.8f;
                                    break;
                            }

                            break;
                        case 5:
                            switch (somersaults)
                            {
                                case 1:
                                case 2:
                                case 3:
                                case 4:
                                    switch (letter2) //position
                                    {
                                        case 1:
                                            C = 1.2f;
                                            break;
                                        case 2:
                                            C = 1.2f;
                                            break;
                                        case 3:
                                            C = 1.2f;
                                            break;
                                        case 4:
                                            C = 1.2f;
                                            break;
                                    }

                                    break;
                                case 5:
                                case 6:
                                case 7:
                                    switch (letter2) //position
                                    {
                                        case 1:
                                            C = 1.2f;
                                            break;
                                        case 2:
                                            C = 1.0f;
                                            break;
                                        case 3:
                                            C = 1.0f;
                                            break;
                                        case 4:
                                            C = 1.2f;
                                            break;
                                    }

                                    break;
                            }

                            break;
                        case 6:
                            switch (letter2) //position
                            {
                                case 1:
                                    C = 1.5f;
                                    break;
                                case 2:
                                    C = 1.4f;
                                    break;
                                case 3:
                                    C = 1.4f;
                                    break;
                                case 4:
                                    C = 1.5f;
                                    break;
                            }

                            break;
                        case 7:
                            switch (somersaults)
                            {
                                case 1:
                                case 2:
                                case 3:
                                case 4:
                                    switch (letter2) //position
                                    {
                                        case 1:
                                            C = 1.6f;
                                            break;
                                        case 2:
                                            C = 1.7f;
                                            break;
                                        case 3:
                                            C = 1.7f;
                                            break;
                                        case 4:
                                            C = 1.6f;
                                            break;
                                    }

                                    break;
                                case 5:
                                case 6:
                                case 7:
                                    switch (letter2) //position
                                    {
                                        case 1:
                                            C = 1.6f;
                                            break;
                                        case 2:
                                            C = 1.5f;
                                            break;
                                        case 3:
                                            C = 1.5f;
                                            break;
                                        case 4:
                                            C = 1.6f;
                                            break;
                                    }

                                    break;
                            }

                            break;
                        case 8:
                            switch (letter2) //position
                            {
                                case 1:
                                    C = 1.9f;
                                    break;
                                case 2:
                                    C = 1.8f;
                                    break;
                                case 3:
                                    C = 1.8f;
                                    break;
                                case 4:
                                    C = 1.9f;
                                    break;
                            }

                            break;
                        case 9:
                            switch (somersaults)
                            {
                                case 1:
                                case 2:
                                case 3:
                                case 4:
                                    switch (letter2) //position
                                    {
                                        case 1:
                                            C = 2.0f;
                                            break;
                                        case 2:
                                            C = 2.1f;
                                            break;
                                        case 3:
                                            C = 2.1f;
                                            break;
                                        case 4:
                                            C = 2.0f;
                                            break;
                                    }

                                    break;
                                case 5:
                                case 6:
                                case 7:
                                    switch (letter2) //position
                                    {
                                        case 1:
                                            C = 2.0f;
                                            break;
                                        case 2:
                                            C = 1.9f;
                                            break;
                                        case 3:
                                            C = 1.9f;
                                            break;
                                        case 4:
                                            C = 2.0f;
                                            break;
                                    }

                                    break;
                            }

                            break;
                    }
                }
                else if (position == 6)
                {
                    switch (twists)
                    {
                        case 1: //number of half twists
                            switch (somersaults)
                            {
                                case 1:
                                case 2:
                                    switch (letter2) //position
                                    {
                                        case 1:
                                            C = 0.4f;
                                            break;
                                        case 2:
                                        case 3:
                                            C = 0.4f;
                                            break;
                                    }

                                    break;
                                case 3:
                                case 4:
                                    switch (letter2) //position
                                    {
                                        case 1:
                                            C = 0.5f;
                                            break;
                                        case 2:
                                        case 3:
                                            C = 0.5f;
                                            break;
                                    }

                                    break;
                                case 5:
                                    switch (letter2) //position
                                    {
                                        case 1:
                                            C = 0.5f;
                                            break;
                                        case 2:
                                        case 3:
                                            C = 0.5f;
                                            break;
                                    }

                                    break;
                                case 6:
                                case 7:
                                    switch (letter2) //position
                                    {
                                        case 1:
                                            C = 0.4f;
                                            break;
                                        case 2:
                                        case 3:
                                            C = 0.5f;
                                            break;
                                    }

                                    break;
                            }

                            break;
                        case 2:
                            switch (somersaults)
                            {
                                case 0:
                                    switch (letter2) //position
                                    {
                                        case 1:
                                            C = 1.2f;
                                            break;
                                        case 2:
                                        case 3:
                                            C = 1.2f;
                                            break;
                                    }
                                    break;
                            }
                            break;
                        case 3:
                            switch (somersaults)
                            {
                                case 1:
                                case 2:
                                case 3:
                                case 4:
                                    switch (letter2) //position
                                    {
                                        case 1:
                                            C = 1.3f;
                                            break;
                                        case 2:
                                        case 3:
                                            C = 1.3f;
                                            break;
                                    }

                                    break;
                                case 5:
                                case 6:
                                case 7:
                                    switch (letter2) //position
                                    {
                                        case 1:
                                            C = 1.3f;
                                            break;
                                        case 2:
                                        case 3:
                                            C = 1.3f;
                                            break;
                                    }
                                    break;
                            }
                            break;
                        case 4:
                            switch (somersaults) //position
                            {
                                case 1:
                                    C = 1.5f;
                                    break;
                                case 2:
                                case 3:
                                    C = 1.3f;
                                    break;
                            }
                            break;
                        case 5:
                            switch (somersaults)
                            {
                                case 1:
                                case 2:
                                case 3:
                                case 4:
                                    switch (letter2) //position
                                    {
                                        case 1:
                                            C = 1.7f;
                                            break;
                                        case 2:
                                        case 3:
                                            C = 1.7f;
                                            break;
                                    }
                                    break;
                                case 5:
                                case 6:
                                case 7:
                                    switch (letter2) //position
                                    {
                                        case 1:
                                            C = 1.7f;
                                            break;
                                        case 2:
                                        case 3:
                                            C = 1.7f;
                                            break;
                                    }
                                    break;
                            }
                            break;
                        case 6:
                            switch (letter2) //position
                            {
                                case 1:
                                    C = 1.9f;
                                    break;
                                case 2:
                                case 3:
                                    C = 1.9f;
                                    break;
                            }
                            break;
                        case 7:
                            switch (somersaults)
                            {
                                case 1:
                                case 2:
                                case 3:
                                case 4:
                                    switch (letter2) //position
                                    {
                                        case 1:
                                            C = 2.1f;
                                            break;
                                        case 2:
                                        case 3:
                                            C = 2.1f;
                                            break;
                                    }
                                    break;
                                case 5:
                                case 6:
                                case 7:
                                    switch (letter2) //position
                                    {
                                        case 1:
                                            C = 2.1f;
                                            break;
                                        case 2:
                                        case 3:
                                            C = 2.1f;
                                            break;
                                    }
                                    break;
                            }
                            break;
                        case 8:
                            switch (letter2) //position
                            {
                                case 1:
                                    C = 2.3f;
                                    break;
                                case 2:
                                case 3:
                                    C = 2.3f;
                                    break;
                            }
                            break;
                        case 9:
                            switch (somersaults)
                            {
                                case 1:
                                case 2:
                                case 3:
                                case 4:
                                    switch (letter2) //position
                                    {
                                        case 1:
                                            C = 2.5f;
                                            break;
                                        case 2:
                                        case 3:
                                            C = 2.5f;
                                            break;
                                    }

                                    break;
                                case 5:
                                case 6:
                                case 7:
                                    switch (letter2) //position
                                    {
                                        case 1:
                                            C = 2.5f;
                                            break;
                                        case 2:
                                        case 3:
                                            C = 2.5f;
                                            break;
                                    }
                                    break;
                            }
                            break;
                    }
                }
            }
            return C;
        }
        private static float CalculateD(int position, int letter2, int somersaults, int height, int twists)
        {
            float D = 0f;
            switch (position)
            {
                case 1:
                    switch (height)
                    {
                        case 1 when somersaults < 8:
                            D = 0f;
                            break;
                        case 1:
                            D = 0.5f;
                            break;
                        case 3 when somersaults < 8:
                            D = 0f;
                            break;
                        case 3:
                            D = 0.3f;
                            break;
                        case 5 when somersaults < 8:
                            D = 0f;
                            break;
                        case 5:
                            D = 0.5f;
                            break;
                        case 7 when somersaults < 8:
                            D = 0f;
                            break;
                        case 7:
                            D = 0.3f;
                            break;
                        case 10 when somersaults < 8:
                            D = 0f;
                            break;
                        case 10:
                            D = 0.2f;
                            break;
                    }
                    break;
                case 2:
                    switch (height)
                    {
                        case 1 when somersaults < 7:
                            D = 0.2f;
                            break;
                        case 1:
                            D = 0.6f;
                            break;
                        case 3 when somersaults < 7:
                            D = 0.2f;
                            break;
                        case 3:
                            D = 0.4f;
                            break;
                        case 5 when somersaults < 7:
                            D = 0.2f;
                            break;
                        case 5:
                            D = 0.5f;
                            break;
                        case 7 when somersaults < 7:
                            D = 0.2f;
                            break;
                        case 7:
                            D = 0.3f;
                            break;
                        case 10 when somersaults < 7:
                            D = 0.2f;
                            break;
                        case 10:
                            D = 0.2f;
                            break;
                    }
                    break;
                case 3:
                    switch (height)
                    {
                        case 1 when somersaults < 7:
                            D = 0.3f;
                            break;
                        case 1:
                            D = 0.5f;
                            break;
                        case 3 when somersaults < 7:
                        case 3:
                            D = 0.3f;
                            break;
                        case 5 when somersaults < 5:
                            D = 0.3f;
                            break;
                        case 5 when somersaults < 7:
                            D = 0.4f;
                            break;
                        case 5:
                            D = 0.6f;
                            break;
                        case 7 when somersaults < 5:
                            D = 0.3f;
                            break;
                        case 7 when somersaults < 7:
                            D = 0.4f;
                            break;
                        case 7:
                            D = 0.4f;
                            break;
                        case 10 when somersaults < 5:
                            D = 0.3f;
                            break;
                        case 10 when somersaults < 7:
                            D = 0.4f;
                            break;
                        case 10:
                            D = 0.3f;
                            break;
                    }
                    break;
                case 4:
                    switch (height)
                    {
                        case 1 when somersaults < 3:
                            D = 0.6f;
                            break;
                        case 1:
                            D = 0.5f;
                            break;
                        case 3 when somersaults < 3:
                        case 3:
                            D = 0.3f;
                            break;
                        case 5 when somersaults < 3:
                            D = 0.6f;
                            break;
                        case 5:
                            D = 0.5f;
                            break;
                        case 7 when somersaults < 3:
                            D = 0.3f;
                            break;
                        case 7:
                            D = 0.3f;
                            break;
                        case 10 when somersaults < 3:
                            D = 0.3f;
                            break;
                        case 10:
                            D = 0.2f;
                            break;
                    }
                    break;
                case 5:
                case 6 when twists != 0:

                    switch (letter2)
                    {
                        case 1:
                            switch (height)
                            {
                                case 1 when somersaults < 8:
                                    D = 0f;
                                    break;
                                case 1:
                                    D = 0.5f;
                                    break;
                                case 3 when somersaults < 8:
                                    D = 0f;
                                    break;
                                case 3:
                                    D = 0.3f;
                                    break;
                                case 5 when somersaults < 8:
                                    D = 0f;
                                    break;
                                case 5:
                                    D = 0.5f;
                                    break;
                                case 7 when somersaults < 8:
                                    D = 0f;
                                    break;
                                case 7:
                                    D = 0.3f;
                                    break;
                                case 10 when somersaults < 8:
                                    D = 0f;
                                    break;
                                case 10:
                                    D = 0.2f;
                                    break;
                            }

                            break;
                        case 2:
                            switch (height)
                            {
                                case 1 when somersaults < 7:
                                    D = 0.2f;
                                    break;
                                case 1:
                                    D = 0.6f;
                                    break;
                                case 3 when somersaults < 7:
                                    D = 0.2f;
                                    break;
                                case 3:
                                    D = 0.4f;
                                    break;
                                case 5 when somersaults < 7:
                                    D = 0.2f;
                                    break;
                                case 5:
                                    D = 0.5f;
                                    break;
                                case 7 when somersaults < 7:
                                    D = 0.2f;
                                    break;
                                case 7:
                                    D = 0.3f;
                                    break;
                                case 10 when somersaults < 7:
                                    D = 0.2f;
                                    break;
                                case 10:
                                    D = 0.2f;
                                    break;
                            }

                            break;
                        case 3:
                            switch (height)
                            {
                                case 1 when somersaults < 7:
                                    D = 0.3f;
                                    break;
                                case 1:
                                    D = 0.5f;
                                    break;
                                case 3 when somersaults < 7:
                                case 3:
                                    D = 0.3f;
                                    break;
                                case 5 when somersaults < 5:
                                    D = 0.3f;
                                    break;
                                case 5 when somersaults < 7:
                                    D = 0.4f;
                                    break;
                                case 5:
                                    D = 0.6f;
                                    break;
                                case 7 when somersaults < 5:
                                    D = 0.3f;
                                    break;
                                case 7 when somersaults < 7:
                                    D = 0.4f;
                                    break;
                                case 7:
                                    D = 0.4f;
                                    break;
                                case 10 when somersaults < 5:
                                    D = 0.3f;
                                    break;
                                case 10 when somersaults < 7:
                                    D = 0.4f;
                                    break;
                                case 10:
                                    D = 0.3f;
                                    break;
                            }

                            break;
                        case 4:
                            switch (height)
                            {
                                case 1 when somersaults < 3:
                                    D = 0.6f;
                                    break;
                                case 1:
                                    D = 0.5f;
                                    break;
                                case 3 when somersaults < 3:
                                case 3:
                                    D = 0.3f;
                                    break;
                                case 5 when somersaults < 3:
                                    D = 0.6f;
                                    break;
                                case 5:
                                    D = 0.5f;
                                    break;
                                case 7 when somersaults < 3:
                                    D = 0.3f;
                                    break;
                                case 7:
                                    D = 0.3f;
                                    break;
                                case 10 when somersaults < 3:
                                    D = 0.3f;
                                    break;
                                case 10:
                                    D = 0.2f;
                                    break;
                            }

                            break;
                    }
                    break;
                case 6:
                    switch (letter2)
                    {
                        case 1:
                            if (somersaults < 5)
                                D = 0.2f;
                            else
                                D = 0.4f;
                            break;
                        case 2:
                            if (somersaults < 2)
                                D = 0.2f;
                            else
                                D = 0.4f;
                            break;
                        case 3:
                            if (somersaults < 2)
                                D = 0.3f;
                            else
                                D = 0.5f;
                            break;
                    }
                    break;
            }
            return D;
        }

        private static float CalculateE(int position, int letter2, int somersaults, int height, int twists)
        {
            float E = 0f;
            if (height < 4)
            {
                switch (position)
                {
                    case 1:
                    case 4:
                        switch (somersaults)
                        {
                            case 1:
                            case 3:
                            case 5:
                            case 7:
                            case 9:
                                E = 0f;
                                break;
                            case 2:
                                E = 0.1f;
                                break;
                            case 4:
                            case 6:
                            case 8:
                                E = 0.2f;
                                break;
                        }

                        break;
                    case 2:
                    case 3:
                        switch (somersaults)
                        {
                            case 1:
                                E = 0.1f;
                                break;
                            case 3:
                                E = 0.2f;
                                break;
                            case 5:
                                E = 0.3f;
                                break;
                            case 7:
                            case 9:
                                E = 0.4f;
                                break;
                            case 2:
                            case 4:
                            case 6:
                            case 8:
                                E = 0f;
                                break;
                        }

                        break;
                    //does not apply to twisting dives. (5)
                    case 6:
                        switch (letter2) //for dives 5 & 6, letter 2 is position value
                        {
                            case 1:
                            case 4:
                                switch (somersaults)
                                {
                                    case 1:
                                    case 3:
                                    case 5:
                                    case 7:
                                    case 9:
                                        E = 0f;
                                        break;
                                    case 2:
                                        E = 0.1f;
                                        break;
                                    case 4:
                                    case 6:
                                    case 8:
                                        E = 0.2f;
                                        break;
                                }

                                break;
                            case 2:
                            case 3:
                                switch (somersaults)
                                {
                                    case 1:
                                        E = 0.1f;
                                        break;
                                    case 3:
                                        E = 0.2f;
                                        break;
                                    case 5:
                                        E = 0.3f;
                                        break;
                                    case 7:
                                    case 9:
                                        E = 0.4f;
                                        break;
                                    case 2:
                                    case 4:
                                    case 6:
                                    case 8:
                                        E = 0f;
                                        break;
                                }

                                break;
                        }

                        break;
                }
            }
            else
            {
                switch (position)
                {
                    case 1:
                    case 4:
                        switch (somersaults)
                        {
                            case 1:
                            case 3:
                            case 5:
                            case 7:
                            case 8:
                            case 9:
                            case 11:
                                E = 0f;
                                break;
                            case 2:
                                E = 0.1f;
                                break;
                            case 4:
                            case 6:
                                E = 0.2f;
                                break;
                        }

                        break;
                    case 2:
                    case 3:
                        switch (somersaults)
                        {
                            case 1:
                                E = 0.1f;
                                break;
                            case 3:
                                E = 0.2f;
                                break;
                            case 5:
                                E = 0.3f;
                                break;
                            case 7:
                            case 9:
                                E = 0.4f;
                                break;
                            case 2:
                            case 4:
                            case 6:
                            case 8:
                            case 11:
                                E = 0f;
                                break;
                        }

                        break;
                    case 5:
                        if (twists == 0)
                        {
                            switch (letter2)
                            {
                                case 1:
                                    switch (somersaults)
                                    {
                                        case 1:
                                            E = 0.1f;
                                            break;
                                        case 3:
                                            E = 0.2f;
                                            break;
                                        case 5:
                                            E = 0.3f;
                                            break;
                                        case 7:
                                        case 9:
                                            E = 0.4f;
                                            break;
                                        case 2:
                                        case 4:
                                        case 6:
                                        case 8:
                                        case 11:
                                            E = 0f;
                                            break;
                                    }
                                    break;
                                case 2:
                                case 3:
                                    switch (somersaults)
                                    {
                                        case 1:
                                        case 3:
                                        case 5:
                                        case 7:
                                        case 9:
                                        case 11:
                                            E = 0f;
                                            break;
                                        case 2:
                                            E = 0.1f;
                                            break;
                                        case 4:
                                        case 6:
                                            E = 0.2f;
                                            break;
                                        case 8:
                                            E = 0.3f;
                                            break;
                                    }
                                    break;
                            }
                        }

                        break;
                    //does not apply to twisting dives. (5)
                    case 6:
                        switch (letter2) //for dives 5 & 6, letter 2 is position value
                        {
                            case 1:
                            case 4:
                                switch (somersaults)
                                {
                                    case 1:
                                        E = 0.1f;
                                        break;
                                    case 3:
                                        E = 0.2f;
                                        break;
                                    case 5:
                                        E = 0.3f;
                                        break;
                                    case 7:
                                    case 9:
                                        E = 0.4f;
                                        break;
                                    case 2:
                                    case 4:
                                    case 6:
                                    case 8:
                                    case 11:
                                        E = 0f;
                                        break;
                                }

                                break;
                            case 2:
                            case 3:



                                switch (somersaults)
                                {
                                    case 1:
                                    case 3:
                                    case 5:
                                    case 7:
                                    case 8:
                                    case 9:
                                    case 11:
                                        E = 0f;
                                        break;
                                    case 2:
                                        E = 0.1f;
                                        break;
                                    case 4:
                                    case 6:
                                        E = 0.2f;
                                        break;
                                }
                                break;
                        }
                        break;
                }
            }
            return E;
        }

    }
}
