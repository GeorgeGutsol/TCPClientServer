using Messages.Handlers;
using Server.Handlers;
using System;
using System.Configuration;
using System.Net;
using System.Threading;

namespace Server
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var serverHandler = new EchoServerHandler(new ProtobufHandler());
            var IP = ConfigurationManager.AppSettings["IP"];
            if (string.IsNullOrEmpty(IP) || !IPAddress.TryParse(IP, out IPAddress address)) address = IPAddress.Parse("127.0.0.1");
            var configPort = ConfigurationManager.AppSettings["Port"];
            if (string.IsNullOrEmpty(configPort) || !int.TryParse(configPort, out int port)) port = 5201;
            Server server = new Server(address, port, serverHandler);
            Console.WriteLine($"Server started on {address}:{port}");
            Thread serverThread = new Thread(server.StartListen);
            serverThread.Start();
            Console.WriteLine("To end press Enter");
            Console.ReadLine();
            server.Close();
        }
    }
}
