using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using Server.Handlers;
using Messages.Handlers;

namespace Server
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var serverHandler = new EchoServerHandler(new ProtobufHandler());
            Server server = new Server(IPAddress.Parse("127.0.0.1"),5201,serverHandler);
            server.StartListen();
            Console.WriteLine("To end press Enter");
            Console.ReadLine();
            server.Close();
        }
    }
}
