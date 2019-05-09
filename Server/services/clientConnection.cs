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
using System.Security.Cryptography;

namespace Server.services
{

    public enum MessageType { NoType, Login, Register, Competition }

    public class Response
    {
        public MessageType Type { get; set; }
        public string Data { get; set; }
        public User user { get; set; }
    }

    public class Message
    {
        public MessageType Type { get; set; }
        public string Data { get; set; }
        private User user { get; set; }

        public Response CreateResponse()
        {
            Database db = new Database();

            Response rsp = new Response();
            switch (this.Type)
            {
                case MessageType.NoType:
                    Console.WriteLine("Recieved notype message: " + this.Data);
                    break;

                case MessageType.Login:
                    rsp.Type = MessageType.Login;
                    string nameLogin = rsp.Data.Split("=;=")[0];
                    string passwLogin = rsp.Data.Split("=;=")[1];

                    rsp.user = db.GetUserByName(nameLogin);
                    if (rsp.user != null)
                    {
                        string salt = db.GetSaltByID(this.user.ID);
                        string hash = db.GetHashByID(this.user.ID);

                        if(crypto.AuthenticateLogin(passwLogin, hash, salt) == true)
                        {
                            //successfull login!
                            Console.WriteLine("Successfull login!");
                            rsp.Data = "Sucessfull login";
                        }
                        else
                        {
                            //wrong password
                            //fail, prompt a new login request.
                            Console.WriteLine("Wrong passsword, try again!");
                        }
                    }
                    else
                    {
                        //fail, prompt a new login request.
                        Console.WriteLine("There is no user with that name, try again!");
                    }
                    break;

                case MessageType.Register:
                    rsp.Type = MessageType.Register;
                    string name = rsp.Data.Split("=;=")[0];
                    string passw = rsp.Data.Split("=;=")[1];

                    rsp.user = db.GetUserByName(name);
                    if (rsp.user != null)
                    {
                        //send(thomas eror);
                    }
                    else
                    { 
                        User temp = crypto.GenerateSaltHash(passw);
                        db.RegisterUser(name, temp.Salt, temp.Hash);
                    }

                    break;

                //Ska vi typ ha, ViewCompetition och CreateCompetition istället? 
                //där vi har i ViewCompetition -> GetAll, GetActive, osv...,
                //men i CreateCompetition, bara skicka all information till databasen
                case MessageType.Competition:   
                    switch(this.Data)
                    {
                        case "GetAll":
                            Competition GetAll = GetAllCompetitions();
                            rsp.Data = JsonConvert.SerializeObject(GetAll);

                            break;

                        case "GetActive":
                            Competition GetActive = GetActiveCompetitions();
                            rsp.Data = JsonConvert.SerializeObject(GetActive);

                            break;

                        case "CreateCompetition":
                            Competition CreateComp = CreateCompetition();
                            rsp.Data = JsonConvert.SerializeObject(CreateComp);

                            break;
                    }
                    break;

                default:
                    break;
            }

            return rsp;
        }

    }

}

