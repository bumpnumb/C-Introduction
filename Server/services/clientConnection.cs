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
        private string Cookie { get; set; }
        private User user { get; set; }

        public Response CreateResponse()
        {
            Response rsp = new Response();
            switch (this.Type)
            {
                case MessageType.NoType:
                    Console.WriteLine("Recieved notype message: " + this.Data);
                    break;

                case MessageType.Login:
                    if (!this.VerifyCookie())
                    {
                        //Cookie was not accepted so we should prompt a new login request.


                    }
                    rsp.Type = MessageType.Login;
                    rsp.Data = "Sucessfull login";
                    rsp.user = GetUserByCookie();
                    break;

                case MessageType.Register:

                    /*
                     * 
                    User,Pass      -> HandleRespons
                    GetUserBy User -> DATABAS
                       DATABAS        -> om USER  -> FAIL
                       DATABAS        -> om !USER
                    Register       -> DATABAS
                    DATABAS        -> ok..

                    */

                    rsp.Type = MessageType.Register;
                    rsp.Data = "New User created";
                    rsp.user.Cookie = crypto.GenerateCookie();
                    break;

                default:
                    break;
            }

            return rsp;
        }

        private User GetUserByCookie()
        {
            Database db = new Database();
            User u = db.GetUserByCookie(this.Cookie);
            // Add error handling
            return u;
        }

        private bool VerifyCookie()
        {
            // Connects with db
            Database db = new Database();
            // Find User by Cookie
            User u = db.GetUserByCookie(this.Cookie);
            // Checks CookieTime & refresh if valid
            if (u != null && u.CookieTime >= DateTime.Now)
            {
                this.RefreshCookie();
                this.user = u;
                return true;
            }
            return false;
        }

        private void RefreshCookie()
        {
            // Update Cookie timeout time with 24h
            Database db = new Database();
            db.UpdateCookieTimeByID(this.user.ID, DateTime.Now.AddHours(24));
        }

    }

}

