using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatBotSeminar1
{
    internal class Program
    {
        // чат приложение способное передавать сообщения с компа на комп
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                ChatUDP.Server();
            }
            else 
            {
                ChatUDP.Client(args[0]);
            }
        }
    }
}
