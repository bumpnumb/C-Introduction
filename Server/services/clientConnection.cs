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
                    string nameLogin = this.Data.Split("=;=")[0];
                    string passwLogin = this.Data.Split("=;=")[1];

                    rsp.user = db.GetUserByName(nameLogin);
                    if (rsp.user != null)
                    {
                        string salt = db.GetSaltByID(rsp.user.ID);
                        string hash = db.GetHashByID(rsp.user.ID);

                        if (crypto.AuthenticateLogin(passwLogin, hash, salt) == true)
                        {
                            //successfull login!
                            //utifrån skickad data skall saker i client hända?
                            Console.WriteLine("Successfull login!");
                            rsp.Data = "success";
                        }
                        else
                        {
                            //wrong password
                            //fail, prompt a new login request.
                            Console.WriteLine("Wrong passsword, try again!");
                            rsp.Data = "password";
                        }
                    }
                    else
                    {
                        //fail, prompt a new login request.
                        Console.WriteLine("There is no user with that name, try again!");
                        rsp.Data = "no user";
                    }
                    break;

                case MessageType.Register:
                    rsp.Type = MessageType.Register;
                    string name = this.Data.Split("=;=")[0];
                    string passw = this.Data.Split("=;=")[1];

                    rsp.user = db.GetUserByName(name);
                    if (rsp.user != null)
                    {
                        //send(thomas eror);
                        Console.WriteLine("User already exists!");
                        rsp.Data = "USER EXISTS!";
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
                            //Competition GetAll = GetAllCompetitions();
                            //rsp.Data = JsonConvert.SerializeObject(GetAll);
                            rsp.Type = MessageType.Competition;

//                            if(competitions != null){ alltså att vi har en eller flera competitions
//                               getAllCompetitions();
//                               rsp.Data = "success competitions";
//                            else
//                               send error
                            break;

                        case "GetActive":
                            //Competition GetActive = GetActiveCompetitions();
                            //rsp.Data = JsonConvert.SerializeObject(GetActive);
                            rsp.Type = MessageType.Competition;

                            break;

                        case "CreateCompetition":
                            //Competition CreateComp = CreateCompetition();
                            //rsp.Data = JsonConvert.SerializeObject(CreateComp);

                            rsp.Type = MessageType.Competition;

                            //ha detta som en array med string istället för en string
                            string Judges = this.Data.Split("=;=")[0];    //Hur blir detta om vi har olika många för vaje comp.
                            string Divers = this.Data.Split("=;=")[1];    //samma sak här
                            string NåttMer = this.Data.Split("=;=")[2];

                            if (competitions.createCompetition(Judges, Divers) == true) //== something
                            {
                                //yes, send done
                            }
                            else
                            {
                                //no, send didnt go through
                            }
                            break;
                    }
                    break;

                default:
                    break;
            }

            //Send(resp);

            return rsp;
        }

    }

}

