using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Server
{
    internal class ByteEchoReader : INetworkReader
    {
        public byte[] Read(NetworkStream stream)
        {
            try
            {
                byte[] buffer = new byte[1];
                stream.Read(buffer, 0, 1);
                Console.WriteLine(Encoding.UTF8.GetString(buffer));
                stream.Write(buffer, 0, buffer.Length);
                return buffer;
            }
            catch (IOException)
            {
                throw;
            }
        }
    }
}
