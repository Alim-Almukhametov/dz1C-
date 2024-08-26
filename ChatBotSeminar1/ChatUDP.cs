using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ChatBotSeminar1
{
    internal class ChatUDP
    {


        public static void Server()
        {
            IPEndPoint localEP = new IPEndPoint(IPAddress.Any, 0);
            UdpClient udpClient = new UdpClient(12345);
            Console.WriteLine("Server started and waiting for connections...");
            while (true) 
            {
                try 
                {
                    var bytes = udpClient.Receive(ref localEP);
                    string json = Encoding.UTF8.GetString(bytes);
                    User user = User.GetFromJSON(json);
                    if (user != null)
                    {
                        Console.WriteLine(user.ToString());
                        User suser = new User("Server", "Message received");
                        var jsonToSend = suser.GetJSON();
                        var bytesToSend = Encoding.UTF8.GetBytes(jsonToSend);
                        udpClient.Send(bytesToSend, bytesToSend.Length, localEP); 
                    }
                    else 
                    {
                        Console.WriteLine("Something went wrong , user is null");
                    }
                 //   if (user.TextMessage == "Exit")
                   
                }
                catch (Exception ex) 
                {
                    Console.WriteLine(ex.Message.ToString());
                }
            
          
            }
           
        }
        public static void Client(string nick)
        {
          IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 12345);
            UdpClient udpClient = new UdpClient();
                

            while (true) 
            {
                Console.WriteLine("Enter the message");
                string message = Console.ReadLine();
                if (String.IsNullOrEmpty(message))
                {
                break;
                }
                else
                {
                    User user = new User(nick,message);

                    var json = user.GetJSON();

                    udpClient.Send(Encoding.UTF8.GetBytes(json), json.Length, iPEndPoint);

                    var buffer = udpClient.Receive(ref iPEndPoint);
                    string jsonReceived = Encoding.UTF8.GetString(buffer);
                    User userReceived = User.GetFromJSON(jsonReceived);
                    Console.WriteLine(userReceived.ToString());
                    if (userReceived != null) 
                    {
                        User userClientConfirmation = new User(user.UserName, "Message received");
                        var jsonToSend = userClientConfirmation.GetJSON();
                        var bytesToSend = Encoding.UTF8.GetBytes(jsonToSend);
                        udpClient.Send(bytesToSend, bytesToSend.Length, iPEndPoint);
                    }
                    else 
                    {
                        Console.WriteLine("Something went wrong , user is null");
                    }
                  
                }
            }
        }
    }
}
