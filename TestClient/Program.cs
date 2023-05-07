using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;

namespace TestClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TcpClient tcpClient = new TcpClient();
            tcpClient.Connect(IPAddress.Parse("127.0.0.1"), 49152);
            using (var stream = tcpClient.GetStream())
            {
                string s = "1";
                do
                {
                    s = Console.ReadLine();
                    byte[] buffer = Encoding.UTF8.GetBytes(s);
                    stream.Write(buffer, 0, buffer.Length);
                }
                while (!string.IsNullOrEmpty(s));
            }

            tcpClient.Close();

        }
    }
}
