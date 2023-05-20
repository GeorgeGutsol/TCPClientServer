using Messages.Handlers;
using Server.Handlers;
using System;
using System.Net;
using System.Threading;

namespace Server
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var serverHandler = new EchoServerHandler(new ProtobufHandler());
            Server server = new Server(IPAddress.Parse("127.0.0.1"), 5201, serverHandler);
            Thread serverThread = new Thread(server.StartListen);
            serverThread.Start();
            Console.WriteLine("To end press Enter");
            Console.ReadLine();
            server.Close();
        }
    }
}
