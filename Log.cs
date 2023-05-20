using Messages;
using System;

namespace Server
{
    internal static class Log
    {
        public static void SendLog(BaseMessage message)
        {
            Console.WriteLine($"Server send to {message}");
        }

        public static void RecieveLog(BaseMessage message)
        {
            Console.WriteLine($"Server recieved from {message}");
        }
    }
}
