using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net;

namespace Server
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Server server = new Server(IPAddress.Parse("127.0.0.1"),5201);
            Console.WriteLine("To end press Enter");
            Console.ReadLine();
            server.Close();
        }
    }
}
