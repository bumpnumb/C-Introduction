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
        /// Uses String Code and Height To calculate Jump Name
        /// </summary>
        public static string GenerateJumpNameFromCode(string code, int height)
        {
            Jump j = ParseDifficulty(code, height);

            return "hello";
        }


        public static float GenerateJumpDifficultyFromCode(string code, int height)
        {


            return 1.0f;
        }

        /// <summary>
        /// Uses String Code and Height To fetch jump stats
        /// </summary>
        public static Jump ParseDifficulty(string code, int height)
        {
            string[] letters = code.Split(',');
            float A = 0, B = 0, C = 0, D = 0, E = 0;

            //A: Somersaults
            switch (Int32.Parse(letters[2]))
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

            if (letters[1] == "1" && Int32.Parse(letters[0]) <= 4) //if flying action
            {
                switch (Int32.Parse(letters[0])) //what group
                {
                    case 1: //front
                        switch (Int32.Parse(letters[2])) //number of somersaults
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
                        switch (Int32.Parse(letters[2])) //number of somersaults
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
                        switch (Int32.Parse(letters[2])) //number of somersaults
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
                        switch (Int32.Parse(letters[2])) //number of somersaults
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
            else //if dive was not 1,2,3,4 with letter 1 not being 1 (all jumps where B is based of letter)
            {
                switch (letters[0])
                {
                    case "1": //front
                        switch (Int32.Parse(letters[2])) //number of somersaults
                        {
                            case 0:
                            case 1:
                            case 2:
                                switch (letters[3]) //flight position
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
                                switch (letters[3]) //flight position
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
                                switch (letters[3]) //flight position
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
                                switch (letters[3]) //flight position
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
                                switch (letters[3]) //flight position
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
                    case "2": //back
                        switch (Int32.Parse(letters[2])) //number of somersaults
                        {
                            case 0:
                            case 1:
                            case 2:
                                switch (letters[3]) //flight position
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
                                switch (letters[3]) //flight position
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
                                switch (letters[3]) //flight position
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
                                switch (letters[3]) //flight position
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
                                switch (letters[3]) //flight position
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
                    case "3": //reversed
                        switch (Int32.Parse(letters[2])) //number of somersaults
                        {
                            case 0:
                            case 1:
                            case 2:
                                switch (letters[3]) //flight position
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
                                switch (letters[3]) //flight position
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
                                switch (letters[3]) //flight position
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
                                switch (letters[3]) //flight position
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
                                switch (letters[3]) //flight position
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
                    case "4": //inward
                        switch (Int32.Parse(letters[2])) //number of somersaults
                        {
                            case 0:
                            case 1:
                            case 2:
                                switch (letters[3]) //flight position
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
                                switch (letters[3]) //flight position
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
                                switch (letters[3]) //flight position
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
                                switch (letters[3]) //flight position
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
                                switch (letters[3]) //flight position
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
                    case "5": //twisting
                        switch (Int32.Parse(letters[1]))
                        {
                            case 1: //front
                                switch (Int32.Parse(letters[2])) //number of somersaults
                                {
                                    case 0:
                                    case 1:
                                    case 2:
                                        switch (letters[4]) //flight position
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
                                        switch (letters[4]) //flight position
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
                                        switch (letters[4]) //flight position
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
                                        switch (letters[4]) //flight position
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
                                        switch (letters[4]) //flight position
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
                                switch (Int32.Parse(letters[2])) //number of somersaults
                                {
                                    case 0:
                                    case 1:
                                    case 2:
                                        switch (letters[4]) //flight position
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
                                        switch (letters[4]) //flight position
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
                                        switch (letters[4]) //flight position
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
                                        switch (letters[4]) //flight position
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
                                        switch (letters[4]) //flight position
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
                                switch (Int32.Parse(letters[2])) //number of somersaults
                                {
                                    case 0:
                                    case 1:
                                    case 2:
                                        switch (letters[4]) //flight position
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
                                        switch (letters[4]) //flight position
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
                                        switch (letters[4]) //flight position
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
                                        switch (letters[4]) //flight position
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
                                        switch (letters[4]) //flight position
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
                                switch (Int32.Parse(letters[2])) //number of somersaults
                                {
                                    case 0:
                                    case 1:
                                    case 2:
                                        switch (letters[4]) //flight position
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
                                        switch (letters[4]) //flight position
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
                                        switch (letters[4]) //flight position
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
                                        switch (letters[4]) //flight position
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
                                        switch (letters[4]) //flight position
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
                            case 6: //armstand
                                switch (Int32.Parse(letters[1]))
                                {
                                    case 1: //front
                                        switch (Int32.Parse(letters[2])) //number of somersaults
                                        {
                                            case 0:
                                            case 1:
                                            case 2:
                                                switch (letters[4]) //flight position
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
                                                switch (letters[4]) //flight position
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
                                                switch (letters[4]) //flight position
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
                                                switch (letters[4]) //flight position
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
                                                switch (letters[4]) //flight position
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
                                        switch (Int32.Parse(letters[2])) //number of somersaults
                                        {
                                            case 0:
                                            case 1:
                                            case 2:
                                                switch (letters[4]) //flight position
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
                                                switch (letters[4]) //flight position
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
                                                switch (letters[4]) //flight position
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
                                                switch (letters[4]) //flight position
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
                                                switch (letters[4]) //flight position
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
                                        switch (Int32.Parse(letters[2])) //number of somersaults
                                        {
                                            case 0:
                                            case 1:
                                            case 2:
                                                switch (letters[4]) //flight position
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
                                                switch (letters[4]) //flight position
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
                                                switch (letters[4]) //flight position
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
                                                switch (letters[4]) //flight position
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
                                                switch (letters[4]) //flight position
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
                                }

                                break;
                            default:
                                break;
                        }

                        break;
                }
            }

            if (letters[0] == "5" || letters[0] == "6")
            {
                switch (Int32.Parse(letters[3]))
                {
                    case 1: //number of half twists
                        switch (Int32.Parse(letters[2]))
                        {
                            case 1:
                            case 2:
                                switch (Int32.Parse(letters[1])) //position
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
                                switch (Int32.Parse(letters[1])) //position
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
                                switch (Int32.Parse(letters[1])) //position
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
                                switch (Int32.Parse(letters[1])) //position
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
                        switch (Int32.Parse(letters[2]))
                        {
                            case 0:
                                switch (Int32.Parse(letters[1])) //position
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
                        switch (Int32.Parse(letters[2]))
                        {
                            case 1:
                            case 2:
                            case 3:
                            case 4:
                                switch (Int32.Parse(letters[1])) //position
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
                                switch (Int32.Parse(letters[1])) //position
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
                        switch (Int32.Parse(letters[1])) //position
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
                        switch (Int32.Parse(letters[2]))
                        {
                            case 1:
                            case 2:
                            case 3:
                            case 4:
                                switch (Int32.Parse(letters[1])) //position
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
                                switch (Int32.Parse(letters[1])) //position
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
                        switch (Int32.Parse(letters[1])) //position
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
                        switch (Int32.Parse(letters[1])) //position
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
                        switch (Int32.Parse(letters[1])) //position
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
                        switch (Int32.Parse(letters[1])) //position
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


            switch (Int32.Parse(letters[0]))
            {
                case 1:
                    if (height == 1)
                    {
                        if (Int32.Parse(letters[2]) < 8)
                        {
                            D = 0f;
                        }
                        else
                        {
                            D = 0.5f;
                        }
                    }
                    else if (height == 3)
                    {
                        if (Int32.Parse(letters[2]) < 8)
                        {
                            D = 0f;
                        }
                        else
                        {
                            D = 0.3f;
                        }

                    }
                    break;
                case 2:
                    if (height == 1)
                    {
                        if (Int32.Parse(letters[2]) < 7)
                        {
                            D = 0.2f;
                        }
                        else
                        {
                            D = 0.6f;
                        }
                    }
                    else if (height == 3)
                    {
                        if (Int32.Parse(letters[2]) < 7)
                        {
                            D = 0.2f;
                        }
                        else
                        {
                            D = 0.4f;
                        }

                    }
                    break;
                case 3:
                    if (height == 1)
                    {
                        if (Int32.Parse(letters[2]) < 7)
                        {
                            D = 0.3f;
                        }
                        else
                        {
                            D = 0.5f;
                        }
                    }
                    else if (height == 3)
                    {
                        if (Int32.Parse(letters[2]) < 7)
                        {
                            D = 0.3f;
                        }
                        else
                        {
                            D = 0.3f;
                        }

                    }
                    break;
                case 4:
                    if (height == 1)
                    {
                        if (Int32.Parse(letters[2]) < 3)
                        {
                            D = 0.6f;
                        }
                        else
                        {
                            D = 0.5f;
                        }
                    }
                    else if (height == 3)
                    {
                        if (Int32.Parse(letters[2]) < 3)
                        {
                            D = 0.3f;
                        }
                        else
                        {
                            D = 0.3f;
                        }

                    }
                    break;
                case 5:
                    switch (Int32.Parse(letters[2]))
                    {
                        case 1:
                            if (height == 1)
                            {
                                if (Int32.Parse(letters[2]) < 8)
                                {
                                    D = 0f;
                                }
                                else
                                {
                                    D = 0.5f;
                                }
                            }
                            else if (height == 3)
                            {
                                if (Int32.Parse(letters[2]) < 8)
                                {
                                    D = 0f;
                                }
                                else
                                {
                                    D = 0.3f;
                                }

                            }
                            break;
                        case 2:
                            if (height == 1)
                            {
                                if (Int32.Parse(letters[2]) < 7)
                                {
                                    D = 0.2f;
                                }
                                else
                                {
                                    D = 0.6f;
                                }
                            }
                            else if (height == 3)
                            {
                                if (Int32.Parse(letters[2]) < 7)
                                {
                                    D = 0.2f;
                                }
                                else
                                {
                                    D = 0.4f;
                                }

                            }
                            break;
                        case 3:
                            if (height == 1)
                            {
                                if (Int32.Parse(letters[2]) < 7)
                                {
                                    D = 0.3f;
                                }
                                else
                                {
                                    D = 0.5f;
                                }
                            }
                            else if (height == 3)
                            {
                                if (Int32.Parse(letters[2]) < 7)
                                {
                                    D = 0.3f;
                                }
                                else
                                {
                                    D = 0.3f;
                                }

                            }
                            break;
                        case 4:
                            if (height == 1)
                            {
                                if (Int32.Parse(letters[2]) < 3)
                                {
                                    D = 0.6f;
                                }
                                else
                                {
                                    D = 0.5f;
                                }
                            }
                            else if (height == 3)
                            {
                                if (Int32.Parse(letters[2]) < 3)
                                {
                                    D = 0.3f;
                                }
                                else
                                {
                                    D = 0.3f;
                                }

                            }
                            break;
                    }
                    break;
                case 6:
                    switch (Int32.Parse(letters[2]))
                    {
                        case 1:
                            if (height == 1)
                            {
                                if (Int32.Parse(letters[2]) < 8)
                                {
                                    D = 0f;
                                }
                                else
                                {
                                    D = 0.5f;
                                }
                            }
                            else if (height == 3)
                            {
                                if (Int32.Parse(letters[2]) < 8)
                                {
                                    D = 0f;
                                }
                                else
                                {
                                    D = 0.3f;
                                }

                            }
                            break;
                        case 2:
                            if (height == 1)
                            {
                                if (Int32.Parse(letters[2]) < 7)
                                {
                                    D = 0.2f;
                                }
                                else
                                {
                                    D = 0.6f;
                                }
                            }
                            else if (height == 3)
                            {
                                if (Int32.Parse(letters[2]) < 7)
                                {
                                    D = 0.2f;
                                }
                                else
                                {
                                    D = 0.4f;
                                }

                            }
                            break;
                        case 3:
                            if (height == 1)
                            {
                                if (Int32.Parse(letters[2]) < 7)
                                {
                                    D = 0.3f;
                                }
                                else
                                {
                                    D = 0.5f;
                                }
                            }
                            else if (height == 3)
                            {
                                if (Int32.Parse(letters[2]) < 7)
                                {
                                    D = 0.3f;
                                }
                                else
                                {
                                    D = 0.3f;
                                }

                            }
                            break;
                        case 4:
                            if (height == 1)
                            {
                                if (Int32.Parse(letters[2]) < 3)
                                {
                                    D = 0.6f;
                                }
                                else
                                {
                                    D = 0.5f;
                                }
                            }
                            else if (height == 3)
                            {
                                if (Int32.Parse(letters[2]) < 3)
                                {
                                    D = 0.3f;
                                }
                                else
                                {
                                    D = 0.3f;
                                }

                            }
                            break;
                    }
                    break;
            }

            switch (Int32.Parse(letters[0]))
            {
                case 1:
                case 4:
                    switch (Int32.Parse(letters[2]))
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
                    switch (Int32.Parse(letters[2]))
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
                case 6:
                    switch (Int32.Parse(letters[1]))
                    {
                        case 1:
                        case 4:
                            switch (Int32.Parse(letters[2]))
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
                            switch (Int32.Parse(letters[2]))
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

            Jump j = new Jump();
            j.Difficulty = A + B + C + D + E;
            return j;
        }

    }
}