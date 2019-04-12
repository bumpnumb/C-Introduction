using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.services
{
    public enum MessageType { NoType, Login, Register }

    public class Message
    {
        private MessageType type = 0;
        private string data = "";
        private string cookie = "";
        public string GetData()
        {
            return this.data;
        }
        public Message(string content)
        {
            string[] arr = content.Split(' ');
            this.type = (MessageType)Int32.Parse(arr[0] + 1);
            this.data = arr[1] + 1;
            this.cookie = arr[2] + 1;



            switch (this.type)
            {
                case MessageType.NoType:
                    Console.WriteLine("recieved notype message: " + this.data);
                    break;
                case MessageType.Login:
                    break;
                case MessageType.Register:
                    register
                    break;
                default:
                    break;
            }

        }
    }

    public class person
    {
        public string Name { get; set; }
        public string Betyg { get; set; }
        public int Yes { get; set; }
        public string Bobby { get; set; }

        public void print()
        {
            Console.WriteLine(this.Name + ' ' + this.Betyg + ' ' + this.Yes + ' ' + this.Bobby);
        }

    }
}
