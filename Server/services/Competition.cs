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