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
    public class Message
    {
        //Whenever we recieve a message from client, we parse it as a Message type
        //A message will have a User, Though is this needed? as we could set this in clienthandler...


        public MessageType Type { get; set; }
        public string Data { get; set; }
        private User user { get; set; }

        public Response CreateResponse()
        {
            //Our protocoll is build of a message and a response.
            //All sent messages expect one response.
            Database db = new Database();

            //Create the response to be sent back
            Response rsp = new Response();
            string[] part;


            //Switch on MessageType for easy handling
            switch (this.Type)
            {
                case MessageType.NoType:
                    //This could be a default but we ended up with having a no_message type.
                    Console.WriteLine("Recieved notype message: " + this.Data);
                    break;

                case MessageType.Login:
                    //Set the response type for parsing on other end.
                    rsp.Type = MessageType.Login;

                    string[] userString = this.Data.Split("\r\n"); //will this
                    //Try to find a user with same name
                    rsp.user = db.GetUserBySSN(userString[0]); //and this work? for "change separator please!"
                    if (rsp.user != null) //this could be made into a one-liner, kept apart for ease of readability
                    {
                        //user was found, fetch salt and hash
                        string salt = db.GetSaltByID(rsp.user.ID);
                        string hash = db.GetHashByID(rsp.user.ID);


                        //Authenticate user with salt & hash
                        if (crypto.AuthenticateLogin(userString[1], hash, salt) == true)
                        {
                            //return some message for client to continue
                            rsp.Data = "success";
                            rsp.user.Hash = "***";
                            rsp.user.Salt = "***";
                            rsp.user.SSN = "YYYY-MM-DD-XXXX";
                        }
                        else
                        {
                            //don't tell the client it has a correct username but wrong password.
                            //this makes bruteforcing easier!
                            rsp.Data = "no user";
                            rsp.user.Hash = "***";
                            rsp.user.Salt = "***";
                            rsp.user.SSN = "YYYY-MM-DD-XXXX";
                            //we use "no user" on both wrong username and wrong password
                        }
                    }
                    else
                    {
                        rsp.Data = "no user";
                    }
                    break;

                case MessageType.Register:
                    rsp.Type = MessageType.Register;

                    string[] registerUser = this.Data.Split("\r\n");
                    //try fo fetch user
                    rsp.user = db.GetUserBySSN(registerUser[1]); //does this work?, "split on other separator!!"
                    if (rsp.user != null)
                    {
                        rsp.Data = "USER EXISTS!";
                    }
                    else
                    {
                        //Generate some salt and hash
                        User tempUser = crypto.GenerateSaltHash(registerUser[2]); //split on other separator!!
                        db.RegisterUser(registerUser[0], registerUser[1], tempUser.Salt, tempUser.Hash);

                        //lets not be done here. as is now, we return nothing.
                        //from here, do a login atempt, and return thee user.
                        //Why should I register then login?
                        //process could be streamlined!

                        //A login attempt would not work since we check the "Group" when we log in.
                        //but the Group is assgined to each user, after some time, by an admin after the register...
                    }
                    break;

                case MessageType.Competition:
                    rsp.Type = MessageType.Competition;
                    //split string in case we want to know more
                    part = this.Data.Split("\r\n"); //correct separator use!
                    List<CompetitionWithUser> comp;


                    //Competition messagetypes will be as following:

                    //Type: Competition,
                    //Data: "prefix \r\n suffix"
                    //User: dont think this is honestly needed here!

                    //We switch on data.prefix, and is suffix is needed, use that to identify specific competitions.
                    switch (part[0])
                    {
                        case "GetAll":
                            //fetch all competitions
                            rsp.Type = MessageType.Competition;
                            comp = db.GetAllCompetitions();

                            //Newtonsoft.Json is used to turn objects to nice looking strings
                            rsp.Data = JsonConvert.SerializeObject(comp);
                            break;

                        case "GetActive":
                            //Fetch all competitions which are started but not ended
                            rsp.Type = MessageType.Competition;
                            comp = db.GetActiveCompetitions();

                            rsp.Data = JsonConvert.SerializeObject(comp);
                            break;

                        case "CreateCompetition":
                            rsp.Type = MessageType.Competition;


                            CompetitionWithUser CompInfo = JsonConvert.DeserializeObject<CompetitionWithUser>(part[1]);
                            List<Jump> jumps = JsonConvert.DeserializeObject<List<Jump>>(part[2]);


                            if (CompInfo != null)
                            {
                                db.CreateCompetition(CompInfo, jumps);
                                rsp.Data = "Competition created";

                            }
                            else
                            {
                                rsp.Data = "Competition failed";
                            }
                            break;
                    }
                    break;

                case MessageType.ScoreToJump:
                    rsp.Type = MessageType.ScoreToJump;

                    Result ScoreInfo = JsonConvert.DeserializeObject<Result>(this.Data);
                    if (ScoreInfo != null)
                    {
                        db.SetScoreToJump(ScoreInfo);
                        //rsp.Data = "Score set";
                    }
                    else
                    {
                        //rsp.Data = "Score not set";
                    }

                    break;

                case MessageType.Judges:
                    rsp.Type = MessageType.Judges;

                    List<User> AllJudges = db.GetAllJudges();
                    rsp.Data = JsonConvert.SerializeObject(AllJudges);

                    break;

                case MessageType.Jumpers:
                    rsp.Type = MessageType.Jumpers;

                    List<User> AllJumpers = db.GetAllJumpers();
                    rsp.Data = JsonConvert.SerializeObject(AllJumpers);

                    break;
                case MessageType.User:
                    part = this.Data.Split("\r\n");
                    switch (part[0])
                    {
                        case "Get All":
                            rsp.Type = MessageType.User;
                            List<User> AllUsers = db.GetAllUsers();
                            rsp.Data = JsonConvert.SerializeObject(AllUsers);
                            break;
                    }

                    break;

                default:
                    break;
            }

            //returns response to be sent
            return rsp;
        }

    }

}

