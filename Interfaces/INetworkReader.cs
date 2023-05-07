using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace Server
{
    internal interface INetworkReader
    {
        byte[] Read(NetworkStream stream);
    }
}
