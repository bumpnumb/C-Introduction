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

    public enum MessageType { NoType, Login, Register }

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
                        //successfull login!
                        Console.WriteLine("Successfull login!");
                    }
                    else
                    {
                        //fail, prompt a new login request.
                        //break;?
                    }

                    rsp.Data = "Sucessfull login";
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
                        rsp.user = db.RegisterUserByIDPW(name, passw);
                        if(rsp.user != null)
                        {
                            //success
                        }
                    }
                    
                    /*
                    User,Pass      -> HandleRespons
                    GetUserBy User -> DATABAS
                       DATABAS        -> om USER  -> FAIL
                       DATABAS        -> om !USER
                    Register       -> DATABAS
                    DATABAS        -> LOGIN 
                    */
                    break;

                default:
                    break;
            }

            return rsp;
        }

    }

}

